﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quiltoni.PixelBot.Commands;
using Quiltoni.PixelBot.Configuration;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Quiltoni.PixelBot
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration config) {

			this.Configuration = config;
			Models.Currency.Name = config["PixelBot:Currency:Name"];
			Models.Currency.MyCommand = config["PixelBot:Currency:MyCommand"];
			Models.Currency.SheetType = config["PixelBot:Currency:SheetType"];

		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services) {

			services.AddOptions();

			//services.Configure<PixelBotConfig>(Configuration.GetSection("PixelBot"));

			services.AddTransient<PixelBotConfig>();

			services.AddSingleton<ITwitchConfig>(
				d => SectionProcessor.GetService<ITwitchConfig>(
					Configuration.GetSection("PixelBot").GetSection("Twitch")));

			services.AddSingleton<ICurrencyConfig>(
				d => SectionProcessor.GetService<ICurrencyConfig>(
					Configuration.GetSection("PixelBot").GetSection("Currency")));

			services.AddSingleton<IGoogleConfig>(
				d => SectionProcessor.GetService<IGoogleConfig>(
					Configuration.GetSection("PixelBot").GetSection("Google")));

			services.AddTransient<IServiceConfig>(d => d.GetService<ITwitchConfig>());
			services.AddTransient<IServiceConfig>(d => d.GetService<IGoogleConfig>());
			services.AddTransient<IServiceConfig>(d => d.GetService<ICurrencyConfig>());

			// Register bot commands
			GetType().Assembly.GetTypes()
				.Where(t => t != typeof(IBotCommand) && typeof(IBotCommand).IsAssignableFrom(t))
				.ToList().ForEach(t => services.AddTransient(typeof(IBotCommand), t));

			_ = services.AddHttpClient("giveaway");

			_ = services.AddSingleton<IHostedService, PixelBot>()
				.AddSingleton<GuessGame>()
				.AddSingleton<GiveawayGame.GiveawayGame>();


			_ = services.AddMvc()
				.AddNewtonsoftJson();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseMvc();

		}
	}
}

using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Quiltoni.PixelBot.Commands;
using Quiltoni.PixelBot.Configuration;
using Quiltoni.PixelBot.GiveawayGame;

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

			var configSection = Configuration.GetSection("PixelBot");

			services.AddSingleton<ITwitchConfig>(
				d => SectionProcessor.GetService<ITwitchConfig>(
					configSection.GetSection("Twitch")));

			services.AddSingleton<ICurrencyConfig>(
				d => SectionProcessor.GetService<ICurrencyConfig>(
					configSection.GetSection("Currency")));

			services.AddSingleton<IGoogleConfig>(
				d => SectionProcessor.GetService<IGoogleConfig>(
					configSection.GetSection("Google")));

			services.AddSingleton<IGiveawayGameConfiguration>(
				d => SectionProcessor.GetService<IGiveawayGameConfiguration>(
					configSection.GetSection("GiveawayGame")));

			services.AddSingleton<ICommandsConfig>(
				d => SectionProcessor.GetService<ICommandsConfig>(
					configSection.GetSection("Commands")));

			services.AddTransient<IServiceConfig>(d => d.GetService<ITwitchConfig>());
			services.AddTransient<IServiceConfig>(d => d.GetService<IGoogleConfig>());
			services.AddTransient<IServiceConfig>(d => d.GetService<ICurrencyConfig>());
			services.AddTransient<IServiceConfig>(d => d.GetService<IGiveawayGameConfiguration>());
			services.AddTransient<IServiceConfig>(d => d.GetService<ICommandsConfig>());

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

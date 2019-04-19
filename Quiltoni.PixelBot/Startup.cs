using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Quiltoni.PixelBot.Commands;
using Quiltoni.PixelBot.Configuration;
using Quiltoni.PixelBot.Configuration.Factories;
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

			SetupConfigurationProvider(services);

			// Register bot commands
			GetType().Assembly.GetTypes()
				.Where(t => t != typeof(IBotCommand) && typeof(IBotCommand).IsAssignableFrom(t))
				.ToList().ForEach(t => services.AddTransient(typeof(IBotCommand), t));

			_ = services.AddHttpClient("giveaway");

			_ = services.AddSingleton<IHostedService, PixelBot>()
				.AddSingleton<GuessGame>()
				.AddSingleton<GiveawayGame.GiveawayGame>();


			_ = services.AddMvc();

		}

		private void SetupConfigurationProvider(IServiceCollection services) {
			IConfigurationSection configSection = Configuration.GetSection("PixelBot");

			services.AddSingleton<ITwitchConfig>(
				_ => new TwitchConfigurationFactory().Create(configSection));

			services.AddSingleton<ICurrencyConfig>(
				_ => new CurrencyConfigurationFactory().Create(configSection));

			services.AddSingleton<IGoogleConfig>(
				_ => new GoogleConfigurationFactory().Create(configSection));

			services.AddSingleton<IGiveawayGameConfiguration>(
				_ => new GiveawayGameConfigurationFactory().Create(configSection));

			services.AddSingleton<ICommandsConfig>(
				_ => new CommandsConfigurationFactory().Create(configSection));

			services.AddTransient<IServiceConfig>(serviceProvider => serviceProvider.GetService<ITwitchConfig>());
			services.AddTransient<IServiceConfig>(serviceProvider => serviceProvider.GetService<IGoogleConfig>());
			services.AddTransient<IServiceConfig>(serviceProvider => serviceProvider.GetService<ICurrencyConfig>());
			services.AddTransient<IServiceConfig>(serviceProvider => serviceProvider.GetService<IGiveawayGameConfiguration>());
			services.AddTransient<IServiceConfig>(serviceProvider => serviceProvider.GetService<ICommandsConfig>());

			services.AddTransient<IPixelBotConfigProvider, PixelBotConfig>();
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

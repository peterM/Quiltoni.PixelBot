using System.Collections.Generic;
using System.Linq;
using Quiltoni.PixelBot.Configuration;

namespace Quiltoni.PixelBot
{

	public class PixelBotConfig
	{
		private readonly IReadOnlyCollection<IServiceConfig> _configs;

		public PixelBotConfig(IEnumerable<IServiceConfig> configs) {
			_configs = configs.ToList();
		}

		public ITwitchConfig Twitch => GetConfig<ITwitchConfig>();

		public IGoogleConfig Google => GetConfig<IGoogleConfig>();

		public GiveawayGame.GiveawayGameConfiguration GiveawayGame { get; set; }

		public ICurrencyConfig Currency => GetConfig<ICurrencyConfig>();

		public Dictionary<string, bool> Commands { get; set; }

		public T GetConfig<T>()
			where T : IServiceConfig {
			return _configs.OfType<T>().FirstOrDefault();
		}
	}
}

using System.Collections.Generic;
using System.Linq;
using Quiltoni.PixelBot.Configuration;
using Quiltoni.PixelBot.GiveawayGame;

namespace Quiltoni.PixelBot
{
	public class PixelBotConfig : IPixelBotConfigProvider
	{
		private readonly IReadOnlyCollection<IServiceConfig> _configs;

		public PixelBotConfig(IEnumerable<IServiceConfig> configs) {
			_configs = configs.ToList();
		}

		public ITwitchConfig Twitch => GetConfig<ITwitchConfig>();

		public IGoogleConfig Google => GetConfig<IGoogleConfig>();

		public IGiveawayGameConfiguration GiveawayGame => GetConfig<IGiveawayGameConfiguration>();

		public ICurrencyConfig Currency => GetConfig<ICurrencyConfig>();

		public ICommandsConfig Commands => GetConfig<ICommandsConfig>();

		public T GetConfig<T>()
			where T : IServiceConfig {
			return _configs.OfType<T>().FirstOrDefault();
		}
	}
}
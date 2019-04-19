using System.Collections.Generic;

namespace Quiltoni.PixelBot.Configuration.Factories
{
	public sealed class TwitchConfigurationFactory : AbstractConfigFactory<ITwitchConfig>
	{
		protected override string SectionName => "Twitch";

		protected override ITwitchConfig GetServiceConfiguration(Dictionary<string, string> dictionary) {
			return new TwitchConfig(
				GetValue(dictionary, nameof(ITwitchConfig.UserName)),
				GetValue(dictionary, nameof(ITwitchConfig.AccessToken)),
				GetValue(dictionary, nameof(ITwitchConfig.Channel)));
		}
	}
}

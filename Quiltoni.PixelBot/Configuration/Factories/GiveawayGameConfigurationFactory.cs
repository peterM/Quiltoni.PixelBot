using System.Collections.Generic;
using Quiltoni.PixelBot.GiveawayGame;

namespace Quiltoni.PixelBot.Configuration.Factories
{
	public sealed class GiveawayGameConfigurationFactory : AbstractConfigFactory<IGiveawayGameConfiguration>
	{
		protected override string SectionName => "GiveawayGame";

		protected override IGiveawayGameConfiguration GetServiceConfiguration(Dictionary<string, string> dictionary) {
			return new GiveawayGameConfiguration(
				GetValue(dictionary, nameof(IGiveawayGameConfiguration.RelayUrl)));
		}

		public static IGiveawayGameConfiguration Empty() {
			return new GiveawayGameConfiguration("relayUrl");
		}
	}
}

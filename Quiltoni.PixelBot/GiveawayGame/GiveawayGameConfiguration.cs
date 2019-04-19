using Quiltoni.PixelBot.Configuration;

namespace Quiltoni.PixelBot.GiveawayGame
{
	public class GiveawayGameConfiguration : AbstractServiceConfig, IGiveawayGameConfiguration
	{
		public GiveawayGameConfiguration(string relayUrl) {
			SetConfig(nameof(RelayUrl), relayUrl);
		}

		/// <summary>
		/// The URL of the video widget relay service
		/// </summary>
		public string RelayUrl => GetConfigValue<string>();
	}
}

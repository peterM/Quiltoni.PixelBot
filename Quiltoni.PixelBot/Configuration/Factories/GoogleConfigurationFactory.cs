using System.Collections.Generic;

namespace Quiltoni.PixelBot.Configuration.Factories
{
	public sealed class GoogleConfigurationFactory : AbstractConfigFactory<IGoogleConfig>
	{
		protected override string SectionName => "Google";

		protected override IGoogleConfig GetServiceConfiguration(Dictionary<string, string> dictionary) {
			return new GoogleConfig(
				GetValue(dictionary, nameof(IGoogleConfig.ClientId)),
				GetValue(dictionary, nameof(IGoogleConfig.ClientSecret)),
				GetValue(dictionary, nameof(IGoogleConfig.SheetId)));
		}

		public static IGoogleConfig Empty() {
			return new GoogleConfig("clientId", "clientSeecret", "sheetId");
		}
	}
}

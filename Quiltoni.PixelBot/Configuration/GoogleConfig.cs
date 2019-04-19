namespace Quiltoni.PixelBot.Configuration
{
	public class GoogleConfig : AbstractServiceConfig, IGoogleConfig
	{
		public GoogleConfig(string clientId, string clientSecret, string sheetId) {
			SetConfig(nameof(ClientId), clientId);
			SetConfig(nameof(ClientSecret), clientSecret);
			SetConfig(nameof(SheetId), sheetId);
		}

		public string ClientId => GetConfigValue<string>();

		public string ClientSecret => GetConfigValue<string>();

		public string SheetId => GetConfigValue<string>();
	}
}

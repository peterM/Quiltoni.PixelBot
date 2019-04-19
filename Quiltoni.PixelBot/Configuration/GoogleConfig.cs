namespace Quiltoni.PixelBot.Configuration
{
	public class GoogleConfig : AbstractServiceConfig, IGoogleConfig
	{
		public GoogleConfig(string clientId, string clientSecret, string sheetId) {
			SetConfig(nameof(ClientId), clientId);
			SetConfig(nameof(ClientSecret), clientSecret);
			SetConfig(nameof(SheetId), sheetId);
		}

		public string ClientId => GetValue<string>();

		public string ClientSecret => GetValue<string>();

		public string SheetId => GetValue<string>();
	}
}

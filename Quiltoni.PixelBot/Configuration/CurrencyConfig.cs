namespace Quiltoni.PixelBot.Configuration
{
	public class CurrencyConfig : AbstractServiceConfig, ICurrencyConfig
	{
		public CurrencyConfig(string name, string myCommand, string sheetType) {
			SetConfig(nameof(Name), name);
			SetConfig(nameof(MyCommand), myCommand);
			SetConfig(nameof(SheetType), sheetType);
		}

		public CurrencyConfig()
			: this("pixels", "mypixels", "GoogleSheetProxy") {
		}

		public string Name => GetConfigValue<string>();

		public string MyCommand => GetConfigValue<string>();

		public string SheetType => GetConfigValue<string>();
	}
}

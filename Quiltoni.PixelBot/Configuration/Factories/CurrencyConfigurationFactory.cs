using System.Collections.Generic;

namespace Quiltoni.PixelBot.Configuration.Factories
{
	public sealed class CurrencyConfigurationFactory : AbstractConfigFactory<ICurrencyConfig>
	{
		protected override string SectionName => "Currency";

		protected override ICurrencyConfig GetServiceConfiguration(Dictionary<string, string> dictionary) {
			return new CurrencyConfig(
				GetValue(dictionary, nameof(ICurrencyConfig.Name)),
				GetValue(dictionary, nameof(ICurrencyConfig.MyCommand)),
				GetValue(dictionary, nameof(ICurrencyConfig.SheetType)));
		}

		public static ICurrencyConfig Empty() {
			return new CurrencyConfig("name", "myCommand", "sheetType");
		}
	}
}

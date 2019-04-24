using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

namespace Quiltoni.PixelBot.Configuration.Factories
{
	public abstract class AbstractConfigFactory<TServiceConfig>
		where TServiceConfig : IServiceConfig
	{
		protected abstract string SectionName { get; }

		protected abstract TServiceConfig GetServiceConfiguration(Dictionary<string, string> dictionary);

		public TServiceConfig Create(IConfigurationSection parentConfigurationSection) {
			IConfigurationSection configurationSection = parentConfigurationSection.GetSection(SectionName);
			Dictionary<string, string> dictionary = GetServiceParameters(configurationSection);
			return Create(dictionary);
		}

		public TServiceConfig Create(Dictionary<string, string> dictionary) {
			return GetServiceConfiguration(dictionary);
		}

		protected static string GetValue(Dictionary<string, string> dictionary, string key) {
			if (!dictionary.ContainsKey(key)) {
				return string.Empty;
			}

			return dictionary[key];
		}

		private static Dictionary<string, string> GetServiceParameters(IConfigurationSection configurationSection) {
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			configurationSection.Bind(dictionary);
			return dictionary;
		}
	}
}

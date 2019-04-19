using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.Extensions.Configuration;

namespace Quiltoni.PixelBot.Configuration
{
	public static class SectionProcessor
	{
		public static TServiceConfig GetService<TServiceConfig>(IConfigurationSection configurationSection)
			where TServiceConfig : IServiceConfig {
			TServiceConfig result = default;
			Dictionary<string, string> dictionary = GetServiceParameters(configurationSection);

			var requestedType = typeof(TServiceConfig);

			if (requestedType == typeof(ITwitchConfig))
				return (TServiceConfig)GetTwitchServiceConfiguration(dictionary);

			if (requestedType == typeof(IGoogleConfig))
				return (TServiceConfig)GetGoogleServiceConfiguration(dictionary);

			if (requestedType == typeof(ICurrencyConfig))
				return (TServiceConfig)GetCurrencyServiceConfiguration(dictionary);

			return result;
		}

		private static ITwitchConfig GetTwitchServiceConfiguration(Dictionary<string, string> dictionary) {
			return new TwitchConfig(
				GetValue(dictionary, nameof(ITwitchConfig.UserName)),
				GetValue(dictionary, nameof(ITwitchConfig.AccessToken)),
				GetValue(dictionary, nameof(ITwitchConfig.Channel)));
		}

		private static IGoogleConfig GetGoogleServiceConfiguration(Dictionary<string, string> dictionary) {
			return new GoogleConfig(
				GetValue(dictionary, nameof(IGoogleConfig.ClientId)),
				GetValue(dictionary, nameof(IGoogleConfig.ClientSecret)),
				GetValue(dictionary, nameof(IGoogleConfig.SheetId)));
		}

		private static ICurrencyConfig GetCurrencyServiceConfiguration(Dictionary<string, string> dictionary) {
			return new CurrencyConfig(
				GetValue(dictionary, nameof(ICurrencyConfig.Name)),
				GetValue(dictionary, nameof(ICurrencyConfig.MyCommand)),
				GetValue(dictionary, nameof(ICurrencyConfig.SheetType)));
		}

		private static string GetValue(Dictionary<string, string> dictionary, string key) {
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

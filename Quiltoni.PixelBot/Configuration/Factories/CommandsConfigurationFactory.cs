using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiltoni.PixelBot.Configuration.Factories
{
	public sealed class CommandsConfigurationFactory : AbstractConfigFactory<ICommandsConfig>
	{
		protected override string SectionName => "Commands";

		protected override ICommandsConfig GetServiceConfiguration(Dictionary<string, string> dictionary) {
			IEnumerable<BotCommandConfig> commands = dictionary.Select(item => new BotCommandConfig(item.Key, ToBool(item.Value)));

			return new CommandsConfig(commands);
		}

		public static ICommandsConfig Empty() {
			return new CommandsConfig(
				new IBotCommandConfig[]
				{
					new BotCommandConfig("testCommand", true)
				});
		}

		private static bool ToBool(string value) {
			return Convert.ToBoolean(value);
		}
	}
}

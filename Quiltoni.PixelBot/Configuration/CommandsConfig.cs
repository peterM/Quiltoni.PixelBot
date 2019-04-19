using System.Collections.Generic;
using System.Linq;

namespace Quiltoni.PixelBot.Configuration
{
	public class CommandsConfig : AbstractServiceConfig, ICommandsConfig
	{
		public CommandsConfig(IEnumerable<IBotCommandConfig> botCommands) {
			SetConfig(nameof(Commands), botCommands);
		}

		private IEnumerable<IBotCommandConfig> Commands => GetConfigValue<IEnumerable<IBotCommandConfig>>();

		public IBotCommandConfig GetCommand(string name) {
			return Commands.SingleOrDefault(d => d.Name == name);
		}
	}
}

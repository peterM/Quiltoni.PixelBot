using System.Collections.Generic;

namespace Quiltoni.PixelBot.Configuration
{
	public class CommandsConfig : AbstractServiceConfig, ICommandsConfig
	{
		public CommandsConfig(Dictionary<string, bool> botCommands) {
			SetConfig(nameof(Commands), botCommands);
		}

		public Dictionary<string, bool> Commands => GetConfigValue<Dictionary<string, bool>>();
	}
}

namespace Quiltoni.PixelBot.Configuration
{
	public class BotCommandConfig : IBotCommandConfig
	{
		public BotCommandConfig(string name, bool isEnabled) {
			Name = name;
			IsEnabled = isEnabled;
		}

		public string Name { get; }

		public bool IsEnabled { get; }
	}
}

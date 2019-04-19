namespace Quiltoni.PixelBot.Configuration
{
	public interface ICommandsConfig : IServiceConfig
	{
		IBotCommandConfig GetCommand(string name);
	}
}

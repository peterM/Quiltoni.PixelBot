namespace Quiltoni.PixelBot.Configuration
{
	public interface IBotCommandConfig
	{
		string Name { get; }
		bool IsEnabled { get; }
	}
}

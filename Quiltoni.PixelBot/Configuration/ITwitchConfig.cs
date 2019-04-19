namespace Quiltoni.PixelBot.Configuration
{
	public interface ITwitchConfig : IServiceConfig
	{
		string UserName { get; }
		string Channel { get; }
		string AccessToken { get; }
	}
}

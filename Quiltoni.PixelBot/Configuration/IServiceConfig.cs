namespace Quiltoni.PixelBot.Configuration
{
	public interface IServiceConfig
	{
		T GetConfigValue<T>(string key);
	}
}

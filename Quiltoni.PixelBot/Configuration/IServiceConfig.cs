namespace Quiltoni.PixelBot.Configuration
{
	public interface IServiceConfig
	{
		T GetValue<T>(string key);
	}
}

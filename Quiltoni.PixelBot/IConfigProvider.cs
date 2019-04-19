using Quiltoni.PixelBot.Configuration;

namespace Quiltoni.PixelBot
{
	public interface IConfigProvider
	{
		T GetConfig<T>()
			where T : IServiceConfig;
	}
}
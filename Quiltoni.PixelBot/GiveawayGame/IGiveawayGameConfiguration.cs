using Quiltoni.PixelBot.Configuration;

namespace Quiltoni.PixelBot.GiveawayGame
{
	public interface IGiveawayGameConfiguration : IServiceConfig
	{
		string RelayUrl { get; }
	}
}

using Quiltoni.PixelBot.Configuration;
using Quiltoni.PixelBot.GiveawayGame;

namespace Quiltoni.PixelBot
{
	public interface IPixelBotConfigProvider : IConfigProvider
	{
		ITwitchConfig Twitch { get; }
		IGoogleConfig Google { get; }
		IGiveawayGameConfiguration GiveawayGame { get; }
		ICurrencyConfig Currency { get; }
		ICommandsConfig Commands { get; }
	}
}
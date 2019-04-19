namespace Quiltoni.PixelBot.Configuration
{
	public interface IGoogleConfig : IServiceConfig
	{
		string ClientId { get; }
		string ClientSecret { get; }
		string SheetId { get; }
	}
}

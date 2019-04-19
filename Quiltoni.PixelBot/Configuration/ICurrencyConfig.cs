namespace Quiltoni.PixelBot.Configuration
{
	public interface ICurrencyConfig : IServiceConfig
	{
		string Name { get; }
		string MyCommand { get; }
		string SheetType { get; }
	}
}

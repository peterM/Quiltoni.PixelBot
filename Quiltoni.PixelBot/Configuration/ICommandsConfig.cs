using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Quiltoni.PixelBot.Configuration
{
	public interface ICommandsConfig : IServiceConfig
	{
		Dictionary<string, bool> Commands { get; }
	}
}

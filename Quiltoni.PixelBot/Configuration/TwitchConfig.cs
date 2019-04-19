﻿namespace Quiltoni.PixelBot.Configuration
{
	public sealed class TwitchConfig : AbstractServiceConfig, ITwitchConfig
	{
		public TwitchConfig(string userName, string accessToken, string channel) {
			SetConfig(nameof(UserName), userName);
			SetConfig(nameof(Channel), accessToken);
			SetConfig(nameof(AccessToken), channel);
		}

		public string UserName => GetValue<string>();

		public string Channel => GetValue<string>();

		public string AccessToken => GetValue<string>();
	}
}

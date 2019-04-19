using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Quiltoni.PixelBot.Configuration
{
	public abstract class AbstractServiceConfig
	{
		protected AbstractServiceConfig() {
		}

		private Dictionary<string, object> _settings = new Dictionary<string, object>();

		private object this[string key] {
			get {
				return _settings.ContainsKey(key) ? _settings[key] : null;
			}

			set {
				if (_settings.ContainsKey(key)) {
					return;
				}

				_settings.Add(key, value);
			}
		}

		protected void SetConfig<T>(string key, T value) {
			this[key] = value;
		}

		public T GetConfigValue<T>([CallerMemberName] string key = null) {
			var resultObject = this[key];
			if (resultObject == null) {
				return default(T);
			}

			if (resultObject is T resultT) {
				return resultT;
			}

			return default(T);
		}
	}
}

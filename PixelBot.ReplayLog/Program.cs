using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using LINQtoCSV;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Quiltoni.PixelBot;
using Quiltoni.PixelBot.Configuration;
using Quiltoni.PixelBot.Configuration.Factories;

namespace PixelBot.ReplayLog
{
	class Program
	{
		static void Main(string[] args) {

			var records = ReadLogFile(@"c:\dev\Quiltoni.PixelBot\PixelBot.ReplayLog\log.csv");

			Console.Out.WriteLine(records.First().DateStamp);
			Console.Out.WriteLine($"Read {records.Count()}");

			var totalRecords = records.GroupBy(r => r.UpdatedUser)
				.Select(r => (r.Key, r.Sum(l => l.Changed), "batch"));

			IGoogleConfig instance = GoogleConfigurationFactory.Empty();

			IPixelBotConfigProvider options = new PixelBotConfig(new IServiceConfig[] { instance });

			var proxy = new DryadGoogleSheetProxy(options, NullLoggerFactory.Instance);
			proxy.BatchAddPixelsForUsers(totalRecords);

			Console.ReadLine();

		}

		public static IEnumerable<LogRecord> ReadLogFile(string fileNameCsv) {

			var fileDesc = new CsvFileDescription {
				SeparatorChar = ',',
				FirstLineHasColumnNames = true
			};

			var context = new CsvContext();
			return context.Read<LogRecord>(fileNameCsv, fileDesc);

		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Quiltoni.PixelBot.Pages
{
	public class IndexModel : PageModel
	{

		public IndexModel(IPixelBotConfigProvider config) {
			this.Config = config;
			PixelBot.Config = config;

			CurrencyName = config.Currency.Name;
			SheetProxyType = config.Currency.SheetType;
			EnableDrinkMeCommand = config.Commands.GetCommand("DrinkMeCommand").IsEnabled;
			EnableGuessCommand = config.Commands.GetCommand("GuessTimeCommand").IsEnabled;

		}

		public IPixelBotConfigProvider Config { get; }

		public IEnumerable<SelectListItem> AvailableGoogleProxy {
			get {

				var iProxyType = typeof(ISheetProxy);
				return GetType().Assembly.GetTypes()
					.Where(t => iProxyType.IsAssignableFrom(t) && t != iProxyType)
					.OrderBy(t => t.Name)
					.Select(t => new SelectListItem(t.Name, t.Name));

			}
		}

		[BindProperty]
		[Required]
		public string BotName { get; set; }

		[BindProperty]
		[Required]
		public string Channel { get; set; }

		[BindProperty]
		[Required]
		public string TwitchAccessToken { get; set; }

		[BindProperty]
		public string SheetId { get; set; }

		[BindProperty]
		[Required]
		public string CurrencyName { get; set; }

		[BindProperty]
		[Required]
		public string MyCurrencyCommand { get; set; }

		[BindProperty]
		[Required]
		public string SheetProxyType { get; set; }

		[BindProperty]
		[Required]
		public bool EnableGuessCommand { get; set; }

		[BindProperty]
		[Required]
		public bool EnableDrinkMeCommand { get; set; }

		[BindProperty]
		[Required]
		public bool EnableGiveawayGameCommand { get; set; }

		public void OnGet() { }

		public async Task<IActionResult> OnPost() {

			if (ModelState.IsValid) {

				var jsonFile = JObject.Parse(System.IO.File.ReadAllText("appsettings.json"));
				var myRoot = jsonFile["PixelBot"];

				var currencyToken = myRoot["Currency"];
				currencyToken["Name"] = this.CurrencyName;
				currencyToken["MyCommand"] = this.MyCurrencyCommand;
				currencyToken["SheetType"] = this.SheetProxyType;

				var commandsToken = myRoot["Commands"];
				commandsToken["GuessTimeCommand"] = EnableGuessCommand;
				commandsToken["DrinkMeCommand"] = EnableDrinkMeCommand;
				commandsToken["GiveawayGameCommand"] = EnableGiveawayGameCommand;

				var twitchToken = myRoot["Twitch"];
				twitchToken["UserName"] = this.BotName;
				twitchToken["Channel"] = this.Channel;
				twitchToken["AccessToken"] = this.TwitchAccessToken;

				var googleToken = myRoot["Google"];
				googleToken["SheetId"] = SheetId;

				// Update the file
				await System.IO.File.WriteAllTextAsync("appsettings.json", jsonFile.ToString());
				await Task.Delay(1000);

				return RedirectToPage("./Index");

			}

			return Page();

		}

	}
}
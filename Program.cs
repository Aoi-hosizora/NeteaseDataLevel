using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.Threading.Tasks;

using Aiursoft.HSharp.Methods;
using Aiursoft.Pylon.Models;
using Aiursoft.Pylon.Services;

namespace NeteaseLevel
{
    class Program
    {
		
		private static string SCKEY;
		private static string UserId;

		static void Main(string[] arg) {

			Console.Write("网易云用户ID: ");
			UserId = Console.ReadLine();
			Console.Write("SCKEY: ");
			SCKEY = Console.ReadLine();

			string url = $"https://music.163.com/#/user/home?id={UserId}";
			
			Console.WriteLine("开始监听数据：");
			MainAsync(url).Wait();
		}

		public static async Task MainAsync(string url) {
			int today = DateTime.Now.Day;
			while (true) {
				Console.WriteLine("Today: " + today);

				if (today != DateTime.Now.Day) {
					today = DateTime.Now.Day;
					try {
						await GetData(url);
					}
					catch (Exception ex) {
						Console.WriteLine(ex.Message);
					}
				}
				
				await Task.Delay(1000*5); // Wait 5s
			}

        }

		public static async Task GetData(string url) {

			var http = new HTTPService(null);
            var response = await http.Get(new AiurUrl(url), false);

			// 1.
			// var doc = HtmlConvert.DeserializeHtml(response);
			// var record = doc.AllUnder
			// 	.Where(t => t.Properties.ContainsKey("class"))
			// 	.Where(t => t.Properties["Class"] == "m-record-title")
			// 	.First();

			// 2.
			// Match match = Regex.Match(response, "<div class=\"u-title u-title-1 f-cb m-record-title\" id=\"rHeader\".*<h4>");
			
			// 3.
			Match match = Regex.Match(response, "累积听歌(.+)首");
			if (match.Success)
				await SendMsg(match.Groups[1].Value);
			else
				throw new Exception("页面为空或者该用户没有播放记录");
		}

		public static async Task SendMsg(string cnt) {
			// http://sc.ftqq.com/?c=code
			var http = new HTTPService(null);
			var url = new AiurUrl($"https://sc.ftqq.com/{SCKEY}.send", new {
				text = "网易云音乐个人信息",
				desp = $"{DateTime.Now}: 累积听歌 {cnt} 首"
			});
			Console.WriteLine("Send MSG To Ftqq");
			try {
				await http.Get(url, false);
			}
			catch (Exception ex) {
				throw new Exception($"信息发送错误，可能由于 SCKEY 填错, {ex.Message}");
			}
		}
	}
}

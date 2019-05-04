using System;
using System.Linq;
using System.Threading.Tasks;

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
			
			Console.WriteLine("\n开始监听数据：");
			WaitDay(UserId).Wait();
		}

		public static async Task WaitDay(string uid) {
			int today = DateTime.Now.Day;
			while (true) {
				Console.WriteLine("Today: " + today);

				if (today != DateTime.Now.Day) {
					today = DateTime.Now.Day;
					try {
						///////////////////////////////
						await GetDetailByApi(uid);
					}
					catch (Exception ex) {
						Console.WriteLine(ex.Message);
					}
				}
				
				await Task.Delay(1000*5); // Wait 5s
			}
		}

		public static async Task GetDetailByApi(string uid) {
			var api = new GetUserDetail.UserDetailAPI();
			var detail = api.GetUserDetail(uid);
			string msg = $"用户 {detail.profile.nickname} 累计播放 {detail.listenSongs} 首";

			await SendMsg(msg);
		}

		public static async Task SendMsg(string msg) {
			// http://sc.ftqq.com/?c=code
			var http = new HTTPService(null);
			var url = new AiurUrl($"https://sc.ftqq.com/{SCKEY}.send", new {
				text = "网易云音乐个人信息",
				desp = $"{DateTime.Now}: {msg}"
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

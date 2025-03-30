using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BletchleyMaker
{
    internal class UpdateChecker
    {
        private const string UpdateJsonUrl = "https://raw.githubusercontent.com/shr0m/BletchleyMaker/refs/heads/master/version.json";

        public static async Task CheckForUpdate()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonResponse = await client.GetStringAsync(UpdateJsonUrl);
                    JObject json = JObject.Parse(jsonResponse);

                    string latestVersion = json["latestVersion"]!.ToString();
                    string currentVersion = "2.0.0.0";

                    if (latestVersion != currentVersion)
                    {
                        ShowUpdateNotification(json);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error checking for updates: {ex.Message}");
                    return;
                }
            }
        }

        private static void ShowUpdateNotification(JObject json)
        {
            DialogResult result = MessageBox.Show("A new version is available! Would you like to update?", "Update Available", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes) {
                Process.Start(new ProcessStartInfo(json["downloadUrl"]!.ToString()) { UseShellExecute = true });
            }
            else
            {
                return;
            }
        }
    }

}

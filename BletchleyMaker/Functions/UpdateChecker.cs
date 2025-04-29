using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;  // Required for MessageBox and DialogResult

namespace BletchleyMaker.Functions
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
                    string currentVersion = GetCurrentAssemblyVersion();

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

        private static string GetCurrentAssemblyVersion()
        {
            // Retrieve the current assembly version
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            return version.ToString();  // This will return the version in format "major.minor.build.revision"
        }

        private static void ShowUpdateNotification(JObject json)
        {
            DialogResult result = MessageBox.Show("A new version is available! Would you like to update?", "Update Available", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Process.Start(new ProcessStartInfo(json["downloadUrl"]!.ToString()) { UseShellExecute = true });
            }
            else
            {
                return;
            }
        }
    }
}
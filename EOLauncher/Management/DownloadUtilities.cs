using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace EOLauncher.Management
{
    public static class DownloadUtilities
    {
        //https://cache.tehsausage.com/EndlessOnline/

        private const string EndlessClientUrl = "https://github.com/ethanmoffat/EndlessClient/releases/latest/download/EndlessClient.{OS}.zip";

        public static string GetEndlessClientDownloadUrl()
        {
            string os = Environment.OSVersion.Platform switch
            {
                PlatformID.Win32NT => "Windows",
                PlatformID.Unix => "Linux",
                PlatformID.MacOSX => "macOS.x64",
                _ => "Linux"
            };

            return EndlessClientUrl.Replace("{OS}", os);
        }

        public static void DownloadEndlessClient()
        {

        }
    }
}

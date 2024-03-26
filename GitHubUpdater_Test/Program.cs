using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubUpdater;

namespace GitHubUpdater_Test
{
    internal class Program
    {
        static AppVersion Qversion = new AppVersion("IPCameraManager", 10);
        static void Main(string[] args)
        {
            GitHubReleased git = new GitHubReleased("Microfcorp", "IPCameraManager");
            var t = git.GetReleases();
            var version = t[0].GetAppVersion();
            var IsNew = git.IsNewVersions(Qversion);
        }
    }
}

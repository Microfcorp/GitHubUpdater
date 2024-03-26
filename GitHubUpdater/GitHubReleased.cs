using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json.Linq;

namespace GitHubUpdater
{
    public class GitHubReleased
    {
        private const string GitURL = "http://api.github.com/repos/{0}/{1}/releases";
        private string _url { get => string.Format(GitURL, UserName, Repositories); }

        /// <summary>
        /// Имя владельца репозитория с обновлениями
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Имя репозитория с обновлениями
        /// </summary>
        public string Repositories { get; }

        private WebClient client = new WebClient();

        /// <summary>
        /// Инициализация класса запроса релизов с GitHub
        /// </summary>
        /// <param name="userName">Имя владельца репозитория с обновлениями</param>
        /// <param name="repositories">Имя репозитория с обновлениями</param>
        public GitHubReleased(string userName, string repositories)
        {
            UserName = userName;
            Repositories = repositories;
        }

        /// <summary>
        /// Запросить список релизова
        /// </summary>
        /// <returns></returns>
        public ReleaseNode[] GetReleases()
        {
            client.Headers.Add("Accept", "application/json");
            client.Headers.Add("User-Agent", "DLL/GitHubUpdater");
            var result = client.OpenRead(_url);
            var text = new StreamReader(result).ReadToEnd();
            var js = JArray.Parse(text);
            List<ReleaseNode> tmp = new List<ReleaseNode>();

            for (int i = 0; i < js.Count; i++)
            {
                var jo = js[i];
                var assets = jo["assets"].Select(x => new AssetNode(x["id"].Value<uint>(), x["node_id"].Value<string>(), x["name"].Value<string>(), x["uploader"]["login"].Value<string>(), x["content_type"].Value<string>(), x["size"].Value<ulong>(), x["download_count"].Value<ulong>(), x["created_at"].Value<DateTime>(), x["updated_at"].Value<DateTime>(), x["browser_download_url"].Value<string>())).ToArray();
                var releas = new ReleaseNode(Repositories, jo["id"].Value<uint>(), jo["author"]["login"].Value<string>(), jo["node_id"].Value<string>(), jo["tag_name"].Value<string>(), jo["target_commitish"].Value<string>(), jo["name"].Value<string>(), jo["draft"].Value<bool>(), jo["prerelease"].Value<bool>(), jo["created_at"].Value<DateTime>(), jo["published_at"].Value<DateTime>(), jo["body"].Value<string>(), jo["zipball_url"].Value<string>(), jo["tarball_url"].Value<string>(), assets);
                tmp.Add(releas);
            }
            return tmp.ToArray();
        }

        /// <summary>
        /// Проверяет, есть ли новые версии приложения
        /// </summary>
        /// <param name="currentVersion">Текущая версия приложения</param>
        /// <param name="isPreReleasedVersion">Использовать версии пре-релиза</param>
        /// <param name="isDraftVersion">Использовать драфт-версии</param>
        /// <returns></returns>
        public bool IsNewVersions(AppVersion currentVersion, bool isPreReleasedVersion = false, bool isDraftVersion = false)
        {
            var releas = GetReleases().Where(x => isPreReleasedVersion ? true : !x.IsPreRelease && isDraftVersion ? true : !isDraftVersion).ToArray();
            foreach (var item in releas)
            {
                if (item.GetAppVersion() > currentVersion)
                    return true;
            }
            return false;
        }
    }
}

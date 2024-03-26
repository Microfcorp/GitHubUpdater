using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUpdater
{
    public class ReleaseNode
    {
        internal ReleaseNode(string repositories, uint iD, string author, string node_ID, string tagName, string commit, string name, bool isDraft, bool isPreRelease, DateTime created, DateTime published, string body, string zipBallURL, string tarBallURL, AssetNode[] assets)
        {
            Repositories = repositories;
            ID = iD;
            Author = author;
            Node_ID = node_ID;
            TagName = tagName;
            Commit = commit;
            Name = name;
            IsDraft = isDraft;
            IsPreRelease = isPreRelease;
            Created = created;
            Published = published;
            Body = body;
            ZipBallURL = zipBallURL;
            TarBallURL = tarBallURL;
            Assets = assets;
        }

        public string Repositories { get; }
        public uint ID { get; }
        public string Author { get; }
        public string Node_ID { get; }
        public string TagName { get; }
        public string Commit { get; }
        public string Name { get; }
        public bool IsDraft { get; }
        public bool IsPreRelease { get; }
        public DateTime Created { get; }
        public DateTime Published { get; }
        public string Body { get; }
        public string ZipBallURL { get; }
        public string TarBallURL { get; }
        public AssetNode[] Assets { get; }

        /// <summary>
        /// Возвращает версию приложения из правильного релиза
        /// </summary>
        /// <returns></returns>
        public AppVersion GetAppVersion()
        {
            return new AppVersion(this);
        }
    }

    public class AssetNode
    {
        internal AssetNode(uint iD, string node_ID, string name, string uploader, string contentType, ulong size, ulong downloading, DateTime created, DateTime published, string downloadURL)
        {
            ID = iD;
            Node_ID = node_ID;
            Name = name;
            Uploader = uploader;
            ContentType = contentType;
            Size = size;
            Downloading = downloading;
            Created = created;
            Published = published;
            DownloadURL = downloadURL;
        }

        public uint ID { get; }
        public string Node_ID { get; }
        public string Name { get; }
        public string Uploader { get; }
        public string ContentType { get; }
        public ulong Size { get; }
        public ulong Downloading { get; }
        public DateTime Created { get; }
        public DateTime Published { get; }
        public string DownloadURL { get; }
    }
}

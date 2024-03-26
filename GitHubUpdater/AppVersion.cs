using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUpdater
{
    /// <summary>
    /// Класс, описывающий версию приложения
    /// </summary>
    public class AppVersion : IEquatable<AppVersion>
    {
        /// <summary>
        /// Локальное указание версии приложения
        /// </summary>
        /// <param name="nameApplication">Имя приложения</param>
        /// <param name="version">Версия приложения</param>
        public AppVersion(string nameApplication, uint version)
        {
            NameApplication = nameApplication;
            Version = version;
        }

        /// <summary>
        /// Указание версии приложения, на основании <paramref name="releaseNode"/>
        /// </summary>
        /// <param name="releaseNode">Нода релиза</param>
        public AppVersion(ReleaseNode releaseNode)
        {
            NameApplication = releaseNode.Repositories;
            Version = uint.Parse(releaseNode.TagName.Replace("v", "").Replace(".", ""));
        }

        /// <summary>
        /// Имя приложения
        /// </summary>
        public string NameApplication { get; set; }

        /// <summary>
        /// Версия приложения
        /// </summary>
        public uint Version { get; set; }

        public static bool operator > (AppVersion left, AppVersion right)
        {
            return left.NameApplication == right.NameApplication && left.Version > right.Version;
        }

        public static bool operator < (AppVersion left, AppVersion right)
        {
            return left.NameApplication == right.NameApplication && left.Version < right.Version;
        }

        public static bool operator == (AppVersion left, AppVersion right)
        {
            return left.NameApplication == right.NameApplication && left.Version == right.Version;
        }

        public static bool operator != (AppVersion left, AppVersion right)
        {
            return left.NameApplication != right.NameApplication || left.Version != right.Version;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AppVersion);
        }

        public bool Equals(AppVersion other)
        {
            return !(other is null) &&
                   NameApplication == other.NameApplication &&
                   Version == other.Version;
        }

        public override int GetHashCode()
        {
            int hashCode = -1876483533;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NameApplication);
            hashCode = hashCode * -1521134295 + Version.GetHashCode();
            return hashCode;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace GameSaveInfo.Data.Programs {
    [XmlRootAttribute("game")]
    public abstract class AProgram : IComparable<AProgram> {



        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("added")]
        public DateTime Added { get; set; }
        [XmlAttribute("updated")]
        public DateTime Updated { get; set; }
        [XmlAttribute("deprecated")]
        public bool IsDeprecated { get; set; }


        [XmlElement("title", Order = 0)]
        public string Title { get; set; }
        [XmlElement("version", Order = 1)]
        public List<ProgramVersion> Versions = new List<ProgramVersion>();
        [XmlElement("comment", Order = 2)]
        public string Comment { get; set; }

        public AProgram() {
            this.Added = DateTime.Now;
            this.Updated = DateTime.Now;
        }

        public AProgram(string name, string title, string comment, bool deprecated): base() {
            this.Name = name;
            this.Title = title;
            this.Comment = comment;
            this.IsDeprecated = deprecated;
        }

        public static string prepareProgramName(string title) {
            if (String.IsNullOrEmpty(title))
                return "";
            string name = Regex.Replace(title, @"[^A-Za-z0-9 ]+", "");
            name = name.Replace("&", "And");
            StringBuilder build = new StringBuilder();
            foreach (string sub in name.Split(' ')) {
                if (sub.Length > 0) {
                    build.Append(sub.Substring(0, 1).ToUpper());
                    build.Append(sub.Substring(1));
                }
            }
            return build.ToString();
        }


        public void addVersion(ProgramVersion version) {
            this.Versions.Add(version);
        }

        //public bool hasVersion(String os, String platform, String release) {
        //    return getVersion(os,platform,release) != null;
        //}

        //public GameVersion getVersion(String os, String platform, String release) {
        //    GameVersion candidate = null;
        //    foreach (GameVersion version in this.Versions) {
        //        if (version.ID.Matches(os, platform, null, null, release, null)) {
        //            if (candidate == null || version.ID.Revision > candidate.ID.Revision) {
        //                candidate = version;
        //            }
        //        }
        //    }
        //    return candidate;
        //}


        public int CompareTo(AProgram file) {
            return this.Name.CompareTo(file.Name);
        }

    }
}

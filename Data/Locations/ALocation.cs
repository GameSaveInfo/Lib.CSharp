using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
namespace GameSaveInfo.Data.Locations {
    public abstract class ALocation : IComparable<ALocation> {
        // Used to add or remove path elements
        [XmlAttribute("append")]
        public string Append { get; set; }
        [XmlAttribute("detract")]
        public string Detract { get; set; }
        [XmlAttribute("deprecated")]
        public bool IsDeprecated { get; set; }
        [XmlAttribute("only_for")]
        public string OnlyFor { get; set; }
        // Used to filter user locations by windows versions and language
        [XmlAttribute("lang")]
        public string Language { get; set; }


        public abstract int CompareTo(ALocation comparable);

        protected ALocation(ALocation loc)
            : base() {
            this.Append = loc.Append;
            this.Detract = loc.Detract;
            this.OnlyFor = loc.OnlyFor;
            this.IsDeprecated = loc.IsDeprecated;

        }



        public bool override_virtual_store = false;

        protected ALocation() : base() { }

        // This receives a path and modifies it based on the object's append and detract settings
        public static string modifyPath(string path, ALocation holder) {
            path = path.TrimEnd(Path.DirectorySeparatorChar);
            if (!String.IsNullOrEmpty(holder.Detract)) {
                if (path.EndsWith(holder.Detract))
                    path = path.Substring(0, path.Length - holder.Detract.Length);
            }
            if (!String.IsNullOrEmpty(holder.Append))
                path = Path.Combine(path, holder.Append);
            return path.TrimEnd(Path.DirectorySeparatorChar);
        }

        public string modifyPath(string path) {
            return modifyPath(path, this);
        }


        protected static int compare(IComparable a, IComparable b) {
            if (a == null) {
                if (b == null)
                    return 0;
                else
                    return -1;
            } else {
                return a.CompareTo(b);
            }
        }

    }

}
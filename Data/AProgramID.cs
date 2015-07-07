using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GameSaveInfo.Data {
    public abstract class AProgramID : IComparable<AProgramID>, IEquatable<AProgramID> {
        [XmlAttribute("name")]
        public String Name { get; set; }
        [XmlAttribute("os")]
        public String OS { get; set; }
        [XmlAttribute("platform")]
        public String Platform { get; set; }
        [XmlAttribute("region")]
        public String Region { get; set; }
        [XmlAttribute("media")]
        public String Media { get; set; }
        [XmlAttribute("release")]
        public String Release { get; set; }
        [XmlAttribute("type")]
        public String Type { get; set; }
        [XmlAttribute("revision")]
        public int Revision { get; set; }


        public static int Compare(AProgramID a, AProgramID b, bool ignore_revision) {
            int result = compare(a.Name, b.Name);

            if (result == 0)
                result = compare(a.Release, b.Release);
            if (result == 0)
                result = compare(a.OS, b.OS);
            if (result == 0)
                result = compare(a.Platform, b.Platform);
            if (result == 0)
                result = compare(a.Region, b.Region);
            if (result == 0)
                result = compare(a.Media, b.Media);
            if (result == 0)
                result = compare(a.Type, b.Type);
            if (!ignore_revision && result == 0)
                result = compare(a.Revision, b.Revision);

            return result;
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

        public int CompareTo(AProgramID comparable) {
            return Compare(this, comparable, false);
        }

        public int CompareTo(AProgramID comparable, bool ignore_revision) {
            return Compare(this, comparable, ignore_revision);
        }

        public static bool Equals(AProgramID a, AProgramID b) {
            return Compare(a, b, false) == 0;
        }

        public Boolean Equals(AProgramID to_me) {
            return Equals(this, to_me as AProgramID);
        }


    }
}

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


        public static int Compare(AProgramID a, AProgramID b) {
            return compare(a.Name, b.Name);
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
            return Compare(this, comparable);
        }


        public static bool Equals(AProgramID a, AProgramID b) {
            return Compare(a, b) == 0;
        }

        public Boolean Equals(AProgramID to_me) {
            return Equals(this, to_me as AProgramID);
        }


    }
}

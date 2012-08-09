using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace GameSaveInfo {
    public class Include: Exclude {
        public List<Exclude> Exclusions = new List<Exclude>();

        public override string ElementName {
            get {
                return "include";
            }
        }

        public Include(string name, string path)
            : base(name, path) {
        }

        public Include(FileType parent, string name, string path)
            : base(parent, name, path) {
        }

        public Include(FileType parent, XmlElement element)
            : base(parent, element) {
        }

        protected override void LoadMoreData(XmlElement element) {
            foreach (XmlElement child in element.ChildNodes) {
                switch (child.Name) {
                    case "exclude":
                        Exclude except = new Exclude(this, child);
                        Exclusions.Add(except);
                        break;
                    default:
                        throw new NotSupportedException(child.Name);
                }
            }
        }

        protected override XmlElement WriteMoreData(XmlElement element) {
            foreach (Exclude ex in Exclusions) {
                element.AppendChild(ex.XML);
            }
            return element;
        }

        public Exclude addExclusion(string filePath, string fileName) {
            Exclude except = new Exclude(this, fileName, filePath);
            Exclusions.Add(except);
            this.XML.AppendChild(except.XML);
            return except;
        }
    }
}

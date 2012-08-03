using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using XmlData;
namespace GameSaveInfo {
    public class Link:AXmlDataSubEntry  {
        public override string ElementName {
            get { return "linkable"; }
        }

        public bool Linked = false;

        private string Path = null;
        public Link(GameVersion version, string path)
            : base(version) {
            Path = path;
        }

        public Link(GameVersion game, XmlElement ele): base(game,ele) {

        }

        protected override void LoadData(XmlElement element) {
            foreach (XmlAttribute attr in element.Attributes) {
                switch (attr.Name) {
                    case "path":
                        Path = attr.Value;
                        break;
                    default:
                        throw new NotSupportedException(attr.Name);
                }
            }
        }


        protected override XmlElement WriteData(XmlElement element) {
            this.addAtribute(element, "path", Path);

            return element;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using XmlData;
namespace GameSaveInfo {
    public class Locations: AXmlDataSubEntry {
        public List<LocationPath> Paths = new List<LocationPath>();
        public List<LocationRegistry> Registries = new List<LocationRegistry>();
        public List<LocationShortcut> Shortcuts = new List<LocationShortcut>();
        public List<LocationParent> Parents = new List<LocationParent>();
        public List<ALocation> AllLocations {
            get {
                List<ALocation> return_me = new List<ALocation>();
                return_me.AddRange(Paths);
                return_me.AddRange(Registries);
                return_me.AddRange(Shortcuts);
                return_me.AddRange(Parents);
                return return_me;
            }
        }


        public override string ElementName {
            get { return "locations"; }
        }

        public Locations(GameVersion parent)
            : base(parent) {
        }

        public Locations(GameVersion parent, XmlElement xml): base(parent, xml) {

        }

        public void addLocation(ALocation loc) {
            if (loc is LocationPath) {
                this.Paths.Add(loc as LocationPath);
            } else 
            if (loc is LocationRegistry) {
                this.Registries.Add(loc as LocationRegistry);
            } else {
                throw new NotSupportedException(loc.GetType().ToString());
            }
            this.XML.AppendChild(loc.XML);
        }

        protected override void LoadData(XmlElement element) {
            foreach (XmlElement sub in element.ChildNodes) {
                switch (sub.Name) {
                    case "path":
                        Paths.Add(new LocationPath(this,sub));
                        break;
                    case "registry":
                        Registries.Add(new LocationRegistry(this,sub));
                        break;
                    case "shortcut":
                        Shortcuts.Add(new LocationShortcut(this,sub));
                        break;
                    case "parent":
                        Parents.Add(new LocationParent(this,sub));
                        break;
                    default:
                        throw new NotSupportedException(sub.Name);
                }
            }
        }

        protected override XmlElement WriteData(XmlElement element) {
            foreach (ALocation path in AllLocations) {
                XmlElement xp = path.XML;
                element.AppendChild(xp);
            }
            return element;
        }
    }
}

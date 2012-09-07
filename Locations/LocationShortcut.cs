using System;
using System.Xml;
namespace GameSaveInfo {
    public class LocationShortcut : ALocation {

        // Used when dealing with a shortcut
        public EnvironmentVariable ev;
        public string path;

        public override string ElementName {
            get { return "shortcut"; }
        }

        public LocationShortcut(Locations loc, XmlElement element)
            : base(loc, element) {
        }

        protected override void LoadMoreData(XmlElement element) {
            foreach (XmlAttribute attrib in element.Attributes) {
                if (attributes.Contains(attrib.Name))
                    continue;

                switch (attrib.Name) {
                    case "ev":
                        this.ev = parseEnvironmentVariable(attrib.Value);
                        break;
                    case "path":
                        this.path = attrib.Value;
                        break;
                    default:
                        throw new NotSupportedException(attrib.Name);
                }
            }
        }

        protected override XmlElement WriteMoreData(XmlElement element) {
            addAtribute(element, "ev", ev.ToString().ToLower());
            addAtribute(element, "path", path);
            return element;
        }

        public override int CompareTo(ALocation comparable) {
            LocationShortcut location = (LocationShortcut)comparable;
            int result = ev.CompareTo(location.ev);
            if (result == 0)
                result = path.CompareTo(location.path);
            return result;
        }
    }
}

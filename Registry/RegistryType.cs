using System;
using System.Collections.Generic;
using System.Xml;
using XmlData;
namespace GameSaveInfo {
    public class RegistryType : AXmlDataSubEntry {
        public string Type { get; protected set; }
        public List<RegistryEntry> Entries = new List<RegistryEntry>();

        public override string ElementName {
            get { return "registry"; }
        }

        public RegistryType(GameVersion version, string type)
            : base(version) {
            this.Type = type;
        }

        public RegistryType(GameVersion version, XmlElement element)
            : base(version, element) {
            this.Type = "";
        }

        public RegistryEntry addEntry(RegRoot root, string key, string value) {
            RegistryEntry entry = new RegistryEntry(this, root, key, value);
            Entries.Add(entry);
            this.XML.AppendChild(entry.XML);
            return entry;
        }

        protected override void LoadData(XmlElement element) {
            foreach (XmlAttribute attr in element.Attributes) {
                switch (attr.Name) {
                    case "type":
                        this.Type = attr.Value;
                        break;
                    default:
                        throw new NotSupportedException(attr.Name);
                }
            }
            foreach (XmlElement ele in element.ChildNodes) {
                switch (ele.Name) {
                    case "entry":
                        Entries.Add(new RegistryEntry(this, ele));
                        break;
                    default:
                        throw new NotSupportedException(ele.Name);
                }
            }

        }

        protected override XmlElement WriteData(XmlElement element) {
            this.addAtribute(element, "type", Type);
            foreach (RegistryEntry entry in Entries) {
                element.AppendChild(entry.XML);
            }
            return element;
        }
    }
}

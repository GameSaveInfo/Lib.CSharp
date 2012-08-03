using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using XmlData;
namespace GameSaveInfo {
    public class RegistryEntry: AXmlDataSubEntry {
        public RegRoot Root { get; protected set; }
        public string Key { get; protected set; }
        public string Value { get; protected set; }

        public override string ElementName {
            get { return "entry"; }
        }

        public RegistryEntry(Registry reg, RegRoot root, string key, string value): base(reg) {
            this.Root = root;
            this.Key = key;
            this.Value = value;
        }

        public RegistryEntry(Registry reg, XmlElement element) : base(reg, element) { 
        
        }

        protected override void LoadData(XmlElement element) {
            foreach (XmlAttribute attr in element.Attributes) {
                switch (attr.Name) {
                    case "root":
                        this.Root = LocationRegistry.parseRegRoot(attr.Value);
                        break;
                    case "key":
                        this.Key = attr.Value;
                        break;
                    case "values":
                        this.Value = attr.Value;
                        break;
                    default:
                        throw new NotSupportedException(attr.Name);
                }
            }
        }

        protected override XmlElement WriteData(XmlElement element) {
            this.addAtribute(element, "root", Root.ToString());
            this.addAtribute(element, "key", Key);
            this.addAtribute(element, "value", Value);
            return element;
        }

    }
}

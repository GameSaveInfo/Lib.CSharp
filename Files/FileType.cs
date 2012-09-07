using System;
using System.Collections.Generic;
using System.Xml;
using XmlData;
namespace GameSaveInfo {
    public class FileType : AXmlDataSubEntry {
        public List<Include> Inclusions = new List<Include>();
        public string Type { get; protected set; }

        public override string ElementName {
            get { return "files"; }
        }


        public FileType(GameVersion version, string name)
            : base(version) {
            this.Type = name;
        }

        public FileType(GameVersion version, XmlElement element)
            : base(version, element) {
        }

        protected override void LoadData(XmlElement element) {
            Type = "";
            foreach (XmlAttribute attr in element.Attributes) {
                switch (attr.Name) {
                    case "type":
                        this.Type = attr.Value;
                        break;
                    default:
                        throw new NotSupportedException(attr.Name);
                }
            }
            foreach (XmlElement child in element.ChildNodes) {
                switch (child.Name) {
                    case "include":
                        Include save = new Include(this, child);
                        Inclusions.Add(save);
                        break;
                    default:
                        throw new NotSupportedException(child.Name);
                }
            }
        }


        protected override XmlElement WriteData(XmlElement element) {
            addAtribute(element, "type", this.Type);
            foreach (Include save in Inclusions) {
                element.AppendChild(save.XML);
            }
            return element;
        }

        public void Add(Include file) {
            this.Inclusions.Add(file);
            this.XML.AppendChild(file.XML);
        }

        public virtual List<string> FindMatching(string location) {
            List<string> files = new List<string>();
            foreach (Include save in Inclusions) {
                files.AddRange(save.FindMatching(location));
            }
            return files;
        }

        public Include addSave(string savePath, string saveFile) {
            Include save = new Include(this, saveFile, savePath);
            this.Inclusions.Add(save);
            this.XML.AppendChild(save.XML);
            return save;
        }

    }
}

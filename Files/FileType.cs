﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using XmlData;
namespace GameSaveInfo {
    public class FileType: AXmlDataSubEntry  {
        public List<SaveFile> Saves = new List<SaveFile>();
        public string Type { get; protected set; }

        public override string ElementName {
            get { return "files"; }
        }


        public FileType( GameVersion version, string name): base(version) {
            this.Type = name;
        }

        public FileType(GameVersion version, XmlElement element)
            : base(version,element) {
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
            foreach (XmlElement child in element.ChildNodes) {
                switch (child.Name) {
                    case "save":
                        SaveFile save = new SaveFile(this,child);
                        Saves.Add(save);
                        break;
                    default:
                        throw new NotSupportedException(child.Name);
                }
            }
        }


        protected override XmlElement WriteData(XmlElement element) {
            addAtribute(element, "type", this.Type);
            foreach (SaveFile save in Saves) {
                element.AppendChild(save.XML);
            }
            return element;
        }

        public void Add(SaveFile file) {
            this.Saves.Add(file);
            this.XML.AppendChild(file.XML);
        }

        public virtual List<string> FindMatching(string location) {
            List<string> files = new List<string>();
            foreach (SaveFile save in Saves) {
                files.AddRange(save.FindMatching(location));
            }
            return files;
        }

        public SaveFile addSave(string savePath, string saveFile) {
            SaveFile save = new SaveFile(this, saveFile, savePath);
            this.Saves.Add(save);
            this.XML.AppendChild(save.XML);
            return save;
        }

    }
}

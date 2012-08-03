using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace GameSaveInfo {
    public class SaveFile: ExceptFile {
        public List<ExceptFile> Excepts = new List<ExceptFile>();

        public override string ElementName {
            get {
                return "save";
            }
        }

        public SaveFile(string name, string path)
            : base(name, path) {
        }

        public SaveFile(FileType parent, string name, string path)
            : base(parent, name, path) {
        }

        public SaveFile(FileType parent, XmlElement element)
            : base(parent, element) {
        }

        protected override void LoadMoreData(XmlElement element) {
            foreach (XmlElement child in element.ChildNodes) {
                switch (child.Name) {
                    case "except":
                        ExceptFile except = new ExceptFile(this, child);
                        Excepts.Add(except);
                        break;
                    default:
                        throw new NotSupportedException(child.Name);
                }
            }
        }

        protected override XmlElement WriteMoreData(XmlElement element) {
            foreach (ExceptFile ex in Excepts) {
                element.AppendChild(ex.XML);
            }
            return element;
        }

        public ExceptFile addException(string filePath, string fileName) {
            ExceptFile except = new ExceptFile(this, fileName, filePath);
            Excepts.Add(except);
            this.XML.AppendChild(except.XML);
            return except;
        }
    }
}

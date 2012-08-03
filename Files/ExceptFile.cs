using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using XmlData;
namespace GameSaveInfo {
    public class ExceptFile: AFile {
        public string Type { get; protected set; }

        public override string ElementName {
            get { return "except"; }
        }

        protected ExceptFile(string name, string path) : base(null, name, path) { }

        public ExceptFile(SaveFile parent, string name, string path)
            : this(parent, name, path, parent.Type) {
        }


        protected ExceptFile(FileType parent, string name, string path): 
             this(parent, name, path, parent.Type) {
        }

        private ExceptFile(AXmlDataSubEntry parent, string name, string path, string type)
            : base(parent, name, path) {
            this.Type = type;
        }


        public ExceptFile(SaveFile parent, XmlElement element) : this(parent, element, parent.Type) { }

        protected ExceptFile(FileType parent, XmlElement element) : this(parent, element, parent.Type) { }



        private ExceptFile(AXmlDataSubEntry parent, XmlElement element, string type)
            : base(parent, element) {
                this.Type = type;
        }

        protected override void LoadMoreData(XmlElement element) {
            
        }

        protected override XmlElement WriteMoreData(XmlElement element) {
            return element;
        }
    }
}

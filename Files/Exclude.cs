using System.Xml;
using XmlData;
namespace GameSaveInfo {
    public class Exclude : AFile {
        public string Type { get; protected set; }

        public override string ElementName {
            get { return "exclude"; }
        }

        protected Exclude(string name, string path, string type)
            : base(name, path) {
            Type = type;
        }

        public Exclude(Include parent, string name, string path)
            : this(parent, name, path, parent.Type) {
        }


        protected Exclude(FileType parent, string name, string path) :
            this(parent, name, path, parent.Type) {
        }

        private Exclude(AXmlDataSubEntry parent, string name, string path, string type)
            : base(parent, name, path) {
            this.Type = type;
        }


        public Exclude(Include parent, XmlElement element) : this(parent, element, parent.Type) { }

        protected Exclude(FileType parent, XmlElement element) : this(parent, element, parent.Type) { }



        private Exclude(AXmlDataSubEntry parent, XmlElement element, string type)
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

using System.Xml;
namespace GameSaveInfo {
    public class Identifier : AFile {
        public Identifier(GameVersion version, XmlElement element) : base(version, element) { }

        public override string ElementName {
            get { return "identifier"; }
        }

        protected override void LoadMoreData(XmlElement element) {

        }

        protected override XmlElement WriteMoreData(XmlElement element) {
            return element;
        }
    }
}

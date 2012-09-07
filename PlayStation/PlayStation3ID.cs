using System.Xml;
namespace GameSaveInfo {
    public class PlayStation3ID : APlayStationID {
        public PlayStation3ID(GameVersion parent, XmlElement element) : base(parent, element) { }
        public override string ToString() {
            return SavePattern();
        }
    }
}

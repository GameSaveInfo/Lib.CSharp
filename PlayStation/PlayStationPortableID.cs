using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace GameSaveInfo {
    public class PlayStationPortableID : APlayStationID {
        public PlayStationPortableID(GameVersion parent, XmlElement element) : base(parent, element) { }
        public override string ToString() {
            return SavePattern();
        }
    }
}

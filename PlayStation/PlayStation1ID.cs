using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace GameSaveInfo {
    public class PlayStation1ID : APlayStationID {
        public PlayStation1ID(GameVersion parent, XmlElement element) : base(parent, element) { }
        public override string ToString() {
            return ExportPattern();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace GameSaveInfo {
    public class PlayStation2ID : APlayStationID {
        public PlayStation2ID(GameVersion parent, XmlElement element) : base(parent, element) { }
        public override string ToString() {
            return ExportPattern();
        }
    }
}

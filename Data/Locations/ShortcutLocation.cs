using System;
using System.Xml;
using System.Xml.Serialization;
using GameSaveInfo.Data.Enums;

namespace GameSaveInfo.Data.Locations {
    public class ShortcutLocation : ALocation {

        // Used when dealing with a shortcut
        [XmlAttribute("ev")]
        public EnvironmentVariable EV;
        [XmlAttribute("path")]
        public string Path;


        public override int CompareTo(ALocation comparable) {
            ShortcutLocation location = (ShortcutLocation)comparable;
            int result = EV.CompareTo(location.EV);
            if (result == 0)
                result = Path.CompareTo(location.Path);
            return result;
        }
    }
}

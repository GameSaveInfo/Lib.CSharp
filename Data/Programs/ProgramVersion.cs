using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace GameSaveInfo.Data.Programs {
    public class ProgramVersion: AProgramID {
        public enum Requirement {
            [XmlEnum("optional")]
            Optional,
            [XmlEnum("required")]
            Required
        }

        [XmlAttribute("deprecated")]
        public bool IsDeprecated { get; set; }
        [XmlAttribute("detect")]
        public Requirement DetectionRequired { get; set; }

        [XmlArrayItem(ElementName = "path", Type = typeof(Locations.PathLocation))]
        [XmlArrayItem(ElementName = "registry", Type = typeof(Locations.RegistryLocation))]
        [XmlArrayItem(ElementName = "parent", Type = typeof(Locations.ParentLocation))]
        [XmlArrayItem(ElementName = "shortcut", Type = typeof(Locations.ShortcutLocation))]
        [XmlArray(ElementName = "locations")]
        public List<Locations.ALocation> Programs = new List<Locations.ALocation>();


        [XmlElement("contributor")]
        public List<string> Contributors = new List<string>();


        [XmlElement("comment")]
        public string Comment { get; set; }
        [XmlElement("restore_comment")]
        public string RestoreComment { get; set; }


        //private bool _deprecated = false;
        //public bool IsDeprecated {
        //    get {
        //        if (Game.IsDeprecated)
        //            return true;
        //        else
        //            return _deprecated;
        //    }
        //    protected set {
        //        _deprecated = value;
        //    }
        //}

    }
}

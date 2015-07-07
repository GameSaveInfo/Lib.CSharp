using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GameSaveInfo.Data.Options {
    public class WindowsOptions {
        public enum VirtualStoreOptions { [XmlEnum("ignore")]Ignore, [XmlEnum("use")]Use }
            
        [XmlAttribute("virtualstore")]
        public VirtualStoreOptions VirtualStore { get; protected set; }
    }
}

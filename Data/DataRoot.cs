using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace GameSaveInfo.Data {
    [XmlRootAttribute("programs")]
    public class DataRoot  {
        [XmlIgnore]
        public Version Version = new Version();

        [XmlAttribute("majorVersion")]
        public int MajorVersion { get; set; }
        [XmlAttribute("minorVersion")]
        public int MinorVersion { get; set; }
        [XmlAttribute("revision")]
        public int Revision { get; set; }
        [XmlAttribute("updated")]
        public DateTime Updated { get; set; }


        [XmlElement(ElementName = "application", Type = typeof(Programs.Application))]
        [XmlElement(ElementName = "expansion", Type = typeof(Programs.Expansion))]
        [XmlElement(ElementName = "game", Type = typeof(Programs.Game))]
        [XmlElement(ElementName = "mod", Type = typeof(Programs.Mod))]
        //[XmlArrayItem(ElementName = "system", Type = typeof(Programs.Application))]
        //[XmlArray(ElementName="programs")]
        public List<Programs.AProgram> Programs = new List<Programs.AProgram>();

        public DataRoot() {
            this.MajorVersion = 2;
            this.MinorVersion = 2;
            this.Revision = 0;
            this.Updated = DateTime.UtcNow;
        }

        public string ToXml() {
            XmlSerializer s = new XmlSerializer(typeof(DataRoot));
            using (StringWriter writer = new StringWriter()) {
                s.Serialize(writer, this);
                return writer.ToString();
            }
        }

        public static DataRoot FromXml(Stream stream) {
            XmlSerializer s = new XmlSerializer(typeof(DataRoot));
            return (DataRoot)s.Deserialize(stream);
        }
    }
}

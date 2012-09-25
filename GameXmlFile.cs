using System;
using System.IO;
using System.Xml;
using XmlData;

namespace GameSaveInfo {
    public class GameXmlFile : AXmlDataFile<Game> {
        public static Version SupportedVersion = new Version(2, 0);

        public const string Schema = "GameSaveInfo20.xsd";

        public DateTime date;
        public Version Version { get; protected set; }

        public GameXmlFile(FileInfo file)
            : base(file, true) {
        }

        protected override void loadXmlFile() {

            if (DocumentElement.HasAttribute("updated"))
                date = DateTime.Parse(DocumentElement.Attributes["updated"].Value);
            else
                date = DateTime.Parse("November 5, 1955");

            if (DocumentElement.HasAttribute("majorVersion") && DocumentElement.HasAttribute("minorVersion"))
                Version = new Version(Int32.Parse(DocumentElement.Attributes["majorVersion"].Value), Int32.Parse(DocumentElement.Attributes["minorVersion"].Value));

            if (Version != SupportedVersion) {
                throw new VersionNotSupportedException(Version);
            }

            base.loadXmlFile();
        }

        protected override Game CreateDataEntry(System.Xml.XmlElement element) {
            return new Game(element);
        }

        public Game getGame(string name) {
            foreach (Game game in this.Entries) {
                if (game.Name == name)
                    return game;
            }
            return null;
        }

        protected override XmlElement CreatRootNode() {
            XmlElement ele = this.CreateElement("programs");

            XmlAttribute attr = this.CreateAttribute("majorVersion");
            attr.Value = SupportedVersion.Major.ToString();
            ele.Attributes.Append(attr);

            attr = this.CreateAttribute("minorVersion");
            attr.Value = SupportedVersion.Minor.ToString();
            ele.Attributes.Append(attr);

            attr = this.CreateAttribute("xmlns:xsi");
            attr.Value = @"http://www.w3.org/2001/XMLSchema-instance";
            ele.Attributes.Append(attr);

            attr = this.CreateAttribute("xsi:noNamespaceSchemaLocation");
            attr.Value = Schema;
            ele.Attributes.Append(attr);

            return ele;
        }

    }
}

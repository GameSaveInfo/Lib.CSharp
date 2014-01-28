using System;
using System.IO;
using System.Xml;
using XmlData;

namespace GameSaveInfo {
    public class GameXmlFile : AXmlDataFile<Game> {
		public static Version MinimumSupportedVersion = new Version(2, 0, 2);
		public static Version MaximumSupportedVersion = new Version(2, 2);

        public const string Schema = "GameSaveInfo22.xsd";
		
		public const string RootElementName = "programs";
		
        public DateTime date;
        public Version Version { get; protected set; }

        public GameXmlFile(FileInfo file)
            : base(file, true, GameXmlFile.RootElementName) {
        }

        protected override void loadXmlFile() {

            if (DocumentElement.HasAttribute("updated"))
                date = DateTime.Parse(DocumentElement.Attributes["updated"].Value);
            else
                date = DateTime.Parse("November 5, 1955");

            if (DocumentElement.HasAttribute("majorVersion") && DocumentElement.HasAttribute("minorVersion"))
				Version = new Version(Int32.Parse(DocumentElement.Attributes["majorVersion"].Value), Int32.Parse(DocumentElement.Attributes["minorVersion"].Value));

            if (Version < MinimumSupportedVersion || Version > MaximumSupportedVersion) {
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
            attr.Value = MaximumSupportedVersion.Major.ToString();
            ele.Attributes.Append(attr);

            attr = this.CreateAttribute("minorVersion");
            attr.Value = MaximumSupportedVersion.Minor.ToString();
            ele.Attributes.Append(attr);

			attr = this.CreateAttribute("revision");
			attr.Value = MaximumSupportedVersion.Minor.ToString();
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

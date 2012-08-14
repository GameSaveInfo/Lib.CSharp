using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using XmlData;

namespace GameSaveInfo {
    public class GameXmlFile: AXmlDataFile<Game> {
        public static Version SupportedVersion = new Version(2, 0);

        public static string Schema = "GameSaveInfo20.xsd";

        public DateTime date;
        public Version Version { get; protected set; }

        public GameXmlFile(FileInfo file): base(file,true) {
            if (DocumentElement.HasAttribute("date"))
                date = DateTime.Parse(DocumentElement.Attributes["date"].Value);
            else
                date = DateTime.Parse("November 5, 1955");


            if (DocumentElement.HasAttribute("majorVersion") && DocumentElement.HasAttribute("minorVersion"))
                Version = new Version(Int32.Parse(DocumentElement.Attributes["majorVersion"].Value), Int32.Parse(DocumentElement.Attributes["minorVersion"].Value));

            if (Version != SupportedVersion) {
                throw new VersionNotSupportedException(Version);
            }
        }


        protected override Game CreateDataEntry(System.Xml.XmlElement element) {
            try {
                return new Game(element);
            } catch (NotSupportedException ex) {

            }
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

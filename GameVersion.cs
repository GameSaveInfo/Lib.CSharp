using System;
using System.Collections.Generic;
using System.Xml;
using XmlData;
namespace GameSaveInfo {
    public class GameVersion : AXmlDataSubEntry {
        public GameIdentifier ID { get; protected set; }

        public Game Game {
            get {
                return this.Parent as Game;
            }
        }

        public List<string> Contributors = new List<string>();

        public string Comment { get; protected set; }
        public string RestoreComment { get; protected set; }

        public bool IgnoreVirtualStore { get; protected set; }

        private bool _deprecated = false;
        public bool IsDeprecated {
            get {
                if (Game.IsDeprecated)
                    return true;
                else
                    return _deprecated;
            }
            protected set {
                _deprecated = value;
            }
        }

        public bool DetectionRequired { get; protected set; }

        public List<Link> Links = new List<Link>();
        public List<Identifier> Identifiers = new List<Identifier>();
        public Dictionary<string, FileType> FileTypes = new Dictionary<string, FileType>();

        public override string ElementName {
            get { return "version"; }
        }

        protected string _title = null;
        public string Title {
            get {
                if (_title == null)
                    return Game.Title;
                return _title;
            }
        }

        public List<ALocation> AllLocations {
            get {
                if (Locations != null)
                    return Locations.AllLocations;
                else
                    return new List<ALocation>();
            }
        }
        private Locations _locations;
        public Locations Locations {
            get {
                if (_locations == null)
                    _locations = new Locations(this);
                return _locations;
            }
            protected set {
                _locations = value;
            }
        }

        public List<APlayStationID> PlayStationIDs = new List<APlayStationID>();
        public List<ScummVM> ScummVMs = new List<ScummVM>();

        public GameVersion(Game parent, string os, string release)
            : base(parent) {
            DetectionRequired = false;
            this.ID = new GameIdentifier(parent.Name, os, null, null, null, release, null);
        }

        public GameVersion(Game parent, XmlElement ele)
            : base(parent, ele) {
            DetectionRequired = false;
        }

        protected override void LoadData(XmlElement element) {
            this.ID = new GameIdentifier(Game.Name, element);

            foreach (XmlAttribute attrib in element.Attributes) {
                if (GameIdentifier.attributes.Contains(attrib.Name))
                    continue;

                switch (attrib.Name) {
                    case "virtualstore":
                        IgnoreVirtualStore = attrib.Value == "ignore";
                        break;
                    case "detect":
                        DetectionRequired = attrib.Value == "required";
                        break;
                    case "deprecated":
                        IsDeprecated = Boolean.Parse(attrib.Value);
                        break;
                    default:
                        throw new NotSupportedException(attrib.Name);
                }
            }
            Links.Clear();
            foreach (XmlElement sub in element.ChildNodes) {
                switch (sub.Name) {
                    case "title":
                        _title = sub.InnerText;
                        break;
                    case "locations":
                        this.Locations = new GameSaveInfo.Locations(this, sub);
                        break;
                    case "files":
                        FileType type = new FileType(this, sub);
                        FileTypes.Add(type.Type, type);
                        break;
                    case "ps_code":
                        PlayStationIDs.Add(APlayStationID.Create(this, sub));
                        break;
                    case "contributor":
                        Contributors.Add(sub.InnerText);
                        break;
                    case "comment":
                        Comment = sub.InnerText;
                        break;
                    case "restore_comment":
                        RestoreComment = sub.InnerText;
                        break;
                    case "identifier":
                        Identifiers.Add(new Identifier(this, sub));
                        break;
                    case "scummvm":
                        ScummVMs.Add(new ScummVM(this, sub));
                        break;
                    case "linkable":
                        Links.Add(new Link(this, sub));
                        break;
                    case "registry":
                        RegistryType rtype = new RegistryType(this, sub);
                        RegistryTypes.Add(rtype.Type, rtype);
                        break;
                    default:
                        throw new NotSupportedException(sub.Name);
                }
            }
        }

        protected override XmlElement WriteData(XmlElement element) {
            // This outputs little more than what's necessary to create a custom game entry
            // Once in the file, the xml wil not need to be re-generated, so it won't need to be outputted again
            // This way manual updates to the xml file won't be lost ;)

            ID.AddAttributes(element);

            if (element.HasAttribute("name"))
                element.RemoveAttribute("name");

            element.AppendChild(Locations.XML);

            foreach (FileType type in FileTypes.Values) {
                element.AppendChild(type.XML);
            }

            foreach (string con in Contributors) {
                element.AppendChild(Game.createElement("contributor", con));
            }

            if (Comment != null)
                element.AppendChild(Game.createElement("comment", Comment));
            if (RestoreComment != null)
                element.AppendChild(Game.createElement("restore_comment", RestoreComment));


            return element;
        }

        public void addLocation(ALocation loc) {
            Locations.addLocation(loc);
        }
        public void addContributor(string name) {
            if (!Contributors.Contains(name)) {
                XmlElement cont = createElement("contributor");
                cont.InnerText = name;
                this.XML.AppendChild(cont);
                Contributors.Add(name);
            }
        }
        public FileType addFileType(string name) {
            if (name == null)
                name = "";
            if (!this.FileTypes.ContainsKey(name)) {
                FileType type = new FileType(this, name);
                this.FileTypes.Add(name, type);
                this.XML.AppendChild(type.XML);
            }
            return this.FileTypes[name];
        }

        public Dictionary<string, RegistryType> RegistryTypes = new Dictionary<string, RegistryType>();

        public RegistryEntry addRegEntry(RegRoot root, string key, string value, string type) {
            if (!RegistryTypes.ContainsKey(type)) {
                RegistryType reg = new RegistryType(this, type);
                this.XML.AppendChild(reg.XML);
                RegistryTypes.Add(type, reg);
            }
            RegistryType registry = RegistryTypes[type];
            RegistryEntry entry = registry.addEntry(root, key, type);


            return entry;
        }

        public Link addLink(string path) {
            Link link = new Link(this, path);
            this.XML.AppendChild(link.XML);
            Links.Add(link);
            return link;
        }

    }
}

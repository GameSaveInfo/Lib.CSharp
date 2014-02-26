using System;
using System.Xml;
namespace GameSaveInfo {
    public class LocationPath : ALocation {
        // Used when dealing with a path
        // ONLY holds the name of the environment variable or wahtever used to figure out the root
        public EnvironmentVariable EV { get; set; }

        public bool IsEnabled { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsSelected { get; set; }

        // Holds only the relative path from the root
        public string Path { get; protected set; }

        public void ReplacePath(string path) {
            this.Path = path;
        }

        public void PrependPath(string path) {
            if (String.IsNullOrEmpty(path))
                return;
            if (String.IsNullOrEmpty(this.Path))
                this.Path = path;
            else
                this.Path = System.IO.Path.Combine(path, this.Path);
        }

        public void AppendPath(string path) {
            if (String.IsNullOrEmpty(path))
                return;
            if (String.IsNullOrEmpty(this.Path))
                this.Path = path;
            else
                this.Path = System.IO.Path.Combine(this.Path, path);
        }

        public override string ElementName {
            get { return "path"; }
        }

        protected LocationPath(GameVersion parent, XmlElement element) : base(parent, element) { }

        protected LocationPath() { }

        public LocationPath(EnvironmentVariable ev, string path) {
            this.EV = ev;
            this.Path = path;
        }

        public LocationPath(Locations loc, EnvironmentVariable ev, string path)
            : base(loc) {
            this.EV = ev;
            this.Path = path;
        }

        public LocationPath(Locations loc, XmlElement element)
            : base(loc, element) {
        }

        public LocationPath(LocationPath copy_me)
            : base(copy_me) {
            this.EV = copy_me.EV;
            this.Path = copy_me.Path;
            this.IsEnabled = copy_me.IsEnabled;
            this.IsExpanded = copy_me.IsExpanded;
            this.IsSelected = copy_me.IsSelected;
        }

        protected override void LoadMoreData(XmlElement element) {
            foreach (XmlAttribute attrib in element.Attributes) {
                if (attributes.Contains(attrib.Name))
                    continue;

                switch (attrib.Name) {
                    case "ev":
                        this.EV = parseEnvironmentVariable(attrib.Value);
                        break;
                    case "path":
                        this.Path = attrib.Value;
                        break;
                    default:
                        throw new NotSupportedException(attrib.Name);
                }
            }
        }

        protected override XmlElement WriteMoreData(XmlElement element) {
            addAtribute(element, "ev", EV.ToString().ToLower());
            addAtribute(element, "path", Path);
            return element;
        }

        public override int CompareTo(ALocation comparable) {
            LocationPath location = (LocationPath)comparable;
            int result = compare(this.EV, location.EV);
            if (result == 0)
                result = compare(this.Path, location.Path);

            return result;
        }


        public override string ToString() {
            if (String.IsNullOrEmpty(Path))
                return EV.ToString();
            else
                return System.IO.Path.Combine(EV.ToString(), Path);
        }
        public virtual string FullRelativeDirPath {
            get {
                if (String.IsNullOrEmpty(Path)) {
                    return EV.ToString();
                } else {
                    return System.IO.Path.Combine(EV.ToString(), Path);
                }
            }
        }


    }
}

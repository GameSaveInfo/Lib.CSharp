using System;
using System.Xml;
using System.Xml.Serialization;
using GameSaveInfo.Data.Enums;
namespace GameSaveInfo.Data.Locations {
    public class PathLocation : ALocation {
        // Used when dealing with a path
        // ONLY holds the name of the environment variable or wahtever used to figure out the root
        [XmlAttribute("ev")]
        public EnvironmentVariable EV { get; set; }
        // Holds only the relative path from the root
        [XmlAttribute("path")]
        public string Path { get; set; }

        protected PathLocation() { }

        public PathLocation(EnvironmentVariable ev, string path) {
            this.EV = ev;
            this.Path = path;
        }

        public PathLocation(PathLocation copy_me)
            : base(copy_me) {
            this.EV = copy_me.EV;
            this.Path = copy_me.Path;
        }

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



        public override int CompareTo(ALocation comparable) {
            PathLocation location = (PathLocation)comparable;
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

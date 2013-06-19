using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace GameSaveInfo {
    public class GameIdentifier : IComparable<GameIdentifier>, IEquatable<GameIdentifier> {
        public String Name { get; protected set; }
        public String OS { get; protected set; }
        public String Platform { get; protected set; }
        public String Region { get; protected set; }
        public String Media { get; protected set; }
        public String Release { get; protected set; }
        public String Type { get; protected set; }
		public int Revision { get; protected set; }


        public static readonly List<string> attributes = new List<string> { "os", "platform", "region", "media", "release", "type", "gsm_id", "revision" };

        public GameIdentifier(string name, string os, string platform, string region, string media, string release, string type, int revision)
            : this(name, release) {
            this.OS = os;
            this.Platform = platform;
            this.Release = release;
            this.Region = region;
            this.Media = media;
            this.Type = type;
			this.Revision = revision;
        }
        public GameIdentifier(string name, string release) {
            this.Name = name;
            this.Release = release;
			this.Revision = 0;
        }
        public GameIdentifier(string name, XmlElement element)
            : this(element) {
            this.Name = name;
		
        }
        public GameIdentifier(XmlElement element) {
            foreach (XmlAttribute attrib in element.Attributes) {
                if (ALocation.attributes.Contains(attrib.Name))
                    continue;

                switch (attrib.Name) {
                    case "name":
                        Name = attrib.Value;
                        break;
                    case "os":
                        OS = attrib.Value;
                        break;
                    case "platform":
                        switch (attrib.Value) {
                            case "Linux":
                            case "DOS":
                            case "OSX":
                            case "PS1":
                            case "PS2":
                            case "PS3":
                            case "PSP":
                            case "Windows":
                                OS = attrib.Value;
                                break;
                            case "Steam":
                                Platform = "SteamCloud";
                                break;
                            default:
                                Platform = attrib.Value;
                                break;
                        }
                        break;
                    case "media":
                        Media = attrib.Value;
                        break;
                    case "release":
                        Release = attrib.Value;
                        break;
                    case "region":
                        Region = attrib.Value;
                        break;
                    case "type":
                        Type = attrib.Value;
                        break;
                    case "detect":
                    case "virtualstore":
                    case "gsm_id":
                        break;
					case "revision":
						this.Revision = int.Parse(attrib.Value);
						break;
                    default:
                        throw new NotSupportedException(attrib.Name);
                }
            }
        }

        public XmlElement AddAttributes(XmlElement element) {
            XmlDocument doc = element.OwnerDocument;
            XmlAttribute attribute;
            if (!element.HasAttribute("name")) {
                attribute = doc.CreateAttribute("name");
                attribute.Value = Name;
                element.SetAttributeNode(attribute);
            }
            if (OS != null) {
                attribute = doc.CreateAttribute("os");
                attribute.Value = OS;
                element.SetAttributeNode(attribute);
            }
            if (Platform != null) {
                attribute = doc.CreateAttribute("platform");
                attribute.Value = Platform;
                element.SetAttributeNode(attribute);
            }
            if (Region != null) {
                attribute = doc.CreateAttribute("region");
                attribute.Value = Region;
                element.SetAttributeNode(attribute);
            }
            if (Media != null) {
                attribute = doc.CreateAttribute("media");
                attribute.Value = Media;
                element.SetAttributeNode(attribute);
            }
            if (Release != null) {
                attribute = doc.CreateAttribute("release");
                attribute.Value = Release;
                element.SetAttributeNode(attribute);
            }
            if (Type != null) {
                attribute = doc.CreateAttribute("type");
                attribute.Value = Type;
                element.SetAttributeNode(attribute);
            }
			if (Revision != 0) {
				attribute = doc.CreateAttribute("revision");
				attribute.Value = Revision.ToString();
				element.SetAttributeNode(attribute);
			}
            return element;
        }

        public static int Compare(GameIdentifier a, GameIdentifier b, bool ignore_revision) {
            int result = compare(a.Name, b.Name);

            if (result == 0)
                result = compare(a.Release, b.Release);
            if (result == 0)
                result = compare(a.OS, b.OS);
            if (result == 0)
                result = compare(a.Platform, b.Platform);
            if (result == 0)
                result = compare(a.Region, b.Region);
            if (result == 0)
                result = compare(a.Media, b.Media);
            if (result == 0)
                result = compare(a.Type, b.Type);
			if (!ignore_revision && result == 0)
				result = compare(a.Revision, b.Revision);

            return result;
        }

		public int CompareTo(GameIdentifier comparable) {
			return Compare(this, comparable, false);
		}

		public int CompareTo(GameIdentifier comparable, bool ignore_revision) {
			return Compare(this, comparable, ignore_revision);
		}

        public static bool Equals(GameIdentifier a, GameIdentifier b) {
            return Compare(a, b, false) == 0;
        }

        public Boolean Equals(GameIdentifier to_me) {
            return Equals(this, to_me as GameIdentifier);
        }

        public static String ToString(GameIdentifier id) {
            StringBuilder return_me = new StringBuilder(id.Name);

            if (id.Release != null)
                return_me.Append(" " + id.Release);
            if (id.Type != null)
                return_me.Append(" " + id.Type);
            if (id.OS != null)
                return_me.Append(" " + id.OS);
            if (id.Platform != null)
                return_me.Append(" " + id.Platform);
            if (id.Region != null)
                return_me.Append(" " + id.Region);
            if (id.Media != null)
                return_me.Append(" " + id.Media);
			if (id.Revision != 0)
				return_me.Append(" " + id.Revision);
            return return_me.ToString();
        }

		public override string ToString() {
			return GameIdentifier.ToString(this);
		}

        public override int GetHashCode() {
            int re = Name.GetHashCode();
            if (OS != null)
                re += OS.GetHashCode();
            if (Platform != null)
                re += Platform.GetHashCode();
            if (Region != null)
                re += Region.GetHashCode();
            if (Media != null)
                re += Media.GetHashCode();
            if (Release != null)
                re += Release.GetHashCode();
            if (Type != null)
                re += Type.GetHashCode();
			if (Revision != 0)
				re += Revision.GetHashCode();
            return re;
        }

        protected static int compare(IComparable a, IComparable b) {
            if (a == null) {
                if (b == null)
                    return 0;
                else
                    return -1;
            } else {
                return a.CompareTo(b);
            }
        }
        public bool Matches(string os, string platform, string region, string media, string release, string type) {
            if(os != this.OS)
                return false;

            if( platform != this.Platform)
                return false;

            if (region !=this.Region)
                return false;

            if( media != this.Media)
                return false;

            if (release != this.Release)
                return false;

            if (type != this.Type)
                return false;

            return true;
        }
    }
}

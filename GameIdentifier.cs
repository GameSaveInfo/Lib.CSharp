using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace GameSaveInfo {
    public class GameIdentifier : IComparable<GameIdentifier>, IEquatable<GameIdentifier> {



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
            if (!String.IsNullOrEmpty(OS)) {
                attribute = doc.CreateAttribute("os");
                attribute.Value = OS;
                element.SetAttributeNode(attribute);
            }
            if (!String.IsNullOrEmpty(Platform)) {
                attribute = doc.CreateAttribute("platform");
                attribute.Value = Platform;
                element.SetAttributeNode(attribute);
            }
            if (!String.IsNullOrEmpty(Region)) {
                attribute = doc.CreateAttribute("region");
                attribute.Value = Region;
                element.SetAttributeNode(attribute);
            }
            if (!String.IsNullOrEmpty(Media)) {
                attribute = doc.CreateAttribute("media");
                attribute.Value = Media;
                element.SetAttributeNode(attribute);
            }
            if (!String.IsNullOrEmpty(Release)) {
                attribute = doc.CreateAttribute("release");
                attribute.Value = Release;
                element.SetAttributeNode(attribute);
            }
            if (!String.IsNullOrEmpty(Type)) {
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



        public static String ToString(GameIdentifier id) {
            StringBuilder return_me = new StringBuilder(id.Name);

            if (!String.IsNullOrEmpty(id.Release))
                return_me.Append(" " + id.Release);
            if (!String.IsNullOrEmpty(id.Type))
                return_me.Append(" " + id.Type);
            if (!String.IsNullOrEmpty(id.OS))
                return_me.Append(" " + id.OS);
            if (!String.IsNullOrEmpty(id.Platform))
                return_me.Append(" " + id.Platform);
            if (!String.IsNullOrEmpty(id.Region))
                return_me.Append(" " + id.Region);
            if (!String.IsNullOrEmpty(id.Media))
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
            if (!String.IsNullOrEmpty(OS))
                re += OS.GetHashCode();
            if (!String.IsNullOrEmpty(Platform))
                re += Platform.GetHashCode();
            if (!String.IsNullOrEmpty(Region))
                re += Region.GetHashCode();
            if (!String.IsNullOrEmpty(Media))
                re += Media.GetHashCode();
            if (!String.IsNullOrEmpty(Release))
                re += Release.GetHashCode();
            if (!String.IsNullOrEmpty(Type))
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

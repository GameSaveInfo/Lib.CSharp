using System;
using System.Text;
using System.Xml;
namespace GameSaveInfo {
    public abstract class APlayStationID : LocationPath {
        public string prefix, suffix, append = null, type = null;
        public Int16 Disc = -1;
        protected APlayStationID(GameVersion parent, XmlElement element)
            : base(parent, element) {
        }

        public void clearPath() {
            this.Path = null;
        }

        protected override void LoadData(XmlElement element) {
            foreach (XmlAttribute atr in element.Attributes) {
                switch (atr.Name) {
                    case "prefix":
                        prefix = atr.Value;
                        break;
                    case "suffix":
                        suffix = atr.Value;
                        break;
                    case "append":
                        append = atr.Value;
                        break;
                    case "type":
                        type = atr.Value;
                        break;
                    case "disc":
                        Disc = Int16.Parse(atr.Value);
                        break;
                    default:
                        throw new NotSupportedException(atr.Name);
                }
            }
        }
        protected override XmlElement WriteData(XmlElement element) {
            addAtribute(element, "prefix", prefix);
            addAtribute(element, "suffix", suffix);
            addAtribute(element, "append", append);
            addAtribute(element, "type", type);
            return element;
        }

        public override string ElementName {
            get { return "ps_code"; }
        }

        public static APlayStationID Create(GameVersion parent, XmlElement element) {
            APlayStationID id;
            switch (parent.ID.OS) {
                case "PS1":
                    id = new PlayStation1ID(parent, element);
                    break;
                case "PS2":
                    id = new PlayStation2ID(parent, element);
                    break;
                case "PS3":
                    id = new PlayStation3ID(parent, element);
                    break;
                case "PSP":
                    id = new PlayStationPortableID(parent, element);
                    break;
                default:
                    throw new NotSupportedException(parent.ID.OS);
            }
            return id;
        }

        public Include convertToInclude() {
            Include save;

            if (this.GetType() == typeof(PlayStationPortableID) ||
                this.GetType() == typeof(PlayStation3ID)) {
                save = new Include(null as string, this.ToString(), this.type);
            } else if (this.GetType() == typeof(PlayStation2ID) || this.GetType() == typeof(PlayStation1ID)) {
                save = new Include(this.ToString(), null as String, this.type);
            } else {
                throw new NotSupportedException(this.GetType().ToString());
            }
            return save;
        }

        protected string SavePattern() {
            StringBuilder pattern = new StringBuilder();
            pattern.Append(prefix);
            pattern.Append(suffix);
            if (append != null) {
                pattern.Append(append);
            }
            pattern.Append("*");
            return pattern.ToString();
        }
        protected string ExportPattern() {
            StringBuilder pattern = new StringBuilder();
            pattern.Append("BA");
            pattern.Append(prefix);
            pattern.Append("?");
            pattern.Append(suffix);
            if (append != null) {
                pattern.Append(append);
            }
            pattern.Append("*");
            return pattern.ToString();
        }
        public abstract override string ToString();
        public override int CompareTo(ALocation comparable) {
            APlayStationID id = comparable as APlayStationID;
            int result = 0;

            result = compare(this.prefix, id.prefix);

            if (result == 0)
                result = compare(this.suffix, id.suffix);

            if (result == 0)
                result = compare(this.append, id.append);


            return result;
        }

    }






}

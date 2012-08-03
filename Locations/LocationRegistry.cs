using System;
using System.Xml;

namespace GameSaveInfo {
    public class LocationRegistry : ALocation {
        // Used when delaing with a registry key
        public string Root { get; protected set; }
        public string Key { get; protected set; }
        public string Value { get; protected set; }

        public override string ElementName {
            get { return "registry"; }
        }

        public LocationRegistry(Locations loc, string root, string key, string value): base(loc) {
            this.Root = root;
            this.Key = key;
            this.Value = value;
        }


        public LocationRegistry(Locations loc, XmlElement element)
            : base(loc,element) {
        }
        protected override void LoadMoreData(XmlElement element) {
            foreach (XmlAttribute attrib in element.Attributes) {
                if (attributes.Contains(attrib.Name))
                    continue;

                switch (attrib.Name) {
                    case "root":
                        Root = attrib.Value;
                        break;
                    case "key":
                        Key = attrib.Value;
                        break;
                    case "value":
                        Value = attrib.Value;
                        break;
                    default:
                        throw new NotSupportedException(attrib.Name);
                }
            }
        }

        protected override XmlElement WriteMoreData(XmlElement element) {
            addAtribute(element, "root", Root);
            addAtribute(element, "key", Key);
            addAtribute(element, "value", Value);
            return element;
        }

        public override int CompareTo(ALocation comparable) {
            LocationRegistry location = (LocationRegistry)comparable;
            int result = compare(Root, location.Root);
            if (result == 0)
                result = compare(Key, location.Key);
            if (result == 0)
                result = compare(Value, location.Value);

            return result;
        }

        public static RegRoot parseRegRoot(string parse_me) {
            if (parse_me.ToLower().StartsWith("hkey_"))
                parse_me = parse_me.Substring(5);

            switch (parse_me.ToLower()) {
                case "classes_root":
                    return RegRoot.classes_root;
                case "current_user":
                    return RegRoot.current_user;
                case "current_config":
                    return RegRoot.current_config;
                case "dyn_data":
                    return RegRoot.dyn_data;
                case "local_machine":
                    return RegRoot.local_machine;
                case "performance_data":
                    return RegRoot.performace_data;
                case "users":
                    return RegRoot.users;
            }
            throw new NotImplementedException("The specified key root in " + parse_me + " is not recognized. You either spelled it wrong or something.");
        }
    }
}

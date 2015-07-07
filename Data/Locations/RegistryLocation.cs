using System;
using System.Xml;
using System.Text;
namespace GameSaveInfo.Data.Locations {
    public class RegistryLocation: ALocation {
        // Used when delaing with a registry key
        public string Root { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public RegistryLocation() { }

        public RegistryLocation(string root, string key, string value) {
            this.Root = root;
            this.Key = key;
            this.Value = value;
        }



        public override int CompareTo(ALocation comparable) {
            RegistryLocation location = (RegistryLocation)comparable;
            int result = compare(Root, location.Root);
            if (result == 0)
                result = compare(Key, location.Key);
            if (result == 0)
                result = compare(Value, location.Value);

            return result;
        }


		public override string ToString() {
			StringBuilder output = new StringBuilder(@"\HKEY_");
			output.Append(this.Root.ToUpper());
			output.Append(@"\");
			output.Append(this.Key);
			output.Append(@"\");
			if (!String.IsNullOrEmpty(Value)) {
				output.Append(this.Value);
			}
			return output.ToString();
			
		}
    }
}

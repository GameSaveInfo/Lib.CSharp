using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSaveInfo {
    class VersionNotSupportedException: NotSupportedException {
        public Version SupportedVersion {
            get {
                return GameXmlFile.SupportedVersion;
            }
        }
        public Version FileVersion { get; protected set; }

        public VersionNotSupportedException(Version fileversion) {
            FileVersion = fileversion;
        }
    }
}

using System.Collections.Generic;
using System.Xml.Serialization;

namespace GameSaveInfo.Data.Enums {
    // Comparer class for alphabetically sorting Enums
    public class EnvironmentVariableAlphabeticalCompare : IComparer<EnvironmentVariable> {
        public int Compare(EnvironmentVariable x, EnvironmentVariable y) {
            return x.ToString().CompareTo(y.ToString());
        }
    }


    public enum EnvironmentVariable {
        // These are ordered according to priority, do NOT reorganize
        [XmlEnum("none")]
        None,
        [XmlEnum("altsavepaths")]
        AltSavePaths,
        [XmlEnum("installlocation")]
        InstallLocation,
		
		// Linux evs, pretty much always just these two.
        [XmlEnum("root")]
		Root,
        [XmlEnum("home")]
        Home,
		
		// Now we start the windows EVs
        [XmlEnum("drive")]
        Drive,

        [XmlEnum("allusersprofile")]
        AllUsersProfile,
        [XmlEnum("public")]
        Public,
        [XmlEnum("commonapplicationdata")]
        CommonApplicationData,

        //The point of the ordering is so that certain relative roots can take precedence
        // For instance, user profile is the most general
        [XmlEnum("userprofile")]
        UserProfile,
        // And all these are within that path, but more specific locations
        [XmlEnum("userdocuments")]
        UserDocuments,
        [XmlEnum("appdata")]
        AppData,
        [XmlEnum("localappdata")]
        LocalAppData,
        [XmlEnum("savedgames")]
        SavedGames,
        [XmlEnum("desktop")]
        Desktop,
        [XmlEnum("startmenu")]
        StartMenu,

        [XmlEnum("virtualstore")]
        VirtualStore,

        // In the analyzer we prefer the real program files path over the virtualstore one, so we give them higher priority
        [XmlEnum("programfiles")]
        ProgramFiles,
        [XmlEnum("programfilesx86")]
        ProgramFilesX86,


        [XmlEnum("flashshared")]
        FlashShared,
        [XmlEnum("ubisoftsavestorage")]
        UbisoftSaveStorage,


        // We also prefer Steam paths over Program Files
        [XmlEnum("steamuser")]
        SteamUser,
        [XmlEnum("steamcommon")]
        SteamCommon,
        [XmlEnum("steamsourcemods")]
        SteamSourceMods,
        [XmlEnum("steamuserdata")]
        SteamUserData,


        [XmlEnum("PS3Export")]
        PS3Export,
        [XmlEnum("PS3Save")]
        PS3Save,
        [XmlEnum("PSPSave")]
        PSPSave
    }
}

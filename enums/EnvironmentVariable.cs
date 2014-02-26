using System.Collections.Generic;
namespace GameSaveInfo {
    // Comparer class for alphabetically sorting Enums
    public class EnvironmentVariableAlphabeticalCompare : IComparer<EnvironmentVariable> {
        public int Compare(EnvironmentVariable x, EnvironmentVariable y) {
            return x.ToString().CompareTo(y.ToString());
        }
    }

    public enum EnvironmentVariable {
        // These are ordered according to priority, do NOT reorganize
        None,
        AltSavePaths,
        InstallLocation,

        Drive,

        AllUsersProfile,
        Public,
        CommonApplicationData,

        //The point of the ordering is so that certain relative roots can take precedence
        // For instance, user profile is the most general
        UserProfile,
        // And all these are within that path, but more specific locations
        UserDocuments,
        AppData,
        LocalAppData,
        SavedGames,
        Desktop,
        StartMenu,

        VirtualStore,

        // In the analyzer we prefer the real program files path over the virtualstore one, so we give them higher priority
        ProgramFiles,
        ProgramFilesX86,


        FlashShared,
        UbisoftSaveStorage,


        // We also prefer Steam paths over Program Files
        SteamUser,
        SteamCommon,
        SteamSourceMods,
        SteamUserData,


        PS3Export,
        PS3Save,
        PSPSave
    }
}

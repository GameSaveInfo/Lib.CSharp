﻿
namespace GameSaveInfo {

    public enum EnvironmentVariable {
        // These are ordered according to priority, do NOT reorganize
        None,
        AltSavePaths,
        InstallLocation,
		
		// Linux evs, pretty much always just these two.
		Root,
		Home,
		
		// Now we start the windows EVs
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

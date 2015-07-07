using System.Xml;

namespace GameSaveInfo.Data.Locations {
    public class ParentLocation : ALocation {
        // Used when dealing with a game root

        public AProgramID game;

        public override int CompareTo(ALocation comparable) {
            ParentLocation location = (ParentLocation)comparable;
            return game.CompareTo(location.game);
        }

    }
}

using System.Xml;

namespace GameSaveInfo {
    public class LocationParent : ALocation {
        // Used when dealing with a game root

        public override string ElementName {
            get { return "parent"; }
        }

        public GameIdentifier game;

        public LocationParent(Locations loc, XmlElement element)
            : base(loc, element) {

        }

        protected override void LoadMoreData(XmlElement element) {
            this.game = new GameIdentifier(element);
        }

        protected override XmlElement WriteMoreData(XmlElement element) {
            game.AddAttributes(element);
            return element;
        }

        public override int CompareTo(ALocation comparable) {
            LocationParent location = (LocationParent)comparable;
            return game.CompareTo(location.game);
        }

    }
}

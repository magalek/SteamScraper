using System;
using System.Text;

namespace SteamScraper {
    public class Categories {
        public const string Action = "19";
        public const string Indie = "2C492";

        internal static string Get(params string[] tags) {
            StringBuilder builder = new StringBuilder();
            builder.Append("&tags=");
            
            for (int i = 0; i < tags.Length; i++) {
                if (string.IsNullOrEmpty(tags[i])) {
                    break;
                }

                string tagFormatted = tags[i].ToLower();

                if (i != 0) {
                    builder.Append("%");
                }
                switch (tagFormatted) {
                    case "action": builder.Append(Action);
                        break;
                    case "indie": builder.Append(Indie);
                        break;
                }
            }
            return builder.ToString();
        }
    }
}
namespace SteamScraper {
    public class Game {
        public int Id { get; }
        public string Name { get; }
        public string Url { get; }
        public float Price { get; }
        public float DiscountPrice { get; }
        public bool Available { get; }

        public Game(int id, string name, string url, float price, float discountPrice = -1, bool available = true) {
            Id = id;
            Name = name;
            Url = url;
            Price = price;
            DiscountPrice = discountPrice;
            Available = available;
        }
    }
}
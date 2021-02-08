using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace SteamScraper {
    // ReSharper disable once UnusedType.Global
    public class Scraper {
        
        // ReSharper disable once UnusedMember.Global
        public List<Game> Scrap(params string[] tags) {
            HtmlWeb web = new HtmlWeb();

            string steamUrl = "https://store.steampowered.com/search/?sort_by=Released_DESC&category3=1";

            if (tags.Length > 0) steamUrl += Categories.Get(tags);

            HtmlDocument document = web.Load(steamUrl);
            
            var results = document.DocumentNode.CssSelect("div");

            return GetGames(results).ToList();
        }

        private IEnumerable<Game> GetGames(IEnumerable<HtmlNode> nodes) {
            foreach (HtmlNode htmlNode in nodes) {
                if (htmlNode.Id == "search_resultsRows") {
                    var children = htmlNode.CssSelect("a");

                    foreach (HtmlNode child in children) {
                        Console.OutputEncoding = Encoding.UTF8;

                        string url = child.Attributes["href"]?.Value;
                        string name = child.SelectSingleNode("div[2]/div[1]/span").InnerText.Trim();
                        int id = Int32.Parse(child.Attributes["data-ds-appid"].Value);
                        string pricesDictionaryUnformatted = child.SelectSingleNode("div[2]/div[4]/div[2]").InnerText.Trim();

                        float price;
                        float discountPrice;

                        if (!String.IsNullOrWhiteSpace(pricesDictionaryUnformatted) && pricesDictionaryUnformatted.All(c => !char.IsDigit(c))) {
                            price = 0;
                            discountPrice = -1;
                        }
                        else if (String.IsNullOrWhiteSpace(pricesDictionaryUnformatted)) {
                            price = -1;
                            discountPrice = -1;
                        }
                        else {
                            string[] pricesDictionaryFormatted = pricesDictionaryUnformatted.Split("zł");
                            string normalPrice = pricesDictionaryFormatted[0].Trim().Replace(",", ".");
                            
                            price = (float)Convert.ToDouble(normalPrice);
                            if (pricesDictionaryFormatted.Length > 1) {
                                string priceAfterDiscount = pricesDictionaryFormatted[1].Trim().Replace(",", ".");
                                if (!string.IsNullOrEmpty(priceAfterDiscount) && float.TryParse(priceAfterDiscount, out discountPrice)) {
                                    continue;
                                }
                                discountPrice = -1;
                            }
                            else discountPrice = -1;
                        }
                        yield return new Game(id, name, url, price, discountPrice, price >= 0);
                    }
                }
            }
        }
    }
}
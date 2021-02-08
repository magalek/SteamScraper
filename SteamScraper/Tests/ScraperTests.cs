using System.Collections.Generic;
using NUnit.Framework;

namespace SteamScraper.Tests {
    [TestFixture]
    public class ScraperTests {
        [Test]
        public void InvokesSuccessfully() {
            Scraper scraper = new Scraper();
            Assert.DoesNotThrow(() => scraper.Scrap());
        }
        
        [Test]
        public void ReturnsData() {
            Scraper scraper = new Scraper();
            var games = scraper.Scrap();
            Assert.IsNotEmpty(games);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_test
{
    [TestClass]
    public class UnitTest5
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void SortingCountries()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));

            driver.FindElement(By.LinkText("Countries")).Click();

            IWebElement tagH1Countries = driver.FindElement(By.TagName("h1"));
            NUnit.Framework.Assert.AreEqual(tagH1Countries.Text, "Countries");

            // 1.а) check countries sorting

            IList<IWebElement> countries = driver.FindElements(By.CssSelector("table.dataTable tr.row"));
            List <String> countriesNames = new List<string>();
            List<String> countriesZonesNames = new List<string>();


            foreach (IWebElement country in countries)
            {
                string countryName = country.FindElement(By.CssSelector("td:nth-of-type(5)")).Text;
                int countryZones = Int32.Parse(country.FindElement(By.CssSelector("td:nth-child(6)")).Text);
                countriesNames.Add(countryName);

                if(countryZones > 0)
                {
                    countriesZonesNames.Add(countryName);
                }

            }
            
            var sortedCountries = new List<string>();
            sortedCountries.AddRange(countriesNames.OrderBy(t => t));
            NUnit.Framework.Assert.IsTrue(countriesNames.SequenceEqual(sortedCountries));

            // 1.b) check if countries have zones and then check sorting

            foreach (string zonesCountry in countriesZonesNames)
            {
                IWebElement countryZoneName = driver.FindElement(By.XPath(".//a[text() = '" + zonesCountry + "']"));
                countryZoneName.Click();

                IList<IWebElement> zones = driver.FindElements(By.CssSelector(".dataTable td:nth-of-type(3)"));
                
                List<String> zonesName = new List<string>();

                foreach(IWebElement zoneElement in zones)
                    {
                    zonesName.Add(zoneElement.Text);
                    }

                zonesName.Remove("");

                var sortedZones = new List<String>();
                sortedZones.AddRange(zonesName.OrderBy(z => z));
                NUnit.Framework.Assert.IsTrue(zonesName.SequenceEqual(sortedZones));

                zones = null;

                driver.Navigate().Back();

            }

            // 2. check geo zones sorting

            driver.FindElement(By.LinkText("Geo Zones")).Click();
            IWebElement tagH1GeoZones = driver.FindElement(By.TagName("h1"));
            NUnit.Framework.Assert.AreEqual(tagH1GeoZones.Text, "Geo Zones");

            IList<IWebElement> Geozones = driver.FindElements(By.CssSelector(".dataTable td:nth-of-type(3)"));

            List<String> geoZonesNames = new List<string>();

            foreach(IWebElement geoElement in Geozones)
            {
                geoZonesNames.Add(geoElement.Text);
            }

            var sortedGeoZones = new List<String>();
            sortedGeoZones.AddRange(geoZonesNames.OrderBy(g => g));
            NUnit.Framework.Assert.IsTrue(geoZonesNames.SequenceEqual(sortedGeoZones));

        }



        

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}

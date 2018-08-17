using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_test
{
    [TestClass]
    public class UnitTest6
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
        public void CampaignsCheck()
        {
            driver.Url = "http://localhost/litecart/";
            wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));

            IWebElement campaignDuck = driver.FindElement(By.XPath("//div[@id='box-campaigns']//ul[@class='listing-wrapper products']/li/a[1]"));

            String campaignProductNameMain = campaignDuck.FindElement(By.CssSelector(".name")).Text;
            String regularPriceMain = campaignDuck.FindElement(By.CssSelector("s.regular-price")).Text;
            String campaignPriceMain = campaignDuck.FindElement(By.CssSelector("strong.campaign-price")).Text;
            String regularPriceMainStyleText = campaignDuck.FindElement(By.CssSelector("s.regular-price")).GetCssValue("text-decoration-line");
            String regularPriceMainStyleColor = campaignDuck.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color");
            String campaignPriceMainStyleText = campaignDuck.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-size");
            String campaignPriceMainStyleColor = campaignDuck.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color");

            campaignDuck.Click();

            IWebElement tagH1Product = driver.FindElement(By.TagName("h1"));
            NUnit.Framework.Assert.AreEqual(tagH1Product.Text, "Yellow Duck");

            String regularPriceProductPage = driver.FindElement(By.CssSelector(".regular-price")).Text;
            String campaignPriceProductPage = driver.FindElement(By.CssSelector(".campaign-price")).Text;

            String regularPriceStyleText = driver.FindElement(By.TagName("s")).GetCssValue("text-decoration-line");
            String regularPriceStyleColor = driver.FindElement(By.CssSelector(".regular-price")).GetCssValue("color");

            String campaignPriceStyleColor = driver.FindElement(By.CssSelector(".campaign-price")).GetCssValue("color");
            String campaignPriceStyleText = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-size");

            NUnit.Framework.Assert.AreEqual(campaignProductNameMain, tagH1Product.Text); // Product Name from main page equals Name from product subpage
            NUnit.Framework.Assert.AreNotEqual(regularPriceMain, regularPriceProductPage); // Product regular price from main page equals regular price from product subpage
            NUnit.Framework.Assert.AreEqual(campaignPriceMain, campaignPriceProductPage); // Product discount price from main page equals discount price from product subpage

            NUnit.Framework.Assert.AreEqual(regularPriceStyleText, "line-through");
            NUnit.Framework.Assert.AreEqual(regularPriceStyleColor, "rgba(102, 102, 102, 1)");
            NUnit.Framework.Assert.AreEqual(campaignPriceStyleColor, "rgba(204, 0, 0, 1)");
            NUnit.Framework.Assert.AreEqual(campaignPriceStyleText, "22px");

        }





        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}

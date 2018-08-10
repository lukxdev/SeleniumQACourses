using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_test
{
    [TestClass]
    public class UnitTest4
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
        public void productStickers()
        {
            driver.Url = "http://localhost/litecart/";            
            wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));
            IList<IWebElement> ducks = driver.FindElements(By.CssSelector("li.product"));
            
            foreach(IWebElement sticker in ducks)
            {
                NUnit.Framework.Assert.AreEqual(isDuckHasStickers(sticker), false);
            }


        }

        private Boolean isDuckHasStickers(IWebElement sticker)
        {
            return sticker.FindElement(By.CssSelector("div.sticker")).Equals(1);
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}

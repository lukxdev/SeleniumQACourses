using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_test
{
    [TestClass]
    public class UnitTest3  
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
        public void liteCartAdminLeftMenuTest()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));
            driver.FindElement(By.LinkText("Appearence")).Click();
            IWebElement tagH1Template = driver.FindElement(By.TagName("h1"));
            NUnit.Framework.Assert.AreEqual(tagH1Template.Text, "Template");

            driver.FindElement(By.LinkText("Logotype")).Click();
            IWebElement tagH1Logotype = driver.FindElement(By.TagName("h1"));
            NUnit.Framework.Assert.AreEqual(tagH1Logotype.Text, "Logotype");

            driver.FindElement(By.XPath("//ul[@id='box-apps-menu']/li[2]//span[@class='name']")).Click();
            IWebElement tagH1Catalog = driver.FindElement(By.TagName("h1"));
            NUnit.Framework.Assert.AreEqual(tagH1Catalog.Text, "Catalog");

            driver.FindElement(By.LinkText("Product Groups")).Click();
            IWebElement tagH1ProductGroups = driver.FindElement(By.TagName("h1"));
            NUnit.Framework.Assert.AreEqual(tagH1ProductGroups.Text, "Product Groups");

            driver.FindElement(By.CssSelector("[href='http\\:\\/\\/localhost\\/litecart\\/admin\\/\\?app\\=countries\\&doc\\=countries'] .name")).Click();
            IWebElement tagH1Countries = driver.FindElement(By.TagName("h1"));
            NUnit.Framework.Assert.AreEqual(tagH1Countries.Text, "Countries");
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}

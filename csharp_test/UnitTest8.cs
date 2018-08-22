using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_test
{
    [TestClass]
    public class UnitTest8
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
        public void AddNewProduct()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));

            driver.FindElement(By.XPath("//ul[@id='box-apps-menu']/li[2]//span[@class='name']")).Click();

            IWebElement addNewProductButton = driver.FindElement(By.XPath(".//a[text() = ' Add New Product']"));
            addNewProductButton.Click();

            IWebElement radio = driver.FindElement(By.Name("status"));
            radio.Click();
                       

            IWebElement productName = driver.FindElement(By.Name("name[en]"));
            String newProduct = "Test Duck";
            productName.Click();
            productName.Clear();
            productName.SendKeys(newProduct);

            IWebElement productCode = driver.FindElement(By.Name("code"));
            productCode.Click();
            productCode.Clear();
            productCode.SendKeys("123456");

            driver.FindElement(By.XPath("/html//div[@id='tab-general']/table/tbody/tr[7]/td/div[@class='input-wrapper']/table/tbody/tr[4]/td[1]/input[@name='product_groups[]']")).Click();

            IWebElement quantity = driver.FindElement(By.Name("quantity"));
            quantity.Clear();
            quantity.SendKeys("10");

            
            driver.FindElement(By.Name("date_valid_from")).SendKeys("01012018");
            driver.FindElement(By.Name("date_valid_to")).SendKeys("31122022");


            driver.FindElement(By.XPath("//a[@href='#tab-information']")).Click();

            SelectElement manufacturer = new SelectElement(driver.FindElement(By.Name("manufacturer_id")));
            manufacturer.SelectByText("ACME Corp.");

            IWebElement keywordsTextBox = driver.FindElement(By.Name("keywords"));
            keywordsTextBox.Click();
            keywordsTextBox.Clear();
            keywordsTextBox.SendKeys("new, duck, test");
            
            IWebElement shortDescriptionText = driver.FindElement(By.Name("short_description[en]"));
            shortDescriptionText.Click();
            shortDescriptionText.Clear();
            shortDescriptionText.SendKeys("This is a test duck");

            IWebElement descriptionText = driver.FindElement(By.CssSelector("div.trumbowyg-editor"));
            descriptionText.SendKeys("This is a description of a new test duck");

            driver.FindElement(By.Name("head_title[en]")).SendKeys("A BRANDNEW DUCK");


            IWebElement metaDescriptionText = driver.FindElement(By.Name("meta_description[en]"));
            metaDescriptionText.SendKeys("a new duck");

            driver.FindElement(By.XPath("//a[@href='#tab-prices']")).Click();


            driver.FindElement(By.Name("purchase_price")).SendKeys("79");
            
            SelectElement currency = new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code")));
            currency.SelectByText("Euros");

            IWebElement usdPrice =  driver.FindElement(By.XPath("//input[@name='gross_prices[USD]']"));
            usdPrice.SendKeys("45,98");

            IWebElement eurPrice = driver.FindElement(By.XPath("//input[@name='gross_prices[EUR]']"));
            eurPrice.SendKeys("35,98");

            driver.FindElement(By.Name("save")).Click();
            

            driver.FindElement(By.XPath(".//span[text() = 'Catalog']")).Click();

            IWebElement isProductAdded = driver.FindElement(By.XPath(".//a[text() = '" + newProduct + "']"));
            NUnit.Framework.Assert.AreEqual(isProductAdded.Text, "Test Duck");

        }





        [TearDown]
        public void Stop()
        {
            //driver.Quit();
            //driver = null;
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_test
{
    [TestClass]
    public class UnitTest9
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
        public void AddDeleteProductsFromCart()
        {
            driver.Url = "http://localhost/litecart/";
            wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));

            for (int i = 0; i < 3; i++)
            {
                driver.FindElement(By.CssSelector("div.content ul li.product a")).Click();
                driver.FindElement(By.Name("add_cart_product")).Click();


                IWebElement cartInfo = driver.FindElement(By.CssSelector("div#cart span.quantity"));
                int cartItems = Int32.Parse(cartInfo.Text);

                WebDriverWait waitInfoCart = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitInfoCart.Until(ExpectedConditions.TextToBePresentInElement(cartInfo, (cartItems + 1).ToString()));

                driver.Navigate().Back();
            }

            driver.FindElement(By.PartialLinkText("Checkout")).Click();

            int cartQuantity =
            driver.FindElements(By.CssSelector("table.dataTable tr td.item")).Count;

            while (cartQuantity > 0)
            {

                if (driver.FindElements(By.CssSelector("table.dataTable tr td.item")).Count == cartQuantity)
                {
                    IWebElement remove = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(
                        "form[name = 'cart_form'] button[name = 'remove_cart_item']")));
                    remove.Click();
                    wait.Until(ExpectedConditions.StalenessOf(remove));
                    cartQuantity--;
                }
            }

        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}

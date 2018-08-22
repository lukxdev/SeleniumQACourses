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
    public class UnitTest7
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
        public void UserRegistration()
        {
            driver.Url = "http://localhost/litecart/";
            wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));

            driver.FindElement(By.LinkText("New customers click here")).Click();

            Dictionary<string, string> accountInformation = new Dictionary<string, string>();
            accountInformation.Add("tax_id", "0000001");
            accountInformation.Add("company", "Sherlock LTD");
            accountInformation.Add("firstname", "Sherlock");
            accountInformation.Add("lastname", "Holmes");
            accountInformation.Add("address1", "Baker Street 221B");
            accountInformation.Add("address2", "W1 6XE");
            accountInformation.Add("postcode", "WC2N 5DU");
            accountInformation.Add("city", "London");

            foreach (KeyValuePair<string, string> entry in accountInformation)
            {
                string element = entry.Key;
                string val = entry.Value;

                IWebElement regText = driver.FindElement(By.Name(element));
                regText.SendKeys(val);
            }

            SelectElement country = new SelectElement(driver.FindElement(By.Name("country_code")));
            country.SelectByText("United Kingdom");

            driver.FindElement(By.Name("email")).SendKeys("sherlock@holmes.com");
            driver.FindElement(By.Name("phone")).SendKeys("+4498752264999");
            driver.FindElement(By.Name("password")).SendKeys("sherlock999");
            driver.FindElement(By.Name("confirmed_password")).SendKeys("sherlock999");

            driver.FindElement(By.Name("create_account")).Click();

            IWebElement isUserLogged = driver.FindElement(By.LinkText("Logout"));
            NUnit.Framework.Assert.AreEqual(isUserLogged.Text, "Logout");

            isUserLogged.Click();

            driver.FindElement(By.Name("email")).SendKeys("sherlock@holmes.com");
            driver.FindElement(By.Name("password")).SendKeys("sherlock999");

            driver.FindElement(By.Name("login")).Click();
            IWebElement isUserLoggedAfterLogin = driver.FindElement(By.LinkText("Logout"));
            NUnit.Framework.Assert.AreEqual(isUserLoggedAfterLogin.Text, "Logout");

            isUserLoggedAfterLogin.Click();

            IWebElement isUserLoggedOut = driver.FindElement(By.CssSelector(".success"));
            NUnit.Framework.Assert.AreEqual(isUserLoggedOut.Text, "You are now logged out.");
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}

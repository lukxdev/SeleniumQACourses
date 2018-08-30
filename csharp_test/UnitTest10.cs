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
    public class UnitTest10
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
        public void LinksOpen()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));

            driver.FindElement(By.LinkText("Countries")).Click();
            driver.FindElement(By.LinkText("Austria")).Click();

            IList<IWebElement> links = driver.FindElements(By.CssSelector("i.fa-external-link"));

            for(int i = links.Count; i > 0; i--)
            {
                string mainWindow = driver.CurrentWindowHandle;
                ICollection<string> oldWindows = driver.WindowHandles;

                IWebElement link = links[i - 1];
                link.Click();

                WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

                wait.Until((d) => driver.WindowHandles.Count == 2);

                List<string> newWindow = driver.WindowHandles.ToList();

                foreach (String handle in newWindow)
                {
                    driver.SwitchTo().Window(handle);
                }

                driver.Close();
                driver.SwitchTo().Window(mainWindow);
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

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
    public class UnitTest11
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
        public void BrowserLogsCheck()
        {
            driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));           

            int products = driver.FindElements(By.XPath("//td[3]/a[contains(@href, 'product_id')]")).Count;

            for (int i = 0; i < products; i++)
            {
                IList<IWebElement> eachProduct = driver.FindElements(By.XPath("//td[3]/a[contains(@href, 'product_id')]"));

                IWebElement duck = eachProduct[i];
                duck.Click();
                
                IList<LogEntry> logs = driver.Manage().Logs.GetLog("browser");

                Console.WriteLine("There are: " + logs.Count.ToString());

                    if (logs.Count > 0)
                    {
                        foreach (LogEntry l in logs)
                        {
                            Console.WriteLine(l);
                        }
                    }

                    driver.Navigate().Back();
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

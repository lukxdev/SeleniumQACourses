using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_test
{
    [TestClass]
    public class UnitTest1
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
        public void GoogleSearchTest()
        {
            driver.Url = "http://google.com/";
            driver.FindElement(By.Name("q")).SendKeys("selenium");
            driver.FindElement(By.Name("q")).Submit();
            wait.Until(ExpectedConditions.TitleIs("selenium - Szukaj w Google"));
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }        
    }
}

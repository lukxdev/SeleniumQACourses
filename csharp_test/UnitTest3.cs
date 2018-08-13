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


            int mainMenuItemPosition = 1;
            int childMenuItemPosition = 1;

            while (true)
            {

                String mainMenuItemCssLocator = "ul#box-apps-menu li#app-:nth-child(" + mainMenuItemPosition + ")";
                

                    if(IsElementPresent(By.CssSelector(mainMenuItemCssLocator)))
                    {
                        IWebElement mainMenuItem = driver.FindElement(By.CssSelector(mainMenuItemCssLocator));
                        mainMenuItem.Click();
                        driver.FindElement(By.CssSelector("h1"));
                    }
                    else
                    {
                    return;                    
                    }

                while (true)
                {

                    String childMenuItemCssLocator = "ul li:nth-child(" + childMenuItemPosition + ")";
                    

                    if (IsElementPresent(By.CssSelector(mainMenuItemCssLocator +
                        " " + childMenuItemCssLocator)))
                    {
                        IWebElement childMenuItem = driver.FindElement(By.CssSelector(mainMenuItemCssLocator +
                        " " + childMenuItemCssLocator));
                        childMenuItem.Click();
                        driver.FindElement(By.CssSelector("h1"));                        
                    }
                    else
                    {
                        break;                        
                    }

                    childMenuItemPosition++;
                }
                childMenuItemPosition = 1;
                mainMenuItemPosition++;
            }
            
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}

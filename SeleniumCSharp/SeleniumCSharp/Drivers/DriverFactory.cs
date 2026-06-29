using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumCSharp.Drivers
{
    public class DriverFactory
    {
        public static IWebDriver CreateDriver(string browser)
        {
            switch(browser)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver();

                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver();

                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    return new FirefoxDriver();

                default:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver();
            }
        }
    }
}

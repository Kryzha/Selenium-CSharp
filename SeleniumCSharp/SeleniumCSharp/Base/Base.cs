using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using SeleniumCSharp.Drivers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SeleniumCSharp.Base
{
    public class Base
    {
        protected IWebDriver driver;
        protected ExtentReports report;
        protected ExtentTest test;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent
                .Parent.FullName;

            String reportPath = projectDirectory + "\\Reports\\index.html";

            var htmlReporter = new ExtentSparkReporter(reportPath);
            report = new ExtentReports();
            report.AttachReporter(htmlReporter);

            report.AddSystemInfo("Environment", "QA");
            report.AddSystemInfo("Name", "Illia");

        }

        [SetUp]
        public void SetUp()
        {
            test = report.CreateTest(TestContext.CurrentContext.Test.Name);
            String browser = TestContext.Parameters["browser"];
            if (browser == null)
                browser = ConfigurationManager.AppSettings["browser"];

            driver = DriverFactory.CreateDriver(browser);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Url = "";
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                test.Fail("Test Failed");
            }

            else
            {
                test.Pass("Test Passed");
            }

            driver.Dispose();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            report.Flush();
        }

    }
}

using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using WebDriverManager.Helpers;

namespace KT4
{
    public class Tests
    {
        public string CategoryUrl = "http://localhost:5106/";

        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            try
            {
                //new DriverManager().SetUpDriver(new EdgeConfig());
                new DriverManager().SetUpDriver(new EdgeConfig()   ,VersionResolveStrategy.MatchingBrowser);

                //new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.EdgeConfig());
                // Автоматическая загрузка драйвера
                //new DriverManager().SetUpDriver(new EdgeConfig());
                EdgeOptions options = new EdgeOptions();
                //options.AddArgument("--headless"); // Режим без интерфейса
                //options.AddArgument("--no-sandbox");
                //options.AddArgument("--disable-dev-shm-usage");
                driver = new EdgeDriver(options);
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(CategoryUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        [TearDown]
        public void Cleanup()
        {
            try
            {
            //    var logs = driver.Manage().Logs.GetLog(LogType.Browser);
            //logs.Where(log => log.Level == LogLevel.Warning || log.Level == LogLevel.Severe)
            //    .ToList()
            //    .ForEach(log => Console.WriteLine($"{log.Level}: {log.Message}"));

            driver.Quit();
            driver.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
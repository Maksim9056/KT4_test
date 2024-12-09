using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KT4
{
    //[TestFixture]
    public class Log
    {
        public string _email_user, _password_user;
        WebDriverWait _wait;
        IWebDriver _driver;
        public string Filename;
        public string screenshotPath;
        public void Parametr(string email_user, string password_user, WebDriverWait wait,IWebDriver driver ,string filename)
        {
            _email_user = email_user;
            _password_user = password_user;
            _wait = wait;
            _driver = driver;
            Filename = filename+".png";
        }

        [Test]
        public void Log_user()
        {
            try
            {
                Console.WriteLine($"Вход с пользователя  почта: {_email_user} и пароль:{_password_user}");
                var email = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Selector.Email_Selector)));
                email.SendKeys(_email_user);
                Console.WriteLine(email.TagName);
                var password = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Selector.Password_Selector)));
                password.SendKeys(_password_user);
                Console.WriteLine(password.TagName);

                var button = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Selector.Button_Selector_log)));
                Console.WriteLine(button.TagName);
                button.Click();
                Console.WriteLine("Вход ");
                Thread.Sleep(TimeSpan.FromSeconds(18)); // Жёсткое ожидание 1 минута


                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), Filename);
                screenshot.SaveAsFile(screenshotPath); // Используйте формат как строку

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }

}


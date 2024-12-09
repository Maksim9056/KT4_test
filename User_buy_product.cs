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
    public class User_buy_product
    {
        string _email_user, _password_user;
        WebDriverWait _wait;
        IWebDriver _driver;
        public string Filename;
        public string screenshotPath;   
        public string screenshotPath_pay;

        public void Parametr(string email_user, string password_user, WebDriverWait wait, IWebDriver driver, string filename,string filename_pay)
        {
            _email_user = email_user;
            _password_user = password_user;
            _wait = wait;
            _driver = driver;
            Filename = filename + ".png";
            screenshotPath_pay = filename_pay + ".png";
        }


        [Test]
        public void Add_product()
        {
            try
            {
                var check  = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Selector.check_buy)));

                var ch = check.Text;
                Console.WriteLine(ch);
                if ("1" == ch)
                {
                    check.Click();
                }
                else
                {
                    var button = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Selector.button_Selector_Product_buy)));

                    button.Click();
                }



                //var  product_count = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Selector.Selector_Product_buy_Count)));

                //product_count.SendKeys("2");
                //var product_button_update_cash = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Selector.button_Selector_Product_update_cash)));

                //product_button_update_cash.Click();

                var button_now_select_product = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Selector.buy_now_click_button)));

                button_now_select_product.Click();
                


                 var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), Filename);
                screenshot.SaveAsFile(screenshotPath); // Используйте формат как строку
                Console.WriteLine($"Скриншот сохранён: {screenshotPath}");



                var button_pay = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Selector.buy_now_click_button_pay)));

                button_pay.Click();
                 screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), screenshotPath_pay);
                screenshot.SaveAsFile(screenshotPath); // Используйте формат как строку
                Console.WriteLine($"Скриншот сохранён: {screenshotPath}");

                Thread.Sleep(TimeSpan.FromSeconds(18)); // Жёсткое ожидание 1 минута

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}

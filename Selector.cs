using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT4
{
    public class Selector
    {
        public static string click = "/html[1]/body[1]/div[1]/header[1]/div[1]/article[1]/section[2]/div[1]/section[1]/div[1]/a[1]";
        public static string Email_Selector = "//input[@id='Input_Email']";
        public static string Password_Selector = "//input[@id='Input_Password']";
        public static string Button_Selector_log = "//button[contains(text(),'Log in')]";
        public static string button_Selector_Product_buy = "//body[1]/div[1]/div[1]/div[2]/div[1]/form[1]/input[1]";
        public static string Selector_Product_buy_Count = "//body/div[1]/div[1]/form[1]/div[1]/article[1]/div[1]/section[4]/input[2]";
        public static string button_Selector_Product_update_cash = "//button[contains(text(),'[ Update ]";
        public static string  check_buy = "/html/body/div/header/div/article/section[3]/a/div[2]";
        public static string buy_now_click_button = "/html/body/div/div/form/div/div[3]/section[2]/a";
        public static string buy_now_click_button_pay = "/html/body/div/div/form/div/div[3]/section[2]/input";
    }
}

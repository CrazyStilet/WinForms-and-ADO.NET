using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Autorization
{
    class CheckError
    {
        // Проверка полей на наличие ошибок ввода
        public static bool CheckErrors(TextBox tb)
        {
            // Проверка полей на заполненность
            if(tb.Text == "" || tb.Text == "Имя пользователя" || tb.Text == "Пароль")
            {
                ChangeBackColor(tb);
                return true;
            }
            #region Проверка длинны пароля
            if(tb.Name.Contains("Password"))
            {
                if(tb.TextLength < 6)
                {
                    ChangeBackColor(tb);
                    return true;
                }
            }
            #endregion
            
            #region  Проверка на провильность ввода Email'a
            if(tb.Name.Contains("Email"))
            {
                int count = 0;
                for(int i = 0; i < tb.Text.Length; i++)
                {
                    if(tb.Text[i] == '@')
                    {
                        count++;
                        i += 2;
                    }
                    else if(count == 1 && tb.Text[i] == '.')
                    {
                        if(tb.Text.Length <= i + 2)
                        {
                            ChangeBackColor(tb);
                            return true;
                        }
                        count++;
                        break;
                    }
                }
                if(count != 2)
                {
                    ChangeBackColor(tb);
                    return true;
                }
            }
            #endregion
            return false;
        }
        // Проверка полей с паролем на одинаковость
        public static bool CheckErrors(TextBox tb1,TextBox tb2)
        {
            if(tb1.Text!=tb2.Text)
            {
                tb1.BackColor = Color.LightCoral;
                tb2.BackColor = Color.LightCoral;
                return true;
            }
            return false;
        }

        public static void ChangeBackColor(TextBox tb)
        {
            tb.BackColor = Color.LightCoral;
        }
    }
}

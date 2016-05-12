using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autorization
{
    class WorkingWithDB
    {
        static string connectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\Registration.mdf;Integrated Security=True";
        public static void SqlConn()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                string cmdSelectData = @"SELECT * FROM UsersInfo";
                SqlCommand commandSelectFirst = new SqlCommand(cmdSelectData, connection);

                DbDataReader dataReader = commandSelectFirst.ExecuteReader();
                while(dataReader.Read() != false)
                {
                    string txt = dataReader["LastName"] + "\n" + dataReader["FirstName"] + "\n";
                    txt += dataReader["Adres"] + "\n" + dataReader["Phone"] + "\n" + dataReader["Code"] + "\n";
                    //tb.Text += txt;
                }
                dataReader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public static int CheckLogin(TextBox tbLogin,TextBox tbPassword, Label lblError)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                //command.CommandText = "SELECT Name, Password, Email, Id FROM Users WHERE Name = '" + tbLogin.Text + "' AND Password = '" + tbPassword.Text + "'";
                command.CommandText = "SELECT Name, Password FROM Users WHERE Name = '" + tbLogin.Text + "' AND Password = '" + tbPassword.Text + "'";

                DbDataReader dataReader = command.ExecuteReader();

                if(!dataReader.HasRows)
                {
                    //CheckError.CheckErrors(tbLogin);
                    //if(lblError.Text == "")
                    //{
                    //    lblError.Text = "Ошибка логина";
                    //}
                    //CheckError.CheckErrors(tbPassword);
                    //if(lblError.Text == "")
                    //{
                    //    lblError.Text = "Ошибка пароля";
                    //}
                    //else
                    //{
                    //    lblError.Text += " и пароля";
                    //}

                    CheckError.ChangeBackColor(tbLogin);
                    CheckError.ChangeBackColor(tbPassword);
                    lblError.Text = "Ошибка логина или пароля";

                    return -1;
                }

                while(dataReader.Read() != false)
                {
                    //string txt = dataReader["Name"].ToString() + "\n" + dataReader["Password"].ToString() + "\n" + dataReader["Email"] + "\n" + dataReader["Id"];
                    //MessageBox.Show(txt);
                }
                return 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return -1;
        }
        public static int Registration(TextBox tbLogin,TextBox tbEmail)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Name FROM Users WHERE Name = '" + tbLogin.Text + "' AND Email = '" + tbEmail.Text + "'";
                DbDataReader dataReader = command.ExecuteReader();
                if(!dataReader.HasRows)
                {
                    return 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return 0;
        }
    }
}

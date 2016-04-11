using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autorization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            tbLogin.LostFocus += tbLogin_LostFocus;
            tbLogin.GotFocus += tbLogin_GotFocus;
            tbPassword.GotFocus += tbPassword_GotFocus;
            tbPassword.LostFocus += tbPassword_LostFocus;

        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            lblErrorData.Visible = false;
            lblInspection.BackColor = Color.Transparent;
            lblInspection.ForeColor = Color.Blue;
            lblInspection.Text = "Идет проверка";
            //int x = (prgLoad.Size.Width - lblPrg.Size.Width) / 2;
            //lblPrg.Location.X.CompareTo(x);// = prgLoad.Size.Width - lblPrg.Size.Width;
            lblInspection.Visible = true;
            prgLoad.Visible = true;
            prgLoad.Value = 0;
            //for(int i = 0; i < prgLoad.Maximum; i++)
            //{
            //    prgLoad.PerformStep();
            //    this.Update();
            //    Thread.Sleep(1000);
            //    if(i >= prgLoad.Maximum / 2)
            //    {
            //        lblInspection.BackColor = Color.FromArgb(6, 176, 37);
            //    }
            //}
            int countError = 0;
            if(CheckError.CheckErrors(tbLogin))
            {
                countError++;
            }
            if(CheckError.CheckErrors(tbPassword))
            {
                countError++;
            }
            if(countError!=0)
            {
                lblInspection.Visible = false;
                lblErrorData.Visible = true;
            }
            else
            {
                this.Visible = false;
                //lblInspection.Text = "Вход выполнен";
                ApplicationForm frm = new ApplicationForm();
                frm.ShowDialog();
            }
        }

        private void tbLogin_LostFocus(object sender, EventArgs e)
        {
            if(tbLogin.BackColor!=Color.White)
            {
                tbLogin.BackColor = Color.White;
            }
            if(tbLogin.Text == "")
            {
                tbLogin.ForeColor = Color.Gray;
                tbLogin.Text = "Имя пользователя";
            }
        }
        private void tbLogin_GotFocus(object sender, EventArgs e)
        {
            if(tbLogin.BackColor!=Color.White)
            {
                tbLogin.BackColor = Color.White;
            }
            if(tbLogin.Text == "Имя пользователя")
            {
                tbLogin.Text = "";
            }
            tbLogin.ForeColor = Color.Black;
        }
        private void tbPassword_LostFocus(object sender, EventArgs e)
        {
            if(tbPassword.BackColor!=Color.White)
            {
                tbPassword.BackColor = Color.White;
            }
            if(tbPassword.Text == "")
            {
                tbPassword.UseSystemPasswordChar = false;
                tbPassword.ForeColor = Color.Gray;
                tbPassword.Text = "Пароль";
            }
        }
        private void tbPassword_GotFocus(object sender, EventArgs e)
        {
            if(tbPassword.BackColor!=Color.White)
            {
                tbPassword.BackColor = Color.White;
            }
            if(tbPassword.Text == "Пароль")
            {
                tbPassword.UseSystemPasswordChar = true;
                tbPassword.Text = "";
            }
            tbPassword.ForeColor = Color.Black;
        }

        private void llbLostPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Visible = false;
            LostPasswordForm lost = new LostPasswordForm();
            lost.ShowDialog();
            this.Visible = true;
        }

        private void llbRegistration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Visible = false;
            RegistrationForm reg = new RegistrationForm();
            reg.ShowDialog();
            this.Visible = true;
        }
    }
}

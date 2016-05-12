using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autorization
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            tbEmail.GotFocus += tbEmail_GotFocus;
            tbLogin.GotFocus += tbLogin_GotFocus;
            tbPassword.GotFocus += tbPassword_GotFocus;
            tbRepeatPassword.GotFocus += tbRepeatPassword_GotFocus;
        }

        void tbRepeatPassword_GotFocus(object sender, EventArgs e)
        {
            if(tbRepeatPassword.BackColor!=Color.White)
            {
                tbRepeatPassword.BackColor = Color.White;
            }
        }

        void tbPassword_GotFocus(object sender, EventArgs e)
        {
            if(tbPassword.BackColor!=Color.White)
            {
                tbPassword.BackColor = Color.White;
            }
        }

        void tbLogin_GotFocus(object sender, EventArgs e)
        {
            if(tbLogin.BackColor!=Color.White)
            {
                tbLogin.BackColor = Color.White;
            }
        }

        void tbEmail_GotFocus(object sender, EventArgs e)
        {
            if(tbEmail.BackColor!=Color.White)
            {
                tbEmail.BackColor = Color.White;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int countError = 0;
            //if(CheckError.CheckErrors(tbLogin))
            //{
            //    countError++;
            //}
            //if(CheckError.CheckErrors(tbPassword))
            //{
            //    countError++;
            //}
            //if(CheckError.CheckErrors(tbRepeatPassword))
            //{
            //    countError++;
            //}
            //if(CheckError.CheckErrors(tbEmail))
            //{
            //    countError++;
            //}
            //if(CheckError.CheckErrors(tbPassword,tbRepeatPassword))
            //{
            //    countError++;
            //}
            if(WorkingWithDB.Registration(tbLogin, tbEmail) == 0)
            {
                MessageBox.Show("OK");
            }
            
            if(countError==0)
            {
                DialogResult = DialogResult.OK;
            }   
        }
    }
}

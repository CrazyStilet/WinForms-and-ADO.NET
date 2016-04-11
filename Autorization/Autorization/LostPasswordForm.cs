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
    public partial class LostPasswordForm : Form
    {
        public LostPasswordForm()
        {
            InitializeComponent();
            tbEmail.GotFocus += tbEmail_GotFocus;
            tbNewPassword.GotFocus += tbNewPassword_GotFocus;
            tbRepeatPassword.GotFocus += tbRepeatPassword_GotFocus;
        }

        void tbRepeatPassword_GotFocus(object sender, EventArgs e)
        {
            if(tbRepeatPassword.BackColor!=Color.White)
            {
                tbRepeatPassword.BackColor = Color.White;
            }
        }

        void tbNewPassword_GotFocus(object sender, EventArgs e)
        {
            if(tbNewPassword.BackColor!=Color.White)
            {
                tbNewPassword.BackColor = Color.White;
            }
        }

        void tbEmail_GotFocus(object sender, EventArgs e)
        {
            if(tbEmail.BackColor!=Color.White)
            {
                tbEmail.BackColor = Color.White;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int countError = 0;
            if(CheckError.CheckErrors(tbEmail))
            {
                countError++;
            }
            if(CheckError.CheckErrors(tbNewPassword))
            {
                countError++;
            }
            if(CheckError.CheckErrors(tbRepeatPassword))
            {
                countError++;
            }
            if(CheckError.CheckErrors(tbNewPassword,tbRepeatPassword))
            {
                countError++;
            }

            if(countError==0)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

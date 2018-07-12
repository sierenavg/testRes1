using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIC.Giant
{
    public partial class FormWarningSign : Form
    {
        public FormWarningSign()
        {
            InitializeComponent();

            this.label11.Hide();
            this.label10.Hide();
           // this.label12.Hide();
            //this.label13.Hide();

            this.label14.Hide();
            this.label15.Hide();
            this.label2.Hide();
            this.label3.Hide();
            this.label4.Hide();
            this.label5.Hide();
            this.label6.Hide();
            this.label7.Hide();
            
            this.label8.Show();
            this.label9.Show();

            this.label2.Hide();
            this.label3.Hide();

            this.label8.Text = "正常";
            this.label9.Text = "正常";

            this.label8.ForeColor = Color.Green;
            this.label9.ForeColor = Color.Green;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            SetAllOK(true);
        }

        private void btn_fail_Click(object sender, EventArgs e)
        {
            SetAllOK(false);
        }
        void SetAllOK(bool isPass)
        {
            if (!isPass)
            {
                this.label2.Hide();
                this.label3.Hide();

                this.label11.Show();
                this.label10.Show();
                
                this.label14.Show();
                this.label15.Show();
                this.label2.Show();
                this.label3.Show();
                this.label4.Show();
                this.label5.Show();
                this.label6.Show();
                this.label7.Show();
                //this.label8.Show();
                //this.label9.Show();

                //this.label2.Hide();
                //this.label3.Hide();
                this.label8.Text = "錯誤";
                this.label9.Text = "錯誤";

                this.label8.ForeColor = Color.Red;
                this.label9.ForeColor = Color.Red;
            }
            else
            {
                this.label11.Hide();
                this.label10.Hide();
                //this.label12.Hide();
                //this.label13.Hide();
                this.label14.Hide();
                this.label15.Hide();
                this.label2.Hide();
                this.label3.Hide();
                this.label4.Hide();
                this.label5.Hide();
                this.label6.Hide();
                this.label7.Hide();
                //this.label8.Hide();
                //this.label9.Hide();

                //this.label12.Show();
               // this.label13.Show();

             

                this.label8.Text = "正常";
                this.label9.Text = "正常";

                this.label8.ForeColor = Color.Green;
                this.label9.ForeColor = Color.Green;
            }
        }
    }
}

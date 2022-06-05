using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        string ap1; //Название роли
        string ap2; //Пароль от роли в базе
        string ap3; //Введёный пароль


        private void Auth_Load(object sender, EventArgs e)
        {
            Atlt1.SetToolTip(Albl2, "Choose the level of rights and enter the password.\nTo contact us on emal: pony_montana@mail.ru");

            Acmbx1.Items.Clear();
            Acmbx1.Text = "";
            Acmbx1.Items.Add("Administrator");
            Acmbx1.Items.Add("Secretary");
            Acmbx1.SelectedIndex = 1;
            this.ActiveControl = Atxb1;
        }

        private void Acmbx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveControl = Atxb1;
            Atxb1.Clear();
        }

        private void Atxb1_TextChanged(object sender, EventArgs e)
        {
            ap1 = Acmbx1.Text;

            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            OleDbCommand ocm = new OleDbCommand("SELECT Us.password FROM Users AS Us WHERE Us.login = '" + ap1 + "'", oleCommand);
            OleDbDataReader or = ocm.ExecuteReader();

            while (or.Read())
            {
                ap2 = or["password"].ToString();
            }

            ap3 = Atxb1.Text;

            if (ap3 == ap2)
            {
                Project.Program.aprun = true;

                if (ap1 == "Administrator")
                {
                    Project.Program.admin = true;
                }

                this.Close();
            }

            else
            {
                Project.Program.aprun = false;
            }
        }
    }
}

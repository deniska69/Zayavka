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
    public partial class MSBXYN_NC : Form
    {
        string id_crt;

        public MSBXYN_NC()
        {
            InitializeComponent();
        }

        //Загрузка формы
        private void MSBXYN_Load(object sender, EventArgs e)
        {
            msbL1.Text =
                "Отдел: " +
                "\nФИО: " +
                "\nФИО 2*: " +
                "\nФИО 2 (Р.п.)*: " +
                "\nКорпус: " +
                "\nКабинет: " +
                "\nДолжность: " +
                "\nE-Mail: " +
                "\nВнутр.тлф: " +
                "\nМоб.тлф: ";

            msbL2.Text =
            Project.Program.Dp +
            "\n" + Project.Program.f_name1 +
            "\n" + Project.Program.f_name2 +
            "\n" + Project.Program.f_name3 +
            "\n" + Project.Program.Hs +
            "\n" + Project.Program.Cr +
            "\n" + Project.Program.function +
            "\n" + Project.Program.email1 +
            "\n" + Project.Program.in_tel +
            "\n" + Project.Program.mb_tel;
        }

        public void SaveCard()
        {
            string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
            OleDbConnection dbCon = new OleDbConnection(conString);
            dbCon.Open();

            if (Project.Program.id_crb == true)
            {
                id_crt = "id_cr = '" + Project.Program.id_cr + "', ";
            }

            else
            {
                id_crt = "";
            }

            string oleCommand0 =
            @"UPDATE Employees SET " +
            "f_name1 = '" + Project.Program.f_name1 + "', " +
            "f_name2 = '" + Project.Program.f_name2 + "', " +
            "f_name3 = '" + Project.Program.f_name3 + "', " +
            id_crt +
            "function = '" + Project.Program.function + "', " +
            "email1 = '" + Project.Program.email1 + "', " +
            "in_tel = '" + Project.Program.in_tel + "', " +
            "mb_tel = '" + Project.Program.mb_tel + "' " +
            "WHERE f_name1 = '" + Project.Program.f_name1 + "';";

            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = oleCommand0;
            cmd.Connection = dbCon;
            cmd.ExecuteNonQuery();

            dbCon.Close();
        }

        public void SaveCardNew()
        {
            //Подключение к базе
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
            OleDbConnection dbCon = new OleDbConnection(conString);
            dbCon.Open();

            //SQL-запрос на добавление записи с фамилией
            string oleCommand3 = @"INSERT INTO " +
            "[Employees]([id_dp], [f_name1]) " +
            "VALUES('" + Project.Program.id_dp + "', '" + Project.Program.f_name1 + "');";

            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = oleCommand3;
            cmd.Connection = dbCon;
            cmd.ExecuteNonQuery();
            dbCon.Close();
        }
        
        //Кнопка "Нет"
        private void msbN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Кнопка "Да"
        private void msbY_Click(object sender, EventArgs e)
        {
            //Сохранение(обновление) существующей записи в базе
            #region SaveCard

            if (Project.Program.SaveCard == true)
            {
                SaveCard();

                Project.Program.SaveCard = false;

                Adm adm = new Adm();
                adm.ClearCardEm();
                adm.CardDL();
                adm.HousingDL();

                this.Close();
            }

            #endregion

            //Сохранение в базе новой записи
            #region SaveCardNew

            if (Project.Program.SaveCardNew == true) //Определяется верность глобальной переменной
            {
                SaveCardNew();

                SaveCard();

                Project.Program.SaveCardNew = false;

                Adm adm = (Adm)this.Owner;
                adm.CloseMSBXYN_NC();

                this.Close();
            }

            #endregion
        }
    }
}

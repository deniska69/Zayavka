using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace Project
{
    public partial class Work : Form
    {
        public Work()
        {
            InitializeComponent();
        }

        //Переменные
        List<string> LIST_iddp = new List<string>();   //Список id отделов
        List<string> LIST_idem = new List<string>();   //Список id сотрудников
        List<string> LIST_idem2 = new List<string>();  //Список id сотрудников2
        List<string> LIST_idhs = new List<string>();   //Список id корпусов
        List<string> LIST_idcr = new List<string>();   //Список id кабинетов/аудиторий
        List<string> LIST_idtp = new List<string>();   //Список id типов заявок
        List<string> LIST_idst = new List<string>();   //Список id статусов заявок
        List<string> LIST_months = new List<string>(); //Список id месяцев
        string id_cr = "";                             //id привязанного кабинета
        string where2 = "";                            //Доп. условие для вывода базы заявок

        //Загрузка формы Work
        private void Work_Load(object sender, EventArgs e)
        {

        }

        //Загрузка списка отделов
        public void Departments_DL()
        {
            comboBox1.Items.Clear();
            LIST_iddp.Clear();

            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            OleDbCommand ocm = new OleDbCommand("SELECT id_dp, title FROM Departments ORDER BY id_dp", oleCommand);
            OleDbDataReader or = ocm.ExecuteReader();

            while (or.Read())
            {
                comboBox1.Items.Add(or["title"].ToString());
                LIST_iddp.Add(or["id_dp"].ToString());
            }
        }

        //Загрузка списка сотрудников из выбранного отдела
        public void Employees_DL()
        {
            //Очистка
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            LIST_idem.Clear();

            //Получаем id выбранного отдела
            int a = comboBox1.SelectedIndex;
            string id_dp = LIST_iddp[a];

            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            OleDbCommand ocm = new OleDbCommand("SELECT id_em, f_name1 FROM Employees WHERE id_dp =" + id_dp + " ORDER BY f_name1", oleCommand);
            OleDbDataReader or = ocm.ExecuteReader();

            while (or.Read())
            {
                comboBox2.Items.Add(or["f_name1"].ToString());
                LIST_idem.Add(or["id_em"].ToString());
            }
        }

        //Загрузка списка корпусов
        public void Housing_DL()
        {
            //Переменные внутри функции
            string Hs = "";
            id_cr = "";

            //Очистка
            comboBox3.Items.Clear();
            LIST_idhs.Clear();

            //Подключаемся к базе
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            //Загружается список корпусов
            OleDbCommand ocm0 = new OleDbCommand("SELECT id_hs, no_hs, street, no_hs2, note FROM Housing ORDER BY id_hs", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();
            while (or0.Read())
            {
                if (or0["note"].ToString() != "")
                {
                    comboBox3.Items.Add("№ " + or0["no_hs"].ToString() + " - " + or0["street"].ToString() + ", " + or0["no_hs2"].ToString() + " (" + or0["note"].ToString() + ")");
                }

                else
                {
                    comboBox3.Items.Add("№ " + or0["no_hs"].ToString() + " - " + or0["street"].ToString() + ", " + or0["no_hs2"].ToString());

                }

                LIST_idhs.Add(or0["id_hs"].ToString());
            }

            //Загрузка id кабинета/аудитории из карты сотрудника в базе
            OleDbCommand ocm1 = new OleDbCommand("SELECT id_cr FROM Employees WHERE f_name1 = '" + comboBox2.Text + "';", oleCommand);
            OleDbDataReader or1 = ocm1.ExecuteReader();

            while (or1.Read())
            {
                id_cr = or1["id_cr"].ToString();
            }

            //Если в базе указан id кабинета/аудитории:
            if(id_cr != "")
            {
                //Загрузка из базы id корпуса, к которому привязан сотрудник
                OleDbCommand ocm2 = new OleDbCommand("SELECT Hs.no_hs, Hs.street, Hs.no_hs2, Hs.note FROM Housing AS Hs INNER JOIN Classrooms AS Cr ON Hs.id_hs = Cr.id_hs WHERE Cr.id_cr = " + id_cr + ";", oleCommand);
                OleDbDataReader or2 = ocm2.ExecuteReader();

                while (or2.Read())
                {
                    if (or2["note"].ToString() != "")
                    {
                        Hs = ("№ " + or2["no_hs"].ToString() + " - " + or2["street"].ToString() + ", " + or2["no_hs2"].ToString() + " (" + or2["note"].ToString() + ")");
                    }

                    else
                    {
                        Hs = ("№ " + or2["no_hs"].ToString() + " - " + or2["street"].ToString() + ", " + or2["no_hs2"].ToString());

                    }
                }

                //Выбор корпуса, привязанного к сотруднику (работает только при наличии)
                int index = comboBox3.FindString(Hs);
                comboBox3.SelectedIndex = index;
            }
        }

        //Загрузка списка кабинетов/аудиторий из выбранного корпуса       
        public void Classrooms_DL()
        {
            //Переменные внутри функции
            string ccr1 = "";
            int ccr0 = 0;

            //Очистка
            comboBox4.Items.Clear();
            LIST_idcr.Clear();

            //Получаем id выбранного корпуса
            int a = comboBox3.SelectedIndex;
            string id_hs = LIST_idhs[a];

            //Подключаемся к базе
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            //Получаем кол-во кабинетов для выбранного корпуса
            OleDbCommand ocm0 = new OleDbCommand("SELECT COUNT(no_cr) FROM Classrooms  WHERE id_hs = " + id_hs + ";", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();

            while (or0.Read())
            {
                ccr1 = or0[0].ToString();
                ccr0 = Convert.ToInt32(ccr1);
            }

            //При отсутствии кабинетов:
            if (ccr0 == 0)
            {
                comboBox4.Items.Clear();
                comboBox4.Items.Add("У выбранного корпуса в базе отсутствуют кабинеты.");
                comboBox4.SelectedIndex = 0;
                comboBox4.Enabled = false;
                dateTimePicker1.Enabled = false;
            }

            else
            {
                comboBox4.Items.Clear();

                //Загружаем список кабинетов
                OleDbCommand ocm1 = new OleDbCommand("SELECT id_cr, no_cr, title FROM Classrooms WHERE id_hs = " + id_hs + " ORDER BY no_cr;", oleCommand);
                OleDbDataReader or1 = ocm1.ExecuteReader();

                while (or1.Read())
                {
                    if (or1["title"].ToString() != "")
                    {
                        comboBox4.Items.Add(or1["no_cr"].ToString() + " (" + or1["title"].ToString() + ")");
                    }

                    else
                    {
                        comboBox4.Items.Add(or1["no_cr"].ToString());
                    }

                    LIST_idcr.Add(or1["id_cr"].ToString());
                }

                //Поиск позиции кабинета в загруженном списке по привязанному id кабинета в карте сотрудника
                int index = LIST_idcr.FindIndex(x => x.StartsWith(id_cr));
                comboBox4.SelectedIndex = index;
            }
        }

        //Загрузка списка типов заявок
        public void Type_DL()
        {
            //Очистка
            comboBox5.Items.Clear();
            LIST_idtp.Clear();
            LIST_idem2.Clear();

            //Подключение к базе
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            //SQL-запрос 1
            OleDbCommand ocm0 = new OleDbCommand("SELECT id_tp, title, id_em FROM Type ORDER BY id_tp", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();

            //Чтение ответа из базы
            while (or0.Read())
            {
                comboBox5.Items.Add(or0["title"].ToString());
                LIST_idtp.Add(or0["id_tp"].ToString());
                LIST_idem2.Add(or0["id_em"].ToString());
            }

            //SQL-запрос 2
            OleDbCommand ocm1 = new OleDbCommand("SELECT DISTINCT id_tp, id_em FROM Type ORDER BY id_tp", oleCommand);
            OleDbDataReader or1 = ocm1.ExecuteReader();

            //Чтение ответа из базы
            while (or1.Read())
            {
                LIST_idem2.Add(or1["id_em"].ToString());
            }
        }

        //Загрузка списка сотрудников, выполнющих заявки >> выбор закреплённого сотрудника
        public void Employees_DL2()
        {
            //Очистка
            comboBox6.Items.Clear();

            //Получаем id сотрудника, закреплённого за типом заявки
            int a = comboBox5.SelectedIndex;
            string id_em2 = LIST_idem2[a];

            //Подключаемся к базе
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            //Загружаем список сотрудников выполняющих заявки
            OleDbCommand ocm = new OleDbCommand("SELECT DISTINCT Em.f_name1 FROM Employees AS Em INNER JOIN Type AS Tp ON Em.id_em=Tp.id_em ORDER BY Em.f_name1;", oleCommand);
            OleDbDataReader or = ocm.ExecuteReader();

            while (or.Read())
            {
                comboBox6.Items.Add(or["f_name1"].ToString());
            }

            //Выбираем сотрудника, закреплённого за выбранным типом заявки
            OleDbCommand ocm1 = new OleDbCommand("SELECT f_name1 FROM Employees WHERE id_em = " + id_em2 + ";", oleCommand);
            OleDbDataReader or1 = ocm1.ExecuteReader();

            while (or1.Read())
            {
                int index = comboBox6.FindString(or1["f_name1"].ToString());
                comboBox6.SelectedIndex = index;
            }
        }

        //Загрузка базы заявок
        public void AppDl()
        {
            int nm = comboBox7.SelectedIndex + 1;         //Номер месяца
            int ny = comboBox8.SelectedIndex + 2016;      //Номер года
            int cd = System.DateTime.DaysInMonth(ny, nm); //Получаем колличество дней, в выбранном месяце

            //Подключаемся к базе
            string conString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
            OleDbConnection dbCon = new OleDbConnection(conString);
            dbCon.Open();

            //SQL-запрос
            string oleCommand = @"

            SELECT             
            Dp.title AS 'Отдел', Em1.f_name1 AS 'Кто подаёт', Hs.no_hs AS 'Корпус', Cr.no_cr AS 'Кабинет',
            Ap.date1 AS 'Дата подачи', Tp.title AS 'Тип заявки', Em2.f_name1 AS 'Кто выполняет',
            St.title AS 'Статус заявки', Ap.note AS 'Заметка', Ap.date2 AS 'Дата выполнения'
            
            FROM ((((((App AS Ap 
            INNER JOIN Employees AS Em1 ON Em1.id_em=Ap.id_em1)
            INNER JOIN Departments AS Dp ON Dp.id_dp=Em1.id_dp)
            INNER JOIN Classrooms AS Cr ON Cr.id_cr = Ap.id_cr)
            INNER JOIN Housing AS Hs ON Hs.id_hs=Cr.id_hs)
            INNER JOIN Type AS Tp ON Tp.id_tp=Ap.id_tp)
            INNER JOIN Employees AS Em2 ON Em2.id_em=Ap.id_em2)
            INNER JOIN Status AS St ON St.id_st=Ap.id_st
            WHERE date1 BETWEEN #" + nm + "/1/" + ny + "# AND #" + nm + "/" + cd + "/" + ny + "# " + where2 + 
            "ORDER BY Ap.id_ap;";

            OleDbDataAdapter da = new OleDbDataAdapter(oleCommand, dbCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dbCon.Close();
        }

        //Загрузка статусов заявок
        public void StatusDl()
        {
            comboBox9.Items.Clear();
            comboBox9.Items.Add("Все");
            LIST_idst.Clear();
            LIST_idst.Add("Все");

            //Подключение к базе
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            //SQL-запрос 1
            OleDbCommand ocm0 = new OleDbCommand("SELECT id_st, title FROM Status ORDER BY id_st", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();

            //Чтение ответа из базы
            while (or0.Read())
            {
                comboBox9.Items.Add(or0["title"].ToString());
                LIST_idst.Add(or0["id_st"].ToString());
            }

            comboBox9.SelectedIndex = 0;
        }

        //Очистка вкладки "Новая заявка"
        public void Clear_NZ()
        {
            //Отключение
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            dateTimePicker1.Enabled = false;
            checkBox1.Enabled = false;
            button1.Enabled = false;

            //Очистка
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();

        }

        //Выбор активной вкладку "Новая заявка"
        private void tabPage1_Enter(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            //Вызов функции очистки
            Clear_NZ();            

            //Вызов функции загрузки отделов
            Departments_DL();
        }

        //Выбор отдела
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear_NZ();
            Employees_DL();
            comboBox2.Enabled = true;
        }

        //Выбор сотрудника 
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            dateTimePicker1.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            Housing_DL();
            comboBox3.Enabled = true;
        }

        //Выбор корпуса
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Classrooms_DL();

            if (comboBox4.Text != "У выбранного корпуса в базе отсутствуют кабинеты.")
            {
                dateTimePicker1.Enabled = true;
                comboBox4.Enabled = true;
            }
        }

        //Выбор кабинета/аудиотрии
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "У выбранного корпуса в базе отсутствуют кабинеты.")
            {
                Type_DL();
                comboBox5.Enabled = true;
                comboBox6.Items.Clear();
                comboBox6.Enabled = false;
            }
        }

        //Выбор типа заявки
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employees_DL2();
            comboBox6.Enabled = true;
        }

        //Выбор сотруднка, выполняющего заявку
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            button1.Enabled = true;
        }

        //Кнопка "Добавить в базу"
        private void button1_Click(object sender, EventArgs e)
        {
            //Получаем id подавшего сотрудника
            int a0 = comboBox2.SelectedIndex;
            string id_em1 = LIST_idem[a0];

            //Получаем id кабинета
            int a1 = comboBox4.SelectedIndex;
            string id_cr = LIST_idcr[a1];

            //Получаем id типа заявки
            int a2 = comboBox5.SelectedIndex;
            string id_tp = LIST_idtp[a2];

            //Получаем id выполняющего сотрудника
            int a3 = comboBox6.SelectedIndex;
            string id_em2 = LIST_idem2[a3];

            string id_st = "1"; //id статуса - "Подано"

            string date1 = dateTimePicker1.Value.Date.ToString("dd.MM.yyyy"); //Дата подачи заявки

            string note = textBox1.Text; //Дополнительная информация




            //Подключение к базе
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
            OleDbConnection dbCon = new OleDbConnection(conString);
            dbCon.Open();

            //SQL-запрос на добавление новой заявки в базу
            string oleCommand3 = @"INSERT INTO " +
            "[App]([id_em1], [id_cr], [date1], [id_tp], [id_em2], [id_st], [note]) " +
            "VALUES('"
            + id_em1 + "', '"
            + id_cr + "', '"
            + date1 + "', '"
            + id_tp + "', '"
            + id_em2 + "', '"
            + id_st + "', '"
            + note + "');";

            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = oleCommand3;
            cmd.Connection = dbCon;
            cmd.ExecuteNonQuery();
            dbCon.Close();           
        }

        //Выбор активной вкладку "База заявок"
        private void tabPage2_Enter(object sender, EventArgs e)
        {
            comboBox7.Items.Clear();
            comboBox8.Items.Clear();

            comboBox7.Items.Add("Январь");
            comboBox7.Items.Add("Февраль");
            comboBox7.Items.Add("Март");
            comboBox7.Items.Add("Апрель");
            comboBox7.Items.Add("Май");
            comboBox7.Items.Add("Июнь");
            comboBox7.Items.Add("Июль");
            comboBox7.Items.Add("Август");
            comboBox7.Items.Add("Сентябрь");
            comboBox7.Items.Add("Октябрь");
            comboBox7.Items.Add("Ноябрь");
            comboBox7.Items.Add("Декабрь");

            comboBox8.Items.Add("2016");
            comboBox8.Items.Add("2017");
            comboBox8.SelectedIndex = 0;

            StatusDl();
        }

        //Событие выбора месяца
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppDl();
        }

        //Событие выбора года
        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex >= 0)
            {
                AppDl();
            }
        }

        //Событие выбора статуса заявки
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex >= 0)
            {
                if (comboBox9.Text == "Все")
                {
                    where2 = "";
                }

                else
                {
                    where2 = "AND Ap.id_st=" + comboBox9.SelectedIndex + " ";
                }

                AppDl();
            }  
        }
    }
}

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

namespace Project
{
    public partial class Adm : Form
    {
        int ec0, id0, nohs0, ccr0;
        string f_name1, en0, eo0, eo1, id1, nohs1, ccr1, nocrtext0, tt0;
        List<string> listHs = new List<string>();
        List<string> listCr = new List<string>();

        public Adm()
        {
            InitializeComponent();
        }

        #region Departments

        //Функция загрузки из базы списка отделов
        public void DepartmentsDL()
        {
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            eComBox1.Items.Clear();
            eComBox1.Text = "";
            eTBox1.Clear();

            OleDbCommand ocm = new OleDbCommand("SELECT Dp.title FROM Departments AS Dp ORDER BY Dp.id_dp", oleCommand);
            OleDbDataReader or = ocm.ExecuteReader();

            while (or.Read())
            {
                eComBox1.Items.Add(or["title"].ToString());
            }
        }

        //Выбор активной вкладки "Отделы"
        private void AdmPage1_Enter(object sender, EventArgs e)
        {
            eBut2.Enabled = false;
            eBut3.Enabled = false;
            eBut5.Enabled = false;

            DepartmentsDL();
        }

        //Выбор отдела в выпадающем списке
        private void eComBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            eTBox1.Text = eComBox1.Text;
            en0 = eComBox1.Text;

            eBut2.Enabled = true;
            eBut3.Enabled = true;
            eBut5.Enabled = true;
        }

        //Кнопка "Удалить"
        private void eBut2_Click(object sender, EventArgs e)
        {
            eo0 = eComBox1.Text;

            if (MessageBox.Show("Вы действительно хотите удалить отдел '' " + eo0 + " '' из базы?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
                OleDbConnection dbCon = new OleDbConnection(conString);
                dbCon.Open();

                string oleCommand0 = @"
                DELETE Departments.* FROM Departments WHERE title = '" + eo0 + "';";

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = oleCommand0;
                cmd.Connection = dbCon;
                cmd.ExecuteNonQuery();

                dbCon.Close();

                DepartmentsDL();
            }
        }

        //Кнопка "Сохранить"
        private void eBut3_Click(object sender, EventArgs e)
        {
            int ea0 = eComBox1.SelectedIndex + 1;
            string ea1 = eTBox1.Text;
            string ea2 = eComBox1.Text;

            if(ea1 == "")
            {
                MessageBox.Show("В поле ''Название'' пусто. Обновление не произведено.", "Уведомление", MessageBoxButtons.OK);
            }

            if(ea1 == ea2)
            {
                MessageBox.Show("Нет изменений в записи. Обновление не произведено.", "Уведомление", MessageBoxButtons.OK);
            }

            
            if((ea1 != ea2) && (ea1 != ""))
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите обновить в базе запись ''" + ea2 + "'' на ''" + ea1 + "''?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes) //Если нажал Да
                {
                    string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
                    OleDbConnection dbCon = new OleDbConnection(conString);
                    dbCon.Open();

                    string oleCommand0 = @"
                    UPDATE Departments SET title = '" + ea1 + "' WHERE title = '" + ea2 + "';";

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = oleCommand0;
                    cmd.Connection = dbCon;
                    cmd.ExecuteNonQuery();

                    dbCon.Close();

                    DepartmentsDL();
                }
            }
        }

        //Кнпока "Добавить новый"
        private void eBut4_Click(object sender, EventArgs e)
        {
            eo0 = eTBox1.Text;

            OleDbConnection oleCommand0 = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand0.Open();

            OleDbCommand ocm0 = new OleDbCommand("SELECT Count(title) AS cnt FROM Departments WHERE title ='" + eo0 + "'", oleCommand0);
            OleDbDataReader or0 = ocm0.ExecuteReader();

            while (or0.Read())
            {
                eo1 = or0["cnt"].ToString();
                ec0 = Convert.ToInt32(eo1);
            }

            if (ec0 > 0 || eo0 == "")
            {
                if (ec0 > 0)
                    MessageBox.Show("Отдел с названием '' " + eo0 + " '' уже есть в базе.", "Уведомление", MessageBoxButtons.OK);
                if (eo0 == "")
                    MessageBox.Show("Нельзя добавить в базу пустую запись.", "Предупреждение", MessageBoxButtons.OK);
            }

            else
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить в базу отдел с названием ''" + eo0 + "''?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
                    OleDbConnection dbCon = new OleDbConnection(conString);
                    dbCon.Open();

                    string oleCommand1 = @"INSERT INTO Departments(title) VALUES('" + eo0 + "');";

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = oleCommand1;
                    cmd.Connection = dbCon;
                    cmd.ExecuteNonQuery();

                    dbCon.Close();

                    DepartmentsDL();
                }
            }
        }

        //Кнопка "Очистить"
        private void eBut5_Click(object sender, EventArgs e)
        {
            eTBox1.Clear();

            eBut2.Enabled = false;
            eBut3.Enabled = false;
            eBut5.Enabled = false;

            DepartmentsDL();
        }

        //Изменение текста в eTbox1
        private void eTBox1_TextChanged(object sender, EventArgs e)
        {
            if(eTBox1.TextLength > 0)
            {
                eBut5.Enabled = true;
            }

            else
            {
                eBut5.Enabled = false;
            }
        }

        #endregion

        #region Function Employees

        //Функция загрузки из базы списка отделов
        public void DepartmentsDL2()
        {
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            eComBox2.Items.Clear();
            eComBox2.Text = "";

            OleDbCommand ocm = new OleDbCommand("SELECT Dp.title FROM Departments AS Dp ORDER BY Dp.id_dp", oleCommand);
            OleDbDataReader or = ocm.ExecuteReader();

            while (or.Read())
            {
                eComBox2.Items.Add(or["title"].ToString());
            }
        }

        //Функция загрузки из базы списка сотрудников
        public void EmployeesDl()
        {
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            eComBox3.Items.Clear();
            eComBox3.Text = "";

            string et0 = eComBox2.Text;

            OleDbCommand ocm0 = new OleDbCommand("SELECT id_dp FROM Departments WHERE title = '" + et0 + "';", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();

            while (or0.Read())
            {
                id1 = or0[0].ToString();
                id0 = Convert.ToInt32(id1);
            }

            OleDbCommand ocm1 = new OleDbCommand("SELECT f_name1 FROM Employees WHERE id_dp =" + id1 + " ORDER BY f_name1", oleCommand);
            OleDbDataReader or1 = ocm1.ExecuteReader();

            while (or1.Read())
            {
                eComBox3.Items.Add(or1["f_name1"].ToString());
            }
        }

        //Функция очистки карты
        public void ClearCardEm()
        {
            eTBox2.Clear();
            eTBox3.Clear();
            eTBox4.Clear();
            eTBox5.Clear();
            eTBox6.Clear();
            eTBox7.Clear();
            eTBox8.Clear();

            eComBox4.Items.Clear();
            eComBox5.Items.Clear();

            eComBox4.Enabled = false;
            eComBox5.Enabled = false;

            eGroupBox1.Enabled = false;

            //Выключаем/показ кнопок:
            eBut8.Visible = false;  //Удалить сотрудника
            eBut9.Visible = false;  //Очистить карту
            eBut10.Visible = false; //Сохранить карту
            eBut23.Enabled = false; //Добавление нового сотрудника
            eBut24.Visible = false; //Добавить в базу
        }

        //Функция загрузки списка корпусов
        public void HousingDL()
        {
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            listHs.Clear();

            //Загружается список корпусов
            OleDbCommand ocm0 = new OleDbCommand("SELECT id_hs, no_hs, street, no_hs2, note FROM Housing ORDER BY id_hs", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();
            while (or0.Read())
            {
                if (or0["note"].ToString() != "")
                {
                    eComBox4.Items.Add("№ " + or0["no_hs"].ToString() + " - " + or0["street"].ToString() + ", " + or0["no_hs2"].ToString() + " (" + or0["note"].ToString() + ")");
                }

                else
                {
                    eComBox4.Items.Add("№ " + or0["no_hs"].ToString() + " - " + or0["street"].ToString() + ", " + or0["no_hs2"].ToString());

                }

                listHs.Add(or0["id_hs"].ToString());
            }

            eComBox4.Enabled = true;

            //Загрузка из базы номера корпуса, к которому привязан сотрудник
            OleDbCommand ocm7 = new OleDbCommand("SELECT Hs.no_hs FROM (Housing AS Hs INNER JOIN Classrooms AS Cr ON Hs.id_hs = Cr.id_hs) INNER JOIN Employees AS Em ON Cr.id_cr = Em.id_cr WHERE Em.f_name1 = '" + eComBox3.Text + "';", oleCommand);
            OleDbDataReader or7 = ocm7.ExecuteReader();
            
            nohs0 = 0;
            nohs1 = "";

            while (or7.Read())
            {
                nohs1 = or7["no_hs"].ToString();
            }

            //Выбор корпуса, привязанного к сотруднику (работает только при наличии)
            if (nohs1 != "")
            {
                nohs0 = Convert.ToInt32(nohs1) - 1;
                eComBox4.SelectedIndex = nohs0;
            }
        }

        //Функция загрузки списка кабинетов
        public void ClassroomsDL()
        {
            ccr1 = "";
            ccr0 = 0;

            eComBox5.Items.Clear();

            int a = eComBox4.SelectedIndex;
            string id_hs = listHs[a];
            

            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            OleDbCommand ocm0 = new OleDbCommand("SELECT COUNT(no_cr) FROM Classrooms  WHERE id_hs = " + id_hs + ";", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();

            while (or0.Read())
            {
                ccr1 = or0[0].ToString();
                ccr0 = Convert.ToInt32(ccr1);
            }

            if (ccr0 == 0)
            {
                eComBox5.Items.Clear();
                eComBox5.Enabled = false;
                eComBox5.Items.Add("У выбранного корпуса в базе отсутствуют кабинеты.");
                eComBox5.SelectedIndex = 0;
            }

            else
            {
                //Загрузка списка кабинетов, для выбранного корпуса
                OleDbCommand ocm1 = new OleDbCommand("SELECT Cr.no_cr, Cr.title FROM Classrooms AS Cr WHERE Cr.id_hs = " + id_hs + " ORDER BY Cr.no_cr;", oleCommand);
                OleDbDataReader or1 = ocm1.ExecuteReader();

                eComBox5.Items.Clear();

                while (or1.Read())
                {
                    if (or1["title"].ToString() != "")
                        eComBox5.Items.Add(or1["no_cr"].ToString() + " (" + or1["title"].ToString() + ")");
                    else
                        eComBox5.Items.Add(or1["no_cr"].ToString());
                }

                eComBox5.Enabled = true;

                nocrtext0 = "";

                //Загрузка номера кабинета, привязанного к сотруднику
                OleDbCommand ocm2 = new OleDbCommand("SELECT Cr.no_cr FROM Classrooms AS Cr INNER JOIN Employees AS Em ON Cr.id_cr = Em.id_cr WHERE Em.f_name1 = '" + eComBox3.Text + "';", oleCommand);
                OleDbDataReader or2 = ocm2.ExecuteReader();

                while (or2.Read())
                {
                    nocrtext0 = or2[0].ToString();
                }

                if (nocrtext0 != "")
                {
                    int index = eComBox5.FindString(nocrtext0);
                    eComBox5.SelectedIndex = index;
                }
            }
        }

        //Функция заполнения карты сотрудника
        public void CardDL()
        {
            //Заполнение карты сотрудника
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            f_name1 = eComBox3.Text;

            //ФИО 1
            eTBox2.Text = f_name1;

            //ФИО 2
            OleDbCommand ocm0 = new OleDbCommand("SELECT f_name2 FROM Employees WHERE f_name1 = '" + f_name1 + "'", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();
            while (or0.Read())
            {
                eTBox3.Text = or0[0].ToString();
            }

            //ФИО 2 (Р.п.)
            OleDbCommand ocm1 = new OleDbCommand("SELECT f_name3 FROM Employees WHERE f_name1 = '" + f_name1 + "'", oleCommand);
            OleDbDataReader or1 = ocm1.ExecuteReader();
            while (or1.Read())
            {
                eTBox4.Text = or1[0].ToString();
            }

            //Должность
            OleDbCommand ocm2 = new OleDbCommand("SELECT function FROM Employees WHERE f_name1 = '" + f_name1 + "'", oleCommand);
            OleDbDataReader or2 = ocm2.ExecuteReader();
            while (or2.Read())
            {
                eTBox8.Text = or2[0].ToString();
            }

            //E-Mail
            OleDbCommand ocm3 = new OleDbCommand("SELECT email1 FROM Employees WHERE f_name1 = '" + f_name1 + "'", oleCommand);
            OleDbDataReader or3 = ocm3.ExecuteReader();
            while (or3.Read())
            {
                eTBox5.Text = or3[0].ToString();
            }

            //Внутренний телефон
            OleDbCommand ocm4 = new OleDbCommand("SELECT in_tel FROM Employees WHERE f_name1 = '" + f_name1 + "'", oleCommand);
            OleDbDataReader or4 = ocm4.ExecuteReader();
            while (or4.Read())
            {
                eTBox6.Text = or4[0].ToString();
            }

            //Мобильный телефон
            OleDbCommand ocm5 = new OleDbCommand("SELECT mb_tel FROM Employees WHERE f_name1 = '" + f_name1 + "'", oleCommand);
            OleDbDataReader or5 = ocm5.ExecuteReader();
            while (or5.Read())
            {
                eTBox7.Text = or5[0].ToString();
            }
        }

        //Функция передачи данных с карты в глобальные переменные
        public void TransferCard()
        {
            //Передаём в глобальные переменные:
            Project.Program.f_name1 = eTBox2.Text;  //ФИО
            Project.Program.f_name2 = eTBox3.Text;  //ФИО 2
            Project.Program.f_name3 = eTBox4.Text;  //ФИО 2 (Р.п.)
            Project.Program.email1 = eTBox5.Text;   //E-mail
            Project.Program.in_tel = eTBox6.Text;   //Внутренний телефон
            Project.Program.mb_tel = eTBox7.Text;   //Мобильный телефон
            Project.Program.function = eTBox8.Text; //Должность
            Project.Program.Dp = eComBox2.Text;     //Название отдела
            Project.Program.Hs = eComBox4.Text;     //Корпус
            Project.Program.Cr = eComBox5.Text;     //Кабинет

            //Подключение к базе
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            //SQL-запрос: Получаем id-отдела, в который добавляем нового сотрудника
            OleDbCommand ocm0 = new OleDbCommand("SELECT id_dp FROM Departments WHERE title = '" + eComBox2.Text + "'", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();

            while (or0.Read())
            {
                string id_dp0 = or0[0].ToString();
                Project.Program.id_dp = Convert.ToInt32(id_dp0); //Заносим id-отдела в глобальную переменную
            }

            //Заносим номер кабинета в глобальную переменную
            if (eComBox5.Text != "У выбранного корпуса в базе отсутствуют кабинеты.")
            {
                if (eComBox5.Text != "")
                {
                    string s = eComBox5.Text;
                    string[] s2 = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    OleDbCommand ocm1 = new OleDbCommand("SELECT id_cr FROM Classrooms WHERE no_cr = '" + s2[0] + "'", oleCommand);
                    OleDbDataReader or1 = ocm1.ExecuteReader();

                    while (or1.Read())
                    {
                        string id_cr0 = or1[0].ToString();
                        Project.Program.id_cr = Convert.ToInt32(id_cr0); //Заносим номер кабинета в глобальную переменную
                    }

                    Project.Program.id_crb = true; //Присваиваем значение - привязанный кабинет - есть!
                }
            }

            else
            {
                Project.Program.id_crb = false; //Присваиваем значение - привязанный кабинет - нету!
            }
        }

        //Функция закрытия вкладки "Новый сотрудник" >> открытие вкладок "Отделы", "Сотрудники"
        public void CloseMSBXYN_NC()
        {
            AdmTabControl.SelectedTab = AdmPage2;

            DepartmentsDL2();

            int index1 = eComBox2.FindString(Project.Program.Dp);
            eComBox2.SelectedIndex = index1;

            int index2 = eComBox3.FindString(Project.Program.f_name1);
            eComBox3.SelectedIndex = index2;
        }

        #endregion

        #region Employees

        //Выбор активной вкладки "Сотрудники"
        private void AdmPage2_Enter(object sender, EventArgs e)
        {
            //Очищаем поля карты сотрудника
            ClearCardEm();

            //Очищаем список сотрудников, отключаем
            eComBox3.Items.Clear();
            eComBox3.Enabled = false;

            //Загружаем список отделов
            DepartmentsDL2();
        }

        //Выбор отдела в выпадающем списке
        private void eComBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Загрузка списка сотрудников
            EmployeesDl();

            //Очистка полей карты сотрудника
            ClearCardEm();

            //Активация списка сотрудников
            eComBox3.Enabled = true;

            //Активация кнопки добавление нового сотрудника
            eBut23.Enabled = true;
        }

        //Выбор сотрудника >> заполнение карты из базы
        private void eComBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearCardEm();

            eGroupBox1.Enabled = true;

            CardDL();

            HousingDL();

            eBut23.Enabled = true;
            eBut8.Visible = true;
            eBut9.Visible = true;
            eBut10.Visible = true;
        }

        //Выбор корпуса >> вызов функции загрузки списка кабинетов
        private void eComBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassroomsDL();            
        }

        //Кнопка "Удалить сотрудника"
        private void eBut8_Click(object sender, EventArgs e)
        {
            eo0 = eComBox3.Text;
            string titleDP = eComBox2.Text;
            int index = eComBox2.FindString(titleDP);

            if (MessageBox.Show("Вы действительно хотите удалить сотрудника '' " + eo0 + " '' из базы?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
                OleDbConnection dbCon = new OleDbConnection(conString);
                dbCon.Open();

                string oleCommand0 = @"
                DELETE Employees.* FROM Employees WHERE f_name1 = '" + eo0 + "';";

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = oleCommand0;
                cmd.Connection = dbCon;
                cmd.ExecuteNonQuery();

                dbCon.Close();


                eComBox3.Items.Clear();
                eComBox3.Enabled = false;
                ClearCardEm();
                DepartmentsDL2();
                eComBox2.SelectedIndex = index;
            }
        }

        //Кнопка "Очистить карту"
        private void eBut9_Click(object sender, EventArgs e)
        {
            ClearCardEm();
            eGroupBox1.Enabled = true;
            eComBox4.Enabled = true;

            HousingDL();

            eBut8.Visible = true;
            eBut9.Visible = true;
            eBut10.Visible = true;
        }

        //Кнпока "Сохранить карту"
        private void eBut10_Click(object sender, EventArgs e)
        {
            if(eTBox2.Text == "")
            {
                MessageBox.Show("Поле ФИО - пустое!", "Предупреждение", MessageBoxButtons.OK);
            }

            else
            {
                TransferCard();

                Project.Program.SaveCard = true;

                MSBXYN_NC msbxyn = new MSBXYN_NC();
                msbxyn.ShowDialog();
            }
        }

        //Кнопка "+" - добавления нового сотрудника
        private void eBut23_Click(object sender, EventArgs e)
        {
            eBut8.Visible = false;
            eBut9.Visible = false;
            eBut10.Visible = false;

            EmployeesDl();

            ClearCardEm();

            eGroupBox1.Enabled = true;

            eBut24.Location = new Point(eBut10.Location.X, eBut17.Location.Y);
            eBut24.Visible = true;

            HousingDL();
        }

        //Кнопка "Добавить в базу"
        private void eBut24_Click(object sender, EventArgs e)
        {
            if (eTBox2.Text == "")
            {
                MessageBox.Show("Поле ФИО - пустое!", "Предупреждение", MessageBoxButtons.OK);
            }

            else
            {
                TransferCard();

                Project.Program.SaveCardNew = true;

                MSBXYN_NC msbxyn = new MSBXYN_NC();
                msbxyn.Show(this);
            }
        }

        #endregion

        #region Housing and Classrooms

        //Функция очистки
        public void ClearHs_Cr()
        {
            eTBox10.Clear();
            eTBox11.Clear();
            eTBox12.Clear();
            eTBox13.Clear();
            eTBox17.Clear();
            eBut16.Visible = true;
            eBut17.Visible = true;
            eBut20.Visible = false;
            eBut22.Enabled = false;
            eComBox7.Items.Clear();
            eComBox8.Items.Clear();
            eComBox8.Enabled = false;
            eGroupBox3.Enabled = false;
            eGroupBox4.Enabled = false;
        }

        //Функция очистки 2
        public void ClearHs_Cr2()
        {
            eTBox15.Clear();
            eTBox16.Clear();

            eBut18.Visible = true;
            eBut19.Visible = true;
            cBox1.Visible = true;

            eBut25.Visible = false;

            eComBox8.Items.Clear();

            eGroupBox4.Enabled = false;
        }

        //Функция загрузки списка корпусов
        public void HousingDL_C()
        {
            listHs.Clear();

            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            //Загружается список корпусов в карту корпуса
            OleDbCommand ocm0 = new OleDbCommand("SELECT id_hs, no_hs, street, no_hs2, note FROM Housing ORDER BY id_hs", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();
            while (or0.Read())
            {
                if (or0["note"].ToString() != "")
                {
                    eComBox7.Items.Add("№ " + or0["no_hs"].ToString() + " - " + or0["street"].ToString() + ", " + or0["no_hs2"].ToString() + " (" + or0["note"].ToString() + ")");
                }

                else
                {
                    eComBox7.Items.Add("№ " + or0["no_hs"].ToString() + " - " + or0["street"].ToString() + ", " + or0["no_hs2"].ToString());

                }

                listHs.Add(or0["id_hs"].ToString());
            }
        }

        //Функция загрузки списка кабинетов
        public void ClassroomsDL_C()
        {
            //Подключаемся к базе
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            //Получаем id корпуса в базе
            int a = eComBox7.SelectedIndex;
            string id_hs = listHs[a];

            string ccr1 = "";
            int ccr0 = 0;

            listCr.Clear();
            eComBox8.Items.Clear();

            //Получаем кол-во кабинетов для выбранного корпуса
            OleDbCommand ocm1 = new OleDbCommand("SELECT COUNT(no_cr) FROM Classrooms  WHERE id_hs = " + id_hs + ";", oleCommand);
            OleDbDataReader or1 = ocm1.ExecuteReader();

            while (or1.Read())
            {
                ccr1 = or1[0].ToString();
                ccr0 = Convert.ToInt32(ccr1);
            }

            if (ccr0 == 0)
            {
                eComBox8.Items.Add("У выбранного корпуса в базе отсутствуют кабинеты.");
                eComBox8.SelectedIndex = 0;
                eComBox8.Enabled = false;
                eGroupBox4.Enabled = false;
                eBut22.Enabled = true;
            }

            //Загрузка списка кабинетов, для выбранного корпуса
            else
            {
                OleDbCommand ocm2 = new OleDbCommand("SELECT Cr.id_cr, Cr.no_cr, Cr.title FROM Classrooms AS Cr WHERE Cr.id_hs = " + id_hs + " ORDER BY Cr.no_cr;", oleCommand);
                OleDbDataReader or2 = ocm2.ExecuteReader();

                while (or2.Read())
                {
                    if (or2["title"].ToString() != "")
                    {
                        eComBox8.Items.Add(or2["no_cr"].ToString() + " (" + or2["title"].ToString() + ")");
                    }

                    else
                    {
                        eComBox8.Items.Add(or2["no_cr"].ToString());
                    }

                    listCr.Add(or2["id_cr"].ToString());
                }

                eComBox8.Enabled = true;
            }

            eTBox14.Text = eComBox7.Text;
        }

        //Функция обновления (сохранения изменений) записи в базе для карты корпуса
        public void SaveHsCr()
        {
            if (eTBox11.Text == "")
            {
                MessageBox.Show("Поле ''Улица'' - пустое!", "Предупреждение", MessageBoxButtons.OK);
            }

            if (eTBox17.Text == "")
            {
                MessageBox.Show("Поле ''№ здания'' - пустое!", "Предупреждение", MessageBoxButtons.OK);
            }

            else
            {
                int a = eComBox7.SelectedIndex;
                string b = listHs[a];

                OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
                oleCommand.Open();

                //Получаем данные о корпусе из базы, которые будут обновляться
                OleDbCommand ocm0 = new OleDbCommand("SELECT no_hs, street, no_hs2, faculties, note FROM Housing WHERE id_hs = " + b + ";", oleCommand);
                OleDbDataReader or0 = ocm0.ExecuteReader();

                while (or0.Read())
                {
                    DialogResult result = MessageBox.Show("Вы уверены, что хотите обновить в базе запись:" +
                    "\n\n№ корпуса: '" + or0[0].ToString() + "' на '" + eTBox10.Text + "'" +
                    "\n\n№ здания: '" + or0[2].ToString() + "' на '" + eTBox17.Text + "'" +
                    "\n\nУлица: '" + or0[1].ToString() + "' на '" + eTBox11.Text + "'" +
                    "\n\nФакультеты: '" + or0[3].ToString() + "' на '" + eTBox12.Text + "'" +
                    "\n\nНазвание: '" + or0[4].ToString() + "' на '" + eTBox13.Text + "'?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes) //Если нажал Да
                    {
                        string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
                        OleDbConnection dbCon = new OleDbConnection(conString);
                        dbCon.Open();

                        string oleCommand0 = @"
                        UPDATE Housing SET " +
                        "[no_hs] = '" + eTBox10.Text +
                        "', [street] = '" + eTBox11.Text +
                        "', [no_hs2] = '" + eTBox17.Text +
                        "', [faculties] = '" + eTBox12.Text +
                        "', [note] = '" + eTBox13.Text +
                        "' WHERE [id_hs] = " + b + ";";

                        OleDbCommand cmd = new OleDbCommand();
                        cmd.CommandText = oleCommand0;
                        cmd.Connection = dbCon;
                        cmd.ExecuteNonQuery();

                        dbCon.Close();

                        ClearHs_Cr();

                        HousingDL_C();
                    }
                }
            }
        }

        //Функция обновления (сохранения изменений) записи в базе для карты корпуса
        public void SaveHsCr2()
        {
            if (eTBox15.Text == "")
            {
                MessageBox.Show("Поле ''Номер кабинета'' - пустое!", "Предупреждение", MessageBoxButtons.OK);
            }

            else
            {
                int a = eComBox8.SelectedIndex;
                string id_cr = listCr[a];

                OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
                oleCommand.Open();

                //Получаем данные о кабинете из базы, которые будут обновляться
                OleDbCommand ocm0 = new OleDbCommand("SELECT no_cr, title FROM Classrooms WHERE id_cr = " + id_cr + ";", oleCommand);
                OleDbDataReader or0 = ocm0.ExecuteReader();

                while (or0.Read())
                {
                    DialogResult result = MessageBox.Show("Вы уверены, что хотите обновить в базе запись:" +
                    "\n\n№ кабинета: '" + or0[0].ToString() + "' на '" + eTBox15.Text + "'" +
                    "\n\nНазвание: '" + or0[1].ToString() + "' на '" + eTBox16.Text + "'?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes) //Если нажал Да
                    {
                        string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
                        OleDbConnection dbCon = new OleDbConnection(conString);
                        dbCon.Open();

                        string oleCommand0 = @"
                        UPDATE [Classrooms] SET " +
                        "[no_cr] = '" + eTBox15.Text +
                        "', [title] = '" + eTBox16.Text +
                        "' WHERE [id_cr] = " + id_cr + ";";

                        OleDbCommand cmd = new OleDbCommand();
                        cmd.CommandText = oleCommand0;
                        cmd.Connection = dbCon;
                        cmd.ExecuteNonQuery();

                        dbCon.Close();

                        ClearHs_Cr2();
                        ClassroomsDL_C();
                    }
                }
            }
        }

        //Функция обновления (сохранения изменений) записи в базе для карты корпуса (без подтверждения)
        public void SaveHsCr3()
        {
            if (eTBox15.Text == "")
            {
                MessageBox.Show("Поле ''Номер кабинета'' - пустое!", "Предупреждение", MessageBoxButtons.OK);
            }

            else
            {
                int a = eComBox8.SelectedIndex;
                string id_cr = listCr[a];

                string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
                OleDbConnection dbCon = new OleDbConnection(conString);
                dbCon.Open();

                string oleCommand0 = @"
                        UPDATE [Classrooms] SET " +
                "[no_cr] = '" + eTBox15.Text +
                "', [title] = '" + eTBox16.Text +
                "' WHERE [id_cr] = " + id_cr + ";";

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = oleCommand0;
                cmd.Connection = dbCon;
                cmd.ExecuteNonQuery();

                dbCon.Close();

                ClearHs_Cr2();
                ClassroomsDL_C();
            }
        }

        //Выбор активной вкладки "Корпуса и Кабинеты"
        private void AdmPage4_Enter(object sender, EventArgs e)
        {
            ClearHs_Cr();
            HousingDL_C();
        }

        //Выбор корпуса >> заполнение карты корпуса
        private void eComBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            eBut20.Visible = false;
            eBut16.Visible = true;
            eBut17.Visible = true;

            ClearHs_Cr2();
            
            int a = eComBox7.SelectedIndex;
            string b = listHs[a];

            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            //Номер корпуса
            OleDbCommand ocm0 = new OleDbCommand("SELECT no_hs FROM Housing WHERE id_hs = " + b + ";", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();
            while (or0.Read())
            {
                eTBox10.Text = or0["no_hs"].ToString();
            }

            //Номер здания
            OleDbCommand ocm1 = new OleDbCommand("SELECT no_hs2 FROM Housing WHERE id_hs = " + b + ";", oleCommand);
            OleDbDataReader or1 = ocm1.ExecuteReader();
            while (or1.Read())
            {
                eTBox17.Text = or1["no_hs2"].ToString();
            }

            //Улица/Проспект
            OleDbCommand ocm2 = new OleDbCommand("SELECT street FROM Housing WHERE id_hs = " + b + ";", oleCommand);
            OleDbDataReader or2 = ocm2.ExecuteReader();
            while (or2.Read())
            {
                eTBox11.Text = or2["street"].ToString();
            }

            //Факультеты
            OleDbCommand ocm3 = new OleDbCommand("SELECT faculties FROM Housing WHERE id_hs = " + b + ";", oleCommand);
            OleDbDataReader or3 = ocm3.ExecuteReader();
            while (or3.Read())
            {
                eTBox12.Text = or3["faculties"].ToString();
            }

            //Название
            OleDbCommand ocm4 = new OleDbCommand("SELECT note FROM Housing WHERE id_hs = " + b + ";", oleCommand);
            OleDbDataReader or4 = ocm4.ExecuteReader();
            while (or4.Read())
            {
                eTBox13.Text = or4["note"].ToString();
            }

            eGroupBox3.Enabled = true;

            ClassroomsDL_C();
            eBut22.Enabled = true;
        }

        //Кнопка "Сохранить" для карты корпуса
        private void eBut17_Click(object sender, EventArgs e)
        {
            SaveHsCr();
        }

        //Кнопка "+" - активация добавления нового корпуса
        private void eBut21_Click(object sender, EventArgs e)
        {
            ClearHs_Cr();
            HousingDL_C();

            eGroupBox3.Enabled = true;

            eBut16.Visible = false;
            eBut17.Visible = false;

            eBut20.Location = new Point(eBut17.Location.X, eBut17.Location.Y);
            eBut20.Visible = true;
        }

        //Кнопка "Добавить новый" для карты корпуса
        private void eBut20_Click(object sender, EventArgs e)
        {
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            //Проверка - есть ли в базе запись с такой улицей
            OleDbCommand ocm0 = new OleDbCommand("SELECT street FROM Housing WHERE street = '" + eTBox11.Text + "';", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();

            while (or0.Read())
            {
                tt0 = or0["street"].ToString();
            }

            if (tt0 == eTBox11.Text)
            {
                MessageBox.Show("Корпус с улицей/проспектом: '" + tt0 + "' уже существует в базе.", "Предупреждение", MessageBoxButtons.OK);
            }

            else
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить в базу запись:" +
                "\n\n№ корпуса: " + eTBox10.Text +
                "\n№ здания: " + eTBox17.Text +
                "\nУлица: " + eTBox11.Text +
                "\nФакультеты: " + eTBox12.Text +
                "\nНазвание: " + eTBox13.Text,
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes) //Если нажал Да
                {
                    string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
                    OleDbConnection dbCon = new OleDbConnection(conString);
                    dbCon.Open();

                    string oleCommand3 = @"INSERT INTO " +
                    "[Housing]([street]) " +
                    "VALUES('" + eTBox11.Text + "');";

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = oleCommand3;
                    cmd.Connection = dbCon;
                    cmd.ExecuteNonQuery();

                    string oleCommand0 = @"
                    UPDATE Housing SET " +
                    "[no_hs] = '" + eTBox10.Text +
                    "', [street] = '" + eTBox11.Text +
                    "', [no_hs2] = '" + eTBox17.Text +
                    "', [faculties] = '" + eTBox12.Text +
                    "', [note] = '" + eTBox13.Text +
                    "' WHERE [street] = '" + eTBox11.Text + "';";

                    cmd.CommandText = oleCommand0;
                    cmd.Connection = dbCon;
                    cmd.ExecuteNonQuery();

                    dbCon.Close();

                    ClearHs_Cr();
                    ClearHs_Cr2();

                    HousingDL_C();
                }
            }
        }

        //Кнопка "Удалить"
        private void eBut16_Click(object sender, EventArgs e)
        {
            int a = eComBox7.SelectedIndex;
            string id_hs = listHs[a];

            if (MessageBox.Show("Вы действительно хотите удалить корпус '' " + eComBox7.Text + " '' из базы?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
                OleDbConnection dbCon = new OleDbConnection(conString);
                dbCon.Open();

                string oleCommand0 = @"
                DELETE Housing.* FROM Housing WHERE id_hs = " + id_hs + ";";

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = oleCommand0;
                cmd.Connection = dbCon;
                cmd.ExecuteNonQuery();

                dbCon.Close();

                ClearHs_Cr();
                HousingDL_C();
            }
        }

        //Кнопка "+" - активация добавления нового кабинета
        private void eBut22_Click(object sender, EventArgs e)
        {
            ClearHs_Cr2();
            ClassroomsDL_C();

            eGroupBox4.Enabled = true;

            eBut18.Visible = false;
            eBut19.Visible = false;
            cBox1.Visible = false;

            eBut25.Location = new Point(eBut19.Location.X, eBut19.Location.Y);
            eBut25.Visible = true;
        }

        //Кнопка "Добавить в базу" для нового кабинета
        private void eBut25_Click(object sender, EventArgs e)
        {
            int a = eComBox7.SelectedIndex;
            string b = listHs[a];

            //Подключаемся к базе
            OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
            oleCommand.Open();

            //Проверка - есть ли в базе кабинет с таким номером
            OleDbCommand ocm0 = new OleDbCommand("SELECT no_cr FROM Classrooms WHERE id_hs = " + b + ";", oleCommand);
            OleDbDataReader or0 = ocm0.ExecuteReader();

            while (or0.Read())
            {
                tt0 = or0["no_cr"].ToString();
            }

            if (tt0 == eTBox15.Text)
            {
                MessageBox.Show("Кабинет с номером: '" + tt0 + "' для выбранного корпуса - уже существует в базе.", "Предупреждение", MessageBoxButtons.OK);
            }

            if (eTBox15.Text == "")
            {
                MessageBox.Show("Поле 'Номер кабинета' - пустое!", "Предупреждение", MessageBoxButtons.OK);
            }

            else
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить в базу запись:" +
                "\n\nКорпус: " + eTBox14.Text +
                "\n№ кабинета: " + eTBox15.Text +
                "\nНазвание: " + eTBox16.Text,
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes) //Если нажал Да
                {
                    string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
                    OleDbConnection dbCon = new OleDbConnection(conString);
                    dbCon.Open();

                    string oleCommand3 = @"INSERT INTO " +
                    "[Classrooms]([no_cr]) " +
                    "VALUES('" + eTBox15.Text + "');";

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = oleCommand3;
                    cmd.Connection = dbCon;
                    cmd.ExecuteNonQuery();

                    string oleCommand0 = @"
                    UPDATE Classrooms SET " +
                    "[id_hs] = '" + b +
                    "', [no_cr] = '" + eTBox15.Text +
                    "', [title] = '" + eTBox16.Text +
                    "' WHERE [no_cr] = '" + eTBox15.Text + "';";

                    cmd.CommandText = oleCommand0;
                    cmd.Connection = dbCon;
                    cmd.ExecuteNonQuery();

                    dbCon.Close();

                    ClearHs_Cr2();
                    ClassroomsDL_C();
                }
            }
        }

        //Выбор кабинета >> заполнение карты кабинета
        private void eComBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eComBox8.Text != "У выбранного корпуса в базе отсутствуют кабинеты.")
            {
                //Получаем id ккабинета в базе
                int a = eComBox8.SelectedIndex;
                string id_cr = listCr[a];

                eBut25.Visible = false;
                eBut18.Visible = true;
                eBut19.Visible = true;
                cBox1.Visible = true;

                OleDbConnection oleCommand = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BD2.accdb; Persist Security Info=False;");
                oleCommand.Open();

                //Номер корпуса
                OleDbCommand ocm0 = new OleDbCommand("SELECT no_cr, title FROM Classrooms WHERE id_cr = " + id_cr + ";", oleCommand);
                OleDbDataReader or0 = ocm0.ExecuteReader();
                while (or0.Read())
                {
                    eTBox15.Text = or0["no_cr"].ToString();
                    eTBox16.Text = or0["title"].ToString();
                }

                eGroupBox4.Enabled = true;
            }

        }

        //Кнопка "Удалить" для карты кабинета
        private void eBut18_Click(object sender, EventArgs e)
        {
            int a = eComBox8.SelectedIndex;
            string id_cr = listCr[a];

            if (MessageBox.Show("Вы действительно хотите удалить кабинет '' " + eComBox8.Text + " '' из базы?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string conString = @"Provider =Microsoft.ACE.OLEDB.12.0;Data Source=BD2.accdb; Persist Security Info=False;";
                OleDbConnection dbCon = new OleDbConnection(conString);
                dbCon.Open();

                string oleCommand0 = @"
                DELETE Classrooms.* FROM Classrooms WHERE id_cr = " + id_cr + ";";

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = oleCommand0;
                cmd.Connection = dbCon;
                cmd.ExecuteNonQuery();

                dbCon.Close();

                ClearHs_Cr2();
                ClassroomsDL_C();
            }
        }

        //Кнопка "Сохранить" для карты кабинета
        private void eBut19_Click(object sender, EventArgs e)
        {
            if(cBox1.Checked == true)
            {
                SaveHsCr3();
            }

            else
            {
                SaveHsCr2();
            }
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    static class Program
    {
        public static bool aprun = false;
        public static bool admin = false;

        public static string f_name1, f_name2, f_name3, function, email1, in_tel, mb_tel;
        public static string Dp, Hs, Cr;
        public static int id_dp, id_cr;

        public static bool SaveCard = false;
        public static bool SaveCardNew = false;

        public static bool id_crb = false;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Auth());
            
            if(aprun == true)
            {
                if(admin == true)
                {
                    Application.Run(new Adm());
                }

                else
                {
                    Application.Run(new Work());
                }
            }
        }
    }
}

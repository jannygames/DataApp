using System;
using System.Windows.Forms;

namespace DataApp
{
    internal static class Program
    {
        /// <summary>
        /// 2022/09/11
        /// Created by Daniel Jankovskij
        /// This application displays XML user data using GUI table.
        /// Lets you change username and password, and saves changes to XML file,
        /// as well as generate new passwords which are 12 symbols in length
        /// for selected users.
        /// 
        /// NOTE: I was unable to generate passwords using shell script,
        /// so I decided to generate passwords using C#. My idea was to
        /// run shell script each time for selected user and print out
        /// username and generated password to the end of text file.
        /// </summary>

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

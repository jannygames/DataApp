using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace DataApp
{
    internal class InOut
    {
        //=====================This method reads data from XML file===========================
        public static List<User> ReadUsers(string fileName)
        {
            List<User> Users = new List<User>();
            XmlDocument document = new XmlDocument(); //declaring xmlDocument variable
            document.Load(fileName); //set it to xml file

            XmlNodeList nodes = document.SelectNodes("//config/person/acc");//path from which we will be scraping data

            foreach (XmlNode node in nodes)
            {
                XmlAttribute id = node.Attributes["id"];
                int data_id = int.Parse(id.Value);

                XmlNode username = node.SelectSingleNode("user_name");
                string data_username = username.InnerText;

                XmlNode password = node.SelectSingleNode("password");
                string data_password = password.InnerText;

                User user = new User(data_id, data_username, data_password);
                Users.Add(user);
            }

            return Users;
        }
        //====================================================================================

        //===============This method prints data to console for testing purposes==============
        public static void PrintUsers(List<User> Users)
        {
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("| {0,-15} | {1,-15} | {2, 20} |", "id", "username", "password");
            Console.WriteLine(new string('-', 60));

            foreach (User user in Users)
            {
                Console.WriteLine("| {0,-15} | {1,-15} | {2, 20} |", user.ID, user.Username, user.Password);
            }
            Console.WriteLine(new string('-', 60));
        }
        //====================================================================================

        //=========================This method updates XML file===============================
        public static void UpdateXML(string fileName, List<User> AllUsers)
        {
            XmlDocument document = new XmlDocument();
            document.Load(fileName);

            XmlNodeList nodes = document.SelectNodes("//config/person/acc");

            int i = 0;
            foreach (XmlNode node in nodes)
            {
                XmlAttribute idAttribute = node.Attributes["id"];

                if (idAttribute != null)
                {
                    XmlNode username = node.SelectSingleNode("user_name");
                    username.InnerText = AllUsers[i].Username;

                    XmlNode password = node.SelectSingleNode("password");
                    password.InnerText = AllUsers[i].Password;
                }
                i++;
            }
            document.Save(fileName);
        }
        //====================================================================================

        //===================This method updates data shown on the table======================
        public static void UpdateTableGUI(List<User> AllUsers, List<TextBox> UsernameTextboxes, List<TextBox> PasswordTextboxes, List<CheckBox> Checkboxes)
        {
            for (int i = 0; i < AllUsers.Count; i++)
            {
                UsernameTextboxes[i].Text = AllUsers[i].Username.ToString();

                PasswordTextboxes[i].Text = AllUsers[i].Password.ToString();

                Checkboxes[i].Checked = false;
            }
        }
        //====================================================================================
    }
}

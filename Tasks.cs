using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DataApp
{
    internal class Tasks
    {
        //=========================This method creates GUI table===============================
        public static void CreateTableGUI(List<User> AllUsers, List<Label> IdLabels, List<TextBox> UsernameTextboxes, List<TextBox> PasswordTextboxes, List<CheckBox> Checkboxes, TableLayoutPanel tableLayoutPanel)
        {
            Label titleIdLabel = new Label();
            Label titleUsernameLabel = new Label();
            Label titlePasswordLabel = new Label();
            Label titleCheckboxLabel = new Label();

            //Calculating how many rows table should have depending on users number
            for (int i = 0; i < AllUsers.Count; i++)
            {
                tableLayoutPanel.RowCount = AllUsers.Count + 1;
                tableLayoutPanel.ColumnCount = 4;
            }
            //---------------------------------
            
            //Setting up title
            titleIdLabel.Parent = tableLayoutPanel;
            tableLayoutPanel.SetRow(titleIdLabel, 0);
            tableLayoutPanel.SetColumn(titleIdLabel, 0);
            titleIdLabel.Text = "ID";
            titleIdLabel.ForeColor = Color.White;
            titleIdLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            titleUsernameLabel.Parent = tableLayoutPanel;
            tableLayoutPanel.SetRow(titleUsernameLabel, 0);
            tableLayoutPanel.SetColumn(titleUsernameLabel, 1);
            titleUsernameLabel.Text = "Username";
            titleUsernameLabel.ForeColor = Color.White;
            titleUsernameLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            titlePasswordLabel.Parent = tableLayoutPanel;
            tableLayoutPanel.SetRow(titlePasswordLabel, 0);
            tableLayoutPanel.SetColumn(titlePasswordLabel, 2);
            titlePasswordLabel.Text = "Password";
            titlePasswordLabel.ForeColor = Color.White;
            titlePasswordLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            titleCheckboxLabel.Parent = tableLayoutPanel;
            tableLayoutPanel.SetRow(titleCheckboxLabel, 0);
            tableLayoutPanel.SetColumn(titleCheckboxLabel, 3);
            titleCheckboxLabel.Text = "";
            titleUsernameLabel.ForeColor = Color.White;
            titleCheckboxLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            //-----------------------------------

            //Displaying config.xml data using GUI
            for (int i = 0; i < AllUsers.Count; i++)
            {
                IdLabels.Add(new Label());
                IdLabels[i].Parent = tableLayoutPanel;
                tableLayoutPanel.SetRow(IdLabels[i], i + 1);
                tableLayoutPanel.SetColumn(IdLabels[i], 0);
                IdLabels[i].Text = AllUsers[i].ID.ToString();
                IdLabels[i].ForeColor = Color.White;
                IdLabels[i].Font = new Font("Segoe UI", 10, FontStyle.Bold);

                UsernameTextboxes.Add(new TextBox());
                UsernameTextboxes[i].Parent = tableLayoutPanel;
                tableLayoutPanel.SetRow(UsernameTextboxes[i], i + 1);
                tableLayoutPanel.SetColumn(UsernameTextboxes[i], 1);
                UsernameTextboxes[i].Text = AllUsers[i].Username.ToString();
                UsernameTextboxes[i].ForeColor = Color.White;
                UsernameTextboxes[i].BackColor = Color.DarkSlateBlue;
                UsernameTextboxes[i].Font = new Font("Segoe UI", 10, FontStyle.Bold);

                PasswordTextboxes.Add(new TextBox());
                PasswordTextboxes[i].Parent = tableLayoutPanel;
                tableLayoutPanel.SetRow(PasswordTextboxes[i], i + 1);
                tableLayoutPanel.SetColumn(PasswordTextboxes[i], 2);
                PasswordTextboxes[i].Text = AllUsers[i].Password.ToString();
                PasswordTextboxes[i].ForeColor = Color.White;
                PasswordTextboxes[i].BackColor = Color.DarkSlateBlue;
                PasswordTextboxes[i].Font = new Font("Segoe UI", 10, FontStyle.Bold);

                Checkboxes.Add(new CheckBox());
                Checkboxes[i].Parent = tableLayoutPanel;
                tableLayoutPanel.SetRow(Checkboxes[i], i + 1);
                tableLayoutPanel.SetColumn(Checkboxes[i], 3);
                Checkboxes[i].Checked = false;
                Checkboxes[i].ForeColor = Color.White;
            }
            //------------------------------------
        }
        //====================================================================================

        //===========This method encrypts XML file using symmetrical encryption===============
        public static Aes StartEncrypt(string fileName, List<User> AllUsers, List<TextBox> UsernameTextboxes, List<TextBox> PasswordTextboxes)
        {
            Aes key = null;

            XmlDocument xmlDoc = new()
            {
                PreserveWhitespace = true
            };
            xmlDoc.Load(fileName);

            key = Aes.Create();
            Encryption.Encrypt(xmlDoc, key);

            xmlDoc.Save(fileName);
            MessageBox.Show("File encrypted successfuly...");

            return key;
        }
        //====================================================================================

        //=========================This method decrypts XML file==============================
        public static void StartDecrypt(string fileName, Aes key)
        {
            XmlDocument xmlDoc = new()
            {
                PreserveWhitespace = true
            };
            xmlDoc.Load(fileName);
            Encryption.Decrypt(xmlDoc, key);

            xmlDoc.Save(fileName);

            MessageBox.Show("File decrypted successfuly...");
        }
        //====================================================================================

        //=========================This method updates GUI table==============================
        public static void ChangeFile(List<User> AllUsers, List<TextBox> UsernameTextboxes, List<TextBox> PasswordTextboxes)
        {
            for (int i = 0; i < AllUsers.Count; i++)
            {
                AllUsers[i].Username = UsernameTextboxes[i].Text;
                AllUsers[i].Password = PasswordTextboxes[i].Text;
            }

            InOut.UpdateXML(@"config.xml", AllUsers);
            MessageBox.Show("Data changed successfuly...");
        }
        //====================================================================================

        //=============This method generates new password for selected users==================
        public static void Generating(List<User> AllUsers, List<TextBox> PasswordTextboxes, List<CheckBox> Checkboxes)
        {
            for(int i = 0; i < AllUsers.Count; i++)
            {
                if (Checkboxes[i].Checked)
                {
                    AllUsers[i].Password = GenerateToken(8);
                }
            }
        }
        //====================================================================================

        //====================================================================================
        //NOTE: I WAS UNABLE TO RUN A SHELL SCRIPT SO I DECIDED THAT I WOULD JUST COMPLETE THE 3rd TASK BY GEENRATING PASSWORDS THROUGH C# CODE
        public static string GenerateToken(int length)
        {
            using (RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[length];
                cryptRNG.GetBytes(tokenBuffer);
                return Convert.ToBase64String(tokenBuffer);
            }
        }
        //====================================================================================
    }
}

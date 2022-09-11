using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace DataApp
{
    public partial class Form1 : Form
    {
        //DECLARING VARIABLES
        List<User> AllUsers = new List<User>(); //this array stores all users from XML file
        List<TextBox> UsernameTextboxes = new List<TextBox>(); //username textbox array
        List<TextBox> PasswordTextboxes = new List<TextBox>(); //password textbox array
        List<CheckBox> Checkboxes = new List<CheckBox>(); //checkbox array

        Aes key_previous = null; //this var is used to store previous encryption key

        public Form1()
        {
            InitializeComponent();
            AllUsers = InOut.ReadUsers(@"config.xml"); //assigning users from xml file to array
            List<Label> IdLabels = new List<Label>(); //declaring new labels which are displayed in the table

            Tasks.CreateTableGUI(AllUsers, IdLabels, UsernameTextboxes, PasswordTextboxes, Checkboxes, tableLayoutPanel2); //Drawing table which size is dependant on user count in XML file
        }

        //=========================This method saves changes to XML file and encrypts it on: SAVE BUTTON CLICK===============================
        public void saveButton_Click_1(object sender, EventArgs e)
        {
            if (key_previous != null)
            {
                Tasks.StartDecrypt(@"config.xml", key_previous);
                Tasks.ChangeFile(AllUsers, UsernameTextboxes, PasswordTextboxes);
                key_previous = Tasks.StartEncrypt(@"config.xml", AllUsers, UsernameTextboxes, PasswordTextboxes);
            }
            else
            {
                Tasks.ChangeFile(AllUsers, UsernameTextboxes, PasswordTextboxes);
                key_previous = Tasks.StartEncrypt(@"config.xml", AllUsers, UsernameTextboxes, PasswordTextboxes);
            }
        }
        //===================================================================================================================================

        //======================This method generates new passwords for selected users and displays it on GUI table==========================
        private void button1_Click(object sender, EventArgs e)
        {
            Tasks.Generating(AllUsers, PasswordTextboxes, Checkboxes);
            InOut.UpdateTableGUI(AllUsers, UsernameTextboxes, PasswordTextboxes, Checkboxes);
        }
        //===================================================================================================================================
    }
}

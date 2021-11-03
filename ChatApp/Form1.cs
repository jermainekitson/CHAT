using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class Form1 : Form
    {
        ChatController c;
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            c = new ChatController("server=b231-14;database=chatappdb;integrated security=true;");
            try
            {
                c.CreateSchema();
            } catch
            {

            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            c.SendMessage(txtMessage.Text);
            txtMessage.Clear();
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //This chooses what column shows up
            //Similar to overriding the ToString()
            lstMessages.ValueMember = "ChatMessage";
            lstMessages.DataSource = c.GetMessages();

            DataTable tmp = c.GetMessages();
            if(tmp.Rows.Count > lstMessages.Items.Count)
            {
                lstMessages.DataSource = tmp;
            }

            lstMessages.SelectedIndex = lstMessages.Items.Count - 1;
        }
    }
}

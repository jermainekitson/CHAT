using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ChatApp
{
    public class ChatController
    {
        public DataTable GetMessages()
        {
            DataTable ret = new DataTable();
            conn = new SqlConnection(connectionString);
            string getAllMessages = "SELECT USERNAME + ': ' + CHATMESSAGE[CHATMESSAGE] FROM Chat ORDER BY POSTED";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(getAllMessages, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ret);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }
        private string connectionString = "";
        SqlConnection conn;

        public ChatController(string connString)
        {
            this.connectionString = connString;
        }

        public void SendMessage(string msg)
        {
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string messageInsert = $@"
            INSERT INTO CHAT
            VALUES (
            '{username}',
            '{msg}',
            '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')
            ";

            conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(messageInsert, conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }


        }

        public void CreateSchema()
        {
            string createTableQuery = @"
            CREATE TABLE CHAT(
            [USERNAME] [VARCHAR](25),
            [CHATMESSAGE][VARCHAR](100),
            [POSTED][DATETIME]
            )
            ";

            conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(createTableQuery, conn);

            try
            {
                conn.Open();
                //Use nonquery, when you dont expect results back
                cmd.ExecuteNonQuery();
            }
            //Jumps to catch only during exception
            catch
            {
                throw;
            }
            //Always jumps to finally
            finally
            {
                //No matter what happens, close the connection
                conn.Close();
            }
        }


    }
}

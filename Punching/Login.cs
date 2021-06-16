using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace LoginClass
{
    class Login
    {
        public static int globalUserID;
        public static string globalUsername;
        public static string globalFullName;
        public static string material;

        public string _fullname { get; set; }
        public string _password { get; set; }

        public Login(string user, string pass)
        {
            this._fullname = user;
            this._password = pass;
        }

        public bool attemptLogin(string fullName, string pass)
        {
            string sql = "select id,username,forename + ' ' + surname as fullname from dbo.[user] where forename + ' ' + surname = '" + fullName + "' AND password = '" + pass + "'";
            using (SqlConnection conn = new SqlConnection(Punching.CONNECT.ConnectionStringUser))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        globalUserID = Convert.ToInt32(reader["id"]);
                        globalUsername = Convert.ToString(reader["username"]);
                        globalFullName = Convert.ToString(reader["fullname"]);
                        if (conn.State == System.Data.ConnectionState.Open)
                            conn.Close();
                        conn.Dispose();
                        return true;
                    }
                    else
                    {
                        
                        if (conn.State == System.Data.ConnectionState.Open)
                            conn.Close();
                        conn.Dispose();
                        return false;
                    }
                }

            }
        }

    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trpo
{
   public class DataBase
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=test; SSL Mode=None");
      
        public void openConn() 
        {
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
        }
         public void closeConn()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
        public MySqlConnection getConn()
        {
          
                return conn;
            
        }
    }

    
}

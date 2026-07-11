using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sql;


namespace TwmApi26.Models
{
    public class DatabaseManager
    {
        //code for insert,update,delete 
        static SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=twmonline;Integrated Security=true;TrustServerCertificate=true");
        static SqlCommand cmd = null;
        static DataTable dt = null;

        public static bool Insert_update_delete(string command)
        {
            try
            {
                cmd = new SqlCommand(command, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        // code for select data
        public static DataTable DisplayRecords(string command)
        {
            cmd = new SqlCommand();
            dt = new DataTable();
            SqlDataAdapter sa = new SqlDataAdapter(command, con);
            sa.Fill(dt);
            return dt;
        }
    }
}

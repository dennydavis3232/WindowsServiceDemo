using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

namespace WindowsServiceDemo
{
    public class Connectioncls
    {
        SqlConnection con =new SqlConnection(ConfigurationManager.ConnectionStrings["CON"].ConnectionString);
        SqlCommand cmd=new SqlCommand();

        public DataTable ExecuteGetDataTable(string strCMDtext)
        {
            DataTable Dt = null; SqlDataAdapter Da = null;
            using (SqlConnection conn=new SqlConnection(ConfigurationManager.ConnectionStrings["CON"].ConnectionString))
            {
                conn.Open();
                cmd = new SqlCommand(strCMDtext, conn);
                cmd.CommandType = CommandType.Text;
                Dt = new DataTable();
                Da = new SqlDataAdapter(cmd);
                Da.Fill(Dt);
                Da.Dispose();

            }
            return Dt;
        }
       
        public int NonQueryFn(string sqlquery) //insert, delete, update
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(sqlquery, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return i;
        }

        public string ScalarFn(string sqlquery) //sum, avg, count
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(sqlquery, con);
            con.Open();
            string i = cmd.ExecuteScalar().ToString();
            con.Close();

            return i;
        }
        public SqlDataReader ReaderFn(string sqlquery) //select
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            cmd = new SqlCommand(sqlquery, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }
        public DataSet DataAdapterFn(string sqlquery)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlquery, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
        public DataTable DatatableFn(string sqlquery)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlquery, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        
    }
}
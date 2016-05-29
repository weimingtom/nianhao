using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace nianhao
{
    public class EraModel
    {
        public string name;
        public string era;
        public string year;
        public string fromTime;

        public EraModel GetById(string id)
        {
            string connStr = Config.GetDstConstr();
            SQLiteConnection conn = new SQLiteConnection(connStr);
            EraModel result = null;
            try
            {
                conn.Open();

                string sql = "select * from user where id=@id";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        result = new EraModel();
                        result.name = DBUtils.GetString(reader, "name");
                        result.era = DBUtils.GetString(reader, "era");
                        result.year = DBUtils.GetString(reader, "year");
                        result.fromTime = DBUtils.GetString(reader, "fromTime");
                    }
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return result;
        }

        public bool Insert(EraModel this_)
        {
            string connStr = Config.GetDstConstr(); 
            SQLiteConnection conn = new SQLiteConnection(connStr);
            try
            {
                conn.Open();

                string sql = "insert into user (id,name,pwd,acc,telephonenumber) values (@id,@name,@pwd,@acc,@telephonenumber)";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", this_.name);
                cmd.Parameters.AddWithValue("@era", this_.era);
                cmd.Parameters.AddWithValue("@year", this_.year);
                cmd.Parameters.AddWithValue("@fromTime", this_.fromTime);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void createTable()
        {
            SQLiteConnection conn;
            SQLiteCommand cmd;
            if (!new FileInfo(Config.DB_DATABASE + ".sqlite").Exists)
            {
                SQLiteConnection.CreateFile(Config.DB_DATABASE + ".sqlite");
            }
            string connStr = Config.GetDstConstr();
            conn = new SQLiteConnection(connStr);
            try
            {
                conn.Open();

                string sql = "CREATE TABLE IF NOT EXISTS user (\n" +
                    "  Id varchar(255),\n" +
                    "  Name varchar(255),\n" +
                    "  Pwd varchar(255),\n" +
                    "  Acc varchar(255),\n" +
                    "  TelephoneNumber varchar(255)\n" +
                    ");";
                cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

public static class DatabaseHelper
{
    private static string connectionString = "server=localhost;user=root;password=;database=hotel_db1";

    // تنفيذ أوامر SELECT وترجيع DataTable مع دعم الباراميترات
    public static DataTable ExecuteSelectCommand(string query, List<MySqlParameter> parameters = null)
    {
        using (MySqlConnection con = new MySqlConnection(connectionString))
        using (MySqlCommand cmd = new MySqlCommand(query, con))
        {
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }
            DataTable table = new DataTable();
            con.Open();
            table.Load(cmd.ExecuteReader());
            return table;
        }
    }

    // تنفيذ أوامر INSERT / UPDATE / DELETE مع الباراميترات
    public static int ExecuteNonQuery(string query, List<MySqlParameter> parameters = null)
    {
        using (MySqlConnection con = new MySqlConnection(connectionString))
        using (MySqlCommand cmd = new MySqlCommand(query, con))
        {
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }
            con.Open();
            return cmd.ExecuteNonQuery();
        }
    }

    // تنفيذ استعلام يُرجع قيمة واحدة (مثل SELECT COUNT(*) أو LAST_INSERT_ID()) مع الباراميترات
    public static object ExecuteScalar(string query, List<MySqlParameter> parameters = null)
    {
        using (MySqlConnection con = new MySqlConnection(connectionString))
        using (MySqlCommand cmd = new MySqlCommand(query, con))
        {
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }
            con.Open();
            return cmd.ExecuteScalar();
        }
    }

    // إنشاء بارامتر
    public static MySqlParameter CreateParameter(string name, object value)
    {
        return new MySqlParameter(name, value ?? DBNull.Value);
    }
}

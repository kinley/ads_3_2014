﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(local)\\sqlexpress;Initial Catalog=sportclub;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            //Вот здесь формируется список таблиц
            SqlCommand cmd = new SqlCommand("select TABLE_NAME from information_schema.tables where TABLE_CATALOG = 'sportclub'", con);

            SqlDataReader dr = cmd.ExecuteReader();
            System.Console.WriteLine("К базе подключились.\nНазвания таблиц:\n");
            int i = 1;
            List<String> tableNames = new List<String>();
            while (dr.Read())
            {
                tableNames.Add(dr[0].ToString());
                System.Console.WriteLine(i+" "+dr[0].ToString());
                i++;
            }

            System.Console.WriteLine("\n" + "Введите номер таблицы для вывода");
            try
            {
                i = Convert.ToInt16(System.Console.ReadLine());
                SqlCommand cmd2 = new SqlCommand("select * from " + tableNames[i] + ";", con);
                dr.Close();
                SqlDataReader dr2;                
                dr2 = cmd2.ExecuteReader();
                while (dr2.Read())//считываем запись
                {
                    for (int j = 0; j < dr2.FieldCount; j++)
                    {
                        System.Console.Write(dr2[j].ToString()+" ");//выводим поля записи
                    }
                    System.Console.WriteLine();
                }
            }
            catch (FormatException exc)
            { 
                System.Console.WriteLine(exc.Message);
            }

            System.Console.ReadKey();
        }
    }
}

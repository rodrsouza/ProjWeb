using System;
using System.Data.OleDb;
using System.Configuration;
using System.Data;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PagedList;
using System.Collections.Generic;

namespace ASPMVCShoppingCart.Models
{
    public class daoAccess
    {
        private static readonly daoAccess instance = new daoAccess();

        private daoAccess() { }

        public static daoAccess getInstance()
        {
            return instance;
        }

        public OleDbConnection getConexao()
        {
            string conn = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ToString();
            return new OleDbConnection(conn);
        }

        public List<tblProduct> getList()
        {
            OleDbConnection conn = instance.getConexao();
            conn.Open();

            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM `tblProduct`";

            var list = new List<tblProduct>();
            using (OleDbDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    list.Add(new tblProduct
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        Price = Convert.ToDecimal(rdr["Price"])
                    });
                }

                rdr.Close();
            }

            return list;
        }

        public tblProduct Find(int id)
        {
            OleDbConnection conn = instance.getConexao();
            conn.Open();

            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM `tblProduct` WHERE Id = " + Convert.ToString(id) + ";";

            var list = new List<tblProduct>();
            using (OleDbDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    list.Add(new tblProduct
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        Price = Convert.ToDecimal(rdr["Price"])
                    });
                }

                rdr.Close();
            }

            if(list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public void Remove(tblProduct new_product)
        {
            OleDbConnection conn = instance.getConexao();
            conn.Open();

            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE * FROM `tblProduct` WHERE Id = " + Convert.ToString(new_product.Id) + ";";

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Edit(tblProduct new_product)
        {
            OleDbConnection conn = instance.getConexao();
            conn.Open();

            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE `tblProduct` SET Name = '" + new_product.Name +
                              "' WHERE Id = " + Convert.ToString(new_product.Id) + ";";

            cmd.ExecuteNonQuery();

            cmd.CommandText = "UPDATE `tblProduct` SET Price = " + new_product.Price.ToString("n2").Replace(',', '.') +
                              " WHERE Id = " + Convert.ToString(new_product.Id) + ";";

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void createData(tblProduct new_product)
        {
            OleDbConnection conn = instance.getConexao();
            conn.Open();

            string str = "INSERT INTO tblProduct (Name, Price) VALUES ('" +
                         new_product.Name + "', " + new_product.Price.ToString("n2").Replace(',','.') + ");";
            Console.WriteLine(str);

            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = str;
            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}

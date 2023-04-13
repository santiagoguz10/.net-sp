using Microsoft.AspNetCore.Mvc.ModelBinding;
using Prueba_Algar.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;

namespace Prueba_Algar.Context
{
    public class Database
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=DbSisVentas;Integrated Security=True");

    
        public async Task<List<Producto>> GetAllProducts()
        {
            using (SqlCommand cmd = new SqlCommand("Sp_ListaProductos", con))
                {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var response = new List<Producto>();
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        response.Add(MapToValue(reader));
                    }
                }

                return response;
                
            }
        }

        public async Task<Producto> GetProductById(int Id)
        {
             using (SqlCommand cmd = new SqlCommand("GetByProductId", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    Producto response = null;
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValue(reader);
                        }
                    }

                    return response;
                
            }
        }


        private Producto MapToValue(SqlDataReader reader)
        {
            return new Producto()
            {
                Id = (int)reader["Id"],
                NombreProducto = (string)reader["NombreProducto"],
               // ValorProducto = (int)reader["ValorProducto"]
            };
        }

        public string CrearProducto(Producto producto)
        {
            string msg = string.Empty;
            try
            {
                SqlCommand com = new SqlCommand("Sp_CrearProducto", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@nombreProducto", producto.NombreProducto);
                com.Parameters.AddWithValue("@valorProducto", producto.ValorProducto);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg; 
        }

        public string CrearVenta(List<Producto> producto, int cedula, string direccion)
        {
            string msg = string.Empty;
            try
            {

                string jsonVenta = JsonSerializer.Serialize(producto); 

                SqlCommand com = new SqlCommand("Sp_CrearProducto", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@jsonVenta", jsonVenta);
                com.Parameters.AddWithValue("@cedula", cedula);
                com.Parameters.AddWithValue("@direccion", direccion);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        public async Task DeleteProduct(int Id)
        {
                using (SqlCommand cmd = new SqlCommand("DeleteProduct", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            
        }
    }
}

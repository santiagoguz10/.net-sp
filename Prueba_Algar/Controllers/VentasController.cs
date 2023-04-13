using Microsoft.AspNetCore.Mvc;
using Prueba_Algar.Context;
using Prueba_Algar.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Prueba_Algar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        Database database = new Database();



        [HttpGet]
        public async Task<List<Producto>> GetAllProducts()
        {

            try
            {
                var response = await database.GetAllProducts();

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpGet("{Id}")]
        public async Task<Producto> GetProductById(int Id)
        {

            try
            {
                var response = await database.GetProductById(Id);

                return response;
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        [HttpPost]
        public string CrearVenta([FromBody] List<Producto> producto, int cedula, string direccion)
        {
            string msg = string.Empty;

            try
            {
                msg = database.CrearVenta(producto, cedula, direccion);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }


        [HttpDelete("{Id}")]
        public async Task DeleteProduct(int Id)
        {

            try
            {
                await database.DeleteProduct(Id);

            }
            catch (Exception ex)
            {
                throw;

            }
        }
    }
} 

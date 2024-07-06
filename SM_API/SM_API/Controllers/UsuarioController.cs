using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SM_API.Entities;

namespace SM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("RegistrarUsuario")]

        public async Task<IActionResult> RegistrarUsuario(Usuario usuario)
        {
            Respuesta resp = new Respuesta();

            using (var database = new SqlConnection("Server=.; Database=Sabados; Trusted_Connection=True; TrustServerCertificate=True"))
            {
                var result = await database.ExecuteAsync("RegistrarUsuario", new { usuario.Identificacion, usuario.Correo, usuario.Contrasenna, usuario.Nombre }, commandType: System.Data.CommandType.StoredProcedure);


                if (result > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "Ok";
                    resp.Contenido = true;
                    return Ok(resp);

                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "La información del usuario ya se encuentra registrada.";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }

        [HttpPost]
        [Route("IniciarUsuario")]

        public async Task<IActionResult> IniciarUsuario(Usuario usuario)
        {
            Respuesta resp = new Respuesta();

            using (var database = new SqlConnection("Server=.; Database=Sabados; Trusted_Connection=True; TrustServerCertificate=True"))
            {
                var result = await database.QueryAsync<Usuario>("IniciarUsuario",
                    new { usuario.Correo, usuario.Contrasenna },
                    commandType: System.Data.CommandType.StoredProcedure);

                if (result.Count() < 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "Ok";
                    resp.Contenido = true;
                    return Ok(resp);

                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "La información del usuario no se encuentra registrada.";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }
    }
}

using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using SM_API.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IConfiguration iConfiguration) : ControllerBase
    {
        [HttpPost]
        [Route("RegistrarUsuario")]

        public async Task<IActionResult> RegistrarUsuario(Usuario usuario)
        {
            Respuesta resp = new Respuesta();

            using (var database = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
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

            using (var database = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await database.QueryFirstOrDefaultAsync<Usuario>("IniciarUsuario",
                    new { usuario.Correo, usuario.Contrasenna },
                    commandType: System.Data.CommandType.StoredProcedure);

                if (result != null)
                {
                    result.Token = GenerarToken(result.Consecutivo);

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

        public string GenerarToken(int Consecutivo)
        {
            string SecretKey = iConfiguration.GetSection("Llaves:SecretKey").Value!;
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, Consecutivo.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}

using SM_WEB.Entities;

namespace SM_WEB.Models
{
    public class UsuarioModel(HttpClient http) : IUsuarioModel
    {
        public void RegistrarUsuario(Usuario usuario)
        {
            string url = "https://localhost:7208/api/Usuario/RegistrarUsuario";
            JsonContent body = JsonContent.Create(usuario);
            var result = http.PostAsync(url, body);
        }

        public void IniciarSesion(Usuario usuario)
        {
            string url = "https://localhost:7208/api/Usuario/IniciarSesion";
            JsonContent body = JsonContent.Create(usuario);
            var result = http.PostAsync(url, body);
        }
    }
}

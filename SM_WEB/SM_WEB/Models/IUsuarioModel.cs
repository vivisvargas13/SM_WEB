﻿using SM_WEB.Entities;

namespace SM_WEB.Models
{
    public interface IUsuarioModel
    {
        void RegistrarUsuario(Usuario usuario);

        void IniciarSesion(Usuario usuario);
    }
}
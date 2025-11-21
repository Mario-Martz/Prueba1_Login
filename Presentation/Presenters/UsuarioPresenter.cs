using Prueba1_Login.AppCore.Services;
using Prueba1_Login.Presentation.Interfaces;
using Prueba1_Login.Domain.Entities;
using System.Collections.Generic;

namespace Prueba1_Login.Presentation.Presenters
{
    public class UsuarioPresenter
    {
        private readonly IUsuarioView _view;
        private readonly UsuarioService _service;

        public UsuarioPresenter(IUsuarioView view, UsuarioService service)
        {
            _view = view;
            _service = service;
        }

        public void CargarUsuarioPorCodigo(string codigo)
        {
            var u = _service.ObtenerPorCodigo(codigo);
            if (u == null)
            {
                _view.MostrarMensaje("Usuario no encontrado.");
                return;
            }
            _view.MostrarUsuario(u);
        }

        // 🔥 AQUI VA EL MÉTODO CORRECTO
        public void CargarTodos()
        {
            var lista = _service.ObtenerTodos();
            _view.MostrarLista(lista);
        }
    }
}

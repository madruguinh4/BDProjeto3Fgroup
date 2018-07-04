using BDProjeto.Dominio;
using BDProjeto.Dominio.contrato;
using System.Collections.Generic;

namespace BDProjeto.Aplicacao

{
    public class UsuarioAplicacao
    {
        private readonly IRepositorio<Usuarios> repositorio;

        public UsuarioAplicacao(IRepositorio<Usuarios> repo)
        {
            repositorio = repo; 
        }

        public void Salvar(Usuarios usuarios)
        {
            repositorio.Salvar(usuarios);
        }
        public void Excluir(Usuarios usuarios)
        {
            repositorio.Excluir(usuarios);
        }
        public IEnumerable<Usuarios> ListarTodos()
        {

            return repositorio.ListarTodos();
        }
        public Usuarios ListarPorId(string id)
        {
            return repositorio.ListarPorId(id);
        }
    }
}

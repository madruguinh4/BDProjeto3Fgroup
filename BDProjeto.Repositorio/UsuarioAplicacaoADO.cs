using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDProjeto.Dominio;
using BDProjeto.Repositorio;
using BDProjeto.Dominio.contrato;

namespace BDProjeto.RepositorioADO 

{
    public class UsuarioAplicacaoADO:IRepositorio<Usuarios>
    {
        private bd bd;
        //Inseri Usuario no BD
        public void Insert(Usuarios usuarios)
        {
            var strQuery = "";
            strQuery += "Insert Into usuarios(nome, cargo, date)";
            strQuery += string.Format("  VALUES ('{0}','{1}','{2}')", usuarios.Nome, usuarios.Cargo, usuarios.Date);

            //chama a classe BD e fecha descartando
            using (bd = new bd())
            {
                bd.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Usuarios usuarios)
        {
            var strQuery = "";
            strQuery += "Update usuarios SET  ";
            strQuery += string.Format("nome = '{0}',", usuarios.Nome);
            strQuery += string.Format("cargo = '{0}',", usuarios.Cargo);
            strQuery += string.Format("date = '{0}'", usuarios.Date);
            strQuery += string.Format("where Id = {0}", usuarios.Id);

            using (bd = new bd())
            {
                bd.ExecutaComando(strQuery);
            }
        }
        public void Salvar(Usuarios usuarios)
        {
            if (usuarios.Id > 0)
            {
                Alterar(usuarios);
            }
            else
            {
                Insert(usuarios);
            }
        }
        public void Excluir(Usuarios usuarios)
        {


            using (bd = new bd())
            {
                var strQuery = string.Format("Delete from usuarios  where Id = {0}", usuarios.Id);
                bd.ExecutaComando(strQuery);
            }
        }
        public IEnumerable<Usuarios> ListarTodos()
        {

            using (bd = new bd())
            {
                var strQuery = "Select * from usuarios";
                var retorno = bd.ExecutaComandoComRetorno(strQuery);
                return ReaderEmLista(retorno);

            }
        }

        public Usuarios ListarPorId(string id)
        {
            using (bd = new bd())
            {
                
                var strQuery = string.Format("Select * from usuarios where Id = {0}", id);
                var retorno = bd.ExecutaComandoComRetorno(strQuery);
                return ReaderEmLista(retorno).FirstOrDefault();
            }
        }
        private List<Usuarios> ReaderEmLista(SqlDataReader reader)
        {
            var usuarios = new List<Usuarios>();
            while (reader.Read())
            {
                var tempoObejto = new Usuarios()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nome = reader["nome"].ToString(),
                    Cargo = reader["cargo"].ToString(),
                    Date = DateTime.Parse(reader["date"].ToString()),
                };
                usuarios.Add(tempoObejto);
            }

            reader.Close();
            return usuarios;
        }
    }
}

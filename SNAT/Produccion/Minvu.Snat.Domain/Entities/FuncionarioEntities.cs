using Minvu.Snat.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class FuncionarioEntities
    {
        public string userName { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int rut { get; set; }
        public char dv { get; set; }
        public int idRegion { get; set; }
        public string region { get; set; }
    }

    public class FuncionarioEntitiesFactory
    {
        public static FuncionarioEntities getFuncionario(string UserName)
        {
            FuncionarioDAOFactory _funcionarioDAOFactory = new FuncionarioDAOFactory();
            FuncionarioDAO _funcionarioDAO = FuncionarioDAOFactory.getFuncionario(UserName);
            FuncionarioEntities _funcionarioEntities = new FuncionarioEntities();

            if (_funcionarioDAO != null)
            {

                _funcionarioEntities.dv = _funcionarioDAO.dv;
                _funcionarioEntities.idRegion = _funcionarioDAO.idRegion;
                _funcionarioEntities.region = _funcionarioDAO.region;
                _funcionarioEntities.rut = _funcionarioDAO.rut;
                _funcionarioEntities.userName = _funcionarioDAO.userName;


                return _funcionarioEntities;
            }else
            {
                return null;

            }
            
        }

        public static string getFuncionarioNombreCompleto(string UserName)
        {
            FuncionarioDAOFactory _funcionarioDAOFactory = new FuncionarioDAOFactory();
            FuncionarioDAO _funcionarioDAO = FuncionarioDAOFactory.getFuncionario(UserName);
            FuncionarioEntities _funcionarioEntities = new FuncionarioEntities();

            if (_funcionarioDAO != null)
            {
                _funcionarioEntities.dv = _funcionarioDAO.dv;
                _funcionarioEntities.idRegion = _funcionarioDAO.idRegion;
                _funcionarioEntities.region = _funcionarioDAO.region;
                _funcionarioEntities.rut = _funcionarioDAO.rut;
                _funcionarioEntities.userName = _funcionarioDAO.userName;
                _funcionarioEntities.Nombre = _funcionarioDAO.Nombre;
                _funcionarioEntities.ApellidoPaterno = _funcionarioDAO.ApellidoPaterno;
                _funcionarioEntities.ApellidoMaterno = _funcionarioDAO.ApellidoMaterno;

                return _funcionarioEntities.Nombre + " "+ _funcionarioEntities.ApellidoPaterno +" " + _funcionarioEntities.ApellidoMaterno;
            }
            else
            {
                return null;

            }

        }

    }
}

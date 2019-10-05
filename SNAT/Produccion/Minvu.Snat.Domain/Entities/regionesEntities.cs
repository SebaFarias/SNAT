using Minvu.Snat.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class RegionesEntities
    {
        [Display(Name = "Región:")]
        public int? idRegion { get; set; }

        public string nombreRegion { get; set; }
        public decimal? ordenRegion { get; set; }

        public List<RegionesEntities> lstRegiones = new List<RegionesEntities>();

        public RegionesEntities()
        {
            idRegion = 0;
            nombreRegion = String.Empty;
            ordenRegion = 0;
        }
        public RegionesEntities(int _idRegion, string _nombreRegion, decimal? _ordenRegion)
        {
            idRegion = _idRegion;
            nombreRegion = _nombreRegion;
            ordenRegion = _ordenRegion;
        }
    }
    public class RegionesEntitiesFactory
    {
        public static RegionesEntities getRegion(int idRegion)
        {
            RegionesEntities Region = new RegionesEntities();

            var objRegion = regionDAO.Obtener(idRegion);

            Region.idRegion = objRegion.REG_ID;
            Region.nombreRegion = objRegion.REG_DES;
            Region.ordenRegion = objRegion.REG_ORD;

            return Region;
        }
        public static int getRegionUserID(string userName)
        {
            var _RegionUser = regionDAO.GetUSERREGION(userName);
            if (_RegionUser != null)
            {
                return _RegionUser.IDRegion;
            }

            return 0;
        }

        internal static List<RegionesEntities> getListRegiones()
        {

            Log.Instance.Info("Ingresa a: getListRegiones ");
            List<RegionesEntities> _auxListRegionesEntities = new List<RegionesEntities>();

            var _RegionDAO = regionDAO.obtenerRegiones();
            
            if (_RegionDAO != null)
            {
                foreach (var item in _RegionDAO)
                {
                    RegionesEntities _RegionesEntities = new RegionesEntities();

                    _RegionesEntities.idRegion = item.REG_ID;
                    _RegionesEntities.nombreRegion = item.REG_DES;
                    _RegionesEntities.ordenRegion = item.REG_ORD;

                    _auxListRegionesEntities.Add(_RegionesEntities);
                }
            }
            else
            {
                Log.Instance.Info("Fallo region");
            }

            return _auxListRegionesEntities;
        }

        internal static List<RegionesEntities> getListRegionesPresupuesto()
        {
            List<RegionesEntities> _auxListRegionesEntities = new List<RegionesEntities>();

            var _RegionDAO = regionDAO.obtenerRegiones();

            if (_RegionDAO != null)
            {
                foreach (var item in _RegionDAO)
                {
                    RegionesEntities _RegionesEntities = new RegionesEntities();

                    _RegionesEntities.idRegion = item.REG_ID;
                    _RegionesEntities.nombreRegion = item.REG_DES;
                    _RegionesEntities.ordenRegion = item.REG_ORD;

                    _auxListRegionesEntities.Add(_RegionesEntities);
                }
            }

            _auxListRegionesEntities.Insert(0, new RegionesEntities { nombreRegion = "-----Seleccione-----", idRegion = 0 });

            return _auxListRegionesEntities;
        }

        internal static string getRegionUser(string userName)
        {
            var _RegionUser = regionDAO.GetUSERREGION(userName);
            if (_RegionUser != null)
            {
                return _RegionUser.IDRegion.ToString();
            }

            return null;
        }

        internal static List<RegionesEntities> GetRegion(int idRegion)
        {
            var _RegionUser = regionDAO.ObtenerRegion(idRegion);
            List<RegionesEntities> _ListaRegionesEntities = new List<RegionesEntities>();
            if (_RegionUser != null)
            {
                foreach (var item in _RegionUser)
                {
                    RegionesEntities _RegionesEntities = new RegionesEntities();

                    _RegionesEntities.idRegion = item.REG_ID;
                    _RegionesEntities.nombreRegion = item.REG_DES;
                    _RegionesEntities.ordenRegion = Convert.ToInt32(item.REG_ORD);

                    _ListaRegionesEntities.Add(_RegionesEntities);

                }
                return _ListaRegionesEntities;
            }
            return _ListaRegionesEntities;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using System.ComponentModel.DataAnnotations;


namespace Minvu.Snat.Domain.Entities
{
    public class ProvinciasEntities
    {
        [Display(Name = "Provincia:")]
        public int? idProvincia { get; set; }
        public string nombreProvincia { get; set; }
        public int idRegion { get; set; }

        public List<ProvinciasEntities> lstProvincias = new List<ProvinciasEntities>();

        public ProvinciasEntities()
        {
            idProvincia = 0;
            nombreProvincia = String.Empty;
            idRegion = 0;
        }
        public ProvinciasEntities(int _idProvincia, string _nombreProvincia, int _idRegion)
        {
            idProvincia = _idProvincia;
            nombreProvincia = _nombreProvincia;
            idRegion = _idRegion;
        }
    }

    public class ProvinciasEntitiesFactory
    {

        internal static List<ProvinciasEntities> getMantenerListProvinciaRegion(long? idRegion, long? idProvincia)
        {
            if (idRegion != null && idProvincia != null)
            {

                List<ProvinciasEntities> lstProvRegion = getListProvincias();

                var ProviRegion = from p in lstProvRegion
                                  where p.idRegion == idRegion
                                  select p;

                lstProvRegion = new List<ProvinciasEntities>();

                foreach (var p in ProviRegion)
                {
                   
                        ProvinciasEntities ProvRegion = new ProvinciasEntities();

                        ProvRegion.idProvincia = p.idProvincia;
                        ProvRegion.nombreProvincia = p.nombreProvincia;
                        ProvRegion.idRegion = p.idRegion;

                        lstProvRegion.Add(ProvRegion);
                    
                }

                return lstProvRegion;
            }
            else if (idRegion != null && idProvincia == null)
                {
                List<ProvinciasEntities> lstProvRegion = getListProvincias();

                var ProviRegion = from p in lstProvRegion
                                  where p.idRegion == idRegion
                                  select p;

                lstProvRegion = new List<ProvinciasEntities>();

                foreach (var p in ProviRegion)
                {
                    ProvinciasEntities ProvRegion = new ProvinciasEntities();

                    ProvRegion.idProvincia = p.idProvincia;
                    ProvRegion.nombreProvincia = p.nombreProvincia;
                    ProvRegion.idRegion = p.idRegion;

                    lstProvRegion.Add(ProvRegion);
                }

                return lstProvRegion;
            }else
            {
                List<ProvinciasEntities> _aux = new List<ProvinciasEntities>();
                //ProvinciasEntities _auxP = new ProvinciasEntities();
                //_auxP.idProvincia = 0;
                //_auxP.nombreProvincia = "-- Seleccione --";

                //_aux.Add(_auxP);

                return _aux;
            }
        }


        internal static List<ProvinciasEntities> getListProvinciaRegion(long? idRegion)
        {
            if (idRegion != null)
            {

                List<ProvinciasEntities> lstProvRegion = getListProvincias();

                var ProviRegion = from p in lstProvRegion
                                  where p.idRegion == idRegion
                                  select p;

                lstProvRegion = new List<ProvinciasEntities>();

                foreach (var p in ProviRegion)
                {
                    ProvinciasEntities ProvRegion = new ProvinciasEntities();

                    ProvRegion.idProvincia = p.idProvincia;
                    ProvRegion.nombreProvincia = p.nombreProvincia;
                    ProvRegion.idRegion = p.idRegion;

                    lstProvRegion.Add(ProvRegion);
                }

                return lstProvRegion;
            }
            else
            {
                List<ProvinciasEntities> _aux = new List<ProvinciasEntities>();
                //ProvinciasEntities _auxP = new ProvinciasEntities();
                //_auxP.idProvincia = 0;
                //_auxP.nombreProvincia = "-- Seleccione --";

                //_aux.Add(_auxP);

                return _aux;
            }
        }

        internal static List<ProvinciasEntities> getListProvincias()
        {
            List<ProvinciasEntities> _auxListProvinciasEntities = new List<ProvinciasEntities>();

            var _ProvinciaDAO = ProvinciaDAO.obtenerProvincias();

            if (_ProvinciaDAO != null)
            {
                foreach (var item in _ProvinciaDAO)
                {
                    ProvinciasEntities _ProvinciasEntities = new ProvinciasEntities();

                    _ProvinciasEntities.idProvincia = item.PRV_ID;
                    _ProvinciasEntities.nombreProvincia = item.PRV_DES;
                    _ProvinciasEntities.idRegion = item.REG_ID;

                    _auxListProvinciasEntities.Add(_ProvinciasEntities);
                }
            }

            return _auxListProvinciasEntities;
        }
    }
}

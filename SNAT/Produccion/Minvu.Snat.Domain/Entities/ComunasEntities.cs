using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using System.ComponentModel.DataAnnotations;
namespace Minvu.Snat.Domain.Entities
{
    public class ComunasEntities
    {
        [Display(Name = "Comuna:")]
        public int? idComuna { get; set; }
        public string nombreComuna { get; set; }
        public int idProvincia { get; set; }
        public int idRegion { get; set; }
        public bool? zonaSaturada { get; set; }
        public int? zonaTermica { get; set; }
        public int? ComIDSII { get; set; }
        public bool? ComQtyHab { get; set; }
        public bool? ComQtyHabExcp { get; set; }
        public int? ComTra { get; set; }
        public int? ComMaxCvsEspTII { get; set; }
        public int? ComQtyNumHab { get; set; }

        public List<ComunasEntities> lstComunas = new List<ComunasEntities>();

        public ComunasEntities()
        {
            idComuna = 0;
            nombreComuna = String.Empty;
            idProvincia = 0;
            idRegion = 0;
        }
        public ComunasEntities(int _idComuna, string _nombreComuna, int _idProvincia, int _idRegion)
        {
            idComuna = _idComuna;
            nombreComuna = _nombreComuna;
            idProvincia = _idProvincia;
            idRegion = _idRegion;
        }
    }
    public class ComunasEntitiesFactory
    {
        internal static ComunasEntities getComuna(int _idComuna)
        {
            ComunasEntities objComuna = new ComunasEntities();

            List<ComunasEntities> lstComunas = getListComunas();

            var ComunaProvi = from p in lstComunas
                              where p.idComuna == _idComuna
                              select p;

            foreach (var p in ComunaProvi)
            {
                objComuna.idComuna = p.idComuna;
                objComuna.nombreComuna = p.nombreComuna;
                objComuna.idProvincia = p.idProvincia;
                objComuna.idRegion = p.idRegion;
                objComuna.zonaSaturada = p.zonaSaturada;
                objComuna.zonaTermica = p.zonaTermica;
                objComuna.ComIDSII = p.ComIDSII;
                objComuna.ComQtyHab = p.ComQtyHab;
                objComuna.ComQtyHabExcp = p.ComQtyHabExcp;
                objComuna.ComTra = p.ComTra;
                objComuna.ComMaxCvsEspTII = p.ComMaxCvsEspTII;
                objComuna.ComQtyNumHab = p.ComQtyNumHab;
            }

            return objComuna;
        }

        internal static List<ComunasEntities> getMantenerListComunaProvincia(long? idProvincia, long? idComuna)
        {
            if (idProvincia != null && idComuna != null)
            {
                List<ComunasEntities> lstComunaProv = getListComunas();

                var ComunaProvi = from p in lstComunaProv
                                  where p.idProvincia == idProvincia 
                                  select p;

                lstComunaProv = new List<ComunasEntities>();

                

                foreach (var p in ComunaProvi)
                {
                    
                        ComunasEntities ComunaProv = new ComunasEntities();

                        ComunaProv.idComuna = p.idComuna;
                        ComunaProv.nombreComuna = p.nombreComuna;
                        ComunaProv.idProvincia = p.idProvincia;
                        ComunaProv.idRegion = p.idRegion;
                        ComunaProv.zonaSaturada = p.zonaSaturada;
                        ComunaProv.zonaTermica = p.zonaTermica;
                        ComunaProv.ComIDSII = p.ComIDSII;
                        ComunaProv.ComQtyHab = p.ComQtyHab;
                        ComunaProv.ComQtyHabExcp = p.ComQtyHabExcp;
                        ComunaProv.ComTra = p.ComTra;
                        ComunaProv.ComMaxCvsEspTII = p.ComMaxCvsEspTII;
                        ComunaProv.ComQtyNumHab = p.ComQtyNumHab;

                        lstComunaProv.Add(ComunaProv);
                       

                }

                return lstComunaProv;
            }
            else if (idProvincia != null && idComuna == null)
            {
                List<ComunasEntities> lstComunaProv = getListComunas();

                var ComunaProvi = from p in lstComunaProv
                                  where p.idProvincia == idProvincia
                                  select p;

                lstComunaProv = new List<ComunasEntities>();


                foreach (var p in ComunaProvi)
                {
                   
                        ComunasEntities ComunaProv = new ComunasEntities();

                        ComunaProv.idComuna = p.idComuna;
                        ComunaProv.nombreComuna = p.nombreComuna;
                        ComunaProv.idProvincia = p.idProvincia;
                        ComunaProv.idRegion = p.idRegion;
                        ComunaProv.zonaSaturada = p.zonaSaturada;
                        ComunaProv.zonaTermica = p.zonaTermica;
                        ComunaProv.ComIDSII = p.ComIDSII;
                        ComunaProv.ComQtyHab = p.ComQtyHab;
                        ComunaProv.ComQtyHabExcp = p.ComQtyHabExcp;
                        ComunaProv.ComTra = p.ComTra;
                        ComunaProv.ComMaxCvsEspTII = p.ComMaxCvsEspTII;
                        ComunaProv.ComQtyNumHab = p.ComQtyNumHab;

                        lstComunaProv.Add(ComunaProv);
                }
                return lstComunaProv;
            }
            else
            {
                List<ComunasEntities> _aux = new List<ComunasEntities>();
                //ComunasEntities _auxP = new ComunasEntities();
                //_auxP.idComuna = 0;
                //_auxP.nombreComuna = "-- Seleccione --";

                //_aux.Add(_auxP);

                return _aux;
            }
        }



        internal static List<ComunasEntities> getComunasProvincia(long? idProvincia)
        {
            if (idProvincia != null)
            {
                List<ComunasEntities> lstComunaProv = getListComunas();

                var ComunaProvi = from p in lstComunaProv
                                  where p.idProvincia == idProvincia
                                  select p;

                lstComunaProv = new List<ComunasEntities>();

                foreach (var p in ComunaProvi)
                {
                    ComunasEntities ComunaProv = new ComunasEntities();

                    ComunaProv.idComuna = p.idComuna;
                    ComunaProv.nombreComuna = p.nombreComuna;
                    ComunaProv.idProvincia = p.idProvincia;
                    ComunaProv.idRegion = p.idRegion;
                    ComunaProv.zonaSaturada = p.zonaSaturada;
                    ComunaProv.zonaTermica = p.zonaTermica;
                    ComunaProv.ComIDSII = p.ComIDSII;
                    ComunaProv.ComQtyHab = p.ComQtyHab;
                    ComunaProv.ComQtyHabExcp = p.ComQtyHabExcp;
                    ComunaProv.ComTra = p.ComTra;
                    ComunaProv.ComMaxCvsEspTII = p.ComMaxCvsEspTII;
                    ComunaProv.ComQtyNumHab = p.ComQtyNumHab;

                    lstComunaProv.Add(ComunaProv);
                }

                return lstComunaProv;
            }
            else
            {
                List<ComunasEntities> _aux = new List<ComunasEntities>();
                //ComunasEntities _auxP = new ComunasEntities();
                //_auxP.idComuna = 0;
                //_auxP.nombreComuna = "-- Seleccione --";

                //_aux.Add(_auxP);

                return _aux;
            }
        }
        internal static List<ComunasEntities> getListComunas()
        {
            List<ComunasEntities> _auxListComunasEntities = new List<ComunasEntities>();

            var _ComunaDAO = comunaDAO.obtenerComunas();

            if (_ComunaDAO != null)
            {
                foreach (var item in _ComunaDAO)
                {
                    ComunasEntities _ComunasEntities = new ComunasEntities();

                    _ComunasEntities.idComuna = item.COM_ID;
                    _ComunasEntities.nombreComuna = item.COM_DES;
                    _ComunasEntities.idProvincia = item.PRV_ID;
                    _ComunasEntities.idRegion = item.REG_ID;
                    _ComunasEntities.zonaSaturada = item.ZONA_SATURADA;
                    _ComunasEntities.zonaTermica = item.ZONA_TERMICA;
                    _ComunasEntities.ComIDSII = item.COM_ID_SII;
                    _ComunasEntities.ComQtyHab = item.COM_QTY_HAB;
                    _ComunasEntities.ComQtyHabExcp = item.COM_QTY_HAB_EXCP;
                    _ComunasEntities.ComTra = item.COM_TRA;
                    _ComunasEntities.ComMaxCvsEspTII = item.COM_MAX_CVS_ESP_TII;
                    _ComunasEntities.ComQtyNumHab = item.COM_QTY_NUM_HAB;

                    _auxListComunasEntities.Add(_ComunasEntities);
                }
            }

            return _auxListComunasEntities;
        }
    }
}

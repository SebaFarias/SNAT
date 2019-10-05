using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroTipologiaEntities
    {
        public long idMaestroTipologia { get; set; }
        public long idMaestroPrograma { get; set; }

        [Display(Name = "Tipología:")]
        [Required(ErrorMessage = "El nombre del llamado es obligatorio")]
        //[RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreMaestroTipologia { get; set; }
        public bool estadoMaestroTipologia { get; set; }

        public maestroTipologiaEntities()
        {
            idMaestroTipologia = 0;
            idMaestroPrograma = 0;
            nombreMaestroTipologia = string.Empty;
            estadoMaestroTipologia = false;
        }
        public maestroTipologiaEntities(long _idMaestroTipologia, long _idMaestroPrograma, string _nombreMaestroTipologia, bool _estadoMaestroTipologia)
        {
            idMaestroPrograma = _idMaestroPrograma;
            idMaestroTipologia = _idMaestroTipologia;
            nombreMaestroTipologia = _nombreMaestroTipologia;
            estadoMaestroTipologia = _estadoMaestroTipologia;
        }
    }

    public class maestroTipologiaEntitiesFactory
    {

        internal static maestroTipologiaEntities getTipologia(long idTipologia)
        {
            var _maestroTipologiaDAO = maestroTipologiaDAO.Get(idTipologia);
            if (_maestroTipologiaDAO != null)
            {
                return new maestroTipologiaEntities
                {

                    idMaestroTipologia = Convert.ToInt64(_maestroTipologiaDAO.IDMAESTROTIPOLOGIA),
                    nombreMaestroTipologia = _maestroTipologiaDAO.NOMBREMAESTROTIPOLOGIA,
                    estadoMaestroTipologia = Convert.ToBoolean(_maestroTipologiaDAO.ESTADOMAESTROTIPOLOGIA)

                };
            }
            else
                return null;


        }


        internal static List<maestroTipologiaEntities> getListTipologiaPrograma(long? idPrograma)
        {
            if (idPrograma != null)
            {


                var _listMaestroTipologiaDAO = maestroTipologiaDAO.GetListTipologiaPrograma(idPrograma);

                List<maestroTipologiaEntities> _auxListMaestroTipologiaEntities = new List<maestroTipologiaEntities>();


                if (_listMaestroTipologiaDAO != null)
                {
                    foreach (var item in _listMaestroTipologiaDAO)
                    {
                        maestroTipologiaEntities _maestroTipologiaEntities = new maestroTipologiaEntities();

                        _maestroTipologiaEntities.idMaestroTipologia = item.IDMAESTROTIPOLOGIA;
                        _maestroTipologiaEntities.nombreMaestroTipologia = item.NOMBREMAESTROTIPOLOGIA;
                        //_maestroTipologiaEntities.estadoMaestroTipologia = Convert.ToBoolean(item.ESTADOMAESTROTIPOLOGIA);

                        _auxListMaestroTipologiaEntities.Add(_maestroTipologiaEntities);
                    }
                    return _auxListMaestroTipologiaEntities;
                }
                else
                    return null;
            }
            else
            {

                List<maestroTipologiaEntities> _auxListMaestroTipologiaEntities = new List<maestroTipologiaEntities>();
                //maestroTipologiaEntities _maestroTipologiaEntities = new maestroTipologiaEntities();
                //_maestroTipologiaEntities.idMaestroTipologia = 0;
                //_maestroTipologiaEntities.nombreMaestroTipologia = "-- Seleccione --";

                //_auxListMaestroTipologiaEntities.Add(_maestroTipologiaEntities);

                return _auxListMaestroTipologiaEntities;

            }


        }

        internal static List<maestroTipologiaEntities> getMantenerListTipologiaPrograma(long? idPrograma, long? idTipologia)
        {
            if (idPrograma != null && idTipologia != null)
            {


                var _listMaestroTipologiaDAO = maestroTipologiaDAO.GetListTipologiaPrograma(idPrograma);

                List<maestroTipologiaEntities> _auxListMaestroTipologiaEntities = new List<maestroTipologiaEntities>();


                if (_listMaestroTipologiaDAO != null)
                {
                   
                    foreach (var item in _listMaestroTipologiaDAO)
                    {
                        
                            maestroTipologiaEntities _maestroTipologiaEntities = new maestroTipologiaEntities();

                            _maestroTipologiaEntities.idMaestroTipologia = item.IDMAESTROTIPOLOGIA;
                            _maestroTipologiaEntities.nombreMaestroTipologia = item.NOMBREMAESTROTIPOLOGIA;
                            //_maestroTipologiaEntities.estadoMaestroTipologia = Convert.ToBoolean(item.ESTADOMAESTROTIPOLOGIA);

                            _auxListMaestroTipologiaEntities.Add(_maestroTipologiaEntities);

                        

                    }
                    return _auxListMaestroTipologiaEntities;
                }
                else if (idPrograma != null && idTipologia == null)
                {

                    foreach (var item in _listMaestroTipologiaDAO)
                    {
                        maestroTipologiaEntities _maestroTipologiaEntities = new maestroTipologiaEntities();

                        _maestroTipologiaEntities.idMaestroTipologia = item.IDMAESTROTIPOLOGIA;
                        _maestroTipologiaEntities.nombreMaestroTipologia = item.NOMBREMAESTROTIPOLOGIA;
                        //_maestroTipologiaEntities.estadoMaestroTipologia = Convert.ToBoolean(item.ESTADOMAESTROTIPOLOGIA);

                        _auxListMaestroTipologiaEntities.Add(_maestroTipologiaEntities);
                    }

                    return _auxListMaestroTipologiaEntities;
                }

                return null;
            }
            else
            {

                List<maestroTipologiaEntities> _auxListMaestroTipologiaEntities = new List<maestroTipologiaEntities>();
                maestroTipologiaEntities _maestroTipologiaEntities = new maestroTipologiaEntities();
                _maestroTipologiaEntities.idMaestroTipologia = 0;
                _maestroTipologiaEntities.nombreMaestroTipologia = "-- Seleccione --";

                _auxListMaestroTipologiaEntities.Add(_maestroTipologiaEntities);

                return _auxListMaestroTipologiaEntities;

            }


        }

        internal static List<maestroTipologiaEntities> getListTipologia()
        {
            var _listMaestroTipologiaDAO = maestroTipologiaDAO.GetList();

            List<maestroTipologiaEntities> _auxListMaestroTipologiaEntities = new List<maestroTipologiaEntities>();


            if (_listMaestroTipologiaDAO != null)
            {
                foreach (var item in _listMaestroTipologiaDAO)
                {
                    maestroTipologiaEntities _maestroTipologiaEntities = new maestroTipologiaEntities();

                    _maestroTipologiaEntities.idMaestroTipologia = item.IDMAESTROTIPOLOGIA;
                    _maestroTipologiaEntities.nombreMaestroTipologia = item.NOMBREMAESTROTIPOLOGIA;
                    _maestroTipologiaEntities.estadoMaestroTipologia = Convert.ToBoolean(item.ESTADOMAESTROTIPOLOGIA);

                    _auxListMaestroTipologiaEntities.Add(_maestroTipologiaEntities);
                }
                return _auxListMaestroTipologiaEntities;
            }
            else
                return null;


        }

    }
}

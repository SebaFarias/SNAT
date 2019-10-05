using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using System.ComponentModel.DataAnnotations;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroServicioEntities
    {
        public long idMaestroServicio { get; set; }
        public int idMaestroPrograma { get; set; }
        [Display(Name = "Servicio:")]
        public string nombreMaestroServicio { get; set; }

        [Display(Name = "Monto servicio:")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public decimal montoServicio { get; set; }
        [Display(Name = "Monto AD:")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public decimal montoAD { get; set; }

        public bool estadoMaestroServicio { get; set; }

        public maestroServicioEntities()
        {
            idMaestroServicio = 0;
            idMaestroPrograma = 0;
            nombreMaestroServicio = string.Empty;
            estadoMaestroServicio = false;
        }

        public maestroServicioEntities(long _idMaestroServicio, int _idMaestroPrograma, string _nombreMaestroServicio, bool _estadoMaestroServicio)
        {
            idMaestroPrograma = _idMaestroPrograma;
            idMaestroServicio = _idMaestroServicio;
            nombreMaestroServicio = _nombreMaestroServicio;
            estadoMaestroServicio = _estadoMaestroServicio;
        }
    }


    public class maestroServicioEntitiesFactory
    {
        public static List<maestroServicioEntities> getListServicio()
        {
            var _listMaestroServicioDAO = maestroServicioDAO.GetList();

            List<maestroServicioEntities> _auxListMaestroTipologiaEntities = new List<maestroServicioEntities>();

            if (_listMaestroServicioDAO != null)
            {
                foreach (var item in _listMaestroServicioDAO)
                {
                    maestroServicioEntities _maestroServicioEntities = new maestroServicioEntities();

                    _maestroServicioEntities.idMaestroServicio = Convert.ToInt32(item.IDMAESTROSERVICIO);
                    _maestroServicioEntities.nombreMaestroServicio = item.NOMBREMAESTROSERVICIO;
                    _maestroServicioEntities.estadoMaestroServicio = Convert.ToBoolean(item.ESTADOMAESTROSERVICIO);

                    _auxListMaestroTipologiaEntities.Add(_maestroServicioEntities);
                }
                return _auxListMaestroTipologiaEntities;
            }
            else
                return null;
        }

        public static List<maestroServicioEntities> getListServicioTipologia(long? idTipologia)
        {
            if (idTipologia != null)
            {
                var _listMaestroServicioDAO = maestroServicioDAO.getListServicioTipologia(idTipologia);

                List<maestroServicioEntities> _auxListMaestroTipologiaEntities = new List<maestroServicioEntities>();

                if (_listMaestroServicioDAO != null)
                {
                    foreach (var item in _listMaestroServicioDAO)
                    {
                        maestroServicioEntities _maestroServicioEntities = new maestroServicioEntities();

                        _maestroServicioEntities.idMaestroServicio = Convert.ToInt64(item.IDMAESTROSERVICIO);
                        _maestroServicioEntities.nombreMaestroServicio = item.NOMBREABREVIADOMAESTROSERVICIO;
                        _maestroServicioEntities.estadoMaestroServicio = Convert.ToBoolean(item.ESTADOMAESTROSERVICIO);

                        _auxListMaestroTipologiaEntities.Add(_maestroServicioEntities);
                    }
                    return _auxListMaestroTipologiaEntities;
                }
                else
                    return null;
            }
            else
            {
                List<maestroServicioEntities> _auxListMaestroTipologiaEntities = new List<maestroServicioEntities>();

                return _auxListMaestroTipologiaEntities;
            }
        }

        public static List<maestroServicioEntities> getListServicioTipologiaPrograma(long? idTipologia, long? idPrograma)
        {
            if (idTipologia != null)
            {
                var _listMaestroServicioDAO = maestroServicioDAO.getListServicioTipologiaPrograma(idTipologia, idPrograma);

                List<maestroServicioEntities> _auxListMaestroTipologiaEntities = new List<maestroServicioEntities>();

                if (_listMaestroServicioDAO != null)
                {
                    foreach (var item in _listMaestroServicioDAO)
                    {
                        maestroServicioEntities _maestroServicioEntities = new maestroServicioEntities();

                        _maestroServicioEntities.idMaestroServicio = Convert.ToInt64(item.IDMAESTROSERVICIO);
                        _maestroServicioEntities.nombreMaestroServicio = item.NOMBREMAESTROSERVICIO;
                        _maestroServicioEntities.estadoMaestroServicio = Convert.ToBoolean(item.ESTADOMAESTROSERVICIO);

                        _auxListMaestroTipologiaEntities.Add(_maestroServicioEntities);
                    }
                    return _auxListMaestroTipologiaEntities;
                }
                else
                    return null;
            }
            else
            {
                List<maestroServicioEntities> _auxListMaestroTipologiaEntities = new List<maestroServicioEntities>();

                return _auxListMaestroTipologiaEntities;
            }
        }

        public static List<maestroServicioEntities> getMantenerListServicioTipologia(long? idTipologia, long? idServicio)
        {
            if (idTipologia != null && idServicio != null)
            {
                var _listMaestroServicioDAO = maestroServicioDAO.getListServicioTipologia(idTipologia);

                List<maestroServicioEntities> _auxListMaestroTipologiaEntities = new List<maestroServicioEntities>();

                if (_listMaestroServicioDAO != null)
                {
                    foreach (var item in _listMaestroServicioDAO)
                    {
                        maestroServicioEntities _maestroServicioEntities = new maestroServicioEntities();

                        _maestroServicioEntities.idMaestroServicio = Convert.ToInt64(item.IDMAESTROSERVICIO);
                        _maestroServicioEntities.nombreMaestroServicio = item.NOMBREABREVIADOMAESTROSERVICIO;
                        _maestroServicioEntities.estadoMaestroServicio = Convert.ToBoolean(item.ESTADOMAESTROSERVICIO);

                        _auxListMaestroTipologiaEntities.Add(_maestroServicioEntities);

                    }
                    return _auxListMaestroTipologiaEntities;
                }
                else
                    return null;
            }
            else
            {
                List<maestroServicioEntities> _auxListMaestroTipologiaEntities = new List<maestroServicioEntities>();
                maestroServicioEntities _maestroServicioEntities = new maestroServicioEntities();

                //_maestroServicioEntities.idMaestroServicio = 0;
                //_maestroServicioEntities.nombreMaestroServicio = "-- Seleccione --";
                //_maestroServicioEntities.estadoMaestroServicio = true;

                //_auxListMaestroTipologiaEntities.Add(_maestroServicioEntities);

                return _auxListMaestroTipologiaEntities;
            }
        }
    }
}
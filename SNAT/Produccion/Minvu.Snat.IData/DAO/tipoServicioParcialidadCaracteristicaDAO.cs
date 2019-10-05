using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;


namespace Minvu.Snat.IData.DAO
{
    public class tipoServicioParcialidadCaracteristicaDAO
    {
        public static int save(TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _tipoServicioParcialidadCaracteristica)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _inf = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();
                try
                {
                    _inf = contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.Where(c => c.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA == _tipoServicioParcialidadCaracteristica.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA).FirstOrDefault<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA>();

                    if (_inf == null)
                    {
                        _inf = _tipoServicioParcialidadCaracteristica;
                        contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.Add(_inf);
                    }
                    else
                    {
                        _inf.IDCARACTERISTICASESPECIALES = _tipoServicioParcialidadCaracteristica.IDCARACTERISTICASESPECIALES;
                        _inf.IDSERVICIOPARCIALIDAD = _tipoServicioParcialidadCaracteristica.IDSERVICIOPARCIALIDAD;
                        _inf.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.IDSOLICITUDPAGO = _tipoServicioParcialidadCaracteristica.IDSOLICITUDPAGO;
                        _inf.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    }

                    contexto.SaveChanges();
                    return (int)_inf.IDCARACTERISTICASESPECIALES;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }

        public static int updateIDSolicitudPago(long idTipoServicioParcialidadCaracteristica, long idSolicitudPago)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _inf = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();
                try
                {
                    _inf = contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.Where(c => c.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA == idTipoServicioParcialidadCaracteristica).FirstOrDefault<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA>();

                    if (_inf != null)
                    {
                        _inf.IDSOLICITUDPAGO = idSolicitudPago;

                    }

                    contexto.SaveChanges();
                    return (int)_inf.IDCARACTERISTICASESPECIALES;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }

        public static TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA Get(long IDTIPOSERVICIOPARCIALIDADCARACTERISTICA)
        {
            TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _inf = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();

            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qTipoServicioParcialidadCaracteristica = from a in contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA
                                                                 where a.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA == IDTIPOSERVICIOPARCIALIDADCARACTERISTICA
                                                                 select a;
                    foreach (var a in qTipoServicioParcialidadCaracteristica)
                    {
                        _inf.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                        _inf.IDSERVICIOPARCIALIDAD = a.IDSERVICIOPARCIALIDAD;
                        _inf.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
                    }

                    return _inf;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA> getList(long idCaracteristicaEspecial, long? idSolicitudPago)
        {
            List<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA> listaTipoServicioParcialidadCaracteristica = new List<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qTipoServicioParcialidadCaracteristica = from a in contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA
                                                             where a.IDCARACTERISTICASESPECIALES == idCaracteristicaEspecial
                                                             && a.IDSOLICITUDPAGO == idSolicitudPago
                                                             select a;
                foreach (var a in qTipoServicioParcialidadCaracteristica)
                {
                    TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _inf = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();

                    _inf.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    _inf.IDSERVICIOPARCIALIDAD = a.IDSERVICIOPARCIALIDAD;
                    _inf.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                    _inf.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    _inf.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    _inf.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    _inf.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    _inf.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
                    _inf.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA = a.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA;

                    listaTipoServicioParcialidadCaracteristica.Add(_inf);
                }

                return listaTipoServicioParcialidadCaracteristica;
            }
        }



        public static void Delete(TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _tipoServicioParcialidadCaracteristica)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA qTipoServicioParcialidadCaracteristica = (from c in contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA
                                                                                                   where c.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA == _tipoServicioParcialidadCaracteristica.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA
                                                                                                   select c).FirstOrDefault();

                contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.Remove(qTipoServicioParcialidadCaracteristica);
                contexto.SaveChanges();
            }
        }

        protected void ChangeStatus(TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _tipoServicioParcialidadCaracteristica)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA qCaracteristicasespeciales = (from c in contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA
                                                                                       where c.IDCARACTERISTICASESPECIALES == _tipoServicioParcialidadCaracteristica.IDCARACTERISTICASESPECIALES
                                                                                       select c).FirstOrDefault();

                //qCaracteristicasespeciales.= false;
                contexto.SaveChanges();
            }
        }

        public static List<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA> getUltimaPlantillaNull(long? idCaracteristicaEspecial)
        {
            List<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA> listaTipoServicioParcialidadCaracteristica = new List<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                //Busco si tiene una nueva plantilla creada
                var objPlantillaNew = from a in contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA
                                      where a.IDCARACTERISTICASESPECIALES == idCaracteristicaEspecial
                                      && a.IDSOLICITUDPAGO == null
                                      select a;

                foreach (var a in objPlantillaNew)
                {
                    TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _inf = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();

                    _inf.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
                    _inf.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                    _inf.IDSERVICIOPARCIALIDAD = a.IDSERVICIOPARCIALIDAD;
                    _inf.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    _inf.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    _inf.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    _inf.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA = a.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    _inf.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA = a.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA;

                    listaTipoServicioParcialidadCaracteristica.Add(_inf);
                }

                //Si tiene no contiene plantilla nueva tomo la primera seteo valores nulos y la ingreso
                if (listaTipoServicioParcialidadCaracteristica.Count == 0)
                {
                    //Se buscan las solicitudes existentes para el proyecto segun idcaracteriscicaespecial
                    long? numSol = 0;
                    var numsSol = (from sop in contexto.SOLICITUD_PAGO
                                   where sop.IDCARACTERISTICASESPECIALES == idCaracteristicaEspecial
                                   && sop.FECHAREALCREACIONSOLICITUDPAGO != null
                                   orderby sop.FECHAREALCREACIONSOLICITUDPAGO descending
                                   select sop.IDSOLICITUDPAGO);
                    foreach (var item in numsSol)//Toma el primer IDSolicitud
                    {
                        numSol = (Int64?)item;
                        break;
                    }
                    //Se buscan datos de plantillas existentes para el IDSolicitud
                    var objPlantillaHist = from tspc in contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA
                                           where tspc.IDCARACTERISTICASESPECIALES == idCaracteristicaEspecial
                                           && tspc.IDSOLICITUDPAGO == numSol
                                           select new
                                           {
                                               tspc.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA,
                                               tspc.IDSERVICIOPARCIALIDAD,
                                               tspc.IDCARACTERISTICASESPECIALES,
                                               tspc.IDSOLICITUDPAGO,
                                               tspc.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA,
                                               tspc.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA
                                           };

                    //Si hay registros de plantillas con IDSolicitud grabado es porque puede tener historial
                    foreach (var item in objPlantillaHist)
                    {
                        TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _inf = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();

                        _inf.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA = 0;
                        _inf.IDSERVICIOPARCIALIDAD = item.IDSERVICIOPARCIALIDAD;
                        _inf.IDCARACTERISTICASESPECIALES = item.IDCARACTERISTICASESPECIALES;
                        _inf.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA = true;
                        _inf.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA = item.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA = item.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = null;
                        _inf.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = null;
                        _inf.IDSOLICITUDPAGO = null;

                        save(_inf);//Grabo el dato nuevo
                        listaTipoServicioParcialidadCaracteristica.Add(_inf);
                    }

                    //Si se encontro plantilla historial
                    if (listaTipoServicioParcialidadCaracteristica.Count > 0)
                    {
                        //Incrementos -- Se ingresan los incrementos
                        var objTipoIncremento = tipoIncrementoDAO.GetList((Int64)idCaracteristicaEspecial, null);

                        if (objTipoIncremento.Count == 0)
                        {
                            objTipoIncremento = tipoIncrementoDAO.GetList((Int64)idCaracteristicaEspecial, numSol);

                            foreach (var incremento in objTipoIncremento)
                            {
                                TIPO_INCREMENTO _tipoIncremento = new TIPO_INCREMENTO();

                                _tipoIncremento.IDTIPOINCREMENTO = 0;
                                _tipoIncremento.IDMAESTROINCREMENTO = incremento.IDMAESTROINCREMENTO;
                                _tipoIncremento.IDCARACTERISTICASESPECIALES = incremento.IDCARACTERISTICASESPECIALES;
                                _tipoIncremento.SELECCIONADOTIPOINCREMENTO = false;
                                _tipoIncremento.IDSOLICITUDPAGO = null;
                                _tipoIncremento.ESTADOTIPOINCREMENTO = true;

                                tipoIncrementoDAO.Save(_tipoIncremento);
                            }
                        }
                    }
                }
                else
                {
                    //Se buscan las solicitudes existentes para el proyecto segun idcaracteriscicaespecial
                    long? numSol = 0;
                    var numsSol = (from sop in contexto.SOLICITUD_PAGO
                                   where sop.IDCARACTERISTICASESPECIALES == idCaracteristicaEspecial
                                   && sop.FECHAREALCREACIONSOLICITUDPAGO != null
                                   orderby sop.FECHAREALCREACIONSOLICITUDPAGO descending
                                   select sop.IDSOLICITUDPAGO);
                    foreach (var item in numsSol)//Toma el primer IDSolicitud
                    {
                        numSol = (Int64?)item;
                        break;
                    }

                    if (numSol != 0)
                    {
                        List<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA> ListaHist = new List<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA>();

                        //Se buscan datos de plantillas existentes para el IDSolicitud
                        var objPlantillaHist = from tspc in contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA
                                               where tspc.IDCARACTERISTICASESPECIALES == idCaracteristicaEspecial
                                               && tspc.IDSOLICITUDPAGO == numSol
                                               select new
                                               {
                                                   tspc.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA,
                                                   tspc.IDSERVICIOPARCIALIDAD,
                                                   tspc.IDCARACTERISTICASESPECIALES,
                                                   tspc.IDSOLICITUDPAGO,
                                                   tspc.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA,
                                                   tspc.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA
                                               };

                        foreach (var item in objPlantillaHist)
                        {
                            TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _inf = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();

                            _inf.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA = 0;
                            _inf.IDSERVICIOPARCIALIDAD = item.IDSERVICIOPARCIALIDAD;
                            _inf.IDCARACTERISTICASESPECIALES = item.IDCARACTERISTICASESPECIALES;
                            _inf.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA = true;
                            _inf.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA = item.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                            _inf.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA = item.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA;
                            _inf.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = null;
                            _inf.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = null;
                            _inf.IDSOLICITUDPAGO = null;

                            ListaHist.Add(_inf);
                        }

                        for (int i = 0; i < listaTipoServicioParcialidadCaracteristica.Count; i++)
                        {
                            listaTipoServicioParcialidadCaracteristica[i].MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA = ListaHist[i].MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        }
                    }
                }

                return listaTipoServicioParcialidadCaracteristica;
            }
        }

        public static void Update(TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _tipoServicioParcialidadCaracteristica)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA qTipoServicioParcialidadCaracteristica = (from c in contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA
                                                                                                   where c.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA == _tipoServicioParcialidadCaracteristica.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA
                                                                                                   select c).FirstOrDefault();

                qTipoServicioParcialidadCaracteristica.IDSERVICIOPARCIALIDAD = _tipoServicioParcialidadCaracteristica.IDSERVICIOPARCIALIDAD;
                qTipoServicioParcialidadCaracteristica.IDCARACTERISTICASESPECIALES = _tipoServicioParcialidadCaracteristica.IDCARACTERISTICASESPECIALES;
                qTipoServicioParcialidadCaracteristica.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                qTipoServicioParcialidadCaracteristica.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                qTipoServicioParcialidadCaracteristica.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                qTipoServicioParcialidadCaracteristica.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                qTipoServicioParcialidadCaracteristica.IDSOLICITUDPAGO = _tipoServicioParcialidadCaracteristica.IDSOLICITUDPAGO;
                qTipoServicioParcialidadCaracteristica.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA;

                contexto.SaveChanges();
            }
        }
    }
}



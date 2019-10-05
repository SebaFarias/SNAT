using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
    public class autorizacionDAO
    {
        public static int Save(AUTORIZACION _autorizacion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                AUTORIZACION _aut = new AUTORIZACION();
                try
                {
                    _aut = contexto.AUTORIZACION.Where(c => c.IDAUTORIZACION == _autorizacion.IDAUTORIZACION).FirstOrDefault<AUTORIZACION>();

                    if (_aut == null)
                    {
                        _aut = _autorizacion;
                        contexto.AUTORIZACION.Add(_aut);
                    }
                    else
                    {
                        _aut.IDMAESTROESTADOAUTORIZACION = _autorizacion.IDMAESTROESTADOAUTORIZACION;
                        _aut.ESPECIALAUTORIZACION = _autorizacion.ESPECIALAUTORIZACION;
                        _aut.CANTIDADPROYECTOSAUTORIZACION = _autorizacion.CANTIDADPROYECTOSAUTORIZACION;
                        _aut.CANTIDADSOLICITUDPAGOAUTORIZACION = _autorizacion.CANTIDADSOLICITUDPAGOAUTORIZACION;
                        _aut.MONTOTOTALAUTORIZACION = _autorizacion.MONTOTOTALAUTORIZACION;
                        _aut.USUARIORESPONSABLEAUTORIZACION = _autorizacion.USUARIORESPONSABLEAUTORIZACION;
                        _aut.FECHAINGRESOAUTORIZACION = _autorizacion.FECHAINGRESOAUTORIZACION;
                        _aut.CODIGOREGIONAUTORIZACION = _autorizacion.CODIGOREGIONAUTORIZACION;
                        _aut.NUMEROAUTORIZACION = _autorizacion.NUMEROAUTORIZACION;
                        _aut.IDMAESTROMODALIDAD = _autorizacion.IDMAESTROMODALIDAD;
                        _aut.IDMAESTROPROGRAMA = _autorizacion.IDMAESTROPROGRAMA;
                        _aut.IDMAESTROTIPOLOGIA = _autorizacion.IDMAESTROTIPOLOGIA;
                        _aut.IDMAESTROTITULO = _autorizacion.IDMAESTROTITULO;
                        _aut.IDPROVEEDOR = _autorizacion.IDPROVEEDOR;
                    }

                    contexto.SaveChanges();
                    return (int)_aut.IDAUTORIZACION;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }

        public static AUTORIZACION Get(long? idAutorizacion)
        {
            AUTORIZACION _aut = new AUTORIZACION();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qAutorizacion = from a in contexto.AUTORIZACION
                                    where a.IDAUTORIZACION == idAutorizacion
                                    select a;
                //foreach (var a in qAutorizacion)
                //{
                //    _aut.IDAUTORIZACION = a.IDAUTORIZACION;
                //    _aut.IDMAESTROESTADOAUTORIZACION = a.IDMAESTROESTADOAUTORIZACION;
                //    _aut.ESPECIALAUTORIZACION = a.ESPECIALAUTORIZACION;
                //    _aut.CANTIDADPROYECTOSAUTORIZACION = a.CANTIDADPROYECTOSAUTORIZACION;
                //    _aut.CANTIDADSOLICITUDPAGOAUTORIZACION = a.CANTIDADSOLICITUDPAGOAUTORIZACION;
                //    _aut.MONTOTOTALAUTORIZACION = a.MONTOTOTALAUTORIZACION;
                //    _aut.USUARIORESPONSABLEAUTORIZACION = a.USUARIORESPONSABLEAUTORIZACION;
                //    _aut.FECHAINGRESOAUTORIZACION = a.FECHAINGRESOAUTORIZACION;
                //    _aut.CODIGOREGIONAUTORIZACION = a.CODIGOREGIONAUTORIZACION;
                //    _aut.NUMEROAUTORIZACION = a.NUMEROAUTORIZACION;
                //    _aut.IDMAESTROMODALIDAD = a.IDMAESTROMODALIDAD;
                //    _aut.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                //    _aut.IDMAESTROTIPOLOGIA = a.IDMAESTROTIPOLOGIA;
                //    _aut.IDMAESTROTITULO = a.IDMAESTROTITULO;
                //    _aut.IDPROVEEDOR = a.IDPROVEEDOR;
                //}
                
                return qAutorizacion.FirstOrDefault<AUTORIZACION>();
            }
        }

        public static List<AUTORIZACION> GetList()
        {
            List<AUTORIZACION> ListaAutorizacion = new List<AUTORIZACION>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qAutorizacion = from a in contexto.AUTORIZACION
                                    select a;
                foreach (var a in qAutorizacion)
                {
                    AUTORIZACION auxAutorizacion = new AUTORIZACION();

                    auxAutorizacion.IDAUTORIZACION = a.IDAUTORIZACION;
                    auxAutorizacion.CANTIDADPROYECTOSAUTORIZACION = a.CANTIDADPROYECTOSAUTORIZACION;
                    auxAutorizacion.CANTIDADSOLICITUDPAGOAUTORIZACION = a.CANTIDADSOLICITUDPAGOAUTORIZACION;
                    auxAutorizacion.ESPECIALAUTORIZACION = a.ESPECIALAUTORIZACION;
                    auxAutorizacion.IDMAESTROESTADOAUTORIZACION = a.IDMAESTROESTADOAUTORIZACION;
                    auxAutorizacion.MAESTRO_ESTADO_AUTORIZACION = a.MAESTRO_ESTADO_AUTORIZACION;
                    auxAutorizacion.MONTOTOTALAUTORIZACION = a.MONTOTOTALAUTORIZACION;
                    auxAutorizacion.TIPO_AUTORIZACION = a.TIPO_AUTORIZACION;
                    auxAutorizacion.USUARIORESPONSABLEAUTORIZACION = a.USUARIORESPONSABLEAUTORIZACION;

                    ListaAutorizacion.Add(auxAutorizacion);
                }

                return ListaAutorizacion;
            }
        }

        protected void Delete(AUTORIZACION _autorizacion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                AUTORIZACION qAutorizacion = (from c in contexto.AUTORIZACION
                                              where c.IDAUTORIZACION == _autorizacion.IDAUTORIZACION
                                              select c).FirstOrDefault();

                contexto.AUTORIZACION.Remove(qAutorizacion);
                contexto.SaveChanges();
            }
        }

        protected void ChangeStatus(AUTORIZACION _autorizacion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                AUTORIZACION qAutorizacion = (from c in contexto.AUTORIZACION
                                              where c.IDAUTORIZACION == _autorizacion.IDAUTORIZACION
                                              select c).FirstOrDefault();

                qAutorizacion.IDMAESTROESTADOAUTORIZACION = 0;

                contexto.SaveChanges();
            }
        }
    }
}
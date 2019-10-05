 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
    public class caracteristicasEspecialesDAO
    {
        public static int Save(CARACTERISTICAS_ESPECIALES _caracteristicasEspeciales)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                CARACTERISTICAS_ESPECIALES _inf = new CARACTERISTICAS_ESPECIALES();
                try
                {
                    _inf = contexto.CARACTERISTICAS_ESPECIALES.Where(c => c.IDCARACTERISTICASESPECIALES == _caracteristicasEspeciales.IDCARACTERISTICASESPECIALES).FirstOrDefault<CARACTERISTICAS_ESPECIALES>();

                    if (_inf == null)
                    {
                        _inf = _caracteristicasEspeciales;
                        contexto.CARACTERISTICAS_ESPECIALES.Add(_inf);
                    }
                    else
                    {
                        _inf.IDCARACTERISTICASESPECIALES = _caracteristicasEspeciales.IDCARACTERISTICASESPECIALES;
                        _inf.IDINFORMACIONPROYECTO = _caracteristicasEspeciales.IDINFORMACIONPROYECTO;
                        _inf.IDMAESTROMODALIDAD = _caracteristicasEspeciales.IDMAESTROMODALIDAD;
                        _inf.IDMAESTROPROGRAMA = _caracteristicasEspeciales.IDMAESTROPROGRAMA;
                        _inf.IDMAESTROTIPOLOGIA = _caracteristicasEspeciales.IDMAESTROTIPOLOGIA;
                        //_inf.TITULOCARACTERISTICASESPECIALES = _caracteristicasEspeciales.TITULOCARACTERISTICASESPECIALES;
                        _inf.IDMAESTROCLASE = _caracteristicasEspeciales.IDMAESTROCLASE;
                        _inf.IDMAESTROSUBMODALIDAD = _caracteristicasEspeciales.IDMAESTROSUBMODALIDAD;
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


        public static CARACTERISTICAS_ESPECIALES getCaracEspecial(long idCaracEspecial)
        {
            CARACTERISTICAS_ESPECIALES _inf = new CARACTERISTICAS_ESPECIALES();

            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qCaracteristicasEspeciales = from a in contexto.CARACTERISTICAS_ESPECIALES
                                                     where a.IDCARACTERISTICASESPECIALES == idCaracEspecial
                                                     select a;
                    foreach (var a in qCaracteristicasEspeciales)
                    {
                        _inf.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                        _inf.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                        _inf.IDMAESTROMODALIDAD = a.IDMAESTROMODALIDAD;
                        _inf.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                        _inf.IDMAESTROTIPOLOGIA = a.IDMAESTROTIPOLOGIA;
                        //_inf.TITULOCARACTERISTICASESPECIALES = a.TITULOCARACTERISTICASESPECIALES;
                        _inf.IDMAESTROCLASE = a.IDMAESTROCLASE;
                        _inf.IDMAESTROSUBMODALIDAD = a.IDMAESTROSUBMODALIDAD;
                    }

                    return _inf;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CARACTERISTICAS_ESPECIALES getCaracEspecialesIdInformacionProyecto(long idProyecto)
        {
            CARACTERISTICAS_ESPECIALES _inf = new CARACTERISTICAS_ESPECIALES();

            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qCaracteristicasEspeciales = from a in contexto.CARACTERISTICAS_ESPECIALES
                                                     where a.IDINFORMACIONPROYECTO == idProyecto
                                                     select a;
                    foreach (var a in qCaracteristicasEspeciales)
                    {
                        _inf.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                        _inf.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                        _inf.IDMAESTROMODALIDAD = a.IDMAESTROMODALIDAD;
                        _inf.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                        _inf.IDMAESTROTIPOLOGIA = a.IDMAESTROTIPOLOGIA;
                        _inf.IDMAESTROSUBMODALIDAD = a.IDMAESTROSUBMODALIDAD;
                        // _inf.TITULOCARACTERISTICASESPECIALES = a.TITULOCARACTERISTICASESPECIALES;
                        _inf.IDMAESTROCLASE = a.IDMAESTROCLASE;
                    }

                    return _inf;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static CARACTERISTICAS_ESPECIALES getIdCaracterisacion(long idCaracterizacion)
        {

            CARACTERISTICAS_ESPECIALES _inf = new CARACTERISTICAS_ESPECIALES();

            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qCaracteristicasEspeciales = from a in contexto.CARACTERISTICAS_ESPECIALES
                                                     where a.IDCARACTERISTICASESPECIALES == idCaracterizacion
                                                     select a;
                    foreach (var a in qCaracteristicasEspeciales)
                    {

                        _inf.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                        _inf.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                        _inf.IDMAESTROMODALIDAD = a.IDMAESTROMODALIDAD;
                        _inf.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                        _inf.IDMAESTROTIPOLOGIA = a.IDMAESTROTIPOLOGIA;
                        //_inf.TITULOCARACTERISTICASESPECIALES = a.TITULOCARACTERISTICASESPECIALES;
                        _inf.IDMAESTROCLASE = a.IDMAESTROCLASE;
                        _inf.IDMAESTROSUBMODALIDAD = a.IDMAESTROSUBMODALIDAD;

                    }

                    return _inf;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static List<CARACTERISTICAS_ESPECIALES> GetList()
        {

            List<CARACTERISTICAS_ESPECIALES> ListaCaracteristicasEspeciales = new List<CARACTERISTICAS_ESPECIALES>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qCaracteristicasEspeciales = from a in contexto.CARACTERISTICAS_ESPECIALES
                                           select a;
                foreach (var a in qCaracteristicasEspeciales)
                {

                    CARACTERISTICAS_ESPECIALES _inf = new CARACTERISTICAS_ESPECIALES();


                    _inf.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                    _inf.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                    _inf.IDMAESTROMODALIDAD = a.IDMAESTROMODALIDAD;
                    _inf.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                    _inf.IDMAESTROTIPOLOGIA = a.IDMAESTROTIPOLOGIA;
                    //_inf.TITULOCARACTERISTICASESPECIALES = a.TITULOCARACTERISTICASESPECIALES;
                    _inf.IDMAESTROCLASE = a.IDMAESTROCLASE;
                    _inf.IDMAESTROSUBMODALIDAD = a.IDMAESTROSUBMODALIDAD;

                    ListaCaracteristicasEspeciales.Add(_inf);


                }

                return ListaCaracteristicasEspeciales;
            }

        }


        protected void Delete(CARACTERISTICAS_ESPECIALES _caracteristicasEspeciales)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                CARACTERISTICAS_ESPECIALES qCaracteristicasEspeciales = (from c in contexto.CARACTERISTICAS_ESPECIALES
                                                                   where c.IDCARACTERISTICASESPECIALES  == _caracteristicasEspeciales.IDCARACTERISTICASESPECIALES
                                                   select c).FirstOrDefault();

                contexto.CARACTERISTICAS_ESPECIALES.Remove(qCaracteristicasEspeciales);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(CARACTERISTICAS_ESPECIALES _informacionProyecto)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                CARACTERISTICAS_ESPECIALES qCaracteristicasespeciales = (from c in contexto.CARACTERISTICAS_ESPECIALES
                                                                   where c.IDCARACTERISTICASESPECIALES == _informacionProyecto.IDCARACTERISTICASESPECIALES
                                                                         select c).FirstOrDefault();


                //qCaracteristicasespeciales.= false;

                contexto.SaveChanges();
            }


        }

    }
}



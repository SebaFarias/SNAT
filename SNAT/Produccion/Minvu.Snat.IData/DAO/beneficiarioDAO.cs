using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
   public class beneficiarioDAO
    {
        public static int Save(BENEFICIARIO _beneficiario)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                BENEFICIARIO _ben = new BENEFICIARIO();
                try
                {
                    _ben = contexto.BENEFICIARIO.Where(c => c.IDBENEFICIARIO == _beneficiario.IDBENEFICIARIO).FirstOrDefault<BENEFICIARIO>();

                    if (_ben == null)
                    {
                        _ben = _beneficiario;
                        contexto.BENEFICIARIO.Add(_ben);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _ben.APELLIDOMATERNOBENEFICIARIO = _beneficiario.APELLIDOMATERNOBENEFICIARIO;
                        _ben.APELLIDOPATERNOBENEFICIARIO = _beneficiario.APELLIDOPATERNOBENEFICIARIO;
                        _ben.DIGITOVERIFICADORBENEFICIARIO = _beneficiario.DIGITOVERIFICADORBENEFICIARIO;
                        _ben.DIRECCION = _beneficiario.DIRECCION;
                        _ben.ESTADOBENEFICIARIO = _beneficiario.ESTADOBENEFICIARIO;
                        _ben.IDDIRECCION = _beneficiario.IDDIRECCION;
                        _ben.IDINFORMACIONPROYECTO = _beneficiario.IDINFORMACIONPROYECTO;
                        _ben.INFORMACION_PROYECTO = _beneficiario.INFORMACION_PROYECTO;
                        _ben.NOMBREBENEFICIARIO = _beneficiario.NOMBREBENEFICIARIO;
                        _ben.RUTBENEFICIARIO = _beneficiario.RUTBENEFICIARIO;
                        
                    }

                    contexto.SaveChanges();
                    return (int)_ben.IDBENEFICIARIO;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static BENEFICIARIO Get(int idBeneficiario)
        {

            BENEFICIARIO _ben = new BENEFICIARIO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qBeneficiario = from a in contexto.BENEFICIARIO
                                    where a.IDBENEFICIARIO == idBeneficiario
                                    select a;
                foreach (var a in qBeneficiario)
                {

                    _ben.IDBENEFICIARIO = a.IDBENEFICIARIO;
                    _ben.APELLIDOMATERNOBENEFICIARIO = a.APELLIDOMATERNOBENEFICIARIO;
                    _ben.APELLIDOPATERNOBENEFICIARIO = a.APELLIDOPATERNOBENEFICIARIO;
                    _ben.DIGITOVERIFICADORBENEFICIARIO = a.DIGITOVERIFICADORBENEFICIARIO;
                    _ben.DIRECCION = a.DIRECCION;
                    _ben.ESTADOBENEFICIARIO = a.ESTADOBENEFICIARIO;
                    _ben.IDDIRECCION = a.IDDIRECCION;
                    _ben.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                    _ben.INFORMACION_PROYECTO = a.INFORMACION_PROYECTO;
                    _ben.NOMBREBENEFICIARIO = a.NOMBREBENEFICIARIO;
                    _ben.RUTBENEFICIARIO = a.RUTBENEFICIARIO;

                }

                return _ben;
            }

        }

        public static List<BENEFICIARIO> GetListProyect(int idProyecto)
        {

            List<BENEFICIARIO> ListaBeneficiario = new List<BENEFICIARIO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qBeneficiario = from a in contexto.BENEFICIARIO
                                    where a.IDINFORMACIONPROYECTO == idProyecto
                                    select a  ;
                foreach (var a in qBeneficiario)
                {

                    BENEFICIARIO auxBeneficiario = new BENEFICIARIO();


                    auxBeneficiario.IDBENEFICIARIO = a.IDBENEFICIARIO;
                    auxBeneficiario.APELLIDOMATERNOBENEFICIARIO = a.APELLIDOMATERNOBENEFICIARIO;
                    auxBeneficiario.APELLIDOPATERNOBENEFICIARIO = a.APELLIDOPATERNOBENEFICIARIO;
                    auxBeneficiario.DIGITOVERIFICADORBENEFICIARIO = a.DIGITOVERIFICADORBENEFICIARIO;
                    auxBeneficiario.DIRECCION = a.DIRECCION;
                    auxBeneficiario.ESTADOBENEFICIARIO = a.ESTADOBENEFICIARIO;
                    auxBeneficiario.IDDIRECCION = a.IDDIRECCION;
                    auxBeneficiario.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                    auxBeneficiario.INFORMACION_PROYECTO = a.INFORMACION_PROYECTO;
                    auxBeneficiario.NOMBREBENEFICIARIO = a.NOMBREBENEFICIARIO;
                    auxBeneficiario.RUTBENEFICIARIO = a.RUTBENEFICIARIO;


                    ListaBeneficiario.Add(auxBeneficiario);


                }

                return ListaBeneficiario;
            }

        }
        public static List<BENEFICIARIO> GetList()
        {

            List<BENEFICIARIO> ListaBeneficiario = new List<BENEFICIARIO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qBeneficiario = from a in contexto.BENEFICIARIO
                                    select a;
                foreach (var a in qBeneficiario)
                {

                    BENEFICIARIO auxBeneficiario = new BENEFICIARIO();


                    auxBeneficiario.IDBENEFICIARIO = a.IDBENEFICIARIO;
                    auxBeneficiario.APELLIDOMATERNOBENEFICIARIO= a.APELLIDOMATERNOBENEFICIARIO;
                    auxBeneficiario.APELLIDOPATERNOBENEFICIARIO = a.APELLIDOPATERNOBENEFICIARIO;
                    auxBeneficiario.DIGITOVERIFICADORBENEFICIARIO = a.DIGITOVERIFICADORBENEFICIARIO;
                    auxBeneficiario.DIRECCION = a.DIRECCION;
                    auxBeneficiario.ESTADOBENEFICIARIO = a.ESTADOBENEFICIARIO;
                    auxBeneficiario.IDDIRECCION = a.IDDIRECCION;
                    auxBeneficiario.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                    auxBeneficiario.INFORMACION_PROYECTO = a.INFORMACION_PROYECTO;
                    auxBeneficiario.NOMBREBENEFICIARIO = a.NOMBREBENEFICIARIO;
                    auxBeneficiario.RUTBENEFICIARIO = a.RUTBENEFICIARIO;
                    

                    ListaBeneficiario.Add(auxBeneficiario);


                }

                return ListaBeneficiario;
            }

        }


        protected void Delete(BENEFICIARIO _autorizacion)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                BENEFICIARIO qBeneficiario = (from c in contexto.BENEFICIARIO
                                              where c.IDBENEFICIARIO == _autorizacion.IDBENEFICIARIO
                                              select c).FirstOrDefault();

                contexto.BENEFICIARIO.Remove(qBeneficiario);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(BENEFICIARIO _beneficiario)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                BENEFICIARIO qBeneficiario = (from c in contexto.BENEFICIARIO
                                              where c.IDBENEFICIARIO == _beneficiario.IDBENEFICIARIO
                                              select c).FirstOrDefault();


                qBeneficiario.ESTADOBENEFICIARIO = false;

                contexto.SaveChanges();
            }


        }
    }
}

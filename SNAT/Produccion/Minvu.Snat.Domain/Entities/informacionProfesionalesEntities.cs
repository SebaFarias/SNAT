using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.SI;
using Minvu.Snat.IData.ServicioInformacionPersona;

namespace Minvu.Snat.Domain.Entities
{
    public class informacionProfesionalesEntities
    {
        [Required(ErrorMessage = "El rut es obligatorio")]
        public string rutProfesional { get; set; }

        [Required(ErrorMessage = "El dv es obligatorio")]
        public string dvProfesional { get; set; }

        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreProfesional { get; set; }

        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string apellidoPaternoProfesional { get; set; }

        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string apellidoMaternoProfesional { get; set; }
        public int estadoProfesional { get; set; }

        [Required(ErrorMessage = "ProfesionalFTO es obligatorio")]
        public bool profesionalITO { get; set; }
        public int idProfesion { get; set; }
        public string nombreProfesion { get; set; }
        public string mensajeSalida { get; set; }
        public string codigoSalida { get; set; }
        public bool menorEdad { get; set; }
        public bool fallecido { get; set; }

        public informacionProfesionalesEntities()
        {
            rutProfesional =string.Empty;
            dvProfesional = string.Empty;
            nombreProfesional = string.Empty;
            apellidoPaternoProfesional = string.Empty;
            apellidoMaternoProfesional = string.Empty;
            estadoProfesional = 0;
            profesionalITO = false;
            idProfesion = 0;
            nombreProfesion = string.Empty;
            mensajeSalida = string.Empty;
            codigoSalida = string.Empty;
            menorEdad = false;
            fallecido = false;
        }
        public informacionProfesionalesEntities(string _rutProfesional, string _dvProfesional, string _nombreProfesional, string _apellidoPaternoProfesional,
            string _apellidoMaternoProfesional, int _estadoProfesional, bool _profesionalITO)
        {
            rutProfesional = _rutProfesional;
            dvProfesional = _dvProfesional;
            nombreProfesional = _nombreProfesional;
            apellidoPaternoProfesional = _apellidoPaternoProfesional;
            apellidoMaternoProfesional = _apellidoMaternoProfesional;
            estadoProfesional = _estadoProfesional;
            profesionalITO = _profesionalITO;
        }
    }

    public class informacionProfesionalesEntitiesFactory
    {

        internal static informacionProfesionalesEntities getProfesional(int rutProfesional, char dvProfesional)
        {
            var _informacionProfesionalesDAO = webAsistecSnatiiiPPPFProyectoDAO.GetProfesional(rutProfesional);
            if (_informacionProfesionalesDAO.PROF_RUT != 0)
            {
                return new informacionProfesionalesEntities
                {
                    rutProfesional = _informacionProfesionalesDAO.PROF_RUT.ToString(),
                    dvProfesional = _informacionProfesionalesDAO.PROF_DGV,
                    nombreProfesional = _informacionProfesionalesDAO.PROF_NOM,
                    apellidoPaternoProfesional = _informacionProfesionalesDAO.PROF_APP_PAT,
                    apellidoMaternoProfesional = _informacionProfesionalesDAO.PROF_APP_MAT,
                    estadoProfesional = _informacionProfesionalesDAO.PROF_EST,
                    profesionalITO = Convert.Equals(1, _informacionProfesionalesDAO.PROF_ITO),
                    idProfesion = _informacionProfesionalesDAO.PROF_PROF_ID
                };
            }
            else
            {
                dynamic infoPersona = ServicioInformacionPersona.InformacionPorRut(rutProfesional, dvProfesional);

                if (infoPersona == null)
                {
                    return new informacionProfesionalesEntities
                    {
                        mensajeSalida = "Error en conexion remota a Registro Civil, intente mas tarde.",
                        codigoSalida = "-4",
                    };
                }
                else
                {
                    if (infoPersona.GetType().GetProperty("Estado").GetValue(infoPersona, null) == 1)
                    {
                        return new informacionProfesionalesEntities
                        {
                            rutProfesional = Convert.ToString(infoPersona.GetType().GetProperty("Rut").GetValue(infoPersona, null)),
                            dvProfesional = Convert.ToString(infoPersona.GetType().GetProperty("dv").GetValue(infoPersona, null)),
                            nombreProfesional = infoPersona.GetType().GetProperty("Nombres").GetValue(infoPersona, null),
                            apellidoPaternoProfesional = infoPersona.GetType().GetProperty("ApellidoPaterno").GetValue(infoPersona, null),
                            apellidoMaternoProfesional = infoPersona.GetType().GetProperty("ApellidoMaterno").GetValue(infoPersona, null),
                            //menorEdad = infoPersona.GetType().GetProperty("MenorEdad").GetValue(infoPersona, null),
                            //fallecido = infoPersona.GetType().GetProperty("Fallecido").GetValue(infoPersona, null),
                        };
                    }
                    else
                    {
                        return new informacionProfesionalesEntities
                        {
                            mensajeSalida = infoPersona.GetType().GetProperty("Nombre").GetValue(infoPersona, null),
                            codigoSalida = "-3",
                        };
                    }
                }
            }
        }

        internal static informacionProfesionalesEntities getProveedorBd (int rutProfesional)
        { 

            var _informacionProveedorDAO = proveedorDAO.GetProveedorByRut(rutProfesional);
            if (_informacionProveedorDAO.IDPROVEEDOR != 0)
            {
                return new informacionProfesionalesEntities
                {
                    rutProfesional = _informacionProveedorDAO.RUTPROVEEDOR.ToString(),
                    dvProfesional = _informacionProveedorDAO.DVPROVEDIGITOVERIFICADORPROVEEDOR,
                    nombreProfesional = _informacionProveedorDAO.NOMBREPROVEEDOR,
                    //apellidoPaternoProfesional = _informacionProfesionalesDAO.PROF_APP_PAT,
                    //apellidoMaternoProfesional = _informacionProfesionalesDAO.PROF_APP_MAT,
                    //estadoProfesional = _informacionProfesionalesDAO.PROF_EST,
                    //profesionalITO = Convert.Equals(1, _informacionProfesionalesDAO.PROF_ITO),
                    //idProfesion = _informacionProfesionalesDAO.PROF_PROF_ID
                };
            }
            else
            {
                return null;
            }
        }
        internal static informacionProfesionalesEntities getProveedor(int rutProfesional, char dvProfesional)
        {
            //var _informacionProfesionalesDAO = webAsistecSnatiiiPPPFProyectoDAO.GetProfesional(rutProfesional);
            var _informacionProveedorDAO = proveedorDAO.GetProveedorByRut(rutProfesional);
            if (_informacionProveedorDAO.IDPROVEEDOR != 0)
            {
                return new informacionProfesionalesEntities
                {
                    rutProfesional = _informacionProveedorDAO.RUTPROVEEDOR.ToString(),
                    dvProfesional = _informacionProveedorDAO.DVPROVEDIGITOVERIFICADORPROVEEDOR,
                    nombreProfesional = _informacionProveedorDAO.NOMBREPROVEEDOR,
                    //apellidoPaternoProfesional = _informacionProfesionalesDAO.PROF_APP_PAT,
                    //apellidoMaternoProfesional = _informacionProfesionalesDAO.PROF_APP_MAT,
                    //estadoProfesional = _informacionProfesionalesDAO.PROF_EST,
                    //profesionalITO = Convert.Equals(1, _informacionProfesionalesDAO.PROF_ITO),
                    //idProfesion = _informacionProfesionalesDAO.PROF_PROF_ID
                };
            }
            else
            {
                dynamic infoPersona = ServicioInformacionPersona.InformacionPorRut(rutProfesional, dvProfesional);

                if (infoPersona == null)
                {
                    return new informacionProfesionalesEntities
                    {
                        mensajeSalida = "Error en conexion remota a Registro Civil, intente mas tarde.",
                        codigoSalida = "-4",
                    };
                }
                else
                {
                    if (infoPersona.GetType().GetProperty("Estado").GetValue(infoPersona, null) == 1)
                    {
                        return new informacionProfesionalesEntities
                        {
                            rutProfesional = Convert.ToString(infoPersona.GetType().GetProperty("Rut").GetValue(infoPersona, null)),
                            dvProfesional = Convert.ToString(infoPersona.GetType().GetProperty("dv").GetValue(infoPersona, null)),
                            nombreProfesional = infoPersona.GetType().GetProperty("Nombres").GetValue(infoPersona, null),
                            apellidoPaternoProfesional = infoPersona.GetType().GetProperty("ApellidoPaterno").GetValue(infoPersona, null),
                            apellidoMaternoProfesional = infoPersona.GetType().GetProperty("ApellidoMaterno").GetValue(infoPersona, null),
                            //menorEdad = infoPersona.GetType().GetProperty("MenorEdad").GetValue(infoPersona, null),
                            //razonSocial = infoPersona.GetType().GetProperty("razonSocial").GetValue(infoPersona, null),
                        };
                    }
                    else
                    {
                        return new informacionProfesionalesEntities
                        {
                            mensajeSalida = infoPersona.GetType().GetProperty("Nombre").GetValue(infoPersona, null),
                            codigoSalida = "-3",
                        };
                    }
                }
            }
        }


        internal static informacionProfesionalesEntities getProfesionalRegistroCivil(int rutProfesional, char dvProfesional)
        {
            dynamic infoPersona = ServicioInformacionPersona.InformacionPorRut(rutProfesional, dvProfesional);

            if (infoPersona == null)
            {
                return new informacionProfesionalesEntities
                {
                    mensajeSalida = "Error en conexion remota a Registro Civil, intente mas tarde.",
                    codigoSalida = "-4",
                };
            }
            else
            {
                if (infoPersona.GetType().GetProperty("Estado").GetValue(infoPersona, null) == 1)
                {
                    return new informacionProfesionalesEntities
                    {
                        rutProfesional = Convert.ToString(infoPersona.GetType().GetProperty("Rut").GetValue(infoPersona, null)),
                        dvProfesional = Convert.ToString(infoPersona.GetType().GetProperty("dv").GetValue(infoPersona, null)),
                        nombreProfesional = infoPersona.GetType().GetProperty("Nombres").GetValue(infoPersona, null),
                        apellidoPaternoProfesional = infoPersona.GetType().GetProperty("ApellidoPaterno").GetValue(infoPersona, null),
                        apellidoMaternoProfesional = infoPersona.GetType().GetProperty("ApellidoMaterno").GetValue(infoPersona, null),
                        //menorEdad = infoPersona.GetType().GetProperty("MenorEdad").GetValue(infoPersona, null),
                        //fallecido = infoPersona.GetType().GetProperty("Fallecido").GetValue(infoPersona, null),
                    };
                }
                else
                {
                    return new informacionProfesionalesEntities
                    {
                        mensajeSalida = infoPersona.GetType().GetProperty("Nombre").GetValue(infoPersona, null),
                        codigoSalida = "-3",
                    };
                }
            }            
        }

        internal static List<informacionProfesionalesEntities> getProfesiones()
        {
            var _informacionProfesionalesDAO = webAsistecSnatiiiPPPFProyectoDAO.GetProfesion();
            List<informacionProfesionalesEntities> informacionProfesionEntities = new List<informacionProfesionalesEntities>();
            if (_informacionProfesionalesDAO != null)
            {
                foreach (var a in _informacionProfesionalesDAO)
                {
                    informacionProfesionalesEntities info = new informacionProfesionalesEntities();

                    info.idProfesion = a.PROF_PROF_ID;
                    info.nombreProfesion = a.PROF_PROF_NOM;

                    informacionProfesionEntities.Add(info);
                };
                return informacionProfesionEntities;
            }
            else
                return informacionProfesionEntities;
        }

        internal static informacionProfesionalesEntities insertaProfesionales(int rutProfesional, string dvProfesional, string nombreProfesional, string apellidoPaterno, string apellidoMaterno, int idProfesion, int profesionalFTO, string usuario)
        {
            var _informacionProfesionalesDAO = webAsistecSnatiiiPPPFProyectoDAO.InsertaProfesional(rutProfesional, dvProfesional, nombreProfesional, apellidoPaterno, apellidoMaterno, idProfesion, profesionalFTO, usuario);
            informacionProfesionalesEntities informacionProfesionalEntities = new informacionProfesionalesEntities();
            if (_informacionProfesionalesDAO != null)
            {
                return new informacionProfesionalesEntities
                {
                    mensajeSalida = _informacionProfesionalesDAO.MSG,
                    codigoSalida = _informacionProfesionalesDAO.err.ToString()
                };
            }
            else
                return informacionProfesionalEntities;
        }

    }
}
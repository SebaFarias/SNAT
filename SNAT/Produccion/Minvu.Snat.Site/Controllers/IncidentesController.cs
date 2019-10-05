using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Minvu.Snat.Site.Controllers
{
    public class IncidentesController : Controller
    {
        // GET: Incidentes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MantenedorIncidentes()
        {
            return View("MantenedorIncidentes");
        }

        public ActionResult ReemplazoBeneficiario(string informacionProyecto, string tipo)
        {
            var informacion = new beneficiarioEntities
            {
                nombreProyecto = informacionProyecto,
                tipoOrigen = tipo
            };

            return View("ReemplazoBeneficiario", informacion);
        }

        public ActionResult Marca(string informacionProyecto, string tipo)
        {
            var informacion = new beneficiarioEntities
            {
                nombreProyecto = informacionProyecto,
                tipoOrigen = tipo
            };

            return View("Marca", informacion);
        }

        public ActionResult ActualizacionVigencia(ConsultaVigenciaFamilia _ConsultaVigenciaFamilia)
        {
            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Actualización de Vigencia Subsidio AVC";
            ViewBag.Message = "El beneficiario no se encuentra en las bases de datos de RUKAN";
            object auxConsultaInformacionProyecto = null;

            if (_ConsultaVigenciaFamilia._beneficiarioEntities != null)
            {
                if (!string.IsNullOrEmpty(_ConsultaVigenciaFamilia._beneficiarioEntities.rutBeneficiarioConsulta))
                {
                    auxConsultaInformacionProyecto = ConsultaVigenciaFamiliaFactory.getBeneficiario(int.Parse(_ConsultaVigenciaFamilia._beneficiarioEntities.rutBeneficiarioConsulta));
                }
                Session["auxConsultaInformacionProyecto"] = auxConsultaInformacionProyecto;
                if (auxConsultaInformacionProyecto == null)
                {
                    ViewBag.Message = "El beneficiario no se encuentra en las bases de datos de RUKAN";
                    return View(auxConsultaInformacionProyecto);
                }
                else
                {
                    return View(auxConsultaInformacionProyecto);
                }
            }
            else
            {
                return View(auxConsultaInformacionProyecto);
            }
        }


        public ActionResult AsignacionProfesionalesContrato(informacionProyectoGrillaEntities informacionProyectoGrilla, string codigo)
        {
            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Asignación de Profesionales a Contrato Serviu en PPPF";
            ViewBag.Message = "El proyecto no se encuentra en las bases de datos de SNAT";
            //object auxConsultaInformacionProyecto = null;
            List<informacionProyectoGrillaEntities> auxConsultaInformacionProyecto = null;

            if (informacionProyectoGrilla.informacionDisponible)
            {

                if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF) && informacionProyectoGrilla.idContrato >= 1 && !string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoServiuPPPF(7, Int32.Parse(idContrato), codProyecto, nombreProyecto);

                }
                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF) && informacionProyectoGrilla.idContrato >= 1)
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoServiuPPPF(8, Int32.Parse(idContrato), codProyecto, string.Empty);
                }
                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF) && !string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoServiuPPPF(9, 0, codProyecto, nombreProyecto);
                }
                else if (informacionProyectoGrilla.idContrato >= 1 && !string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoServiuPPPF(10, Int32.Parse(idContrato), string.Empty, nombreProyecto);
                }

                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF))
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoServiuPPPF(4, 0, codProyecto, string.Empty);
                }
                else if (informacionProyectoGrilla.idContrato >= 1)
                {
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();
                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoServiuPPPF(5, Int32.Parse(idContrato), string.Empty, string.Empty);

                }
                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoServiuPPPF(6, 0, string.Empty, nombreProyecto);
                }
                else if (informacionProyectoGrilla.idContrato == 0)
                {
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();
                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoServiuPPPF(5, Int32.Parse(idContrato), string.Empty, string.Empty);
                }

                var json = Json(auxConsultaInformacionProyecto, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 500000000;
                return json;
            }
            else
            {
                if (codigo == "0")
                {
                    ViewBag.mensajeExito = codigo;
                }
                return View(auxConsultaInformacionProyecto);
            }
        }

        public ActionResult EliminarProyecto(informacionProyectoGrillaEntities informacionProyectoGrilla)
        {
            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Eliminar Proyecto PPPF";
            ViewBag.Message = "El proyecto no se encuentra en las bases de datos de SNAT";
            object auxConsultaInformacionProyecto = null;

            if (informacionProyectoGrilla.informacionDisponible)
            {

                if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF) && informacionProyectoGrilla.idContrato >= 1 && !string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoElimPPPF(7, Int32.Parse(idContrato), codProyecto, nombreProyecto);

                }
                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF) && informacionProyectoGrilla.idContrato >= 1)
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoElimPPPF(8, Int32.Parse(idContrato), codProyecto, string.Empty);
                }
                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF) && !string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoElimPPPF(9, 0, codProyecto, nombreProyecto);
                }
                else if (informacionProyectoGrilla.idContrato >= 1 && !string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoElimPPPF(10, Int32.Parse(idContrato), string.Empty, nombreProyecto);
                }

                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF))
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoElimPPPF(4, 0, codProyecto, string.Empty);
                }
                else if (informacionProyectoGrilla.idContrato >= 1)
                {
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();
                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoElimPPPF(5, Int32.Parse(idContrato), string.Empty, string.Empty);

                }
                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoElimPPPF(6, 0, string.Empty, nombreProyecto);
                }
                else if (informacionProyectoGrilla.idContrato == 0)
                {
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();
                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoElimPPPF(5, Int32.Parse(idContrato), string.Empty, string.Empty);

                }

                var json = Json(auxConsultaInformacionProyecto, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 500000000;
                return json;
            }
            else
            {
                return View(auxConsultaInformacionProyecto);
            }
        }

        [HttpPost]
        public ActionResult ConfirmaEliminaProyecto()
        {
            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Eliminar Proyecto PPPF";
            object auxConsultaInformacionProyecto = null;
            var idProyectoRequest = Request.Form["codigoProyectoPPPFHidden"];
            var idContratoRequest = Request.Form["idContratoHidden"];
            string usuario = SiteHelper.UserName;

            string codProyecto = idProyectoRequest;
            int idContrato = Int32.Parse(idContratoRequest);
            auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.deleteProyectoPPPF(codProyecto, idContrato, usuario);

            return View("EliminarProyecto", auxConsultaInformacionProyecto);
        }

        [HttpPost]
        public ActionResult ConfirmaModificaContrato(ConsultaContratoPPPF _ConsultaContratoPPPF)
        {
            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Cambiar Tipo de Contrato PPPF";
            var idTipoContrato = Request.Form["ddltipo"];
            var idContratoRequest = Request.Form["idContratoHidden"];
            object auxConsultaInformacionProyecto = null;
            int idContrato = Int32.Parse(idContratoRequest);
            string usuario = SiteHelper.UserName;

            auxConsultaInformacionProyecto = ConsultaContratoPPPFFactory.modificaTipoContratoPPPF(idContrato, Int32.Parse(idTipoContrato), usuario);

            //auxConsultaInformacionProyecto = ConsultaContratoPPPFFactory.getContratoPPPF(5, idContrato, string.Empty, string.Empty);

            return View("CambiarTipoContrato", auxConsultaInformacionProyecto);
        }


        [HttpPost]
        public ActionResult ConfirmaModificaProfesional(beneficiarioEntities beneficiario)
        {

            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Asignación de Profesionales a Contrato PPPF";
            ViewBag.Message = "El proyecto no se encuentra en las bases de datos de SNAT";
            object auxConsultaInformacionProyecto = null;

            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            
            int idProyecto = beneficiario.idInformacionProyecto;
            int rutProfesional = beneficiario.rutBeneficiario;
            int rutProfReemplazo = Int32.Parse(beneficiario.rutBeneficiarioConsulta);
            int tipoContrato = Int32.Parse(beneficiario.tipoOrigen);

            string usuario = SiteHelper.UserName;

            _ConsultaProyectoPPPF = ConsultaProyectoPPPFFactory.modificaProfesionalPPPF(idProyecto, rutProfesional, rutProfReemplazo, tipoContrato, usuario);
            auxConsultaInformacionProyecto = _ConsultaProyectoPPPF;

            string codigoSalida = _ConsultaProyectoPPPF._informacionProyectoEntities.codigoSalida;
            string mensaje = _ConsultaProyectoPPPF._informacionProyectoEntities.mensajeSalida;            

            //if (codigoSalida != "0")
            //{
            return Json(new { codigoSalida, mensaje, auxConsultaInformacionProyecto });
            //}

            //return View("AsignacionProfesionalesContrato", auxConsultaInformacionProyecto);

        }

        public ActionResult ConfirmaCambioCertificado(ConsultaProyectoPPPF _ConsultaProyectoPPPF)
        {
            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Cambiar Número de Certificado PPPF";
            ViewBag.Message = "El proyecto no se encuentra en las bases de datos de SNAT";
            object auxConsultaInformacionProyecto = null;
            var idProyectoPPPF = Request.Form["idProyectoPPPF"];

            //string idProyecto = _ConsultaProyectoPPPF._informacionProyectoRukanEntities.codigoProyectoPPPF;
            string idProyecto = idProyectoPPPF;
            string numCertificado = _ConsultaProyectoPPPF._informacionProyectoRukanEntities.numeroCertificadoPPPF;
            string usuario = SiteHelper.UserName;
            string rutRukan = _ConsultaProyectoPPPF._informacionProyectoRukanEntities.rutProyectoPPPF;
            string rut = _ConsultaProyectoPPPF._informacionProyectoRukanEntities.codigoProyectoPPPF;

            auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.changeCertificadoProyectoPPPF(idProyecto, numCertificado, rut, rutRukan, usuario);

            return View("CambiarNumeroCertificado", auxConsultaInformacionProyecto);
        }

        public ActionResult ConfirmaActualizaVigencia(ConsultaVigenciaFamilia _ConsultaVigenciaFamilia)
        {
            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Actualización de Vigencia Subsidio AVC";
            ViewBag.Message = "El beneficiario no se encuentra en las bases de datos de SNAT";
            object auxConsultaInformacionProyecto = null;

            long idProyecto = _ConsultaVigenciaFamilia._beneficiarioRukanEntities.rutBeneficiario;
            DateTime fechaVigencia = _ConsultaVigenciaFamilia._beneficiarioRukanEntities.fechaVigencia;
            string usuario = @SiteHelper.UserName;

            auxConsultaInformacionProyecto = ConsultaVigenciaFamiliaFactory.changeVigenciaPPPF(idProyecto, fechaVigencia, usuario);

            return View("ActualizacionVigencia", auxConsultaInformacionProyecto);
        }

        public ActionResult CambiarMontoOferta(string informacionProyecto, string tipo)
        {
            var informacion = new beneficiarioEntities
            {
                nombreProyecto = informacionProyecto,
                tipoOrigen = tipo
            };

            return View("CambiarMontoOferta", informacion);
        }

        public ActionResult GuardaProfesional(ConsultaProfesionalPPPF _ConsultaProfesionalPPPF)
        {
            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Cambiar Número de Certificado PPPF";
            ViewBag.Message = "El profesional no se encuentra en las bases de datos de SNAT";
            object auxConsultaInformacionProfesional = null;

            string rut = _ConsultaProfesionalPPPF._informacionProfesionalEntities.rutProfesional;
            string dv = _ConsultaProfesionalPPPF._informacionProfesionalEntities.dvProfesional;
            string nombre = _ConsultaProfesionalPPPF._informacionProfesionalEntities.nombreProfesional;
            string apellidoPaterno = _ConsultaProfesionalPPPF._informacionProfesionalEntities.apellidoPaternoProfesional;
            string apellidoMaterno = _ConsultaProfesionalPPPF._informacionProfesionalEntities.apellidoMaternoProfesional;
            int idProfesion = _ConsultaProfesionalPPPF._informacionProfesionalEntities.idProfesion;
            int ProfesionalFTO = _ConsultaProfesionalPPPF._informacionProfesionalEntities.profesionalITO ? 1 : 0;
            string usuario = SiteHelper.UserName;

            auxConsultaInformacionProfesional = ConsultaProfesionalPPPFFactory.insertaProfesionalPPPF(Int32.Parse(rut), dv, nombre, apellidoPaterno, apellidoMaterno, idProfesion, ProfesionalFTO, usuario);

            return View("MantenedorProfesionales", auxConsultaInformacionProfesional);
        }


        public ActionResult CambiarTipoContrato(informacionProyectoGrillaEntities informacionProyectoGrilla)
        {
            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Cambiar Tipo de Contrato PPPF";
            ViewBag.Message = "El contrato no se encuentra en las bases de datos de SNAT";
            object auxConsultaInformacionContrato = null;

            if (informacionProyectoGrilla.informacionDisponible)
            {
                if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF) && informacionProyectoGrilla.idContrato >= 1 && !string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionContrato = ConsultaContratoPPPFFactory.getContratoPPPF(7, Int32.Parse(idContrato), codProyecto, nombreProyecto);
                }

                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF) && informacionProyectoGrilla.idContrato >= 1)
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();

                    auxConsultaInformacionContrato = ConsultaContratoPPPFFactory.getContratoPPPF(8, Int32.Parse(idContrato), codProyecto, string.Empty);
                }

                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF) && !string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionContrato = ConsultaContratoPPPFFactory.getContratoPPPF(9, 0, codProyecto, nombreProyecto);
                }

                else if (informacionProyectoGrilla.idContrato >= 1 && !string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionContrato = ConsultaContratoPPPFFactory.getContratoPPPF(10, Int32.Parse(idContrato), string.Empty, nombreProyecto);
                }

                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.codigoProyectoPPPF))
                {
                    string codProyecto = informacionProyectoGrilla.codigoProyectoPPPF;

                    auxConsultaInformacionContrato = ConsultaContratoPPPFFactory.getContratoPPPF(4, 0, codProyecto, string.Empty);
                }
                else if (informacionProyectoGrilla.idContrato >= 1)
                {
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();

                    auxConsultaInformacionContrato = ConsultaContratoPPPFFactory.getContratoPPPF(5, Int32.Parse(idContrato), string.Empty, string.Empty);
                }
                else if (!string.IsNullOrEmpty(informacionProyectoGrilla.nombrePSATPPPF))
                {
                    string nombreProyecto = informacionProyectoGrilla.nombrePSATPPPF;
                    string textoNormalizado = nombreProyecto.Normalize(NormalizationForm.FormD);
                    Regex reg = new Regex("[^(a-zA-Z0-9 )]");
                    string textoSinAcentos = reg.Replace(textoNormalizado, "");
                    nombreProyecto = textoSinAcentos;

                    auxConsultaInformacionContrato = ConsultaContratoPPPFFactory.getContratoPPPF(6, 0, string.Empty, nombreProyecto);
                }
                else if (informacionProyectoGrilla.idContrato == 0)
                {
                    string idContrato = informacionProyectoGrilla.idContrato.ToString();
                    auxConsultaInformacionContrato = ConsultaContratoPPPFFactory.getContratoPPPF(5, Int32.Parse(idContrato), string.Empty, string.Empty);
                }


                var json = Json(auxConsultaInformacionContrato, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 500000000;
                return json;
            }
            else
            {
                return View(auxConsultaInformacionContrato);
            }
        }

        public ActionResult MantenedorProfesionales(ConsultaProfesionalPPPF _ConsultaProfesionalPPPF)
        {
            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Mantenedor de Profesionales PPPF";
            ViewBag.Message = "El profesional no se encuentra en las bases de datos de SNAT";

            object auxConsultaInformacionProfesional = null;

            if (_ConsultaProfesionalPPPF._informacionProfesionalEntities != null)
            {
                if (_ConsultaProfesionalPPPF._informacionProfesionalEntities.rutProfesional != "0")
                {
                    string rutProfesional = _ConsultaProfesionalPPPF._informacionProfesionalEntities.rutProfesional;
                    char dvProfesional = Convert.ToChar(_ConsultaProfesionalPPPF._informacionProfesionalEntities.dvProfesional);

                    auxConsultaInformacionProfesional = ConsultaProfesionalPPPFFactory.getProfesionalPPPF(Int32.Parse(rutProfesional), dvProfesional);


                    return View(auxConsultaInformacionProfesional);
                }
                else
                {
                    return View(auxConsultaInformacionProfesional);
                }
            }
            else
            {
                return View(auxConsultaInformacionProfesional);
            }
        }

        public ActionResult CambiarNumeroCertificado(ConsultaProyectoPPPF _ConsultaProyectoPPPF)
        {
            ViewBag.tipoOrigen = "SNAT III";
            ViewBag.nombreProyecto = "Cambiar Número de Certificado PPPF";
            ViewBag.Message = "El proyecto no se encuentra en las bases de datos de RUKAN";
            object auxConsultaInformacionProyecto = null;


            if (_ConsultaProyectoPPPF._informacionProyectoEntities != null)
            {
                //string codProyecto = _ConsultaProyectoPPPF._informacionProyectoEntities.codigoProyectoPPPFConsulta.ToString();

                if (!string.IsNullOrEmpty(_ConsultaProyectoPPPF._informacionProyectoEntities.codigoProyectoPPPFConsulta) && !string.IsNullOrEmpty(_ConsultaProyectoPPPF._informacionProyectoEntities.numeroCertificadoPPPFConsulta))
                {
                    string codProyecto = _ConsultaProyectoPPPF._informacionProyectoEntities.codigoProyectoPPPFConsulta.ToString();
                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getProyectoPPPFCertificadoTodos(3, codProyecto, _ConsultaProyectoPPPF._informacionProyectoEntities.numeroCertificadoPPPFConsulta);
                }
                else if (!string.IsNullOrEmpty(_ConsultaProyectoPPPF._informacionProyectoEntities.codigoProyectoPPPFConsulta))
                {
                    string codProyecto = _ConsultaProyectoPPPF._informacionProyectoEntities.codigoProyectoPPPFConsulta.ToString();
                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getProyectoPPPFRutCertificado(1, codProyecto, string.Empty);
                }
                else if (!string.IsNullOrEmpty(_ConsultaProyectoPPPF._informacionProyectoEntities.numeroCertificadoPPPFConsulta))
                {
                    auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getProyectoPPPFCertificado(2, string.Empty, _ConsultaProyectoPPPF._informacionProyectoEntities.numeroCertificadoPPPFConsulta);
                }

                Session["auxConsultaInformacionProyecto"] = auxConsultaInformacionProyecto;
                if (auxConsultaInformacionProyecto == null)
                {
                    ViewBag.Message = "El proyecto no se encuentra en las bases de datos de RUKAN";
                    return View(auxConsultaInformacionProyecto);
                }
                else
                {
                    return View(auxConsultaInformacionProyecto);
                }
            }
            else
            {
                return View(auxConsultaInformacionProyecto);
            }
        }
    }
}
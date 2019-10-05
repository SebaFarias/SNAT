using Minvu.Snat.IData.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class beneficiarioEntities
    {
        public int idBeneficiario { get; set; }
        public int idDireccion { get; set; }
        public int idInformacionProyecto { get; set; }
        public int rutBeneficiario { get; set; }

        [Required(ErrorMessage = "El dv es obligatorio")]
        public char digitoVerificadorBeneficiario { get; set; }
        public string nombreBeneficiario { get; set; }
        public string apellidoPaternoBeneficiario { get; set; }
        public string apellidoMaternoBeneficiario { get; set; }
        public bool estadoBeneficiario { get; set; }
        public string nombreProyecto { get; set; }
        public string tipoOrigen { get; set; }
        public DateTime fechaVigencia { get; set; }

        [Required(ErrorMessage = "El rut es obligatorio")]
        //[RegularExpression(@"(^[1-9]\d*$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string rutBeneficiarioConsulta { get; set; }
        public string mensajeSalida { get; set; }
        public string codigoSalida { get; set; }
        public beneficiarioEntities()
        {
            idBeneficiario = 0;
            idDireccion = 0;
            idInformacionProyecto = 0;
            rutBeneficiario = 0;
            digitoVerificadorBeneficiario = '0';
            nombreBeneficiario = "";
            apellidoPaternoBeneficiario = "";
            apellidoMaternoBeneficiario = "";
            estadoBeneficiario = false;
            nombreProyecto = "";
            tipoOrigen = "";
            fechaVigencia = new DateTime();
            rutBeneficiarioConsulta = string.Empty;
            mensajeSalida = string.Empty;
            codigoSalida = string.Empty;
        }

        public beneficiarioEntities(int _idBeneficiario, int _idDireccion, int _idInformacionProyecto, int _rutBeneficiario, char _digitoVerificadorBeneficiario, string _nombreBeneficiario, string _apellidoPaternoBeneficiario, string _apellidoMaternoBeneficiario, bool _estadoBeneficiario, string _nombreProyecto, string _tipoOrigen, DateTime _fechaVigencia)
        {
            idBeneficiario = _idBeneficiario;
            idDireccion = _idDireccion;
            idInformacionProyecto = _idInformacionProyecto;
            rutBeneficiario = _rutBeneficiario;
            digitoVerificadorBeneficiario = _digitoVerificadorBeneficiario;
            nombreBeneficiario = _nombreBeneficiario;
            apellidoPaternoBeneficiario = _apellidoPaternoBeneficiario;
            apellidoMaternoBeneficiario = _apellidoMaternoBeneficiario;
            estadoBeneficiario = _estadoBeneficiario;
            nombreProyecto = _nombreProyecto;
            tipoOrigen = _tipoOrigen;
            fechaVigencia = _fechaVigencia;
        }

        public class informacionBeneficiarioEntitiesFactory
        {
            internal static beneficiarioEntities getinformacionBeneficiarioEntities(int rutBeneficiario)
            {
                var _informacionBeneficiarioDAO = webAsistecSnatiiiFamiliaDAO.Get(rutBeneficiario);
                beneficiarioEntities beneficiarioEntities = new beneficiarioEntities();
                if (_informacionBeneficiarioDAO.FAM_RUT > 0)
                {
                    return new beneficiarioEntities
                    {
                        rutBeneficiario = _informacionBeneficiarioDAO.FAM_RUT,
                        digitoVerificadorBeneficiario = Char.Parse(_informacionBeneficiarioDAO.FAM_DGV),
                        nombreBeneficiario = _informacionBeneficiarioDAO.FAM_NOM,
                        apellidoPaternoBeneficiario = _informacionBeneficiarioDAO.FAM_APP_PAT,
                        apellidoMaternoBeneficiario = _informacionBeneficiarioDAO.FAM_APP_MAT,
                        fechaVigencia = Convert.ToDateTime(_informacionBeneficiarioDAO.FAM_FEC_FIN_VIG_SUB)
                    };

                }
                else
                    return beneficiarioEntities;
            }

            internal static beneficiarioEntities getinformacionBeneficiarioRukanEntities(int rutBeneficiario)
            {
                var _informacionBeneficiarioDAO = rukanMigra.GetRukan(rutBeneficiario);
                beneficiarioEntities beneficiarioEntities = new beneficiarioEntities();
                if (_informacionBeneficiarioDAO.RUT_FAMILIA != null)
                {
                    return new beneficiarioEntities
                    {
                        rutBeneficiario = Int32.Parse(_informacionBeneficiarioDAO.RUT_FAMILIA),
                        digitoVerificadorBeneficiario = Char.Parse(_informacionBeneficiarioDAO.DV_FAMILIA),
                        nombreBeneficiario = _informacionBeneficiarioDAO.NOMBRES,
                        apellidoPaternoBeneficiario = _informacionBeneficiarioDAO.APELLIDO_PATERNO,
                        apellidoMaternoBeneficiario = _informacionBeneficiarioDAO.APELLIDO_MATERNO,
                        fechaVigencia = Convert.ToDateTime(_informacionBeneficiarioDAO.NUEVA_VIGENCIA)
                    };

                }
                else
                    return beneficiarioEntities;
            }

            internal static beneficiarioEntities changeVigenciaPPPFEntities(long codProyecto, DateTime fechaVigencia, string usuario)
            {
                var _informacionBeneficiarioDAO = webAsistecSnatiiiFamiliaDAO.ActulizaVigencia(codProyecto, fechaVigencia, usuario);

                beneficiarioEntities informacionProyectoEntities = new beneficiarioEntities();
                if (_informacionBeneficiarioDAO != null)
                {
                    return new beneficiarioEntities
                    {
                        mensajeSalida = _informacionBeneficiarioDAO.MSG,
                        codigoSalida = _informacionBeneficiarioDAO.err.ToString()
                    };
                }
                else
                    return informacionProyectoEntities;
            }
        }
    }
}
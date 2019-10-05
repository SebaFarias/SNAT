using Minvu.Snat.IData.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Minvu.Snat.Domain.Entities
{
    public class autorizacionEntities
    {
        [Display(Name = "ID autorización:")]
        public long idAutorizacion { get; set; }
        public long? idMaestroEstadoAutorizacion { get; set; }
        public bool? especialAutorizacion { get; set; }
        public int? cantidadProyectoAutorizacion { get; set; }
        public int? cantidadSolicitudPagoAutorizacion { get; set; }
        public decimal? montoTotalAutorizacion { get; set; }
        public string usuarioResponsable { get; set; }
        public DateTime? FechaIngresoAutorizacion { get; set; }
        public long? CodigoRegionAutorizacion { get; set; }
        public long? NumeroAutorizacion { get; set; }
        public long? idMaestroModalidad { get; set; }
        public long? idMaestroPrograma { get; set; }
        public long? idMaestroTipologia { get; set; }
        public long? idMaestroTitulo { get; set; }
        public long? idProveedor { get; set; }

        public autorizacionEntities()
        {
            idAutorizacion = 0;
            idMaestroEstadoAutorizacion = null;
            especialAutorizacion = null;
            cantidadProyectoAutorizacion = null;
            cantidadSolicitudPagoAutorizacion = null;
            montoTotalAutorizacion = null;
            usuarioResponsable = null;
            FechaIngresoAutorizacion = null;
            CodigoRegionAutorizacion = null;
            NumeroAutorizacion = null;
            idMaestroModalidad = null;
            idMaestroPrograma = null;
            idMaestroTipologia = null;
            idMaestroTitulo = null;
            idProveedor = null;
        }
        public autorizacionEntities(long _idAutorizacion, long? _idMaestroEstadoAutorizacion, bool _especialAutorizacion, int? _cantidadProyectoAutorizacion,
                                    int? _cantidadSolicitudPagoAutorizacion, decimal? _montoTotalAutorizacion, string _usuarioResponsable,
                                    DateTime? _FechaIngresoAutorizacion, long? _CodigoRegionAutorizacion, long? _NumeroAutorizacion, long? _idMaestroModalidad,
                                    long? _idMaestroPrograma, long? _idMaestroTipologia, long? _idMaestroTitulo, long? _idProveedor)
        {
            idAutorizacion = _idAutorizacion;
            idMaestroEstadoAutorizacion = _idMaestroEstadoAutorizacion;
            especialAutorizacion = _especialAutorizacion;
            cantidadProyectoAutorizacion = _cantidadProyectoAutorizacion;
            cantidadSolicitudPagoAutorizacion = _cantidadSolicitudPagoAutorizacion;
            montoTotalAutorizacion = _montoTotalAutorizacion;
            usuarioResponsable = _usuarioResponsable;
            FechaIngresoAutorizacion = _FechaIngresoAutorizacion;
            CodigoRegionAutorizacion = _CodigoRegionAutorizacion;
            NumeroAutorizacion = _NumeroAutorizacion;
            idMaestroModalidad = _idMaestroModalidad;
            idMaestroPrograma = _idMaestroPrograma;
            idMaestroTipologia = _idMaestroTipologia;
            idMaestroTitulo = _idMaestroTitulo;
            idProveedor = _idProveedor;
        }
    }

    public class autorizacionEntitiesFactory
    {
        internal static autorizacionEntities getAutorizacion(long? IdAutorizacion)
        {
            var _AutorizacionDAO = autorizacionDAO.Get(IdAutorizacion);

            if (_AutorizacionDAO != null)
            {
                return new autorizacionEntities
                {
                    idAutorizacion = _AutorizacionDAO.IDAUTORIZACION,
                    idMaestroEstadoAutorizacion = _AutorizacionDAO.IDMAESTROESTADOAUTORIZACION,
                    especialAutorizacion = _AutorizacionDAO.ESPECIALAUTORIZACION,
                    cantidadProyectoAutorizacion = _AutorizacionDAO.CANTIDADPROYECTOSAUTORIZACION,
                    cantidadSolicitudPagoAutorizacion = _AutorizacionDAO.CANTIDADSOLICITUDPAGOAUTORIZACION,
                    montoTotalAutorizacion = _AutorizacionDAO.MONTOTOTALAUTORIZACION,
                    usuarioResponsable = _AutorizacionDAO.USUARIORESPONSABLEAUTORIZACION,
                    FechaIngresoAutorizacion = _AutorizacionDAO.FECHAINGRESOAUTORIZACION,
                    CodigoRegionAutorizacion = _AutorizacionDAO.CODIGOREGIONAUTORIZACION,
                    NumeroAutorizacion = _AutorizacionDAO.NUMEROAUTORIZACION,
                    idMaestroModalidad = _AutorizacionDAO.IDMAESTROMODALIDAD,
                    idMaestroPrograma = _AutorizacionDAO.IDMAESTROPROGRAMA,
                    idMaestroTipologia = _AutorizacionDAO.IDMAESTROTIPOLOGIA,
                    idMaestroTitulo = _AutorizacionDAO.IDMAESTROTITULO,
                    idProveedor = _AutorizacionDAO.IDPROVEEDOR,
                };
            }
            else
                return null;
        }
    }

    public class User
    {
        public int RutUsuario { get; set; }
        public string DvUsuario { get; set; }
        public int idRegionUsuario { get; set; }

        public User()
        {
            RutUsuario = 0;
            DvUsuario = String.Empty;
            idRegionUsuario = 0;
        }
    }

    public class UserFactory
    {
        internal static User GetUser(string userLog)
        {
            User objUser = new User();

            var _RegionUser = regionDAO.GetUSERREGION(userLog);
            var _ObtieneRutUser = contratoDAO.GetRutUsuario(userLog);

            objUser.RutUsuario = (int)_ObtieneRutUser.Rut;
            objUser.DvUsuario = _ObtieneRutUser.DVRut;
            objUser.idRegionUsuario = _RegionUser == null ? 0 : _RegionUser.IDRegion;

            return objUser;
        }
    }
}
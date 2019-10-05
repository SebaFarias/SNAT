using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.Domain.Entities
{
    public class solicitudAutorizacionEntities
    {
        public long? idSolicitudAutorizacion { get; set; }
        public long? numeroAutorizacionSolicitudAutorizacion { get; set; }
        public long? numeroSolicitudSolicitudAutorizacion { get; set; }
        public long? codigoProyectoSolicitudAutorizacion { get; set; }
        public string nombreProyectoSolicitudAutorizacion { get; set; }
        public string ubicacionComunaSolicitudAutorizacion { get; set; }
        public decimal? S1 { get; set; }
        public decimal? S2 { get; set; }
        public decimal? S3 { get; set; }
        public decimal? S4 { get; set; }
        public decimal? S5 { get; set; }
        public decimal? S6 { get; set; }
        public decimal? S7 { get; set; }
        public decimal? S8 { get; set; }
        public decimal? S9 { get; set; }
        public decimal? S10 { get; set; }
        public decimal? FTOSolicitudAutorizacion { get; set; }
        public decimal? montoFTOTotalSolicitudAutorizacion { get; set; }
        public decimal? montoATSolicitudAutorizacion { get; set; }
        public decimal? montoAPagoSolicitudAutorizacion { get; set; }


        public solicitudAutorizacionEntities(long? _idSolicitudAutorizacion, long? _numeroSolicitudSolicitudAutorizacion, long? _codigoProyectoSolicitudAutorizacion, string _nombreProyectoSolicitudAutorizacion
        , string _ubicacionComunaSolicitudAutorizacion, decimal? _S1, decimal? _S2, decimal? _S3, decimal? _S4, decimal? _S5, decimal? _S6, decimal? _S7
            , decimal? _S8, decimal? _S9, decimal? _S10, decimal? _FTOSolicitudAutorizacion, decimal? _montoFTOTotalSolicitudAutorizacion
        , decimal? _montoATSolicitudAutorizacion, decimal? _montoAPagoSolicitudAutorizacion, long _numeroAutorizacionSolicitudAutorizacion)
        {
            numeroAutorizacionSolicitudAutorizacion = _numeroAutorizacionSolicitudAutorizacion;
            idSolicitudAutorizacion = _idSolicitudAutorizacion;
            numeroSolicitudSolicitudAutorizacion = _numeroSolicitudSolicitudAutorizacion;
            codigoProyectoSolicitudAutorizacion = _codigoProyectoSolicitudAutorizacion;
            nombreProyectoSolicitudAutorizacion = _nombreProyectoSolicitudAutorizacion;
            ubicacionComunaSolicitudAutorizacion = _ubicacionComunaSolicitudAutorizacion;
            S1 = _S1;
            S2 = _S2;
            S3 = _S3;
            S4 = _S4;
            S5 = _S5;
            S6 = _S6;
            S7 = _S7;
            S8 = _S8;
            S9 = _S9;
            S10 = _S10;
            FTOSolicitudAutorizacion = _FTOSolicitudAutorizacion;
            montoFTOTotalSolicitudAutorizacion = _montoFTOTotalSolicitudAutorizacion;
            montoATSolicitudAutorizacion = _montoATSolicitudAutorizacion;
            montoAPagoSolicitudAutorizacion = montoAPagoSolicitudAutorizacion;
        }
        public solicitudAutorizacionEntities()
        {
            idSolicitudAutorizacion = null;
            numeroSolicitudSolicitudAutorizacion =null;
            codigoProyectoSolicitudAutorizacion = null;
            nombreProyectoSolicitudAutorizacion = null;
            ubicacionComunaSolicitudAutorizacion = null;
            S1 = null;
            S2 = null;
            S3 = null;
            S4 = null;
            S5 = null;
            S6 = null;
            S7 = null;
            S8 = null;
            S9 = null;
            S10 =null;
            FTOSolicitudAutorizacion = null;
            montoFTOTotalSolicitudAutorizacion = null;
            montoATSolicitudAutorizacion = null;
            montoAPagoSolicitudAutorizacion = null;
            numeroAutorizacionSolicitudAutorizacion = null;
        }
    }

    public class solicitudAutorizacionEntitiesFactory
    {
        internal static List<solicitudAutorizacionEntities> getList(long numeroAutorizacion)
        {

            List<solicitudAutorizacionEntities> _list = new List<solicitudAutorizacionEntities>();
            

            var aux = solicitudAutorizacionDAO.GetList(numeroAutorizacion);
            if (aux != null)
            {
                foreach (var item in aux)
                {
                    solicitudAutorizacionEntities _class = new solicitudAutorizacionEntities();

                    _class.codigoProyectoSolicitudAutorizacion = item.CODIGOPROYECTOSOLICITUDAUTORIZACION;
                    _class.FTOSolicitudAutorizacion = item.FTOSOLICITUDAUTORIZACION;
                    _class.idSolicitudAutorizacion = item.IDSOLICITUDAUTORIZACION;
                    _class.montoAPagoSolicitudAutorizacion = item.MONTOAPAGOSOLICITUDAUTORIZACION;
                    _class.montoATSolicitudAutorizacion = item.MONTOATSOLICITUDAUTORIZACION;
                    _class.montoFTOTotalSolicitudAutorizacion = item.MONTOFTOTOTALSOLICITUDAUTORIZACION;
                    _class.nombreProyectoSolicitudAutorizacion = item.NOMBREPROYECTOSOLICITUDAUTORIZACION;
                    _class.numeroAutorizacionSolicitudAutorizacion = item.NUMEROAUTORIZACIONSOLICITUDAUTORIZACION;
                    _class.S1 = item.S1SOLICITUDAUTORIZACION;
                    _class.S10 = item.S10SOLICITUDAUTORIZACION;
                    _class.S2 = item.S2SOLICITUDAUTORIZACION;
                    _class.S3 = item.S3SOLICITUDAUTORIZACION;
                    _class.S4 = item.S4SOLICITUDAUTORIZACION;
                    _class.S5 = item.S5SOLICITUDAUTORIZACION;
                    _class.S6 = item.S6SOLICITUDAUTORIZACION;
                    _class.S7 = item.S7SOLICITUDAUTORIZACION;
                    _class.S8 = item.S8SOLICITUDAUTORIZACION;
                    _class.S9 = item.S9SOLICITUDAUTORIZACION;
                    _class.ubicacionComunaSolicitudAutorizacion = item.UBICACIONCOMUNASOLICITUDAUTORIZACION;


                    _list.Add(_class);
                }

                return _list;
            }
            else
                return null;
        }

        internal static void delete(long numeroAutorizacion)
        {
            solicitudAutorizacionDAO.Delete(numeroAutorizacion);
        }


        internal static void save(List<solicitudAutorizacionEntities> _ListSolicitudAutorizacionEntities)
        {


            foreach (solicitudAutorizacionEntities item in _ListSolicitudAutorizacionEntities)
            {
                SOLICITUD_AUTORIZACION _aux = new SOLICITUD_AUTORIZACION();

                _aux.CODIGOPROYECTOSOLICITUDAUTORIZACION = item.codigoProyectoSolicitudAutorizacion;
                _aux.FTOSOLICITUDAUTORIZACION = item.FTOSolicitudAutorizacion;
                _aux.IDSOLICITUDAUTORIZACION = Convert.ToInt64(item.idSolicitudAutorizacion);
                _aux.MONTOAPAGOSOLICITUDAUTORIZACION = item.montoAPagoSolicitudAutorizacion;
                _aux.MONTOATSOLICITUDAUTORIZACION = item.montoATSolicitudAutorizacion;
                _aux.MONTOFTOTOTALSOLICITUDAUTORIZACION = item.montoFTOTotalSolicitudAutorizacion;
                _aux.NOMBREPROYECTOSOLICITUDAUTORIZACION = item.nombreProyectoSolicitudAutorizacion;
                _aux.NUMEROAUTORIZACIONSOLICITUDAUTORIZACION = Convert.ToInt64(item.numeroAutorizacionSolicitudAutorizacion);

                _aux.NUMEROSOLICITUDSOLICITUDAUTORIZACION = item.numeroSolicitudSolicitudAutorizacion;
                _aux.S10SOLICITUDAUTORIZACION = item.S10;
                _aux.S1SOLICITUDAUTORIZACION = item.S1;
                _aux.S2SOLICITUDAUTORIZACION = item.S2;
                _aux.S3SOLICITUDAUTORIZACION = item.S3;
                _aux.S4SOLICITUDAUTORIZACION = item.S4;
                _aux.S5SOLICITUDAUTORIZACION = item.S5;
                _aux.S6SOLICITUDAUTORIZACION = item.S6;

                _aux.S7SOLICITUDAUTORIZACION = item.S7;
                _aux.S8SOLICITUDAUTORIZACION = item.S8;
                _aux.S9SOLICITUDAUTORIZACION = item.S9;
                _aux.UBICACIONCOMUNASOLICITUDAUTORIZACION = item.ubicacionComunaSolicitudAutorizacion;
                


                solicitudAutorizacionDAO.Save(_aux);
            }

            


        }
    }
}

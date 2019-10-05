using Minvu.Snat.IData.DAO;
using System;
using System.Collections.Generic;
using Minvu.Snat.Helper;
namespace Minvu.Snat.Domain.Entities
{
    public class OperacionesMuninEntities
    {

        public string alternativaPostulacion { get; set; }
        public string anoLlamadoProyecto { get; set; }
        public string avanceRealActual { get; set; }
        public string codigoProgramaMunin { get; set; }
        public string codigoProgramaSNAT { get; set; }
        public string codigoProyecto { get; set; }
        public string dvEC { get; set; }
        public string estadoFiniquitadaEC { get; set; }
        public string estadoGeneral { get; set; }
        public string estadoGeneralDescripcion { get; set; }
        public string idEC { get; set; }
        public string nombreEC { get; set; }
        public string nombreEmpresaFiscalizadora { get; set; }
        public string nroLlamadoProyecto { get; set; }
        public string programa { get; set; }
        public string regiónProyecto { get; set; }
        public string rutEC { get; set; }
        public string tipologiaProyecto { get; set; }
        public string tituloProyecto { get; set; }


    }

    public class OperacionesMuninFactory
    {
        public static List<OperacionesMuninEntities> getAvanceObraTrazabilidadProyecto(int idPrograma, string codProyecto, int? region, int? titulo, int? año, int? numeroLLamado)
        {

            try
            {
                List<OperacionesMuninEntities> listOperacionesMuninEntities = new List<OperacionesMuninEntities>();
                var _operacionesMuninEntitiesDAO = OperacionesMuninDAO.getAvanceObraTrazabilidadProyecto(idPrograma, codProyecto, region, titulo, año, numeroLLamado);

                if (_operacionesMuninEntitiesDAO != null)
                {
                    if (_operacionesMuninEntitiesDAO.Count > 0)
                    {
                        foreach (var item in _operacionesMuninEntitiesDAO)
                        {
                            

                            OperacionesMuninEntities _operacionesMuninEntities = new OperacionesMuninEntities();

                            if(item.AlternativaPostulacion != null && item.AlternativaPostulacion != string.Empty )
                            _operacionesMuninEntities.alternativaPostulacion = item.AlternativaPostulacion;
                            if (item.AnoLlamadoProyecto != null && item.AnoLlamadoProyecto.ToString() != string.Empty)
                                _operacionesMuninEntities.anoLlamadoProyecto = item.AnoLlamadoProyecto.ToString();
                            if (item.AvanceRealActual != null && item.AvanceRealActual.ToString() != string.Empty)
                                _operacionesMuninEntities.avanceRealActual = item.AvanceRealActual.ToString();
                            if (item.CodigoPrograma != null && item.CodigoPrograma.ToString() != string.Empty)
                                _operacionesMuninEntities.codigoProgramaMunin = item.CodigoPrograma.ToString();

                            if (item.Programa != string.Empty && item.CodigoPrograma != null)
                                _operacionesMuninEntities.codigoProgramaSNAT = ConvalidacionDatosEntreSistemasEntities.getIdProgramaInternoPorSistemaExterno(item.Programa, Convert.ToInt64(item.CodigoPrograma), "MUNIN").ToString();
                            if (item.CodigoProyecto != null && item.CodigoProyecto != string.Empty)
                                _operacionesMuninEntities.codigoProyecto = item.CodigoProyecto;
                            if (item.DvEC != null && item.DvEC != string.Empty)
                                _operacionesMuninEntities.dvEC = item.DvEC;
                            if (item.EstadoFiniquitadaEC != null && item.EstadoFiniquitadaEC.ToString() != string.Empty)
                                _operacionesMuninEntities.estadoFiniquitadaEC = item.EstadoFiniquitadaEC.ToString();
                            if (item.EstadoGeneral != null && item.EstadoGeneral.ToString() != string.Empty)
                                _operacionesMuninEntities.estadoGeneral = item.EstadoGeneral.ToString();
                            if (item.EstadoGeneralDescripcion != null && item.EstadoGeneralDescripcion != string.Empty)
                                _operacionesMuninEntities.estadoGeneralDescripcion = item.EstadoGeneralDescripcion;
                            if (item.IdEC != null && item.IdEC.ToString() != string.Empty)
                                _operacionesMuninEntities.idEC = item.IdEC.ToString();
                            if (item.NombreEC != null && item.NombreEC != string.Empty)
                                _operacionesMuninEntities.nombreEC = item.NombreEC;
                            if (item.NombreEmpresaFiscalizadora != null && item.NombreEmpresaFiscalizadora != string.Empty)
                                _operacionesMuninEntities.nombreEmpresaFiscalizadora = item.NombreEmpresaFiscalizadora;
                            if (item.NroLlamadoProyecto != null && item.NroLlamadoProyecto.ToString() != string.Empty)
                                _operacionesMuninEntities.nroLlamadoProyecto = item.NroLlamadoProyecto.ToString();
                            if (item.Programa != null && item.Programa != string.Empty)
                                _operacionesMuninEntities.programa = item.Programa;
                            if (item.RegiónProyecto != null && item.RegiónProyecto.ToString() != string.Empty)
                                _operacionesMuninEntities.regiónProyecto = item.RegiónProyecto.ToString();
                            if (item.RutEC != null && item.RutEC.ToString() != string.Empty)
                                _operacionesMuninEntities.rutEC = item.RutEC.ToString();
                            if (item.TipologiaProyecto != null && item.TipologiaProyecto != string.Empty)
                                _operacionesMuninEntities.tipologiaProyecto = item.TipologiaProyecto;
                            if (item.TituloProyecto != null && item.TituloProyecto.ToString() != string.Empty)
                                _operacionesMuninEntities.tituloProyecto = item.TituloProyecto.ToString();


                            listOperacionesMuninEntities.Add(_operacionesMuninEntities);
                        }
                        return listOperacionesMuninEntities;
                    }

                    return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Error en getAvanceObraTrazabilidadProyecto: " + ex);

                return null;
            }
          
        }

    }
}

using System;
using System.Collections.Generic;
using Minvu.Snat.IData.DAO;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroIncrementoEntities
    {
        public int idMaestroIncremento { get; set; }
        public string nombreMaestroIncremento { get; set; }
        public bool seleccionadoMaestroIncremento { get; set; }
        public bool estadoMaestroIncremento { get; set; }


        public maestroIncrementoEntities()
        {
            idMaestroIncremento = 0;
            nombreMaestroIncremento = string.Empty;
            estadoMaestroIncremento = false;
            seleccionadoMaestroIncremento = false;
        }
        public maestroIncrementoEntities(int _idMaestroIncremento, string _nombreMaestroIncremento, bool _estadoMaestroIncremento,bool _seleccionadoMaestroIncremento )
        {
            idMaestroIncremento = _idMaestroIncremento;
            nombreMaestroIncremento = _nombreMaestroIncremento;
            estadoMaestroIncremento = _estadoMaestroIncremento;
            seleccionadoMaestroIncremento = _seleccionadoMaestroIncremento;
        }
    }
    public class maestroIncrementoEntitiesFactory
    {

        internal static maestroIncrementoEntities getMaestroIncremento(long idMaestroIncremento)
        {
            var _maestroIncrementoVar = maestroIncrementoDAO.Get(idMaestroIncremento);

            maestroIncrementoEntities _maestroIncrementoEntities = new maestroIncrementoEntities();


            if (_maestroIncrementoVar != null)
            {
               
                    maestroIncrementoEntities _maestroIncremento = new maestroIncrementoEntities();

                    _maestroIncremento.idMaestroIncremento = Convert.ToInt32(_maestroIncrementoVar.IDMAESTROINCREMENTO);
                _maestroIncremento.nombreMaestroIncremento = _maestroIncrementoVar.NOMBREMAESTROINCREMENTO;
                    _maestroIncremento.estadoMaestroIncremento = Convert.ToBoolean(_maestroIncrementoVar.ESTADOMAESTROINCREMENTO);
                

              
                
                return _maestroIncremento;
            }
            else
                return null;


        }

    
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Minvu.Snat.IData.ORM;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Controls
    
{
    
    public class regionControl
    {

        public IList<SelectListItem> LstRegion { get; set; }
        private static regionControl instancia;

        public regionControl()
        {
            
                CargarLista();
                        
        }

        public static regionControl Instancia
        {
            get
            {

                instancia = new regionControl();

                return instancia;
            }
        }

        


        public static List<SelectListItem> CargarLista()
        {
            List<SelectListItem> LstRegion = new List<SelectListItem>();
            foreach (REGION _region in regionDAO.obtenerRegiones())
            {
                LstRegion.Add(new SelectListItem
                {
                    Text = _region.REG_DES,
                    Value = _region.REG_ID.ToString()
                });
            }

            return LstRegion;
        }

        public static string getRegion(int region)
        {
            REGION _region= regionDAO.Obtener(region);

            return _region.REG_DES;
        }
		
		 public static List<SelectListItem> getRegiones()
        {
            //List<REGION> _region = regionDAO.obtenerRegiones();

            List<SelectListItem> lista = new List<SelectListItem>();

            foreach (REGION _region in regionDAO.obtenerRegiones())
            {
                lista.Add(new SelectListItem
                {
                    Text = _region.REG_DES,
                    Value = _region.REG_ID.ToString()
                });
            }

            return lista;
        }
    }
}

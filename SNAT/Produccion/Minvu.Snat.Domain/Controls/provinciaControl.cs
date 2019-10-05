
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
    public class provinciaControl
    {
        private static provinciaControl instancia;

        public provinciaControl()
        {
            CargarLista();
        }

        public static provinciaControl Instancia
        {
            get
            {

                instancia = new provinciaControl();

                return instancia;
            }
        }

        public IList<SelectListItem> LstProvincia { get; set; }


        public static List<SelectListItem>CargarLista()
        {
            List<SelectListItem> LstProvincia = new List<SelectListItem>();
            foreach (PROVINCIA _provincia in ProvinciaDAO.obtenerProvincias())
            {
                LstProvincia.Add(new SelectListItem
                {
                    Text = _provincia.PRV_DES,
                    Value = _provincia.PRV_ID.ToString()
                });
            }
            return LstProvincia;

        }

        public static string getProvincia(int provincia)
        {
            return ProvinciaDAO.getProvinciaDESC(provincia);
        }
    }
}

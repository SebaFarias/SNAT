
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
    public class comunaControl
    {
        private static comunaControl instancia;


        public comunaControl()
        {
            LstComuna = new List<SelectListItem>();
        }

        public comunaControl(int Region)
        {
            CargarLista(Region);
        }

        public IList<SelectListItem> LstComuna { get; set; }

        public static comunaControl Instancia
        {
            get
            {
                instancia = new comunaControl();

                return instancia;
            }
        }

        public static int obtenerIdComuna(long idRegion, string nombreCom)
        {
            try
            {
                return comunaDAO.obtenerIdComuna(idRegion, nombreCom);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public static List<SelectListItem> CargarLista(int provincia)
        {
            List<SelectListItem>  LstComuna = new List<SelectListItem>();
            foreach (COMUNA _comuna in comunaDAO.obtenerComunas(provincia))
            {
                LstComuna.Add(new SelectListItem
                {
                    Text = _comuna.COM_DES,
                    Value = (_comuna.COM_ID).ToString()
                });
            }

            return LstComuna;
        }

        public static string getComuna(int comuna)
        {
            COMUNA _comuna = comunaDAO.Obtener(comuna);

            return _comuna.COM_DES;
        }
    }
}

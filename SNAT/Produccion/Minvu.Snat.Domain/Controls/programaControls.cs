
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
    public class programaControl
    {
        private static programaControl instancia;


        

        public programaControl()
        {
            CargarLista();
        }

        public IList<SelectListItem> LstPrograma { get; set; }

        public static programaControl Instancia
        {
            get
            {
                instancia = new programaControl();

                return instancia;
            }
        }

        

        protected void CargarLista()
        {
            LstPrograma = new List<SelectListItem>();
            foreach (MAESTRO_PROGRAMA _maestroPrograma in maestroProgramaDAO.getList())
            {
                LstPrograma.Add(new SelectListItem
                {
                    Text = _maestroPrograma.NOMBREMAESTROPROGRAMA,
                    Value = (_maestroPrograma.IDMAESTROPROGRAMA).ToString()
                });
            }
        }

       
    }
}

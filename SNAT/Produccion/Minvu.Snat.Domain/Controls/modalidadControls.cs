
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
    public class modalidadControl
    {
        private static modalidadControl instancia;


       

        public modalidadControl()
        {
            CargarLista();
        }

        public IList<SelectListItem> LstModalidad { get; set; }

        public static modalidadControl Instancia
        {
            get
            {
                instancia = new modalidadControl();

                return instancia;
            }
        }


        protected void CargarLista()
        {
            LstModalidad = new List<SelectListItem>();
            foreach (MAESTRO_MODALIDAD _maestroModalidad in maestroModalidadDAO.GetList())
            {
                LstModalidad.Add(new SelectListItem
                {
                    Text = _maestroModalidad.NOMBREMAESTROMODALIDAD,
                    Value = (_maestroModalidad.IDMAESTROMODALIDAD).ToString()
                });
            }
        }

        public static string getModalidad(long? modalidad)
        {
            MAESTRO_MODALIDAD _maestroModalidad = maestroModalidadDAO.get(modalidad);

            return _maestroModalidad.NOMBREMAESTROMODALIDAD;
        }
    }
}

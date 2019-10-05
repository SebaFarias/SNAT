using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minvu.Snat.Site.Models
{

    public static class Enum
    {
        public static IEnumerable<SelectListItem> GetItems<T>()
        {

            return (System.Enum.GetValues(typeof(T)).Cast<int>().Select(e => new SelectListItem() { Selected = true, Text = System.Enum.GetName(typeof(T), e), Value = e.ToString() })).ToList();

        }
    }
    public class ProyectosModel
    {
        public long? CodProyecto { get; internal set; }
    }

    //public partial class ProyectosModel
    //{
    //    public int CodProyecto { get; set; }
    //    [Key]
    //    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    //    public string NumCertificado { get; set; }
    //    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    //    public string NombreProyecto { get; set; }
    //    public string Titulo { get; set; }
    //    public int ProfesionalFTO { get; set; }
    //    public int ProfesionalFTOReemplazo { get; set; }

    //}
}
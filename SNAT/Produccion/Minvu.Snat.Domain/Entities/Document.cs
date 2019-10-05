using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class Document
    {
        public string Nombre_Documento { get; set; }
        public string Publicado { get; set; }
        public string Tipo_Documento { get; set; }
        public string Ruta { get; set; }

        public Document()
        {

        }
        public Document(string _Nombre_Documento, string _Publicado, string _Tipo_Documento, string _Ruta)
        {
            Nombre_Documento = _Nombre_Documento;
            Publicado = _Publicado;
            Tipo_Documento = _Tipo_Documento;
            Ruta = _Ruta;
        }
    }
}

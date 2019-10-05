using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class direccionEntities
    {
        public long idDireccion { get; set; }

        [Display(Name = "Número dirección:")]
        [Required(ErrorMessage = "El número dirección es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string numeroDireccion { get; set; }

        [Display(Name = "Comuna:")]
        [Required(ErrorMessage = "La comuna es obligatoria")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreComuna { get; set; }

        [Display(Name = "Región:")]
        [Required(ErrorMessage = "El número dirección es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreRegion { get; set; }

        [Display(Name = "Provincia:")]
        [Required(ErrorMessage = "El número dirección es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreProvincia { get; set; }
        public long codigoComunaDireccion { get; set; }
        public long codigoProvinciaDireccion { get; set; }
        public long codigoRegionDireccion { get; set; }


        public direccionEntities(string _nombreComuna, string _nombreRegion,string _nombreProvincia, int _idDireccion, string _numeroDireccion,  int _codigoComunaDireccion, int _codigoProvinciaDireccion , int _codigoRegionDireccion)
        {
            nombreComuna = _nombreComuna;
            nombreRegion = _nombreRegion;
            nombreProvincia = _nombreProvincia;
            idDireccion = _idDireccion;
            numeroDireccion = _numeroDireccion;
            codigoComunaDireccion = _codigoComunaDireccion;
            codigoProvinciaDireccion = _codigoProvinciaDireccion;
            codigoRegionDireccion = _codigoRegionDireccion;
        }
        public direccionEntities()
        {
            nombreComuna = string.Empty;
            nombreRegion = string.Empty;
            nombreProvincia = string.Empty;
            idDireccion = 0;
            numeroDireccion = string.Empty;
            codigoComunaDireccion = 0;
            codigoProvinciaDireccion = 0;
            codigoRegionDireccion = 0;
        }
    }

    public class direccionEntitiesFactory
    {
        internal static direccionEntities getDireccion(long idDireccion)
        {
            var _direccionDAO = direccionDAO.Get(idDireccion);
            if (_direccionDAO != null)
            {
                return new direccionEntities
                {
                    idDireccion = Convert.ToInt64(_direccionDAO.IDDIRECCION),
                    numeroDireccion = _direccionDAO.NUMERODIRECCION,
                    codigoComunaDireccion = Convert.ToInt32(_direccionDAO.CODIGOCOMUNADIRECCION),
                    codigoProvinciaDireccion = Convert.ToInt32(_direccionDAO.CODIGOPROVINCIADIRECCION),
                    codigoRegionDireccion = Convert.ToInt32(_direccionDAO.CODIGOREGIONDIRECCION)
                };
            }
            else
                return null;
        }
    }
}

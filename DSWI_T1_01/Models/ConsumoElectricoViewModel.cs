using System.ComponentModel.DataAnnotations;

namespace DSWI_T1_01.Models
{
    public class ConsumoElectricoViewModel
    {
        [Required(ErrorMessage = "Ingrese los kWh consumidos.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El consumo debe ser mayor a 0.")]
        [Display(Name = "Consumo (kWh)")]
        public double KwhConsumidos { get; set; }

        public decimal Subtotal { get; set; }
        public decimal PorcentajeImpuesto { get; set; }
        public decimal MontoImpuesto { get; set; }
        public decimal TotalPagar { get; set; }
        public bool Calculado { get; set; }
    }
}
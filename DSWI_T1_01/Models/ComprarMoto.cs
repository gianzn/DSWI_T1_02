namespace DSWI_T1_01.Models
{
    public class ComprarMoto
    {
        public string Dia { get; set; }
        public int PorcentajeDescuento { get; set; }
        public decimal ImporteDescuento { get; set; }
        public decimal TotalAPagar { get; set; }

        public ComprarMoto(string dia, int porcentajeDescuento, decimal precioMoto)
        {
            Dia = dia;
            PorcentajeDescuento = porcentajeDescuento;
            ImporteDescuento = precioMoto * porcentajeDescuento / 100;
            TotalAPagar = precioMoto - ImporteDescuento;
        }
    }
}

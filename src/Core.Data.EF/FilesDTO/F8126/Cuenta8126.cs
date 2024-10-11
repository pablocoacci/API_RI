namespace Core.Data.EF.FilesDTO.F8126
{
    public class Cuenta8126 : IConcatenar
    {
        public string TipoDeRegistro { get; private set; } = "02";
        public string TipoDeCuenta { get; set; }
        public string IdentificadorCuenta { get; set; }
        public int CantidadRegistros {  get; set; }
        public DateTime FechaAltaCuenta { get; set; }
        public string TipoOperacion { get; set; }
        public DateTime FechaMod { get; set; }
        public int SignoSaldoPesos { get; set; }
        public int SaldoPesos { get; set; }
        public int SignoSaldoMonedaExtranjera { get; set; }
        public int SaldoMonedaExtranjera { get; set; } 
        public int SignoSaldoMonedaDigital { get; set; }
        public int SaldoMonedaDigital { get;set; }
        public int? CVU { get; set; }

        public string Concatenar()
            => $"{TipoDeRegistro}{TipoDeCuenta}{IdentificadorCuenta.PadRight(100, ' ')}{CantidadRegistros.ToString().PadLeft(2, '0')}{FechaAltaCuenta.ToString("yyyyMMdd")}{TipoOperacion}{FechaMod.ToString("yyyyMMdd")}{SignoSaldoPesos}{SaldoPesos.ToString().PadLeft(12, '0')}{SignoSaldoMonedaExtranjera}{SaldoMonedaExtranjera.ToString().PadLeft(12, '0')}{SignoSaldoMonedaDigital}{SaldoMonedaDigital.ToString().PadLeft(12, '0')}{GetCVU()}";

        private string GetCVU()
            => CVU.HasValue ? CVU.Value.ToString() : "".PadRight(14, ' ');
    }
}

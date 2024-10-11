namespace Core.Data.EF.FilesDTO.F8126
{
    public class MovimientosMensuales8126 : IConcatenar
    {
        public string TipoDeRegistro { get; private set; } = "04";
        public string TipoOperacion { get; set; }
        public string TipoMovimiento { get; set; }
        public int MonedaOriginal { get; set; }
        public int MontoMensualMonedaOriginal { get; set; }
        public int MontoMensualMonedaPesos { get; set; }

        public string Concatenar()
            => $"{TipoDeRegistro}{TipoOperacion}{TipoMovimiento}{MonedaOriginal}{MontoMensualMonedaOriginal.ToString().PadLeft(12, '0')}{MontoMensualMonedaPesos.ToString().PadLeft(12, '0')}";
    }
}

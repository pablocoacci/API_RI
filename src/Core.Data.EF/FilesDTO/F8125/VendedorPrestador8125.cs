namespace Core.Data.EF.FilesDTO.F8125
{
    public class VendedorPrestador8125 : IConcatenar
    {
        public string TipoDeRegistro { get; private set; } = "02";
        public string TipoIdentificadorVendedor { get; set; }
        public string IdentificadorVendedorPrestador { get; set; }
        public string CodigoRubro { get; set; }
        public int SignoMontoTotal {  get; set; }
        public int MontoTotalOperacionesPesos {  get; set; }
        public int ImporteComisionCobrada { get; set; }

        public string Concatenar()
            => $"{TipoDeRegistro}{TipoIdentificadorVendedor}{IdentificadorVendedorPrestador.Replace("-", "")}{CodigoRubro}{SignoMontoTotal}{MontoTotalOperacionesPesos.ToString().PadLeft(12, '0')}{ImporteComisionCobrada.ToString().PadLeft(12, '0')}";
    }
}

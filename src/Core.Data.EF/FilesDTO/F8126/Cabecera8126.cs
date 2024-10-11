namespace Core.Data.EF.FilesDTO.F8126
{
    public class Cabecera8126 : IConcatenar
    {
        public string TipoDeRegistro { get; private set; } = "01";
        public string CuitInformante { get; set; }
        public int PeriodoInformado { get; set; }
        public string Secuencia { get; set; }
        public string Denominacion { get; set; }
        public int Hora { get; set; }
        public string CodigoDeImpuestos { get; private set; } = "0103";
        public string CodigoDeConcepto { get; private set; } = "812";
        public int NumeroVerificador { get; set; }
        public string NumeroDeFormulario { get; private set; } = "8126";
        public string NumeroVersionAplicativo { get; private set; } = "00200";
        public string Establecimiento { get; private set; } = "00";
        public int CantidadDeRegistros { get; set; }

        public string Concatenar()
            => $"{TipoDeRegistro}{CuitInformante.Replace("-", "")}{PeriodoInformado}{Secuencia}{Denominacion}{Hora}{CodigoDeImpuestos}{CodigoDeConcepto}{NumeroVerificador}{NumeroDeFormulario}{NumeroVersionAplicativo}{Establecimiento}{CantidadDeRegistros.ToString().PadLeft(10, '0')}";
    }
}

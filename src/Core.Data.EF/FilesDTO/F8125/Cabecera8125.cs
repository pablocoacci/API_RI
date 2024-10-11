namespace Core.Data.EF.FilesDTO.F8125
{
    public class Cabecera8125 : IConcatenar
    {
        public string TipoDeRegistro { get; private set; } = "01";
        public string Cuit { get; set; }
        public int PeriodoInformado { get; set; }
        public int Secuencia { get; set; }
        public string Denominacion { get;set; }
        public string Hora { get; set; }
        public string CodigoImpuesto { get; private set; } = "0103";
        public string CodigoConcepto { get; private set; } = "830";
        public int NumeroVerificador { get; set; }
        public string NumeroFormulario { get; private set; } = "8125";
        public string NumeroVersionAplicativo { get; private set; } = "00100";
        public string Establecimiento { get; private set; } = "00";
        public int CantidadRegistrosDetalle { get; set; }

        public string Concatenar()
            => $"{TipoDeRegistro}{Cuit.Replace("-", "")}{PeriodoInformado}{Secuencia.ToString().PadLeft(2, '0')}{Denominacion.PadRight(200, ' ')}{Hora}{CodigoImpuesto}{CodigoConcepto}{NumeroVerificador.ToString().PadLeft(6, '0')}{NumeroFormulario}{NumeroVersionAplicativo}{Establecimiento}{CantidadRegistrosDetalle.ToString().PadLeft(10, '0')}";
    }
}

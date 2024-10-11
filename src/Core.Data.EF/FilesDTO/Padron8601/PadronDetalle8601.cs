namespace Core.Data.EF.FilesDTO.Padron8601
{
    public class PadronDetalle8601 : IConcatenar
    {
        public string CodigoDiseño { get; private set; } = "8601";
        public int CodigoEntidad {  get; set; }
        public int FechaInformacion { get; set; }
        public string TipoIdentificacionTributaria { get; set; }
        public string NumeroIdentificacionTributaria { get; set; }
        public string TipoIdentificacionPersonal { get; set; }
        public string NumeroIdentificacoinPersonal { get; set; }
        public string Denominacion { get; set; }
        public int CondicionClientePep { get; set; }
        public string CodigoPostal {  get; set; }
        public int TipoMovimiento {  get; set; }
        public string Rectificativa { get; set; }

        public string Concatenar()
            => $"{CodigoDiseño}{CodigoEntidad}{FechaInformacion}{TipoIdentificacionTributaria}{NumeroIdentificacionTributaria.PadRight(11, ' ')}{TipoIdentificacionPersonal}{NumeroIdentificacoinPersonal.PadRight(16, ' ')}{Denominacion.PadRight(80, ' ')}{CondicionClientePep}{CodigoPostal}{TipoMovimiento}{Rectificativa}";
    }
}

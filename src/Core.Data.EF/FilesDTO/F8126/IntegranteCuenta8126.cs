namespace Core.Data.EF.FilesDTO.F8126
{
    public class IntegranteCuenta8126 : IConcatenar
    {
        public string TipoDeRegistro { get; private set; } = "03";
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nif { get;set; }
        public string ApellidoNombre { get; set; }
        public int TipoPersona { get; set; }
        public int Residencia { get; set; }
        public string PaisResidencia { get; set; }
        public int Caracter { get; set; }

        public string Concatenar()
            => $"{TipoDeRegistro}{GetTipoDocumento()}{GetNroDocumento()}{GetNif()}{ApellidoNombre.PadRight(60, ' ')}{TipoPersona}{Residencia}{Caracter.ToString().PadLeft(2, '0')}";

        private string GetTipoDocumento()
            => string.IsNullOrEmpty(TipoDocumento) ? "".PadRight(2, ' ') : TipoDocumento;

        private string GetNroDocumento()
            => string.IsNullOrEmpty(NumeroDocumento) ? "".PadRight(50,' ') : NumeroDocumento.PadRight(50,' ');

        private string GetNif()
            => string.IsNullOrEmpty(Nif) ? "".PadRight(20,' ') : Nif.PadRight(20,' ');

    }
}

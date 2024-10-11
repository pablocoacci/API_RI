namespace Core.Data.EF.FilesDTO.F8126
{
    public class DetalleMovimientoTransferenciaBancaria : IConcatenar
    {
        public string TipoDeRegistro { get; private set; } = "05";
        public int CBU { get; set; }
        public int MontoPesos { get; set; }

        public string Concatenar()
            => $"{TipoDeRegistro}{CBU.ToString().PadLeft(22, '0')}{MontoPesos.ToString().PadLeft(12, '0')}";
    }
}

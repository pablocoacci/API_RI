using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.EF.FilesDTO.F8125
{
    public class DetalleOperacion8125 : IConcatenar
    {
        public string TipoDeRegistros { get; private set; } = "03";
        public string MetodologiaAcreditacoines { get; set; }
        public string TipoCuenta { get; set; }
        public int NumeroIdentificacion { get; set; }
        public int SignoMonto { get; set; }
        public int MontoPesos { get; set; }

        public string Concatenar()
            => $"{TipoDeRegistros}{TipoCuenta}{NumeroIdentificacion.ToString().PadLeft(22, '0')}{SignoMonto}{MontoPesos.ToString().PadLeft(12, '0')}";
    }
}

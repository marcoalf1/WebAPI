using System;

namespace WebAPI.Core.Models.Helpers
{
    public class AuditEntity
    {
        public DateTime? CreadoFecha { get; set; }
        public string CreadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }
        public string EliminadoPor { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
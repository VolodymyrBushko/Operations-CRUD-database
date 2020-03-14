namespace VolodEF.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vacation")]
    public partial class Vacation
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idEmployee { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime dtStart { get; set; }

        public int? duration { get; set; }

        public virtual Employee Employee { get; set; }
    }
}

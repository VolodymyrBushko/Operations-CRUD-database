namespace VolodEF.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Salary")]
    public partial class Salary
    {
        public int id { get; set; }

        public int? amount { get; set; }

        public int? idPosition { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dtSalary { get; set; }

        public virtual Position Position { get; set; }
    }
}

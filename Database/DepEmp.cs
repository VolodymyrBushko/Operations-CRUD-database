namespace VolodEF.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DepEmp")]
    public partial class DepEmp
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idDepartament { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idEmployee { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dt { get; set; }

        public int? idPosition { get; set; }

        public virtual Department Departament { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Position Position { get; set; }
    }
}

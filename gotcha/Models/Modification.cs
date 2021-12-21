//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gotcha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Modification
    {
        public Modification()
        {
            this.Processing = new HashSet<Processing>();
        }
    
        public int id_mod { get; set; }
        [Required]
        [StringLength(30)]
        public string nameMod { get; set; }
        [Required]
        [Range(typeof(double), "0.01", "2.00")]
        public Nullable<double> LabourInput { get; set; }
    
        public virtual ICollection<Processing> Processing { get; set; }
    }
}
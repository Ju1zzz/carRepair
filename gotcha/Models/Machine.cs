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

    public partial class Machine
    {
        public Machine()
        {
            this.Draft = new HashSet<Draft>();
        }
    
        public int id_mach { get; set; }
        public int fk_id_guild { get; set; }
        [Required]
        [StringLength(40)]
        public string firmMach { get; set; }
        [Required]
        [Range(typeof(double), "0.01", "5.00")]
        public Nullable<double> timeProcessing { get; set; }
    
        public virtual ICollection<Draft> Draft { get; set; }
        public virtual Guild Guild { get; set; }
    }
}

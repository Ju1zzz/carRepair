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

    public partial class Part
    {
        public Part()
        {
            this.Draft = new HashSet<Draft>();
            this.Processing = new HashSet<Processing>();
        }
    
        public int id_part { get; set; }
        [Required]
        [StringLength(30)]
        public string namePart { get; set; }
        [Required]
        [StringLength(40)]
        public string firmPart { get; set; }
    
        public virtual ICollection<Draft> Draft { get; set; }
        public virtual ICollection<Processing> Processing { get; set; }
    }
}

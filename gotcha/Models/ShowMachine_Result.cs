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
    
    public partial class ShowMachine_Result
    {
        public int id_mach { get; set; }
        public int fk_id_guild { get; set; }
        public string firmMach { get; set; }
        public Nullable<double> timeProcessing { get; set; }
    }
}

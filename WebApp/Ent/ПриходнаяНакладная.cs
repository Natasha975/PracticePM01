//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.Ent
{
    using System;
    using System.Collections.Generic;
    
    public partial class ПриходнаяНакладная
    {
        public int IDНакладной { get; set; }
        public string НомерНакладной { get; set; }
        public System.DateTime ДатаПоступления { get; set; }
        public int Поставщик { get; set; }
        public int СписокТоваров { get; set; }
        public decimal ОбщаяСумма { get; set; }
    
        public virtual Поставщики Поставщики { get; set; }
        public virtual ТоварВНакладной ТоварВНакладной { get; set; }
    }
}

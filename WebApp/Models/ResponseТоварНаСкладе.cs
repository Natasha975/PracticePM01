using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebApp.Ent;

namespace WebApp.Models
{
    public class ResponseТоварНаСкладе
    {
        //public ResponseТоварНаСкладе(ТоварНаСкладе товарНаСкладе)
        //{
        //    НомерЗаписи = товарНаСкладе.НомерЗаписи;
        //    НомерТовара = товарНаСкладе.НомерТовара;
        //    НомерСклада = товарНаСкладе.НомерСклада;
        //    Количество = товарНаСкладе.Количество;
        //}

        //public int НомерЗаписи { get; set; }
        //public int НомерТовара { get; set; }
        //public int НомерСклада { get; set; }

        public string НазваниеСклада { get; set; }
        public string НазваниеТовара { get; set; }
        public int Количество { get; set; }
        //public int Количество { get; set; }

        //public virtual Склад Склад { get; set; }
        //public virtual Товар Товар { get; set; }
    }
}
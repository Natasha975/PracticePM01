using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Ent;

namespace WebApp.Models
{
    public class ResponseСклад
    {
        public ResponseСклад(Склад склад)
        {
            Номер = склад.Номер;
            НазваниеСклада = склад.НазваниеСклада;
            Адрес = склад.Адрес;
            ТипСклада = склад.ТипCклад.Номер;
            ЗоныХранения = склад.ЗоныХранения;
        }
        public int Номер { get; set; }
        public string НазваниеСклада { get; set; }
        public int Адрес { get; set; }
        public int ТипСклада { get; set; }
        public Nullable<int> ЗоныХранения { get; set; }
    }
}
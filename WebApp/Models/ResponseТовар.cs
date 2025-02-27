using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Ent;

namespace WebApp.Models
{
    public class ResponseТовар
    {
        public ResponseТовар(Товар товар)
        {
            Номер = товар.Номер;
            Название = товар.Название;
            Артикул = товар.Артикул;
            Штрихкод = товар.Штрихкод;
            Категория = товар.Категория;
            ЕдиницаИзмерения = товар.ЕдиницаИзмерения;
            ЦенаЗаЕдиницу = товар.ЦенаЗаЕдиницу;
            СерийныйНомер = товар.СерийныйНомер;
            МинимальныйОстаток = товар.МинимальныйОстаток;
        }

        public int Номер { get; set; }
        public string Название { get; set; }
        public string Артикул { get; set; }
        public string Штрихкод { get; set; }
        public string Категория { get; set; }
        public string ЕдиницаИзмерения { get; set; }
        public decimal ЦенаЗаЕдиницу { get; set; }
        public Nullable<int> СерийныйНомер { get; set; }
        public int МинимальныйОстаток { get; set; }
    }
}
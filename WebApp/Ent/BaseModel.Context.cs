﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WarehouseEntities : DbContext
    {
        public WarehouseEntities()
            : base("name=WarehouseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Адрес> Адрес { get; set; }
        public virtual DbSet<ЗоныХранения> ЗоныХранения { get; set; }
        public virtual DbSet<Инвентаризация> Инвентаризация { get; set; }
        public virtual DbSet<Клиент> Клиент { get; set; }
        public virtual DbSet<Пользователь> Пользователь { get; set; }
        public virtual DbSet<Поставщики> Поставщики { get; set; }
        public virtual DbSet<ПриходнаяНакладная> ПриходнаяНакладная { get; set; }
        public virtual DbSet<РасходнаяНакладная> РасходнаяНакладная { get; set; }
        public virtual DbSet<Роль> Роль { get; set; }
        public virtual DbSet<Склад> Склад { get; set; }
        public virtual DbSet<ТипCклад> ТипCклад { get; set; }
        public virtual DbSet<Товар> Товар { get; set; }
        public virtual DbSet<ТоварВНакладной> ТоварВНакладной { get; set; }
        public virtual DbSet<ТоварНаСкладе> ТоварНаСкладе { get; set; }
    }
}

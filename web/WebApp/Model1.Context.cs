﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WebApp
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class hacaEntities : DbContext
{
    public hacaEntities()
        : base("name=hacaEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }

    public virtual DbSet<Alerts> Alerts { get; set; }

    public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

    public virtual DbSet<DeviceGIO> DeviceGIO { get; set; }

    public virtual DbSet<Devices> Devices { get; set; }

    public virtual DbSet<DevLayout> DevLayout { get; set; }

    public virtual DbSet<LayoutSettings> LayoutSettings { get; set; }

    public virtual DbSet<Stuff> Stuff { get; set; }

    public virtual DbSet<stuff_Has_Device> stuff_Has_Device { get; set; }

    public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

}

}


﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SloVVo.Data.Models;

namespace SloVVo.Data.Mappings
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping() : this("dbo")
        {
            
        }

        public UserMapping(string schema)
        {
            ToTable("Users", schema);
            HasKey(item => item.UserId);

            Property(item => item.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired()
                .HasColumnType("int");

            Property(item => item.Forename)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnType("nvarchar");

            Property(item => item.Surname)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnType("nvarchar");

            Property(item => item.Telephone)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsOptional()
                .HasColumnType("int");

            Property(item => item.Address)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnType("nvarchar");

            Property(item => item.Location)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired()
                .HasColumnType("nvarchar");
        }
    }
}

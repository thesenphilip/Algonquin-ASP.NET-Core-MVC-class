﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab5.Models;
using Microsoft.EntityFrameworkCore;


namespace lab5.Data
{
    public class AnswerImageDataContext : DbContext
    {
        public AnswerImageDataContext(DbContextOptions<AnswerImageDataContext> options) : base(options)
        {

        }

        public DbSet<AnswerImage> AnswerImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnswerImage>().ToTable("AnswerImage");
        }
    }

}

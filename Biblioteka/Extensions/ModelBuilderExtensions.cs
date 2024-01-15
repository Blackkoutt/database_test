using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Biblioteka.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author()
                {
                    authorId=1,
                    name="Henryk",
                    surname="Sienkiewicz",
                    birthDate=new DateTime(1846, 5, 5),
                    country="Polska",
                    nickname="Litwos",
                    description="opis"
                }
           );
        }
    }
}

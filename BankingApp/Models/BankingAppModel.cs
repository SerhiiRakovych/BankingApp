namespace BankingApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;



    class AppBankingContextInitializer : DropCreateDatabaseAlways<BankingAppModel>
    {
        protected override void Seed(BankingAppModel db)
        {
            IList<Client> defClients = new List<Client>();

            defClients.Add(new Client() { FirstName = "Роман", LastName = "Поддубный", MiddleName = "Владимирович", DOB = Convert.ToDateTime("12/04/1988")});
            defClients.Add(new Client() { FirstName = "Иван", LastName = "Дикой", MiddleName = "Иванович", DOB = Convert.ToDateTime("03/12/1964") });
            defClients.Add(new Client() { FirstName = "Сергей", LastName = "Савин", MiddleName = "Михайлович", DOB = Convert.ToDateTime("02/10/1991") });
            defClients.Add(new Client() { FirstName = "Ростислав", LastName = "Смирнов", MiddleName = "Сергеевич", DOB = Convert.ToDateTime("25/02/1986") });
            defClients.Add(new Client() { FirstName = "Ирина", LastName = "Соколова", MiddleName = "Петровна", DOB = Convert.ToDateTime("13/07/1971") });

            db.Clients.AddRange(defClients);
            base.Seed(db);
        }
    }

    public partial class BankingAppModel : DbContext
    {
        public BankingAppModel(): base("name=BankingApp")
        {
            Database.SetInitializer<BankingAppModel>(new AppBankingContextInitializer());
        }


        public virtual DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

    }

    public class Client
    {
        public int Id { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
    }
}

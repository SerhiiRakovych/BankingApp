namespace BankingApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    class AppBankingContextInitializer : DropCreateDatabaseAlways<BankingAppModel>
    {
        protected override void Seed(BankingAppModel db)
        {
            IList<Client> defClients = new List<Client>();

            defClients.Add(new Client() { FirstName = "�����", LastName = "���������", MiddleName = "������������", DOB = Convert.ToDateTime("12/04/1988"), UserName = "Romych", Password = "28D09B09BB65724D53A7DEE882DB20A9", ConfirmPassword = "28D09B09BB65724D53A7DEE882DB20A9", Salt = "d7c61aef98214704ae0deecb529e310c", isAdmin = true });
            defClients.Add(new Client() { FirstName = "����", LastName = "�����", MiddleName = "��������", DOB = Convert.ToDateTime("03/12/1964"), UserName = "Idikoy", Password = "28D09B09BB65724D53A7DEE882DB20A9", ConfirmPassword = "28D09B09BB65724D53A7DEE882DB20A9", Salt = "d7c61aef98214704ae0deecb529e310c", isAdmin = false });
            defClients.Add(new Client() { FirstName = "������", LastName = "�����", MiddleName = "����������", DOB = Convert.ToDateTime("02/10/1991"), UserName = "Sova", Password = "28D09B09BB65724D53A7DEE882DB20A9", ConfirmPassword = "28D09B09BB65724D53A7DEE882DB20A9", Salt = "d7c61aef98214704ae0deecb529e310c", isAdmin = false });
            defClients.Add(new Client() { FirstName = "���������", LastName = "�������", MiddleName = "���������", DOB = Convert.ToDateTime("25/02/1986"), UserName = "Smirna", Password = "28D09B09BB65724D53A7DEE882DB20A9", ConfirmPassword = "28D09B09BB65724D53A7DEE882DB20A9", Salt = "d7c61aef98214704ae0deecb529e310c", isAdmin = false });
            defClients.Add(new Client() { FirstName = "�����", LastName = "��������", MiddleName = "��������", DOB = Convert.ToDateTime("13/07/1971"), UserName = "Romych", Password = "28D09B09BB65724D53A7DEE882DB20A9", ConfirmPassword = "28D09B09BB65724D53A7DEE882DB20A9", Salt = "d7c61aef98214704ae0deecb529e310c", isAdmin = false });

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

        [Display(Name = "���")]
        [Required(ErrorMessage = "���������� ��������� ���� \"���\"")]
        public string FirstName { get; set; }

        [Display(Name = "�������")]
        [Required(ErrorMessage = "���������� ��������� ���� \"�������\"")]
        public string LastName { get; set; }

        [Display(Name = "��������")]
        [Required(ErrorMessage = "���������� ��������� ���� \"��������\"")]
        public string MiddleName { get; set; }

        [Display(Name = "���� ��������")]
        [Required(ErrorMessage = "���������� ��������� ���� \"���� ��������\"")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Display(Name = "�����")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "���������� ��������� ���� \"������\"")]
        [StringLength(100, ErrorMessage = "���� \"{0}\" ������ ��������� ������ {2} ��������", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "������")]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "�������������")]
        [Compare("Password", ErrorMessage = "��������� ������ �� ���������")]
        public string ConfirmPassword { get; set; }

        [StringLength(32, ErrorMessage = "���� \"{0}\" ������ ��������� ������ {2} ��������", MinimumLength = 32)]
        public string Salt { get; set; }

        public bool isAdmin { get; set; }
    }
}

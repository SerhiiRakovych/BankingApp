using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Models;
using System.Data.Entity;

namespace BankingApp
{
   public interface IRepository: IDisposable
    {
        List<Client> GetClientsList();
        Client GetClient(int id);
        void Create(Client item);
        void Update(Client item);
        void Delete(int id);
        void Save();
    }

   public class ClientRepository : IRepository
   {
       private BankingAppModel db;
    public ClientRepository()
    {
        this.db = new BankingAppModel();
    }
    public List<Client> GetClientsList()
    {
        return db.Clients.ToList();
    }
    public Client GetClient(int id)
    {
        return db.Clients.Find(id);
    }
  
    public void Create(Client c)
    {
        db.Clients.Add(c);
    }
  
    public void Update(Client c)
    {
        db.Entry(c).State = EntityState.Modified;
    }
  
    public void Delete(int id)
    {
        Client c = db.Clients.Find(id);
        if(c!=null)
            db.Clients.Remove(c);
    }
  
    public void Save()
    {
        db.SaveChanges ();
    }
  
    private bool disposed = false;
  
    public virtual void Dispose(bool disposing)
    {
        if(!this.disposed)
        {
            if(disposing)
            {
                db.Dispose();
            }
        }
        this.disposed = true;
    }
  
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
   } 
}

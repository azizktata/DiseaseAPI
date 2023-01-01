using Domain.Interfaces;
using DataAcess.TypeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAcess.UnitOfWork
{
    public  class UnitOfWork : IUnitOfWork
    {
        private diseaseDbContext context;
        public UnitOfWork(diseaseDbContext context)
        {
            this.context = context;
            Admin = new AdminRepository(this.context);
            User = new UserRepository(this.context);
            Disease = new DiseaseRepository(this.context);
            Article = new ArticleRepository(this.context);
            Doctor_Info = new Doctor_infoRepository(this.context);

        }

        public IAdminRepository Admin { get; private set; }

        public IUserRepository User { get; private set; }
        public IArticleRepository Article { get; private set; }
        public IDoctor_infoRepository Doctor_Info { get; private set; }
        public IDiseaseRepository Disease { get; private set; }

        public void Dispose()
        {
            context.Dispose();
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}

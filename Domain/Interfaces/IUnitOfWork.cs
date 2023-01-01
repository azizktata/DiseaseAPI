using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminRepository Admin { get; }
        IUserRepository User { get; }
        IDiseaseRepository Disease { get; }
        IArticleRepository Article { get; }
        IDoctor_infoRepository Doctor_Info { get; }

        int Save();
    }
}

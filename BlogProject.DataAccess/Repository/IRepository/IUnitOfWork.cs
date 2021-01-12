using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject12.DataAccess.Repository.IRepository
{
   public interface IUnitOfWork:IDisposable
    {
        IUserRepository User { get; }
        ISP_Call SP_Call { get; }

    }
}

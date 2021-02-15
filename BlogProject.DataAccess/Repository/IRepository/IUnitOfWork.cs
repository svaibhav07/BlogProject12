using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject12.DataAccess.Repository.IRepository
{
   public interface IUnitOfWork:IDisposable
    {
        IUserRepository User { get; }


        ITagRepository Tag { get; }
        IBlogRepository Blog { get; }

        IPaymentRepository Payment { get; }
        
        IPaymentsRepository Payments { get; }
        ISP_Call SP_Call { get; }
        void Save();
    }
}

using BlogProject12.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject12.DataAccess.Repository.IRepository
{
    public interface IPaymentsRepository : IRepository<Payments1>
    {
        void Update(Payments1 payments1);

    }
}
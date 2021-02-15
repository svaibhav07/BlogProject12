using BlogProject.Models;
using BlogProject12.DataAccess.Data;
using BlogProject12.DataAccess.Repository.IRepository;
using BlogProject12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogProject12.DataAccess.Repository
{
    public class PaymentsRepository:Repository<Payments1>, IPaymentsRepository
    {
        private readonly ApplicationDbContext _db;

        public PaymentsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public void Update(Payments1 payments)
        {
            var objFromDb = _db.Payments1.FirstOrDefault(s => s.PaymentId == payments.PaymentId);
            if (objFromDb != null)
            {
                objFromDb.Status = payments.Status;

                _db.SaveChanges();

            }
        }
    }
}

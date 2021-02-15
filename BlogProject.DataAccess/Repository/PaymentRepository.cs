using BlogProject12.DataAccess.Data;
using BlogProject12.DataAccess.Repository.IRepository;
using BlogProject12.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject12.DataAccess.Repository
{
   public class PaymentRepository:Repository<PaymentInitiateModel>, IPaymentRepository
    {
       
            private readonly ApplicationDbContext _db;

            public PaymentRepository(ApplicationDbContext db) : base(db)
            {
                _db = db;

            }

        }
    }

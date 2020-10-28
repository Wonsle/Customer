using System;
using System.Linq;
using System.Collections.Generic;

namespace Customer.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(q => q.是否已刪除 == false);
        }
        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = false;
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}
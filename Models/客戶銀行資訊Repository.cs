using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Customer.Models
{
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(q => q.是否已刪除 == false);
        }
        public override void Delete(客戶銀行資訊 entity)
        {
            entity.是否已刪除 = true;
        }

        public 客戶銀行資訊 FindById(int id)
        {
            return All().FirstOrDefault(q => q.Id == id);
        }
        public CustomFile GetXLSXReport()
        {
            //https://dotblogs.com.tw/jennifer0201/2018/06/25/120535
            var wb = new XSLXHelper().Export<客戶銀行資訊>(All().OrderBy(q => q.Id));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return new CustomFile()
                {
                    FileContents = memoryStream.ToArray(),
                    ContentType = "application/vnd.ms-excel",
                    FileName = "Report.xlsx"
                };
            }
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}
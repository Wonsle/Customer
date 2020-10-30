using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Customer.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(q => q.是否已刪除 == false);
        }
        public override void Delete(客戶聯絡人 entity)
        {

            entity.是否已刪除 = true;
        }
        public 客戶聯絡人 FindById(int? id)
        {
            return All().FirstOrDefault(q => q.Id == id);
        }
        public override void Add(客戶聯絡人 entity)
        {
            int count = All().Where(q => q.客戶Id == entity.客戶Id).Where(q => q.Email == entity.Email).Count(); 
            if (count == 0)
                base.Add(entity);

        }
        public CustomFile GetXLSXReport()
        {
            //https://dotblogs.com.tw/jennifer0201/2018/06/25/120535
            var wb = new XSLXHelper().Export<客戶聯絡人>(All().OrderBy(q => q.Id));

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

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}
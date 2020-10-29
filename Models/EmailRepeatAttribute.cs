using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Customer.Models
{
    public class EmailRepeatAttribute : DataTypeAttribute
    {
        public EmailRepeatAttribute() : base(DataType.EmailAddress)
        {
            ErrorMessage = "請輸入Email";
        }
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            string EmailAddress = value.ToString();
            return true;
            
        }
        
    }
}
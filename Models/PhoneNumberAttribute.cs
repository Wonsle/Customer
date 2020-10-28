using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Customer.Models
{
    public class PhoneNumberAttribute : DataTypeAttribute
    {
        public PhoneNumberAttribute() : base(DataType.PhoneNumber)
        {
            ErrorMessage = "請輸入09xx-xxxxxx";
        }
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            string phoneNumber = value.ToString();            
            return Regex.IsMatch(phoneNumber, @"^09[0-9]{2}-[0-9]{6}$"); ;
        }
    }
}
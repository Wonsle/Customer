using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Customer.Models
{
	//https://dotblogs.com.tw/jennifer0201/2018/06/25/120535
	public interface IXSLXHelper
	{
		/// <summary>
		/// 匯出Excel
		/// </summary>
		/// <typeparam name="T">Model</typeparam>
		/// <param name="data">匯出資料</param>
		/// <returns>XLWorkbook</returns>
		XLWorkbook Export<T>(IEnumerable<T> data);
	}
	/// <summary>
	/// XSLXHelper
	/// </summary>
	public class XSLXHelper : IXSLXHelper
	{
		/// <summary>
		/// 產生excel
		/// </summary>
		/// <typeparam name="T">傳入的物件型別</typeparam>
		/// <param name="data">物件資料集</param>
		/// <returns>XLWorkbook</returns>
		public XLWorkbook Export<T>(IEnumerable<T> data)
		{
			////建立 excel 物件
			XLWorkbook workbook = new XLWorkbook();
			////加入 excel 工作表名為 "Report"
			var sheet = workbook.Worksheets.Add("Report");
			////欄位起始位置
			int colIdx = 1;
			////使用 reflection 將物件屬性取出當作工作表欄位名稱(列表名稱)
			foreach (var item in typeof(T).GetProperties())
			{
				#region - 可以使用 DescriptionAttribute 設定，找不到 DescriptionAttribute 時改用屬性名稱 -
				////可以使用 DescriptionAttribute 設定，找不到 DescriptionAttribute 時改用屬性名稱
				DescriptionAttribute description = item.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
				if (description != null)
				{
					sheet.Cell(1, colIdx++).Value = description.Description;
					continue;
				}
				else
				{
					sheet.Cell(1, colIdx++).Value = item.Name;
				}
				#endregion

				#region - 直接使用物件屬性名稱
				////直接使用物件屬性名稱
				//sheet.Cell(1, colIdx++).Value = item.Name;
				#endregion

			}

			////資料起始列位置(列表資料)
			int rowIdx = 2;
			foreach (var item in data)
			{
				////每筆資料欄位起始位置
				int conlumnIndex = 1;
				foreach (var content in item.GetType().GetProperties())
				{
					////將資料內容加上 "'" 避免受到 excel 預設格式影響，並依 row 及 column 填入
					sheet.Cell(rowIdx, conlumnIndex).Value = string.Concat("'", Convert.ToString(content.GetValue(item, null)));
					conlumnIndex++;
				}
				rowIdx++;
			}

			return workbook;
		}
	}
}
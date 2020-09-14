using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace ProjectTracking.Data
{

    public static class Shared
    {
        public static DataTable ToDataTable<T>(this IList<T> data, List<string> includedProperties = null)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            if (includedProperties != null)
            {
                includedProperties = includedProperties.Select(k => k.ToLower()).ToList();
            }

            List<int> propIndexes = new List<int>();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];

                //Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                string name = Regex.Replace(prop.Name, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");

                //if (t.GetType() == typeof(bool))
                //{
                //    t = ;
                //}

                if (includedProperties != null)
                {

                    if (includedProperties.Contains(prop.Name.ToLower()))
                    {
                        propIndexes.Add(i);
                        table.Columns.Add(name);
                    }
                }
                else
                {
                    table.Columns.Add(name);
                }
            }

            object[] values = new object[propIndexes.Count];


            string handleValueDisplay(string val)
            {
                if (val.ToLower() == "false" || val.ToLower() == "true")
                {
                    val = val.ToLower() == "true" ? "Yes" : "No";
                }

                return val;
            }

            foreach (T item in data)
            {
                int index = 0;
                for (int i = 0; i < props.Count; i++)
                {
                    if (includedProperties != null)
                    {
                        if (propIndexes.Contains(i))
                        {
                            values[index++] = handleValueDisplay(props[i].GetValue(item).ToString());
                        }
                    }
                    else
                    {
                        values[index++] = handleValueDisplay(props[i].GetValue(item).ToString());
                    }
                }

                table.Rows.Add(values);
            }

            return table;
        }

        public static byte[] ExportToCSV(DataTable dataTable)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Put whatever you want here in the sheet
                // For example, for cell on row1 col1
                //worksheet.Cells[1, 1].Value = "Long text";

                //worksheet.Cells[1, 1].Style.Font.Size = 12;
                //worksheet.Cells[1, 1].Style.Font.Bold = true;

                worksheet.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

                worksheet.Cells.AutoFitColumns();

                // Finally when you're done, export it to byte array.
                byte[] arr = package.GetAsByteArray();


                return arr;
            }
        }
    }
}

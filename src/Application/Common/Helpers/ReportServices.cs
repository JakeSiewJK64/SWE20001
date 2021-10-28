using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CleanArchitecture.Application.Common.Helpers
{
    public class ReportServices
    {
        public static (byte[], string) GenerateCSVReport(List<Item> itemDtoList, List<SalesDto> salesDtoList, DateTime reportDate)
        {
            var fileName = $"SalesReport({reportDate.ToString("MMM-yyyy")}).csv";

            if (File.Exists(fileName)) File.Delete(fileName);

            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.Write("SalesRecordId,Remarks,SalesDate");

                foreach (Item i in itemDtoList)
                {
                    sw.Write("," + i.ItemName);
                }
                sw.WriteLine();
                foreach (SalesDto s in salesDtoList)
                {
                    sw.Write(s._salesRecordId + "," + s._remarks + "," + s._salesDate + ",");
                    foreach (Item d in itemDtoList)
                    {
                        int tempQuantity = 0;
                        foreach (SalesItemListDto i in s._salesItemList)
                        {
                            if (i.ItemId.Equals(d.ItemId)) tempQuantity = i.Quantity;
                        }
                        sw.Write(tempQuantity + ",");
                    }
                    sw.WriteLine();
                }
                sw.Close();
            }
            return (File.ReadAllBytes(fileName), fileName);
        }
    }
}

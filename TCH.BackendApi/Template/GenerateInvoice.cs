// using System;
// using System.IO;
// using System.Linq;
// using System.Text;
// using System.Xml;
// using System.Xml.Xsl;
// using TCH.BackendApi.EF;
// using TCH.BackendApi.Entities;
// using TCH.BackendApi.Models.Error;
//
// namespace Service_Payment.Template
// {
//     public static class GenerateInvoice
//     {
//         public static string HtmlRender(Order invoice, string Username)
//         {
//             try
//             {
//                 string newPathXML = Path.Combine(Directory.GetCurrentDirectory(), "Template", "DefaultTemplate", "DefaultTemplate.xml");
//                 string newPathXSLT = Path.Combine(Directory.GetCurrentDirectory(), "Template", "DefaultTemplate", "DefaultTemplate.xslt");
//                 string DinnerTableName = null;
//                 using (var _context = new APIContext())
//                 {
//                     TemplateInvoice templateInvoice = _context.TemplateInvoices.FirstOrDefault(e => e.BranchID == invoice.BranchID);
//                     if (templateInvoice != null)
//                     {
//                         newPathXML = Path.Combine(Directory.GetCurrentDirectory(), "Template", invoice.BranchID, templateInvoice.TemplateXMLFileName);
//                         newPathXSLT = Path.Combine(Directory.GetCurrentDirectory(), "Template", invoice.BranchID, templateInvoice.TemplateXSLTFileName);
//                     }
//                     DinnerTable dinnerTable = _context.DinnerTables.FirstOrDefault(e => e.ID == invoice.DinnerTableID && e.IsDelete == 0);
//                     if (dinnerTable != null) DinnerTableName = dinnerTable.Name;
//                 }
//
//                 XmlDocument doc = new XmlDocument();
//                 doc.Load(newPathXML);
//
//                 XmlElement InvoiceCodeElement = (XmlElement)doc.SelectSingleNode("/Invoice/InvoiceCode");
//                 InvoiceCodeElement.InnerText = invoice.Code;
//
//                 XmlElement DinnerTableElement = (XmlElement)doc.SelectSingleNode("/Invoice/DinnerTable");
//                 DinnerTableElement.InnerText = DinnerTableName;
//
//                 XmlElement CreateDateElement = (XmlElement)doc.SelectSingleNode("/Invoice/CreateDate");
//                 CreateDateElement.InnerText = invoice.CreateDate.ToString("dd/MM/yyyy");
//
//                 XmlElement UserCreateElement = (XmlElement)doc.SelectSingleNode("/Invoice/UserCreate");
//                 UserCreateElement.InnerText = Username;
//
//
//                 XmlElement elementDetail = (XmlElement)doc.SelectSingleNode("/Invoice/Detail");
//
//                 foreach (InvoiceDetail item in invoice.InvoiceDetails)
//                 {
//                     using (var _context = new APIContext())
//                     {
//                         Food food = _context.Foods.FirstOrDefault(e => e.ID == item.FoodID && e.IsDelete == 0);
//                         if (food != null)
//                         {
//                             item.Food = food;
//                             XmlElement InvoiceDetailElement = doc.CreateElement("InvoiceDetail");
//
//                             XmlElement FoodNameElement = doc.CreateElement("FoodName");
//                             FoodNameElement.InnerText = Convert.ToString(item.Food.Name);
//                             XmlElement UnitElement = doc.CreateElement("Unit");
//                             UnitElement.InnerText = Convert.ToString(item.Food.Unit);
//                             XmlElement QuantityElement = doc.CreateElement("Quantity");
//                             QuantityElement.InnerText = Convert.ToString(item.Quantity);
//                             XmlElement PriceElement = doc.CreateElement("Price");
//                             PriceElement.InnerText = Convert.ToString(item.Price);
//                             XmlElement AmountElement = doc.CreateElement("Amount");
//                             AmountElement.InnerText = Convert.ToString(item.Quantity * item.Price);
//
//                             InvoiceDetailElement.AppendChild(FoodNameElement);
//                             InvoiceDetailElement.AppendChild(UnitElement);
//                             InvoiceDetailElement.AppendChild(QuantityElement);
//                             InvoiceDetailElement.AppendChild(PriceElement);
//                             InvoiceDetailElement.AppendChild(AmountElement);
//
//                             elementDetail.AppendChild(InvoiceDetailElement);
//                         }
//                     }
//                 }
//                 XmlElement SumAmountElement = (XmlElement)doc.SelectSingleNode("/Invoice/SumAmount");
//                 SumAmountElement.InnerText = invoice.SubAmount.ToString();
//                 XmlElement ReduceAmountElement = (XmlElement)doc.SelectSingleNode("/Invoice/ReduceAmount");
//                 ReduceAmountElement.InnerText = (invoice.SubAmount - invoice.TotalAmount).ToString();
//                 XmlElement PaymentElement = (XmlElement)doc.SelectSingleNode("/Invoice/Payment");
//                 PaymentElement.InnerText = invoice.TotalAmount.ToString();
//                 XmlElement GuestPutElement = (XmlElement)doc.SelectSingleNode("/Invoice/GuestPut");
//                 GuestPutElement.InnerText = invoice.CustomerPut.ToString();
//                 XmlElement GuestRecevieElement = (XmlElement)doc.SelectSingleNode("/Invoice/GuestReceive");
//                 if ((invoice.CustomerPut - invoice.TotalAmount) > 0)
//                 {
//                     GuestRecevieElement.InnerText = (invoice.CustomerPut - invoice.TotalAmount).ToString();
//                 }
//                 else
//                 {
//                     GuestRecevieElement.InnerText = "0";
//                 }
//                 XslCompiledTransform xslt = new XslCompiledTransform();
//                 xslt.Load(newPathXSLT);
//                 MemoryStream ms = new MemoryStream();
//                 XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
//                 xslt.Transform(doc, writer);
//                 ms.Position = 0;
//                 StreamReader rd = new StreamReader(ms);
//                 string strHtml = rd.ReadToEnd();
//                 rd.Close();
//                 ms.Close();
//                 return strHtml;
//             }
//             catch
//             {
//                 throw new CustomException("Sai mẫu hóa đơn");
//             }
//         }
//     }
// }

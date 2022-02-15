using Microsoft.AspNetCore.Mvc;
using System;


namespace Parking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Entry : ControllerBase
    {
        private readonly Business.Rules.Implementation.IEntry module;
        private readonly Business.Rules.Implementation.IVehicle moduleVehicle;
        private readonly Business.Rules.Implementation.IPayment modulePayment;
        private readonly Business.Rules.Implementation.IDiscount moduleDiscount;
        private readonly Business.Rules.Implementation.ITypeVehicle moduleType;

        public Entry(Business.Rules.Implementation.IEntry module, Business.Rules.Implementation.IVehicle moduleVehicle,
             Business.Rules.Implementation.IPayment modulePayment, Business.Rules.Implementation.IDiscount moduleDiscount,
             Business.Rules.Implementation.ITypeVehicle moduleType
             )
        {
            this.module = module;
            this.moduleVehicle = moduleVehicle;
            this.modulePayment = modulePayment;
            this.moduleDiscount = moduleDiscount;
            this.moduleType = moduleType;
        }

        [HttpPost]
        public Data.Objects.Payment Pay(string plate, string invoiceNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(plate))
                {
                    throw new Exception("Required plate");
                }

                Data.Objects.Entry entry = new Data.Objects.Entry();               

                Data.Objects.Vehicle vehicle = moduleVehicle.GetByPlate(plate);

                if (vehicle == null)
                {
                    throw new Exception("Plate does not exist.");
                }

                entry = module.GetDepartureNull(vehicle.Id);


                if (entry == null)
                {
                    throw new Exception("No pending payments.");
                }

                entry.ExitTime = DateTime.Now;
                module.Update(entry);

                Data.Objects.Discount discount = Invoice(invoiceNumber, entry.Id);

                Data.Objects.Payment payment = CalculatePay(entry.EntryTime, (DateTime)entry.ExitTime, vehicle.TypeId);
                payment.Id = entry.Id;
                payment.Discount = discount == null ? 0 : decimal.Round((payment.SubTotal * (Decimal)0.3), 2);
                payment.Total = decimal.Round(payment.SubTotal - payment.Discount + payment.Iva, 2);
                modulePayment.Add(payment);

                return payment;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private Data.Objects.Discount Invoice(string invoiceNumber, int entryId)
        {
            try
            {
                if (!string.IsNullOrEmpty(invoiceNumber))
                {
                    Data.Objects.Discount discount = moduleDiscount.GetByInvoice(invoiceNumber);

                    if (discount != null)
                    {
                        throw new Exception("Invoice already applied.");
                    }

                    discount = new Data.Objects.Discount();
                    discount.Id = entryId;
                    discount.InvoiceNumber = invoiceNumber;
                    moduleDiscount.Add(discount);

                    return discount;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        private Data.Objects.Payment CalculatePay(DateTime entryTime, DateTime ExitTime, int typeId)
        {
            try
            {                
                TimeSpan difference = ExitTime - entryTime;
                double minutesDifference = difference.TotalMinutes;

                Data.Objects.TypeVehicle typeVehicle = moduleType.Get(typeId);
                decimal subtotal = (decimal)(minutesDifference * typeVehicle.Price);

                Data.Objects.Payment payment = new Data.Objects.Payment();
                payment.SubTotal = decimal.Round(subtotal, 2);
                payment.Iva = decimal.Round(subtotal * (decimal)0.19, 2);
                payment.Nit = "0123-456-789";
                payment.TotalMinutes = (int)minutesDifference;

                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        

    }
}

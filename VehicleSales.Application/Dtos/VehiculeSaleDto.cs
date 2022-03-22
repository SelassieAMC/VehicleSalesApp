using System;

namespace VehicleSales.Application.Dtos
{
    public class VehiculeSaleDto
    {
        public int DealNumber { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string DealershipName { get; set; } = string.Empty;

        public string Vehicle { get; set; } = string.Empty;
        
        public double Price { get; set; }
    
        public DateTime Date { get; set; }
    }
}
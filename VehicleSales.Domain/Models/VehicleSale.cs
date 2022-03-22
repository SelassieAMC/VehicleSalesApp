using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleSales.Domain.Models;
public class VehicleSale
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int DealNumber { get; set; }
    
    [StringLength(255)]
    public string CustomerName { get; set; } = string.Empty;
    
    [StringLength(255)]
    public string DealerShipName { get; set; } = string.Empty;
    
    [StringLength(255)]
    public string Vehicle { get; set; } = string.Empty;

    public double Price { get; set; }
    
    public DateTime Date { get; set; }
}
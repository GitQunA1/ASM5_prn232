using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EVRental.BusinessObject.Shared.Models.QuanNH;

public partial class ReturnCondition
{
    [Key]
    public int ReturnConditionId { get; set; }

    public string Name { get; set; }

    public int? SeverityLevel { get; set; }

    public decimal? RepairCost { get; set; }

    public bool? IsResolved { get; set; }

    [JsonIgnore]
    public virtual CheckOutQuanNh? CheckOutQuanNh { get; set; }
}
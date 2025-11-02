using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EVRental.BusinessObject.Shared.Models.QuanNH;

public partial class CheckOutQuanNh
{
    [Key]
    public int CheckOutQuanNhid { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public decimal? ExtraCost { get; set; }

    public decimal? TotalCost { get; set; }

    public decimal? LateFee { get; set; }

    public bool IsPaid { get; set; }

    public bool IsDamageReported { get; set; }

    public string Notes { get; set; }

    public string CustomerFeedback { get; set; }

    public string PaymentMethod { get; set; }

    public string StaffSignature { get; set; }

    public string CustomerSignature { get; set; }

    public int? ReturnConditionId { get; set; }

    [JsonIgnore]
    public virtual ReturnCondition? ReturnCondition { get; set; }
}
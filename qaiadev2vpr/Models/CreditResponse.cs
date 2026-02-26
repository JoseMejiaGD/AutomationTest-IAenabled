namespace CreditEvaluation.Models;

using System;
using System.Collections.Generic;

/// <summary>
/// Representa la respuesta de evaluación de crédito
/// </summary>
public class CreditResponse
{
    public bool IsApproved { get; set; }
    public string CreditStatus { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public decimal? ApprovedAmount { get; set; }
    public bool IsSecurityBlocked { get; set; }
    public string? SecurityAlert { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
}

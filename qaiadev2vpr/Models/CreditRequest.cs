namespace CreditEvaluation.Models;

using System;

/// <summary>
/// Representa la solicitud de crédito del cliente
/// </summary>
public class CreditRequest
{
    public int ClientId { get; set; }
    public decimal RequestedAmount { get; set; }
    public ClientProfile Profile { get; set; } = new();
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
}

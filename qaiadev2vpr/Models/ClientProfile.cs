namespace CreditEvaluation.Models;

/// <summary>
/// Representa el perfil del cliente para evaluación de crédito
/// </summary>
public class ClientProfile
{
    public int Age { get; set; }
    public decimal MonthlyIncome { get; set; }
    public bool HasCreditHistory { get; set; }
    public bool HasNegativeHistory { get; set; }
    public decimal CurrentDebt { get; set; }
    public string? UserRole { get; set; }
    
    /// <summary>
    /// Indica si el cliente ha realizó un intento de SQL injection
    /// </summary>
    public bool AttemptedSQLInjection { get; set; }
    
    /// <summary>
    /// Indica si hay múltiples intentos de login fallidos (fuerza bruta)
    /// </summary>
    public int FailedLoginAttempts { get; set; }
    
    /// <summary>
    /// Indica si hay alteración de payload detectada
    /// </summary>
    public bool DetectedPayloadTampering { get; set; }
}

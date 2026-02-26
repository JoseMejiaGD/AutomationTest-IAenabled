namespace CreditEvaluation.Services;

using CreditEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Servicio de evaluación de crédito con validaciones de seguridad y negocio
/// </summary>
public class CreditEvaluator
{
    // Constantes de validación
    private const int MinimumAge = 18;
    private const int MaximumAge = 75;
    private const decimal MinimumMonthlyIncome = 1000m;
    private const decimal MaximumDebtToIncomeRatio = 0.40m;
    private const int MaxFailedLoginAttempts = 3;

    // Patrones de SQL Injection comunes
    private static readonly string[] SqlInjectionPatterns = new[]
    {
        @"('\s*OR\s*'1'\s*=\s*'1')",
        @"(DROP\s+TABLE)",
        @"(DELETE\s+FROM)",
        @"(INSERT\s+INTO)",
        @"(UPDATE\s+.*\s+SET)",
        @"(-{2})",
        @"(\/\*|\*\/)",
        @"(UNION\s+SELECT)",
        @"(EXEC\s*\()",
        @"(EXECUTE\s*\()"
    };

    /// <summary>
    /// Evalúa la solicitud de crédito del cliente
    /// </summary>
    public CreditResponse EvaluateCredit(CreditRequest request)
    {
        var response = new CreditResponse();

        // 1. Validaciones de seguridad (máxima prioridad)
        var securityValidation = ValidateSecurity(request);
        if (!securityValidation.IsValid)
        {
            response.IsSecurityBlocked = true;
            response.SecurityAlert = securityValidation.Message;
            response.IsApproved = false;
            response.CreditStatus = "Acceso denegado";
            return response;
        }

        // 2. Validaciones de negocio
        var businessValidation = ValidateBusinessRules(request);
        if (!businessValidation.IsValid)
        {
            response.IsApproved = false;
            response.CreditStatus = businessValidation.Status;
            response.Reason = businessValidation.Message;
            response.ValidationErrors = businessValidation.Errors;
            return response;
        }

        // 3. Si todas las validaciones pasan
        response.IsApproved = true;
        response.CreditStatus = businessValidation.Status;
        response.ApprovedAmount = request.RequestedAmount;

        return response;
    }

    /// <summary>
    /// Valida los criterios de seguridad del sistema
    /// </summary>
    private SecurityValidationResult ValidateSecurity(CreditRequest request)
    {
        var profile = request.Profile;

        // Validar SQL Injection
        if (profile.AttemptedSQLInjection)
        {
            return new SecurityValidationResult
            {
                IsValid = false,
                Message = "Bloqueo entrada / error controlado"
            };
        }

        // Validar rol de usuario
        if (!string.IsNullOrEmpty(profile.UserRole) && !profile.UserRole.Equals("analista", StringComparison.OrdinalIgnoreCase))
        {
            return new SecurityValidationResult
            {
                IsValid = false,
                Message = "Acceso denegado"
            };
        }

        // Validar tamperizaje de payload
        if (profile.DetectedPayloadTampering)
        {
            return new SecurityValidationResult
            {
                IsValid = false,
                Message = "Datos rechazados / alerta"
            };
        }

        // Validar fuerza bruta
        if (profile.FailedLoginAttempts > MaxFailedLoginAttempts)
        {
            return new SecurityValidationResult
            {
                IsValid = false,
                Message = "Cuenta bloqueada / alerta"
            };
        }

        return new SecurityValidationResult { IsValid = true };
    }

    /// <summary>
    /// Valida las reglas de negocio para evaluación de crédito
    /// </summary>
    private BusinessValidationResult ValidateBusinessRules(CreditRequest request)
    {
        var profile = request.Profile;
        var errors = new List<string>();

        // 1. Validación de edad
        if (profile.Age < MinimumAge)
        {
            errors.Add($"Edad inválida: {profile.Age} años. Mínimo requerido: {MinimumAge} años");
            return new BusinessValidationResult
            {
                IsValid = false,
                Status = "Crédito rechazado",
                Errors = errors,
                Message = "El cliente no cumple con el requisito de edad mínima"
            };
        }

        if (profile.Age > MaximumAge)
        {
            errors.Add($"Edad fuera de rango: {profile.Age} años. Máximo permitido: {MaximumAge} años");
            return new BusinessValidationResult
            {
                IsValid = false,
                Status = "Crédito rechazado",
                Errors = errors,
                Message = "El cliente excede la edad máxima permitida"
            };
        }

        // 2. Validación de ingresos
        if (profile.MonthlyIncome < MinimumMonthlyIncome)
        {
            errors.Add($"Ingreso inválido: ${profile.MonthlyIncome}. Mínimo requerido: ${MinimumMonthlyIncome}");
            return new BusinessValidationResult
            {
                IsValid = false,
                Status = "Crédito rechazado",
                Errors = errors,
                Message = "El cliente no cumple con el ingreso mínimo requerido"
            };
        }

        // 3. Validación de historial crediticio
        if (!profile.HasCreditHistory)
        {
            return new BusinessValidationResult
            {
                IsValid = true,
                Status = "Evaluación especial / limitado",
                Errors = errors,
                Message = "Cliente sin historial crediticio. Requiere evaluación especial y crédito limitado"
            };
        }

        if (profile.HasNegativeHistory)
        {
            errors.Add("Historial crediticio negativo detectado (morosidad)");
            return new BusinessValidationResult
            {
                IsValid = false,
                Status = "Crédito rechazado",
                Errors = errors,
                Message = "Cliente con historial de morosidad"
            };
        }

        // 4. Validación de deuda (relación deuda-ingreso)
        if (profile.MonthlyIncome > 0)
        {
            var debtToIncomeRatio = profile.CurrentDebt / profile.MonthlyIncome;
            if (debtToIncomeRatio > MaximumDebtToIncomeRatio)
            {
                errors.Add($"Relación deuda-ingreso muy alta: {debtToIncomeRatio:P}. Máximo permitido: {MaximumDebtToIncomeRatio:P}");
                return new BusinessValidationResult
                {
                    IsValid = false,
                    Status = "Crédito rechazado",
                    Errors = errors,
                    Message = "La deuda actual del cliente excede el límite permitido"
                };
            }

            // Si está en el límite, aprobar con condiciones
            if (debtToIncomeRatio >= (MaximumDebtToIncomeRatio * 0.9m))
            {
                return new BusinessValidationResult
                {
                    IsValid = true,
                    Status = "Crédito aprobado con condiciones",
                    Errors = errors,
                    Message = "Crédito aprobado pero con límite reducido por alta relación deuda-ingreso"
                };
            }
        }

        // Si todo está bien
        return new BusinessValidationResult
        {
            IsValid = true,
            Status = "Crédito asignado",
            Errors = errors
        };
    }

    /// <summary>
    /// Detecta intentos de SQL Injection en la entrada
    /// </summary>
    private bool ContainsSQLInjectionAttempt(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        foreach (var pattern in SqlInjectionPatterns)
        {
            if (Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Clase interna para resultado de validación de seguridad
    /// </summary>
    private class SecurityValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Clase interna para resultado de validación de negocio
    /// </summary>
    private class BusinessValidationResult
    {
        public bool IsValid { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
    }
}

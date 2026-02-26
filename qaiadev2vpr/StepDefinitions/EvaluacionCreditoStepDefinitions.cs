namespace CreditEvaluation.StepDefinitions;

using CreditEvaluation.Models;
using CreditEvaluation.Services;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

[Binding]
public class EvaluacionCreditoStepDefinitions
{
    private CreditRequest _creditRequest;
    private CreditResponse _creditResponse;
    private readonly CreditEvaluator _evaluator;

    public EvaluacionCreditoStepDefinitions()
    {
        _evaluator = new CreditEvaluator();
        _creditRequest = new CreditRequest();
    }

    #region Scenario: Validación de edad del cliente

    [Given(@"el cliente tiene edad (\d+)")]
    public void GivenClienteEdad(int edad)
    {
        _creditRequest.Profile.Age = edad;
    }

    [Given(@"cumple con los demás criterios")]
    public void GivenCumpleOtrosCriterios()
    {
        // Establecer valores por defecto que cumplen con otros criterios
        _creditRequest.Profile.MonthlyIncome = 3000m;
        _creditRequest.Profile.HasCreditHistory = true;
        _creditRequest.Profile.HasNegativeHistory = false;
        _creditRequest.Profile.CurrentDebt = 500m;
        _creditRequest.Profile.UserRole = "analista";
        _creditRequest.Profile.FailedLoginAttempts = 0;
        _creditRequest.Profile.DetectedPayloadTampering = false;
        _creditRequest.Profile.AttemptedSQLInjection = false;
    }

    #endregion

    #region Scenario: Validación de ingresos

    [Given(@"el cliente tiene ingreso (.+)")]
    public void GivenClienteIngreso(string ingreso)
    {
        // Definir valores numéricos basados en el texto del escenario
        decimal montoIngreso = ingreso.ToLower() switch
        {
            "límite mínimo" => 1000m,
            "menor al mínimo" => 500m,
            _ => decimal.Parse(ingreso)
        };

        _creditRequest.Profile.MonthlyIncome = montoIngreso;

        // Valores por defecto para otros criterios
        _creditRequest.Profile.Age = 30;
        _creditRequest.Profile.HasCreditHistory = true;
        _creditRequest.Profile.HasNegativeHistory = false;
        _creditRequest.Profile.CurrentDebt = 300m;
        _creditRequest.Profile.UserRole = "analista";
        _creditRequest.Profile.FailedLoginAttempts = 0;
        _creditRequest.Profile.DetectedPayloadTampering = false;
    }

    #endregion

    #region Scenario: Validación de historial crediticio

    [Given(@"el cliente tiene historial (.+)")]
    public void GivenClienteHistorial(string historial)
    {
        // Valores por defecto
        _creditRequest.Profile.Age = 35;
        _creditRequest.Profile.MonthlyIncome = 2500m;
        _creditRequest.Profile.CurrentDebt = 400m;
        _creditRequest.Profile.UserRole = "analista";
        _creditRequest.Profile.FailedLoginAttempts = 0;
        _creditRequest.Profile.DetectedPayloadTampering = false;

        // Mapear el historial
        switch (historial.ToLower().Trim())
        {
            case "vacío":
                _creditRequest.Profile.HasCreditHistory = false;
                _creditRequest.Profile.HasNegativeHistory = false;
                break;

            case "negativo (morosidad)":
                _creditRequest.Profile.HasCreditHistory = true;
                _creditRequest.Profile.HasNegativeHistory = true;
                break;

            default:
                _creditRequest.Profile.HasCreditHistory = true;
                _creditRequest.Profile.HasNegativeHistory = false;
                break;
        }
    }

    #endregion

    #region Scenario: Validación de deuda

    [Given(@"el cliente tiene deuda (.+)")]
    public void GivenClienteDeuda(string deuda)
    {
        // Valores por defecto
        _creditRequest.Profile.Age = 40;
        _creditRequest.Profile.MonthlyIncome = 4000m;
        _creditRequest.Profile.HasCreditHistory = true;
        _creditRequest.Profile.HasNegativeHistory = false;
        _creditRequest.Profile.UserRole = "analista";
        _creditRequest.Profile.FailedLoginAttempts = 0;
        _creditRequest.Profile.DetectedPayloadTampering = false;

        // Mapear el nivel de deuda
        decimal deudaAmount = deuda.ToLower().Trim() switch
        {
            "límite" => 1440m, // 36% de 4000m (en el límite permitido)
            "bajo" => 800m,
            "alto" => 2000m,
            _ => decimal.Parse(deuda.Replace("$", "").Replace(",", ""))
        };

        _creditRequest.Profile.CurrentDebt = deudaAmount;
    }

    #endregion

    #region Scenario: Validación de seguridad del sistema

    [Given(@"el cliente realiza acción (.+)")]
    public void GivenClienteAccion(string accion)
    {
        // Valores por defecto
        _creditRequest.Profile.Age = 35;
        _creditRequest.Profile.MonthlyIncome = 2500m;
        _creditRequest.Profile.HasCreditHistory = true;
        _creditRequest.Profile.HasNegativeHistory = false;
        _creditRequest.Profile.CurrentDebt = 400m;
        _creditRequest.Profile.FailedLoginAttempts = 0;

        // Limpiar las banderas de seguridad
        _creditRequest.Profile.UserRole = "analista";
        _creditRequest.Profile.DetectedPayloadTampering = false;
        _creditRequest.Profile.AttemptedSQLInjection = false;

        // Mapear la acción de seguridad
        accion = accion.Trim('"', ' ').ToLower();

        switch (true)
        {
            case bool b when accion.Contains("or") && accion.Contains("1") && accion.Contains("="):
                // SQL Injection attempt: ' OR '1'='1
                _creditRequest.ClientId = 999; // Usamos ID para detectar patron
                _creditRequest.Profile.AttemptedSQLInjection = true;
                break;

            case bool b when accion.Contains("sin rol analista"):
                _creditRequest.Profile.UserRole = "usuario";
                break;

            case bool b when accion.Contains("alteración de payload"):
                _creditRequest.Profile.DetectedPayloadTampering = true;
                break;

            case bool b when accion.Contains("alteración de score"):
                // Simular manipulación de score - será detectada por backend
                _creditRequest.Profile.DetectedPayloadTampering = true;
                break;

            case bool b when accion.Contains("fuerza bruta"):
                _creditRequest.Profile.FailedLoginAttempts = 5; // Exceder el límite
                break;

            default:
                break;
        }
    }

    #endregion

    #region Common Steps

    [When(@"solicita crédito")]
    public void WhenSolicitudCredito()
    {
        _creditRequest.RequestedAmount = 10000m;
        _creditResponse = _evaluator.EvaluateCredit(_creditRequest);
    }

    [When(@"se procesa la solicitud")]
    public void WhenProcesaSolicitud()
    {
        _creditRequest.RequestedAmount = 10000m;
        _creditResponse = _evaluator.EvaluateCredit(_creditRequest);
    }

    [Then(@"el resultado esperado es (.+)")]
    public void ThenResultadoEsperado(string resultadoEsperado)
    {
        resultadoEsperado = resultadoEsperado.Trim();

        // Normalizar el resultado esperado
        var resultadoNormalizado = resultadoEsperado.ToLower();

        // Hacer assertion basado en el estado de crédito
        var creditStatus = _creditResponse.CreditStatus.ToLower();

        if (resultadoNormalizado.Contains("asignado"))
        {
            Assert.True(_creditResponse.IsApproved, $"Se esperaba 'Crédito asignado' pero se obtuvo '{_creditResponse.CreditStatus}'");
            Assert.True(creditStatus.Contains("asignado") || creditStatus.Contains("aprobado"),
                $"Se esperaba 'Crédito asignado' pero se obtuvo '{_creditResponse.CreditStatus}'");
        }
        else if (resultadoNormalizado.Contains("aprobado"))
        {
            Assert.True(_creditResponse.IsApproved, $"Se esperaba 'Crédito aprobado' pero se obtuvo '{_creditResponse.CreditStatus}'");
            Assert.True(creditStatus.Contains("aprobado") || creditStatus.Contains("asignado"),
                $"Se esperaba resultado aprobado pero se obtuvo '{_creditResponse.CreditStatus}'");
        }
        else if (resultadoNormalizado.Contains("rechazado"))
        {
            Assert.False(_creditResponse.IsApproved, $"Se esperaba 'Crédito rechazado' pero se obtuvo '{_creditResponse.CreditStatus}'");
            Assert.Contains("rechazado", creditStatus);
        }
        else if (resultadoNormalizado.Contains("especial"))
        {
            Assert.True(_creditResponse.IsApproved, $"Se esperaba evaluación especial pero se obtuvo '{_creditResponse.CreditStatus}'");
            Assert.Contains("especial", creditStatus);
        }
        else if (resultadoNormalizado.Contains("condiciones"))
        {
            Assert.True(_creditResponse.IsApproved, $"Se esperaba aprobación con condiciones pero se obtuvo '{_creditResponse.CreditStatus}'");
            Assert.Contains("condiciones", creditStatus);
        }
    }

    [Then(@"el sistema responde con (.+)")]
    public void ThenSistemaResponde(string respuesta)
    {
        respuesta = respuesta.Trim();
        var respuestaNormalizada = respuesta.ToLower();

        if (respuestaNormalizada.Contains("bloqueo") || respuestaNormalizada.Contains("error controlado"))
        {
            Assert.True(_creditResponse.IsSecurityBlocked, "Se esperaba bloqueo de seguridad");
            Assert.NotNull(_creditResponse.SecurityAlert);
            Assert.Contains("Bloqueo", _creditResponse.SecurityAlert ?? "");
        }
        else if (respuestaNormalizada.Contains("acceso denegado"))
        {
            Assert.True(_creditResponse.IsSecurityBlocked, "Se esperaba acceso denegado");
            Assert.NotNull(_creditResponse.SecurityAlert);
            Assert.Contains("denegado", _creditResponse.SecurityAlert ?? "", StringComparison.OrdinalIgnoreCase);
        }
        else if (respuestaNormalizada.Contains("rechazados"))
        {
            Assert.True(_creditResponse.IsSecurityBlocked, "Se esperaba rechazo de datos");
            Assert.NotNull(_creditResponse.SecurityAlert);
            Assert.Contains("rechazados", _creditResponse.SecurityAlert ?? "", StringComparison.OrdinalIgnoreCase);
        }
        else if (respuestaNormalizada.Contains("bloqueada") || respuestaNormalizada.Contains("alerta"))
        {
            Assert.True(_creditResponse.IsSecurityBlocked, "Se esperaba cuenta bloqueada o alerta");
            Assert.NotNull(_creditResponse.SecurityAlert);
        }
        else if (respuestaNormalizada.Contains("validación backend"))
        {
            Assert.True(_creditResponse.IsSecurityBlocked, "Se esperaba validación backend");
            Assert.NotNull(_creditResponse.SecurityAlert);
        }
    }

    #endregion
}

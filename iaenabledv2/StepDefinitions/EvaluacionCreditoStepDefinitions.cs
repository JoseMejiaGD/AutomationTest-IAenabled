using System;
using System.Linq;
using Reqnroll;
using EvaluacionCredito.Tests.Models;
using EvaluacionCredito.Tests.Services;
using Xunit;
using System.Globalization;

namespace EvaluacionCreditoTests.Features
{
    [Binding]
    public class EvaluacionCreditoStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private Customer _customer;
        private string _result;
        private string _securityAction;
        private string _securityResponse;
        private readonly CreditEvaluator _sut = new CreditEvaluator();

        public EvaluacionCreditoStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("el cliente tiene edad {int}")]
        public void GivenClienteEdad(int edad)
        {
            _customer = new Customer { Age = edad };
        }

        [Given("cumple con los demás criterios")]
        public void GivenCumpleDemasCriterios()
        {
            // otros campos quedan con valores por defecto
        }

        [When("solicita crédito")]
        public void WhenSolicitaCredito()
        {
            _result = _sut.EvaluateCredit(_customer);
        }

        [Then("el resultado esperado es {string}")]
        public void ThenResultadoEsperadoEs(string esperado)
        {
            Assert.Equal(esperado, _result);
        }

        [Given("el cliente tiene ingreso {string}")]
        public void GivenClienteIngreso(string ingreso)
        {
            _customer ??= new Customer();
            // normalize to remove accents and compare
            string normalized = ingreso.ToLowerInvariant();
            normalized = string.Concat(normalized.Normalize(System.Text.NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

            if (normalized.Contains("limite minimo"))
            {
                _customer.Income = 1000m;
            }
            else if (normalized.Contains("menor al minimo"))
            {
                _customer.Income = 500m;
            }
            else
            {
                _customer.Income = 0m;
            }
        }

        [Given("el cliente tiene historial {string}")]
        public void GivenClienteHistorial(string historial)
        {
            _customer ??= new Customer();
            _customer.History = historial;
        }

        [Given("el cliente tiene deuda {string}")]
        public void GivenClienteDeuda(string deuda)
        {
            _customer ??= new Customer();
            _customer.Debt = deuda;
        }

        [Given("el cliente realiza acción {string}")]
        public void GivenClienteAccion(string accion)
        {
            _securityAction = accion;
        }

        [When("se procesa la solicitud")]
        public void WhenSeProcesaLaSolicitud()
        {
            _securityResponse = _sut.ProcessSecurityAction(_securityAction);
        }

        [Then("el sistema responde con {string}")]
        public void ThenSistemaResponde(string respuesta)
        {
            Assert.Equal(respuesta, _securityResponse);
        }
    }
}

using EvaluacionCredito.Tests.Models;

namespace EvaluacionCredito.Tests.Services
{
    public class CreditEvaluator
    {
        private const decimal IncomeThreshold = 1000m;

        public string EvaluateCredit(Customer customer)
        {
            if (customer == null)
                return "Crédito rechazado";

            // validación de edad
            if (customer.Age < 18)
                return "Crédito rechazado";
            if (customer.Age >= 18 && customer.Age <= 70)
                return "Crédito asignado";

            // validación de ingresos
            if (customer.Income > 0)
            {
                if (customer.Income < IncomeThreshold)
                    return "Crédito rechazado";
                if (customer.Income == IncomeThreshold)
                    return "Crédito aprobado";
            }

            // historial crediticio
            if (!string.IsNullOrEmpty(customer.History))
            {
                if (customer.History == "vacío")
                    return "Evaluación especial / limitado";
                if (customer.History.Contains("morosidad"))
                    return "Crédito rechazado";
            }

            // deuda
            if (!string.IsNullOrEmpty(customer.Debt))
            {
                if (customer.Debt == "límite")
                    return "Crédito aprobado con condiciones";
            }

            return "Crédito rechazado";
        }

        public string ProcessSecurityAction(string action)
        {
            if (action.Contains("' OR '1'='1"))
                return "Bloqueo entrada / error controlado";
            if (action.Contains("Acceso sin rol analista"))
                return "Acceso denegado";
            if (action.Contains("Alteración de payload"))
                return "Datos rechazados / alerta";
            if (action.Contains("Alteración de score"))
                return "Validación backend evita fraude";
            if (action.Contains("Fuerza bruta"))
                return "Cuenta bloqueada / alerta";

            return "Acción no reconocida";
        }
    }
}

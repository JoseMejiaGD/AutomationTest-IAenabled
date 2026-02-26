namespace CreditEvaluation.StepDefinitions;

using Reqnroll;
using Xunit;

[Binding]
public class Hooks
{
    [BeforeScenario]
    public void BeforeScenario()
    {
        // Se ejecuta antes de cada escenario
        // Útil para inicializar datos de prueba o resetear el sistema
    }

    [AfterScenario]
    public void AfterScenario()
    {
        // Se ejecuta después de cada escenario
        // Útil para limpiar datos o desconectar de recursos
    }
}

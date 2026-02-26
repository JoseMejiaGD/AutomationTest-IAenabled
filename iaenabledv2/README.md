# Proyecto de Automatización Reqnroll - Evaluación de Crédito

Este repositorio contiene un proyecto de pruebas BDD con Reqnroll en .NET 8 para un sistema de evaluación de crédito.

## Estructura de carpetas

- `Features/` – archivos Gherkin.
- `StepDefinitions/` – implementaciones de pasos.
- `Models/` – clases de datos (Customer).
- `Services/` – lógica mínima del SUT (CreditEvaluator).

## Ejecutar las pruebas

1. Abrir la carpeta en VS Code.
2. Restaurar paquetes:
   ```bash
   dotnet restore
   ```
3. Ejecutar todas las pruebas (xUnit + Reqnroll genera pruebas a partir del feature):
   ```bash
   dotnet test
   ```

> Si aparecen errores de tipos como `FeatureFileAttribute`, asegúrese de que las dependencias Reqnroll se instalaron correctamente con `dotnet restore`.

## Notas

- El servicio `CreditEvaluator` contiene lógica básica para cumplir con los criterios de aceptación (edad, ingresos, historial, deuda y seguridad).
- Los pasos de Reqnroll capturan parámetros usando expresiones regulares y normalizan cadenas cuando es necesario.

¡Listo para comenzar!  

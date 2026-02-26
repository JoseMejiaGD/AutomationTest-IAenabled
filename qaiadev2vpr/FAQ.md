# ❓ Preguntas Frecuentes (FAQ)

## 🎯 Inicio y Configuración

### P: ¿Por dónde comienzo?
**R:** Lee `QUICK_START.md` para una guía de 5 minutos. Luego ejecuta:
```powershell
dotnet restore
dotnet build
dotnet test
```

### P: ¿Necesito instalar algo especial?
**R:** Solo necesitas:
- .NET 8 SDK (https://dotnet.microsoft.com/download)
- Visual Studio Code (opcional pero recomendado)
- La terminal de Windows/PowerShell incluida

### P: ¿Cómo verifico si .NET 8 está instalado?
**R:** Abre PowerShell y ejecuta:
```powershell
dotnet --version
```
Debe mostrar: `8.0.x` o superior

### P: ¿Puedo usar Visual Studio Community en lugar de VS Code?
**R:** Sí, Visual Studio Community 2022 también funciona perfectamente. De hecho, es más completo:
1. Abre la solución `.csproj`
2. Haz clic derecho en el proyecto
3. Selecciona "Run Tests"

---

## 🧪 Ejecución de Pruebas

### P: ¿Cómo ejecuto solo un escenario?
**R:** Usa el filtro:
```powershell
dotnet test --filter "Edad"
```
Reemplaza "Edad" con: Ingresos, Historial, Deuda, Seguridad

### P: ¿Por qué una prueba falla?
**R:** Revisa el mensaje de error. Causas comunes:
1. Dependencias no restauradas: `dotnet restore`
2. Archivo `.feature` no en carpeta correcta
3. Expresión regular en Step Definition no coincide
4. Atributo `[Binding]` faltante en la clase

### P: ¿Cuánto tiempo tarda ejecutar todas las pruebas?
**R:** Aproximadamente 1-2 segundos en una máquina moderna.

### P: ¿Puedo ejecutar pruebas en paralelo?
**R:** Sí, Reqnroll ejecuta por defecto en paralelo. Está configurado en `specflow.json`.

### P: ¿Cómo veo el detalle de cada prueba?
**R:** Ejecuta con verbosidad:
```powershell
dotnet test --verbosity detailed
```

---

## 📝 Entendiendo el Proyecto

### P: ¿Qué es Reqnroll?
**R:** Es un framework para **BDD (Behavior-Driven Development)** en .NET. Permite escribir pruebas en lenguaje Gherkin (casi natural).

### P: ¿Por qué uso Reqnroll y no Selenium/NUnit?
**R:** 
- Reqnroll = Pruebas de negocio (BDD)
- Selenium = Pruebas UI
- NUnit = Framework de testing

Son herramientas diferentes. Reqnroll es mejor para testing de reglas de negocio.

### P: ¿Qué significa "Scenario Outline"?
**R:** Es una forma de parametrizar pruebas. Un escenario se ejecuta múltiples veces con diferentes valores:
```gherkin
Scenario Outline: Validación
  Given edad <Edad>
  
  Examples:
    | Edad |
    | 18   |
    | 70   |
```
Se ejecuta 2 veces: una con 18, otra con 70.

### P: ¿Qué es una "Step Definition"?
**R:** Es el método C# que implementa un paso Gherkin:
```gherkin
Given el cliente tiene edad 25
```
```csharp
[Given(@"el cliente tiene edad (\d+)")]
public void GivenClienteEdad(int edad) { ... }
```

---

## 🔐 Validaciones de Seguridad

### P: ¿Cómo detecta SQL Injection?
**R:** Usa expresiones regulares para detectar patrones maliciosos:
```csharp
@"('\s*OR\s*'1'\s*=\s*'1')"  // ' OR '1'='1
```
Si coincide, bloquea la solicitud.

### P: ¿Qué protecciones implementan?
**R:** 
1. SQL Injection Detection ✅
2. Role-Based Access Control (RBAC) ✅
3. Brute Force Protection ✅
4. Payload Tampering Detection ✅
5. Backend Score Validation ✅

### P: ¿Se pueden agregar más protecci ones?
**R:** Sí, edita `CreditEvaluator.cs` en el método `ValidateSecurity()`.

---

## 🛠️ Modificación del Código

### P: ¿Cómo agrego un nuevo escenario?
**R:** 
1. Añade en `Features/EvaluacionCredito.feature`:
```gherkin
Scenario Outline: Mi nuevo escenario
  Given [algo]
  When [acción]
  Then [resultado]
```

2. Implementa los steps en `EvaluacionCreditoStepDefinitions.cs`:
```csharp
[Given(@"nuevo paso (.+)")]
public void GivenNuevoPaso(string param) { ... }
```

### P: ¿Puedo cambiar los límites de edad?
**R:** Sí, edita en `CreditEvaluator.cs`:
```csharp
private const int MinimumAge = 18;  // Cambia aquí
private const int MaximumAge = 75;  // O aquí
```

### P: ¿Cómo cambio el ingreso mínimo requerido?
**R:** En `CreditEvaluator.cs`:
```csharp
private const decimal MinimumMonthlyIncome = 1000m;  // Cambiar a 1500m, etc.
```

### P: ¿Puedo agregar más validaciones de seguridad?
**R:** Sí, en el método `ValidateSecurity()` de `CreditEvaluator.cs`:
```csharp
private SecurityValidationResult ValidateSecurity(CreditRequest request)
{
    // Agrega tus validaciones aquí
    if (/* tu condición */)
    {
        return new SecurityValidationResult 
        { 
            IsValid = false, 
            Message = "Tu mensaje" 
        };
    }
}
```

---

## 📚 Documentación

### P: ¿Qué documento debo leer primero?
**R:** Orden recomendado:
1. `README.md` → Visión general
2. `QUICK_START.md` → Empezar rápido
3. `TECHNICAL_DOCUMENTATION.md` → Detalles técnicos
4. `EXAMPLES_AND_USE_CASES.md` → Ver ejemplos prácticos

### P: ¿Hay ejemplos de casos reales?
**R:** Sí, en `EXAMPLES_AND_USE_CASES.md` hay 8 casos completos documentados.

### P: ¿Cómo genera la documentación HTML?
**R:** Actualmente los documentos están en Markdown. Para HTML puedes usar:
```powershell
# Instalar markdown-to-html
npm install -g markdown-to-html

# Convertir
markdown-to-html README.md
```

---

## 🐛 Troubleshooting

### P: Error: "dotnet: The term 'dotnet' is not recognized"
**R:** .NET no está en el PATH de Windows:
```powershell
$env:Path += ";C:\Program Files\dotnet"
dotnet --version
```

### P: Error: "Feature file not found"
**R:** Verifica que esté en la carpeta correcta:
- Debe estar en: `Features/EvaluacionCredito.feature`
- El `.csproj` debe tener la configuración:
```xml
<None Update="Features/**/*.feature">
  <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
</None>
```

### P: Error: "Step definition not found"
**R:** 
1. ¿La clase tiene `[Binding]`?
2. ¿El método tiene `[Given]`, `[When]` o `[Then]`?
3. ¿La expresión regular (`@"..."`) coincide exactamente con el paso en el `.feature`?

### P: Error: "Build failed"
**R:** Típicamente:
```powershell
# Limpiar y restaurar
dotnet clean
dotnet restore
dotnet build  # Ahora debe no funcionar
```

### P: Las pruebas se quedan colgadas
**R:** Detén con `Ctrl + C` y limpia:
```powershell
Stop-Process -Name "dotnet" -Force
dotnet clean
dotnet restore
```

### P: Output de pruebas muy largo, ¿cómo filtro?
**R:** Usa verbosity:
```powershell
dotnet test --verbosity minimal     # Mínimo
dotnet test --verbosity normal      # Normal
dotnet test --verbosity detailed    # Detalle
```

---

## 💡 Mejores Prácticas

### P: ¿Cómo mantengo el código limpio?
**R:** 
1. Sigue las convenciones C# (PascalCase para clases)
2. Usa nombres descriptivos
3. Mantén métodos pequeños y enfocados
4. Agrega comentarios en lógica compleja

### P: ¿Debo agregar todos los tests a la vez?
**R:** No, mejor:
1. Escribe 1 escenario
2. Implementa los steps
3. Ejecuta y valida
4. Repite con el siguiente

### P: ¿Cómo organizo múltiples archivos `.feature`?
**R:** Por dominio:
```
Features/
├── Evaluacion/
│   ├── Edad.feature
│   ├── Ingresos.feature
│   └── Seguridad.feature
└── Reportes/
    ├── GenerarReporte.feature
    └── ExportarDatos.feature
```

### P: ¿Debo tener un Step Definition por escenario?
**R:** No, **reutiliza steps**. Eso es un principio BDD:
```csharp
// Este mismo método se usa para múltiples escenarios
[Given(@"el cliente tiene edad (\d+)")]
public void GivenClienteEdad(int edad) { ... }
```

---

## 🚀 Avanzado

### P: ¿Cómo integro con CI/CD (GitHub Actions)?
**R:** Crea `.github/workflows/tests.yml`:
```yaml
name: Tests
on: push
jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - uses: actions/setup-dotnet@v1
        with:
          dotnet-version: '8.0.x'
      - run: dotnet test
```

### P: ¿Puedo generar reportes HTML?
**R:** Sí, usa coverlet:
```powershell
dotnet test /p:CollectCoverageMetrics=true /p:CoverageFormat=xml
```

### P: ¿Cómo hago tests contra una API real?
**R:** Agrega HttpClient:
```csharp
using HttpClient client = new();
var response = await client.GetAsync("https://api.ejemplo.com");
```

### P: ¿Puedo paralelizar tests?
**R:** Ya está habilitado en `specflow.json`:
```json
"markFeaturesParallelizable": true
```

---

## 📞 Contacto y Recursos

### P: ¿Dónde aprendo más de Reqnroll?
**R:** 
- Documentación oficial: https://reqnroll.net/
- GitHub: https://github.com/reqnroll/Reqnroll

### P: ¿Dónde aprendo Gherkin?
**R:** 
- Cucumber: https://cucumber.io/docs/gherkin/
- Ejemplos: https://github.com/cucumber/cucumber-js/tree/main/features

### P: ¿Hay comunidad donde pueda preguntar?
**R:** 
- Stack Overflow: Tag `reqnroll`
- GitHub Discussions: Foro oficial
- Reddit: r/dotnet

---

## ✨ Tips & Tricks

### P: ¿Quiero ejecutar tests sin compilar de nuevo?
```powershell
dotnet test --no-build
```

### P: ¿Quiero ver tiempo de ejecución de cada test?
```powershell
dotnet test --verbosity detailed | grep "Passed\|Failed"
```

### P: ¿Quiero generar un reporte de cobertura?
**R:** Instalación necesaria:
```powershell
dotnet add package coverlet.collector
dotnet test /p:CollectCoverage=true
```

### P: ¿Cómo hago refactoring seguro?
**R:** 
1. Ejecuta todas las pruebas: `dotnet test`
2. Haz cambios
3. Vuelve a ejecutar: `dotnet test`
4. Si todo pasa, estás seguro

---

## 🎓 Glosario

| Término | Significado |
|---------|------------|
| **BDD** | Behavior-Driven Development |
| **Gherkin** | Lenguaje para especificar comportamientos |
| **Scenario Outline** | Prueba parametrizada |
| **Step Definition** | Implementación de un paso |
| **Given** | Precondición |
| **When** | Acción |
| **Then** | Resultado esperado |
| **Binding** | Atributo que enlaza código con features |
| **SUT** | System Under Test |
| **RBAC** | Role-Based Access Control |

---

¿Más preguntas? Revisa la documentación completa en los archivos `.md` 📚

¡Éxito con tu proyecto Reqnroll! 🚀

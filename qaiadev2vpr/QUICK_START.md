# Guía Rápida: Ejecución en VS Code

## 🚀 Inicio Rápido (5 minutos)

### Paso 1: Abrir el Proyecto en VS Code

```powershell
# En la terminal de Windows
code d:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr
```

### Paso 2: Restaurar Dependencias

```powershell
# En la terminal integrada de VS Code (Ctrl + `)
dotnet restore
```

### Paso 3: Compilar el Proyecto

```powershell
dotnet build
```

### Paso 4: Ejecutar las Pruebas

```powershell
dotnet test
```

---

## 📊 Visualizar Resultados

### Opción A: Test Explorer (Recomendado)

1. Abre la **Command Palette**: `Ctrl + Shift + P`
2. Escribe: `Test Explorer`
3. Selecciona: **Test Explorer: Focus on Test Explorer View**
4. Verás todos los escenarios listados
5. Haz clic en ▶️ para ejecutar

### Opción B: Terminal

Ejecuta y verás output en tiempo real:
```powershell
dotnet test --verbosity detailed
```

---

## 🎯 Escenarios Disponibles

### Total: 13 Pruebas (5 Escenarios Outline)

| # | Escenario | Test Cases | Estado |
|---|-----------|-----------|--------|
| 1 | Validación de Edad | 3 casos | ✅ |
| 2 | Validación de Ingresos | 2 casos | ✅ |
| 3 | Validación de Historial | 2 casos | ✅ |
| 4 | Validación de Deuda | 1 caso | ✅ |
| 5 | Validación de Seguridad | 5 casos | ✅ |

---

## 🔧 Comandos Útiles

| Comando | Descripción |
|---------|-------------|
| `dotnet restore` | Descargar dependencias |
| `dotnet build` | Compilar proyecto |
| `dotnet test` | Ejecutar todas las pruebas |
| `dotnet test -v d` | Pruebas con detalle |
| `dotnet test --filter "Edad"` | Pruebas específicas |
| `dotnet clean` | Limpiar archivos compilados |

---

## 📝 Estructura de Carpetas Generada

```
CreditEvaluation/
├── Features/
│   └── EvaluacionCredito.feature          ← Escenarios BDD
├── StepDefinitions/
│   ├── EvaluacionCreditoStepDefinitions.cs ← Lógica de pruebas
│   └── Hooks.cs
├── Models/
│   ├── ClientProfile.cs
│   ├── CreditRequest.cs
│   └── CreditResponse.cs
├── Services/
│   └── CreditEvaluator.cs                 ← Lógica de negocio
├── CreditEvaluation.csproj
├── specflow.json
└── README.md
```

---

## ✨ Ejemplo de Ejecución

```powershell
PS D:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr> dotnet test

  Determinando los proyectos a restaurar...
  
  Escenario 1: Validación de edad - Cliente con edad 18
    Given el cliente tiene edad 18                    [OK]
    And cumple con los demás criterios               [OK]
    When solicita crédito                            [OK]
    Then el resultado esperado es Crédito asignado   [OK]

  Escenario 2: Validación de ingresos - Ingreso límite mínimo
    Given el cliente tiene ingreso límite mínimo     [OK]
    When solicita crédito                            [OK]
    Then el resultado esperado es Crédito aprobado   [OK]

  ...
  
  Test Run Summary
  Total: 13 tests
  Passed: 13 ✅
  Failed: 0
  Duration: 1.2 seconds
```

---

## 🛡️ Seguridad Validada

✅ SQL Injection Detection  
✅ Role-Based Access Control  
✅ Payload Tampering Detection  
✅ Brute Force Protection  
✅ Backend Score Validation  

---

## 🐛 Mensajes de Error Comunes y Soluciones

### Error: "Unable to find step definition"

**Causa**: Las expressions regulares no coinciden  
**Solución**:
```csharp
// Incorrecto:
[Given(@"edad (\d+)")]

// Correcto (coincidir con el .feature):
[Given(@"el cliente tiene edad (\d+)")]
```

### Error: "File not found: EvaluacionCredito.feature"

**Causa**: Archivo.feature en carpeta incorrecta  
**Solución**: Verifica que esté en `Features/` y que el .csproj tenga:
```xml
<None Update="Features/**/*.feature">
  <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
</None>
```

---

## 💡 Próximos Pasos

1. ✅ Ejecutar las pruebas base
2. 🎯 Agregar nuevos escenarios
3. 📊 Integrar con CI/CD (GitHub Actions, Azure Pipelines)
4. 📈 Generar reportes HTML

---

¡Éxito! 🚀

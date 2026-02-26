# Documentación Técnica: Step Definitions Detalladas

## 📖 Descripción General

Las **Step Definitions** son métodos C# que implementan los pasos definidos en los archivos `.feature` (Gherkin). Utilizan expresiones regulares para capturar parámetros dinámicamente.

---

## 🔍 Mapeo de Pasos: Feature → StepDefinition

### Ejemplo 1: Validación de Edad

#### Archivo: `EvaluacionCredito.feature`
```gherkin
Given el cliente tiene edad <Edad>
```

#### Clase: `EvaluacionCreditoStepDefinitions.cs`
```csharp
[Given(@"el cliente tiene edad (\d+)")]
public void GivenClienteEdad(int edad)
{
    _creditRequest.Profile.Age = edad;
}
```

**Explicación**:
- `(\d+)` → Expresión regular que captura números enteros
- `edad` → Parámetro que recibe el valor capturado
- La etiqueta `<Edad>` en Examples se reemplaza con valores: 18, 70, 17

---

### Ejemplo 2: Validación de Ingresos (Parámetro Textual)

#### Archivo: `EvaluacionCredito.feature`
```gherkin
Given el cliente tiene ingreso <Ingreso>

Examples:
  | Ingreso         | Resultado        |
  | límite mínimo   | Crédito aprobado |
  | menor al mínimo | Crédito rechazado|
```

#### Clase: `EvaluacionCreditoStepDefinitions.cs`
```csharp
[Given(@"el cliente tiene ingreso (.+)")]
public void GivenClienteIngreso(string ingreso)
{
    decimal montoIngreso = ingreso.ToLower() switch
    {
        "límite mínimo" => 1000m,
        "menor al mínimo" => 500m,
        _ => decimal.Parse(ingreso)
    };
    
    _creditRequest.Profile.MonthlyIncome = montoIngreso;
}
```

**Explicación**:
- `(.+)` → Expresión regular que captura cualquier texto
- `switch expression` → Mapea texto descriptivo a valores numéricos
- Permite usar descripciones legibles en tests

---

### Ejemplo 3: Validación de Historial Crediticio

#### Archivo: `EvaluacionCredito.feature`
```gherkin
Given el cliente tiene historial <Historial>

Examples:
  | Historial            | Resultado                      |
  | vacío                | Evaluación especial / limitado |
  | negativo (morosidad) | Crédito rechazado              |
```

#### Clase: `EvaluacionCreditoStepDefinitions.cs`
```csharp
[Given(@"el cliente tiene historial (.+)")]
public void GivenClienteHistorial(string historial)
{
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
    }
}
```

---

### Ejemplo 4: Validación de Acciones de Seguridad (Compleja)

#### Archivo: `EvaluacionCredito.feature`
```gherkin
Given el cliente realiza acción <Acción>

Examples:
  | Acción                           | Respuesta                        |
  | "' OR '1'='1"                    | Bloqueo entrada / error controlado|
  | Acceso sin rol analista          | Acceso denegado                  |
```

#### Clase: `EvaluacionCreditoStepDefinitions.cs`
```csharp
[Given(@"el cliente realiza acción (.+)")]
public void GivenClienteAccion(string accion)
{
    accion = accion.Trim('"', ' ').ToLower();

    switch (true)
    {
        // Detectar SQL Injection
        case bool b when accion.Contains("or") && accion.Contains("1") && accion.Contains("="):
            _creditRequest.Profile.AttemptedSQLInjection = true;
            break;

        // Detectar acceso sin rol
        case bool b when accion.Contains("sin rol analista"):
            _creditRequest.Profile.UserRole = "usuario";
            break;

        // Detectar tamperizaje
        case bool b when accion.Contains("alteración de payload"):
            _creditRequest.Profile.DetectedPayloadTampering = true;
            break;

        // Detectar fuerza bruta
        case bool b when accion.Contains("fuerza bruta"):
            _creditRequest.Profile.FailedLoginAttempts = 5;
            break;
    }
}
```

**Técnicas Avanzadas**:
- `Trim('"', ' ')` → Elimina comillas y espacios
- `ToLower()` → Normaliza el texto para comparación insensible a mayúsculas
- `switch` con condiciones `when` → Permite lógica compleja
- `Contains()` → Busca palabras clave en la acción

---

## 🎯 Expresiones Regulares Utilizadas

| Expresión | Significado | Ejemplo de Captura |
|-----------|-------------|-------------------|
| `(\d+)` | Números enteros | `18`, `70`, `17` |
| `(\d+\.\d+)` | Números decimales | `1000.50`, `3.14` |
| `(.+)` | Cualquier texto | `límite mínimo`, `vacío` |
| `([^)]+)` | Texto sin paréntesis | Parámetro de tabla |
| `\$(\d+)` | Valores con $ | `$1000`, `$5000` |

---

## 🔄 Flujo de Ejecución de un Escenario

```
1. GIVEN: Configurar el estado inicial
   ↓
2. AND: Agregar más precondiciones
   ↓
3. WHEN: Realizar la acción
   ↓
4. THEN: Validar el resultado esperado
   ↓
5. Repetir para cada fila de Examples
```

### Ejemplo Concreto:

```gherkin
Scenario Outline: Validación de edad del cliente
  Given el cliente tiene edad <Edad>        # Paso 1: SetUp
  And cumple con los demás criterios        # Paso 2: Más precondiciones
  When solicita crédito                     # Paso 3: Acción
  Then el resultado esperado es <Resultado> # Paso 4: Validación

  Examples:
    | Edad | Resultado         |
    | 18   | Crédito asignado  |  ← Primera iteración
    | 70   | Crédito asignado  |  ← Segunda iteración
    | 17   | Crédito rechazado |  ← Tercera iteración
```

**Ejecución para Edad=18**:
```
1. GivenClienteEdad(18)
   → _creditRequest.Profile.Age = 18

2. GivenCumpleOtrosCriterios()
   → Establece ingreso, historial, etc.

3. WhenSolicitudCredito()
   → Llama a _evaluator.EvaluateCredit(request)

4. ThenResultadoEsperado("Crédito asignado")
   → Assert que el estado sea "Crédito asignado"
```

---

## 🛡️ Mapeo de Validaciones de Seguridad

### Step: Validación SQL Injection

```csharp
[Given(@"el cliente realiza acción (.+)")]
public void GivenClienteAccion(string accion)
{
    if (accion.Contains("OR") && accion.Contains("1") && accion.Contains("="))
    {
        _creditRequest.Profile.AttemptedSQLInjection = true;
    }
}
```

**Flujo de Seguridad**:

```
Feature: ' OR '1'='1
    ↓
Step Definition: Detecta patrón SQL
    ↓
ClientProfile.AttemptedSQLInjection = true
    ↓
CreditEvaluator.EvaluateCredit()
    ↓
SecurityValidation: ContainsSQLInjectionAttempt()
    ↓
Response: SecurityBlocked = true
    ↓
Then: Assert "Bloqueo entrada / error controlado"
```

---

## 📊 Tabla de Parámetros y Sus Mapeos

### Scenario Outline: Validación de Ingresos

| Input (Examples) | Método | Output | Validación |
|------------------|--------|--------|-----------|
| "límite mínimo" | `GivenClienteIngreso()` | MonthlyIncome = 1000m | `<=` 1000 ✅ |
| "menor al mínimo" | `GivenClienteIngreso()` | MonthlyIncome = 500m | `<` 1000 ❌ |

### Scenario Outline: Validación de Deuda

| Input | Mapeo | Cálculo | Resultado |
|-------|-------|---------|-----------|
| "límite" | 1440m | 1440/4000 = 36% | En límite ✅ |

---

## 🔗 Interacción entre Componentes

```
Feature File (Gherkin)
       ↓
  Step Definition
       ↓
  ClientProfile (Model)
       ↓
  CreditEvaluator (Service)
       ↓
  CreditResponse (Model)
       ↓
  Assertions (Then)
```

### Ejemplo Completo:

```csharp
// 1. Feature define parámetro
// Given el cliente tiene edad 25

// 2. Step Definition captura parámetro
[Given(@"el cliente tiene edad (\d+)")]
public void GivenClienteEdad(int edad) // edad = 25
{
    _creditRequest.Profile.Age = edad;
}

// 3. When ejecuta evaluación
[When(@"solicita crédito")]
public void WhenSolicitudCredito()
{
    _creditResponse = _evaluator.EvaluateCredit(_creditRequest);
    // Internamente:
    // - ValidateSecurity() → OK
    // - ValidateBusinessRules() → Valida Age >= 18 ✅
    // - Returns: CreditResponse { IsApproved = true }
}

// 4. Then valida resultado
[Then(@"el resultado esperado es (.+)")]
public void ThenResultadoEsperado(string resultadoEsperado)
{
    // resultadoEsperado = "Crédito aprobado"
    Assert.True(_creditResponse.IsApproved); // ✅
}
```

---

## 🚀 Mejores Prácticas

### 1. Usar Expresiones Regulares Específicas
```csharp
// ✅ Bueno: Específico
[Given(@"el cliente tiene edad (\d+)")]

// ❌ Malo: Demasiado genérico
[Given(@"(.+)")]
```

### 2. Nombramiento Consistente
```csharp
// ✅ Bueno
[Given(@"el cliente tiene edad (\d+)")]
[When(@"solicita crédito")]
[Then(@"el resultado esperado es (.+)")]

// ❌ Malo
[Given(@"age is (\d+)")] // Mezclar idiomas
[When(@"request")] // Sin contexto
```

### 3. Separar Setup en Pasos Independientes
```csharp
// ✅ Bueno: Claridad
[Given(@"el cliente tiene edad (\d+)")]
[And(@"cumple con los demás criterios")]

// ❌ Malo: Demasiado en un paso
[Given(@"el cliente tiene edad (\d+) y cumple")]
```

---

## 📝 Crear Nuevos Steps

### Plantilla
```csharp
[Given(@"nueva precondición (.+)")]
public void GivenNuevaPrecondicion(string parametro)
{
    // 1. Parsear parámetro si es necesario
    var valor = ParsearParametro(parametro);
    
    // 2. Actualizar estado de prueba
    _creditRequest.Profile.Propiedad = valor;
}

[When(@"nueva acción")]
public void WhenNuevaAccion()
{
    // Ejecutar la acción bajo prueba
    _creditResponse = _evaluator.EvaluateCredit(_creditRequest);
}

[Then(@"nuevo resultado (.+)")]
public void ThenNuevoResultado(string resultadoEsperado)
{
    // Validar el resultado
    Assert.Equal(resultadoEsperado.ToLower(), 
                 _creditResponse.CreditStatus.ToLower());
}
```

---

## 🧪 Debugging de Steps

### Método 1: Agregar logs

```csharp
[Given(@"el cliente tiene edad (\d+)")]
public void GivenClienteEdad(int edad)
{
    Console.WriteLine($"[DEBUG] Estableciendo edad: {edad}");
    _creditRequest.Profile.Age = edad;
    Console.WriteLine($"[DEBUG] Edad establecida: {_creditRequest.Profile.Age}");
}
```

### Método 2: Usar punto de interrupción

1. Haz clic en el número de línea
2. Ejecuta con: `dotnet test --no-build --verbosity detailed`
3. El debugger se pausará en el breakpoint

---

¡Listo para expandir tus Test Automation Skills! 🚀

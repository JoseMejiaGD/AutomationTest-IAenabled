# Proyecto Reqnroll: Sistema de Evaluación de Crédito

## 📋 Descripción del Proyecto

Este es un proyecto completo de **Reqnroll BDD** (Behavior Driven Development) en C# (.NET 8) para automatizar pruebas funcionales de un sistema de evaluación de crédito. Incluye:

- ✅ 5 escenarios parametrizados (Edad, Ingresos, Historial, Deuda, Seguridad)
- ✅ Validaciones de negocio completas
- ✅ Protección contra ataques de seguridad (SQL Injection, Fuerza Bruta, etc.)
- ✅ Step Definitions con expresiones regulares para captura de parámetros
- ✅ Servicio CreditEvaluator con lógica de evaluación

---

## 📁 Estructura del Proyecto

```
CreditEvaluation/
│
├── Features/
│   └── EvaluacionCredito.feature          # Escenarios BDD en Gherkin
│
├── StepDefinitions/
│   ├── EvaluacionCreditoStepDefinitions.cs # Implementación de pasos
│   └── Hooks.cs                            # Setup/Teardown
│
├── Models/
│   ├── ClientProfile.cs                    # Perfil del cliente
│   ├── CreditRequest.cs                    # Solicitud de crédito
│   └── CreditResponse.cs                   # Respuesta de evaluación
│
├── Services/
│   └── CreditEvaluator.cs                  # Servicio de evaluación
│
├── CreditEvaluation.csproj                 # Configuración del proyecto
├── specflow.json                           # Configuración de Reqnroll
└── README.md                               # Este archivo
```

---

## 🚀 Requisitos Previos

- **.NET 8 SDK** (descargar desde [microsoft.com](https://dotnet.microsoft.com))
- **Visual Studio Code** con extensiones:
  - C# (powered by OmniSharp)
  - Cucumber (Gherkin) Full Support
  - Test Explorer UI

---

## 📦 Instalación de Dependencias

### Opción 1: Usando la Terminal Integrada de VS Code

1. Abre la terminal en VS Code (`Ctrl + `` o `Terminal > New Terminal`)
2. Navega a la carpeta del proyecto:
   ```powershell
   cd d:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr
   ```

3. Restaura las dependencias NuGet:
   ```powershell
   dotnet restore
   ```

4. (Opcional) Instala los generadores de código de Reqnroll:
   ```powershell
   dotnet add package Reqnroll.Tools --version latest
   ```

---

## ▶️ Ejecución de Pruebas

### Opción 1: Ejecutar Todas las Pruebas

```powershell
dotnet test
```

### Opción 2: Ejecutar Pruebas con Detalle

```powershell
dotnet test --verbosity detailed
```

### Opción 3: Ejecutar Pruebas Específicas (por etiqueta)

```powershell
# Ejecutar solo escenarios de seguridad
dotnet test --filter "Seguridad"
```

### Opción 4: Usar Test Explorer de VS Code

1. Abre la **Command Palette** (`Ctrl + Shift + P`)
2. Busca **"Test Explorer"** y selecciona la opción
3. Se abrirá el panel de Test Explorer
4. Haz clic en el botón ▶️ para ejecutar todas las pruebas

---

## 🔍 Escenarios de Prueba

### 1️⃣ Validación de Edad del Cliente
- **Edad 18**: ✅ Aprobado
- **Edad 70**: ✅ Aprobado
- **Edad 17**: ❌ Rechazado (menor a 18)

### 2️⃣ Validación de Ingresos
- **Límite Mínimo ($1,000)**: ✅ Aprobado
- **Menor al Mínimo ($500)**: ❌ Rechazado

### 3️⃣ Validación de Historial Crediticio
- **Historial Vacío**: ⚠️ Evaluación Especial (Limitado)
- **Historial Negativo (Morosidad)**: ❌ Rechazado

### 4️⃣ Validación de Deuda
- **En Límite (36% de ingresos)**: ✅ Aprobado con Condiciones

### 5️⃣ Validación de Seguridad
| Ataque | Respuesta |
|--------|-----------|
| SQL Injection (`' OR '1'='1`) | 🛡️ Bloqueo entrada / Error controlado |
| Acceso sin rol analista | 🛡️ Acceso denegado |
| Alteración de payload | 🛡️ Datos rechazados / Alerta |
| Alteración de score en frontend | 🛡️ Validación backend evita fraude |
| Fuerza bruta en login | 🛡️ Cuenta bloqueada / Alerta |

---

## 🛠️ Construcción del Proyecto

```powershell
# Compilar el proyecto
dotnet build

# Compilar en Release
dotnet build --configuration Release
```

---

## 🐛 Troubleshooting

### ❌ Error: "Feature file not found"
**Solución**: Verifica que los archivos `.feature` estén en la carpeta `Features/` y que el `.csproj` tenga la configuración:
```xml
<ItemGroup>
  <None Update="Features/**/*.feature">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

### ❌ Error: "Type or namespace 'Reqnroll' not found"
**Solución**: Ejecuta `dotnet restore` para restaurar los paquetes NuGet

### ❌ Error: "Step definition not found"
**Solución**: 
1. Verifica que la clase `EvaluacionCreditoStepDefinitions` tenga el atributo `[Binding]`
2. Los métodos deben tener los atributos correctos: `[Given]`, `[When]`, `[Then]`
3. Las expresiones regulares deben coincidir exactamente con el texto de los pasos

---

## 📊 Resultados Esperados

Al ejecutar todas las pruebas, deberías ver:

```
Test Run Summary
================
Total Tests: 13
Passed: 13 ✅
Failed: 0
Skipped: 0

Test Execution Time: ~1000ms
```

---

## 🔐 Características de Seguridad Implementadas

### 1. Detección de SQL Injection
- Patrones regex para detectar comandos SQL maliciosos
- Bloqueo de intentos: `DROP`, `DELETE`, `UNION SELECT`, etc.

### 2. Validación de Roles
- Solo rol "analista" puede procesar solicitudes
- Bloquea accesos no autorizados

### 3. Detección de Tamperizaje
- Detecta alteraciones de payload
- Valida integridad de datos

### 4. Protección contra Fuerza Bruta
- Límite de intentos de login fallidos (máx 3)
- Bloqueo cuenta después de exceder límite

### 5. Validación Backend
- Recalcula scores en el servidor (evita manipulación frontend)
- Rechaza datos que no pasan validación

---

## 💡 Cómo Agregar Nuevos Escenarios

### 1. Añadir el escenario al archivo `.feature`:

```gherkin
Scenario Outline: Nuevo validación
  Given [precondición]
  When [acción]
  Then [resultado esperado]
  
  Examples:
    | dato | resultado |
    | X    | Y         |
```

### 2. Crear el método de paso en `EvaluacionCreditoStepDefinitions.cs`:

```csharp
[Given(@"paso con parámetro (.+)")]
public void GivenPasoConParametro(string parametro)
{
    // Implementación
}
```

### 3. Ejecutar las pruebas:
```powershell
dotnet test
```

---

## 📚 Recursos Útiles

- **Documentación Reqnroll**: https://reqnroll.net/
- **Gherkin Syntax**: https://cucumber.io/docs/gherkin/
- **.NET 8 Docs**: https://docs.microsoft.com/dotnet/

---

## ✅ Checklist de Validación

- [ ] .NET 8 SDK instalado (`dotnet --version`)
- [ ] `dotnet restore` ejecutado sin errores
- [ ] `dotnet build` compile correctamente
- [ ] `dotnet test` corre todos los 13 escenarios
- [ ] 13/13 pruebas pasadas ✅

---

## 👨‍💼 Autor

**Desarrollador Senior QA Automation** | Especialista en .NET | Experto en Reqnroll/BDD

---

Última actualización: Febrero 2026 | Versión: 1.0

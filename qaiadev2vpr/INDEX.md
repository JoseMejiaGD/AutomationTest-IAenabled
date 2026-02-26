# 📑 Índice de Navegación Centralizado

## 🎯 Estructura de Documentación

```
PROYECTO REQNROLL - EVALUACIÓN DE CRÉDITO
│
├─ 🚀 INICIO RÁPIDO
│  ├─ QUICK_START.md          ← Lee primero! (5 min)
│  ├─ README.md               ← Guía principal completa
│  └─ INSTALL_WINDOWS.md      ← Instalación paso a paso
│
├─ 🔧 TÉCNICO & DESARROLLO
│  ├─ TECHNICAL_DOCUMENTATION.md  ← Cómo funcionan los Steps
│  ├─ ARCHITECTURE.md             ← Diseño del proyecto
│  └─ EXAMPLES_AND_USE_CASES.md   ← 8 casos prácticos reales
│
├─ 💻 EJECUCIÓN
│  ├─ COMMANDS.md             ← Comandos listos para copiar
│  └─ FAQ.md                  ← Preguntas frecuentes
│
└─ 📋 RESUMEN
   ├─ PROJECT_SUMMARY.md      ← Estado del proyecto
   └─ INDEX.md                ← Este archivo
```

---

## 📖 Guía por Rol

### 👨‍💻 Desarrollador Nuevo

**Inicio en 30 minutos:**
1. [QUICK_START.md](QUICK_START.md) - Guía rápida
2. [INSTALL_WINDOWS.md](INSTALL_WINDOWS.md) - Instalación
3. [COMMANDS.md](COMMANDS.md) - Comandos útiles
4. `dotnet test` - Ejecutar pruebas

**Próximo aprendizaje:**
- [TECHNICAL_DOCUMENTATION.md](TECHNICAL_DOCUMENTATION.md)
- [EXAMPLES_AND_USE_CASES.md](EXAMPLES_AND_USE_CASES.md)

---

### 🏗️ Arquitecto / Tech Lead

**Entender el diseño:**
1. [ARCHITECTURE.md](ARCHITECTURE.md) - Visión general
2. [README.md](README.md) - Requisitos y estructura
3. [TECHNICAL_DOCUMENTATION.md](TECHNICAL_DOCUMENTATION.md) - Detalles técnicos

**Validación:**
- [PROJECT_SUMMARY.md](PROJECT_SUMMARY.md) - Checklist

---

### 🧪 QA Automation Engineer (Intermedio)

**Entender pruebas:**
1. [README.md](README.md) - Overview
2. [TECHNICAL_DOCUMENTATION.md](TECHNICAL_DOCUMENTATION.md) - Step definitions
3. [EXAMPLES_AND_USE_CASES.md](EXAMPLES_AND_USE_CASES.md) - Casos reales

**Para agregar escenarios:**
- [TECHNICAL_DOCUMENTATION.md](TECHNICAL_DOCUMENTATION.md#-crear-nuevos-steps)

---

### 🔐 Security Researcher

**Validaciones de seguridad:**
1. [ARCHITECTURE.md](ARCHITECTURE.md#-características-de-seguridad-implementadas)
2. Ver: `Services/CreditEvaluator.cs` → método `ValidateSecurity()`
3. [EXAMPLES_AND_USE_CASES.md](EXAMPLES_AND_USE_CASES.md#-caso-3-sql-injection-detectado-y-bloqueado)

---

## 🎓 Aprendizaje Progresivo

### Nivel 1: Principiante (Día 1)
```
┌─────────────────────────────────────┐
│ QUICK_START.md (5 min)              │
│ ↓                                   │
│ INSTALL_WINDOWS.md (10 min)         │
│ ↓                                   │
│ dotnet test (para ver funcionar)    │
│ ↓                                   │
│ COMMANDS.md (consulta rápida)       │
└─────────────────────────────────────┘
```

### Nivel 2: Intermedio (Día 2-3)
```
┌─────────────────────────────────────┐
│ README.md (lectura completa)        │
│ ↓                                   │
│ TECHNICAL_DOCUMENTATION.md          │
│ ↓                                   │
│ Editar 1 scenario existente         │
│ ↓                                   │
│ Ejecutar: dotnet test               │
└─────────────────────────────────────┘
```

### Nivel 3: Avanzado (Semana 1)
```
┌─────────────────────────────────────┐
│ ARCHITECTURE.md (diseño completo)   │
│ ↓                                   │
│ EXAMPLES_AND_USE_CASES.md (8 casos) │
│ ↓                                   │
│ Crear nuevo scenario                │
│ ↓                                   │
│ Implementar validación nueva        │
│ ↓                                   │
│ Integrar con CI/CD                  │
└─────────────────────────────────────┘
```

---

## 📚 Documentos por Propósito

### 🚀 "Necesito ejecutar las pruebas AHORA"
→ [QUICK_START.md](QUICK_START.md)

### ❓ "No funciona, necesito ayuda"
→ [FAQ.md](FAQ.md) (90% de las preguntas están aquí)

### 📝 "Quiero entender cómo funcionan los steps"
→ [TECHNICAL_DOCUMENTATION.md](TECHNICAL_DOCUMENTATION.md)

### 💻 "Necesito copiar un comando"
→ [COMMANDS.md](COMMANDS.md)

### 🏗️ "Quiero entender la estructura"
→ [ARCHITECTURE.md](ARCHITECTURE.md)

### 🔐 "Quiero ver cómo detecta SQL injection"
→ [EXAMPLES_AND_USE_CASES.md](EXAMPLES_AND_USE_CASES.md) (Caso 3)

### ⚙️ "Necesito instalar .NET 8"
→ [INSTALL_WINDOWS.md](INSTALL_WINDOWS.md)

### 📊 "¿Qué está terminado en el proyecto?"
→ [PROJECT_SUMMARY.md](PROJECT_SUMMARY.md)

---

## 🔍 Búsqueda Rápida por Tema

| Tema | Documento | Ubicación |
|------|-----------|-----------|
| Inicio rápido | QUICK_START.md | Toda |
| Instalación .NET 8 | INSTALL_WINDOWS.md | Línea 1 |
| Estructura carpetas | ARCHITECTURE.md | Línea 45 |
| Cómo usan Step Definitions | TECHNICAL_DOCUMENTATION.md | Línea 10 |
| Ejemplo SQL Injection | EXAMPLES_AND_USE_CASES.md | Línea 250 |
| Comandos para ejecutar | COMMANDS.md | Toda |
| Preguntas frecuentes | FAQ.md | Toda |
| Estado del proyecto | PROJECT_SUMMARY.md | Línea 3 |

---

## 🎯 Tareas Comunes

### "Quiero agregar un nuevo escenario"
1. Lee: [TECHNICAL_DOCUMENTATION.md](TECHNICAL_DOCUMENTATION.md#-crear-nuevos-steps)
2. Ver ejemplo: [EXAMPLES_AND_USE_CASES.md](EXAMPLES_AND_USE_CASES.md)
3. Modifica: `Features/EvaluacionCredito.feature`
4. Implementa: `StepDefinitions/EvaluacionCreditoStepDefinitions.cs`
5. Ejecuta: `dotnet test`

### "Quiero cambiar el ingreso mínimo"
1. Lee: [FAQ.md](FAQ.md#p-puedo-cambiar-los-límites-de-edad)
2. Edita: `Services/CreditEvaluator.cs` línea ~20
3. Ejecuta: `dotnet test`

### "Quiero mejorar seguridad"
1. Lee: [ARCHITECTURE.md](ARCHITECTURE.md#-validaciones-de-seguridad-implementadas)
2. Ver ejemplos: [EXAMPLES_AND_USE_CASES.md](EXAMPLES_AND_USE_CASES.md#-caso-3-sql-injection-detectado-y-bloqueado)
3. Edita: `Services/CreditEvaluator.cs` método `ValidateSecurity()`

### "Quiero entender un step"
1. Abre: `Features/EvaluacionCredito.feature`
2. Encuentra el paso
3. Lee: [TECHNICAL_DOCUMENTATION.md](TECHNICAL_DOCUMENTATION.md) buscando ese paso

---

## 📱 Acceso Rápido desde Terminal

```powershell
# Abre cualquier documento
notepad README.md
code README.md              # Si tienes VS Code abierto
explorer .                  # Ver archivos en carpeta

# Buscar texto en documentos
Select-String "SQL" *.md
Select-String "edad" TECHNICAL_DOCUMENTATION.md
```

---

## 🗂️ Estructura Completa de Archivos

```
d:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr\
│
├── 📁 Features/
│   └── EvaluacionCredito.feature
│
├── 📁 StepDefinitions/
│   ├── EvaluacionCreditoStepDefinitions.cs
│   └── Hooks.cs
│
├── 📁 Models/
│   ├── ClientProfile.cs
│   ├── CreditRequest.cs
│   └── CreditResponse.cs
│
├── 📁 Services/
│   └── CreditEvaluator.cs
│
├── 📁 .vscode/
│   ├── settings.json
│   ├── launch.json
│   ├── tasks.json
│   └── extensions.json
│
├── 📄 CreditEvaluation.csproj          [Configuración proyecto]
├── 📄 specflow.json                    [Configuración Reqnroll]
├── 📄 .editorconfig                    [Estilos código]
├── 📄 .gitignore                       [Archivos ignorados]
│
├── 📖 INDEX.md                         [Este archivo - COMIENZA AQUÍ]
├── 📖 README.md                        [Guía principal]
├── 📖 QUICK_START.md                   [Inicio 5 minutos]
├── 📖 INSTALL_WINDOWS.md               [Instalación detallada]
├── 📖 TECHNICAL_DOCUMENTATION.md       [Detalles técnicos]
├── 📖 ARCHITECTURE.md                  [Diseño proyecto]
├── 📖 EXAMPLES_AND_USE_CASES.md        [8 casos prácticos]
├── 📖 PROJECT_SUMMARY.md               [Resumen estado]
├── 📖 FAQ.md                           [Preguntas frecuentes]
└── 📖 COMMANDS.md                      [Comandos listos]
```

---

## 🎯 Punto de Entrada por Escenario

### Escenario: "Soy nuevo, necesito empezar ya"
```
1. QUICK_START.md (5 min)
2. COMMANDS.md    (copia 3 comandos)
3. dotnet test    (ve que funciona)
4. ¡Listo!
```

### Escenario: "Necesito entender todo"
```
1. README.md                    (visión general)
2. TECHNICAL_DOCUMENTATION.md   (cómo funcionan)
3. EXAMPLES_AND_USE_CASES.md   (ejemplos reales)
4. ARCHITECTURE.md              (diseño detallado)
5. FAQ.md                       (preguntas)
```

### Escenario: "Tengo un error"
```
1. COMMANDS.md                  (ejecutar comandos básicos)
2. FAQ.md                       (buscar error)
3. INSTALL_WINDOWS.md           (si es problema de instalación)
4. TECHNICAL_DOCUMENTATION.md   (si es problema de código)
```

### Escenario: "Quiero agregar features"
```
1. TECHNICAL_DOCUMENTATION.md   (cómo crear steps)
2. EXAMPLES_AND_USE_CASES.md   (ver ejemplos)
3. Editar: Features/*.feature
4. Editar: StepDefinitions/*StepDefinitions.cs
5. dotnet test                  (validar)
```

---

## 💡 Tips de Navegación

### En VS Code
- `Ctrl + Shift + P` → "Markdown Preview" para leer documentos
- `Ctrl + F` → Buscar dentro de un documento
- `Ctrl + P` → Ir a un archivo específico

### En Terminal
```powershell
# Buscar palabra en todos los .md
Select-String "palabra" *.md

# Ver un archivo
Get-Content README.md | more

# Contar palabras
(Get-Content README.md | Measure-Object -Word).Words
```

---

## 📊 Estadísticas de Documentación

| Documento | Líneas | Tema |
|-----------|--------|------|
| README.md | 250 | Guía principal |
| QUICK_START.md | 150 | Inicio rápido |
| TECHNICAL_DOCUMENTATION.md | 300 | Explicación técnica |
| INSTALL_WINDOWS.md | 200 | Instalación |
| ARCHITECTURE.md | 350 | Diseño |
| EXAMPLES_AND_USE_CASES.md | 400 | Casos prácticos |
| FAQ.md | 450 | Preguntas frecuentes |
| COMMANDS.md | 300 | Comandos útiles |
| PROJECT_SUMMARY.md | 400 | Resumen proyecto |
| **TOTAL** | **2400+** | **Documentación completa** |

---

## ✅ Preguntas Importantes

- ¿Soy principiante? → *Comienza con QUICK_START.md*
- ¿Tengo experiencia? → *Ve a ARCHITECTURE.md*
- ¿Tengo error? → *Busca en FAQ.md*
- ¿Necesito comando? → *Copia de COMMANDS.md*
- ¿Necesito entender step? → *Lee TECHNICAL_DOCUMENTATION.md*
- ¿Quiero ver ejemplo? → *Mira EXAMPLES_AND_USE_CASES.md*

---

## 🚀 Ahora Qué?

```
¿DÓNDE ESTOY? → INDEX.md (este archivo)
    ↓
¿QUÉ HAGO? → Ver sección "Punto de Entrada por Escenario"
    ↓
¿NECESITO AYUDA? → Consulta FAQ.md
    ↓
¿TENGO ERROR? → Busca en FAQ.md o TECHNICAL_DOCUMENTATION.md
    ↓
¿QUIERO APRENDER MÁS? → Lee los documentos sugeridos
    ↓
¡A CÓDIGO!
```

---

## 📞 Contacto & Soporte

**Si tienes duda:**
1. Busca en [FAQ.md](FAQ.md) (90% de las preguntas están)
2. Si no la encuentras, busca en los documentos
3. Si sigues sin respuesta, consulta [TECHNICAL_DOCUMENTATION.md](TECHNICAL_DOCUMENTATION.md)

---

**Última actualización:** Febrero 2026  
**Versión del proyecto:** 1.0  
**Status:** ✅ Documentación Completa

¡Bienvenido al proyecto! 🚀

---

## 🎁 Bonus: Mapeo Mental del Proyecto

```
REQNROLL PROJECT
│
├─ ENTRADA (Gherkin)
│  └─ Features/EvaluacionCredito.feature
│
├─ MAPEO (Step Definitions)
│  └─ StepDefinitions/EvaluacionCreditoStepDefinitions.cs
│
├─ DATOS (Models)
│  ├─ ClientProfile.cs
│  ├─ CreditRequest.cs
│  └─ CreditResponse.cs
│
├─ LÓGICA (Service - System Under Test)
│  └─ Services/CreditEvaluator.cs
│
├─ VALIDACIÓN (Assertions)
│  └─ Then steps en StepDefinitions
│
└─ RESULTADO
   └─ ✅ Tests Passed
```

¡A propósito, leíste esto completamente? ¡Bravo! 🎉 Ya estás listo para contribuir. 💪

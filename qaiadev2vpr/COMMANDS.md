# Comandos Listos para Copiar y Pegar

## 🚀 Comandos Rápidos de Ejecución

### Inicio Básico (Copia y Pega)

```powershell
cd "d:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr"
dotnet restore
dotnet build
dotnet test
```

### Compilación y Pruebas

```powershell
# Compilar solamente
dotnet build

# Compilar en modo Release
dotnet build --configuration Release

# Limpiar compilación anterior
dotnet clean

# Restaurar paquetes NuGet
dotnet restore
```

### Ejecución de Pruebas

```powershell
# Ejecutar todas las pruebas
dotnet test

# Pruebas con salida detallada
dotnet test --verbosity detailed

# Pruebas con salida mínima
dotnet test --verbosity minimal

# Ejecutar sobre compilación existente
dotnet test --no-build

# Ejecutar pruebas específicas por nombre
dotnet test --filter "Edad"
dotnet test --filter "Ingresos"
dotnet test --filter "Seguridad"
dotnet test --filter "Historial"
dotnet test --filter "Deuda"
```

### Filtros Avanzados de Pruebas

```powershell
# Ejecutar solo pruebas que contienen "Validación"
dotnet test --filter "Validación"

# Ejecutar excluyendo pruebas de seguridad
dotnet test --filter "FullyQualifiedName!~Seguridad"

# Ejecutar múltiples filtros
dotnet test --filter "Edad|Ingresos"
```

### Gestión de Paquetes NuGet

```powershell
# Listar paquetes instalados
dotnet list package

# Listar paquetes desactualizados
dotnet list package --outdated

# Actualizar un paquete específico
dotnet add package Reqnroll --version latest

# Remover un paquete
dotnet remove package NombreDelPaquete
```

---

## 📂 Comandos de Navegación

```powershell
# Cambiar a carpeta del proyecto
cd "d:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr"

# Ver ubicación actual
Get-Location

# Listar archivos
Get-ChildItem

# Listar con detalles
Get-ChildItem -Recurse

# Ver nombre del proyecto
Get-ChildItem *.csproj
```

---

## 🔧 Compilación y Debuggeo

```powershell
# Compilar con información de debuggeo
dotnet build --configuration Debug

# Compilar sin restaurar
dotnet build --no-restore

# Compilar y mostrar advertencias
dotnet build --verbosity detailed

# Ejecutar un archivo compilado
dotnet run

# Ejecutar bin Debug
dotnet bin\Debug\net8.0\CreditEvaluation.dll
```

---

## 📊 Reportes y Análisis

```powershell
# Generar reporte de cobertura de código (si está configurado)
dotnet test /p:CollectCoverageMetrics=true

# Ejecutar con opciones de diagrama
dotnet test --logger "console;verbosity=detailed"

# Generar reporte XML
dotnet test -l "trx;LogFileName=TestResults.trx"
```

---

## 🐛 Debugging y Troubleshooting

```powershell
# Ver versión de .NET instalada
dotnet --version

# Listar runtimes disponibles
dotnet --list-runtimes

# Listar SDKs disponibles
dotnet --list-sdks

# Ver información del sistema
dotnet --info

# Verificar instalación
dotnet --version; dotnet build --version
```

---

## 🎯 Comandos Combinados (Workflow Completo)

### Workflow Básico (1 comando)
```powershell
dotnet clean && dotnet restore && dotnet build && dotnet test
```

### Workflow con Reportes
```powershell
dotnet clean; dotnet restore; dotnet build; dotnet test --verbosity detailed
```

### Workflow Completo con Limpieza
```powershell
Remove-Item bin, obj -Recurse -Force 2>$null; dotnet restore; dotnet build; dotnet test
```

### Workflow para CI/CD
```powershell
dotnet restore --nologo
dotnet build --configuration Release --no-restore
dotnet test --configuration Release --no-build --verbosity detailed
```

---

## VS Code Task Runner (Atajos Teclado)

```
Ctrl + Shift + B     -> Ejecutar tarea "build"
Ctrl + Shift + T     -> Ejecutar tarea "test" (si está configurada)
Ctrl + Shift + P     -> Abrir Command Palette
```

---

## ⚡ PowerShell Aliases Útiles (Opcional)

```powershell
# Crear alias temporal en sesión actual
Set-Alias test-all "dotnet test"
Set-Alias build-proj "dotnet build"
Set-Alias restore-pkg "dotnet restore"

# Luego puedes usar:
test-all
build-proj
restore-pkg

# Para hacer permanente, agregar al perfil de PowerShell
# Ubicación: $PROFILE
# Contenido: Set-Alias test-all "dotnet test"
```

---

## 🔍 Diagnóstico Rápido

```powershell
# Verificar todo
Write-Host "🔍 Verificando instalación..."
Write-Host "Versión .NET: $(dotnet --version)"
Write-Host "Versión PowerShell: $($PSVersionTable.PSVersion)"
Write-Host "SO: $([System.Environment]::OSVersion.VersionString)"
Write-Host "Ruta actual: $(Get-Location)"
Write-Host "Archivos .csproj: $(Get-ChildItem *.csproj | Measure-Object | Select-Object -ExpandProperty Count)"
Write-Host "✅ Sistema listo" -ForegroundColor Green
```

---

## 📝 Crear Script Batch Windows (Opcional)

**Archivo: `run-tests.cmd`**
```batch
@echo off
echo Building project...
dotnet clean
dotnet restore
dotnet build
echo.
echo Running tests...
dotnet test --verbosity detailed
pause
```

Luego ejecutar con: `run-tests.cmd`

---

## 🎓 Comandos Educativos

```powershell
# Ver estructura del proyecto
tree /A

# Contar líneas de código
(Get-ChildItem -Recurse -Include *.cs, *.feature | 
 Get-Content | 
 Measure-Object -Lines).Lines

# Listar todas las clases
Select-String "public class" *.cs -Recurse

# Listar todos los tests
Select-String "public void" StepDefinitions\*.cs
```

---

## 🚨 Solución de Problemas (Comandos)

```powershell
# Si dotnet no se reconoce:
$env:Path += ";C:\Program Files\dotnet"
dotnet --version

# Si hay problemas de permisos:
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser

# Si quedan procesos hangueados:
Stop-Process -Name "dotnet" -Force

# Verificar puerto en uso (si hubiera API):
netstat -ano | findstr :5000
```

---

## 📊 Monitorea tu Build en Tiempo Real

```powershell
# Compilación en watch mode (si lo necesitas)
dotnet watch test

# O simplemente loop de pruebas
while($true) { dotnet test; Write-Host "Presiona Ctrl+C para salir"; Start-Sleep 5 }
```

---

## 🎯 One-Liners Útiles

```powershell
# Ejecutar pruebas y abrir resultados
dotnet test; explorer .\bin\Debug\net8.0\

# Check and test
if((dotnet build) -eq 0) { dotnet test }

# Timestamp de ejecución
"Inicio: $(Get-Date)"; dotnet test; "Fin: $(Get-Date)"

# Contar test cases
[regex]::Matches((Get-Content Features\*.feature), "Scenario").Count
```

---

## 📱 Comandos para VS Code Terminal

```
# Abierto en VS Code: Ctrl + `

cd "d:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr"
dotnet build
dotnet test

# O mediante tareas:
Press: Ctrl + Shift + B (build)
Press: Ctrl + Shift + P -> Tasks: Run Task -> test
```

---

## ✨ Script Completo de Setup (Copia y Pega)

```powershell
# ============================================
# SETUP COMPLETO DEL PROYECTO REQNROLL
# ============================================

Write-Host "🚀 Iniciando setup del proyecto..." -ForegroundColor Green

# 1. Navegación
$projectPath = "d:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr"
Set-Location $projectPath
Write-Host "✅ Ubicación: $(Get-Location)" -ForegroundColor Green

# 2. Limpiar
Write-Host "🧹 Limpiando compilaciones anteriores..." -ForegroundColor Yellow
dotnet clean --nologo 2>$null

# 3. Restaurar
Write-Host "📦 Restaurando dependencias..." -ForegroundColor Yellow
dotnet restore --nologo

# 4. Compilar
Write-Host "🔨 Compilando proyecto..." -ForegroundColor Yellow
dotnet build --nologo

# 5. Ejecutar Pruebas
Write-Host "🧪 Ejecutando pruebas..." -ForegroundColor Yellow
dotnet test --nologo

Write-Host "`n✅ ¡Setup completado!" -ForegroundColor Green
Write-Host "Ubicación del proyecto: $projectPath" -ForegroundColor Cyan
```

---

## 🎁 Bonus: Alias Productivos

```powershell
# Agregar a tu perfil de PowerShell para usar siempre

function proj { Set-Location "d:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr" }
function test-me { dotnet test --verbosity detailed }
function build-me { dotnet build --nologo }
function clean-me { dotnet clean --nologo }
function do-all { dotnet clean; dotnet restore; dotnet build; dotnet test }

# Uso:
# proj        -> Ir a carpeta del proyecto
# build-me    -> Compilar
# test-me     -> Ejecutar pruebas con detalle
# do-all      -> Hacer todo
```

---

¡Copia y Pega estos comandos en tu terminal PowerShell! 🚀

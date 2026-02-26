# Instrucciones de Instalación para Windows PowerShell

## Verificar si .NET 8 está instalado

```powershell
dotnet --version
```

Si no está instalado, descargar desde: https://dotnet.microsoft.com/download

## Pasos de Instalación Rápida

### 1. Abrir PowerShell como Administrador

```powershell
# Navegar a la carpeta del proyecto
cd "d:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr"
```

### 2. Restaurar Dependencias

```powershell
dotnet restore
```

**Salida esperada:**
```
Determinando los proyectos a restaurar...
  Restaurando... [====] 100%
  Paquetes restaurados con éxito.
```

### 3. Compilar el Proyecto

```powershell
dotnet build
```

**Salida esperada:**
```
Build iniciado...
Build completado exitosamente.
```

### 4. Ejecutar Pruebas

```powershell
dotnet test
```

**Salida esperada:**
```
Test Run Summary
Total tests: 13
Passed: 13 ✅
Failed: 0
Duration: ~1.2 seconds
```

## Comandos PowerShell Útiles

| Comando | Descripción |
|---------|-------------|
| `dotnet new` | Crear nuevo proyecto |
| `dotnet add package <NombrePaquete>` | Agregar paquete NuGet |
| `dotnet remove package <NombrePaquete>` | Remover paquete |
| `dotnet list package` | Listar paquetes instalados |
| `dotnet build -c Release` | Compilar en Release |
| `dotnet test --verbosity detailed` | Pruebas con detalle |
| `dotnet clean` | Limpiar archivos compilados |

## Solución de Problemas en PowerShell

### Error: "dotnet: The term 'dotnet' is not recognized"

```powershell
# Reinstalar .NET 8 o agregar a PATH manualmente
$env:Path += ";C:\Program Files\dotnet"
```

### Error: "No such file or directory"

```powershell
# Verificar ruta actual
Get-Location

# Navegar a la carpeta correcta
Set-Location "d:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr"
```

### Error: "Access denied"

```powershell
# Ejecutar PowerShell como Administrador (tecla Windows + X, opción A)
```

## Integración con VS Code en Windows

### Abrir proyecto en VS Code desde PowerShell

```powershell
# Abrir carpeta actual en VS Code
code .

# O especificar la ruta
code "d:\vidapogosoft\cursos\2026\sinergiass\qaGDF\qaiadev2vpr"
```

### Ejecutar tareas desde VS Code

1. Presiona `Ctrl + Shift + B` para compilar
2. Presiona `Ctrl + Shift + D` para el debugger
3. Presiona `Ctrl + Shift + P` → "Test" → "Run Tests"

## Variables de Entorno (Opcional)

```powershell
# Ver todas las variables
Get-ChildItem Env:

# Establecer variable temporal
$env:DOTNET_ROOT = "C:\Program Files\dotnet"

# Establecer variable permanentemente
[System.Environment]::SetEnvironmentVariable("NETCORE_ROOT", "C:\Program Files\dotnet", [System.EnvironmentVariableTarget]::User)
```

## Verificación Final de Instalación

```powershell
# Script de validación
Write-Host "Verificando instalación..."
Write-Host "Versión .NET: $(dotnet --version)"
Write-Host "Versión PowerShell: $($PSVersionTable.PSVersion)"
Write-Host "SO: $([System.Environment]::OSVersion.VersionString)"
Write-Host "Ubicación: $(Get-Location)"
Write-Host "✅ Todo listo para comenzar!"
```

## Próximos Pasos

1. ✅ Instalar .NET 8
2. ✅ Ejecutar `dotnet restore`
3. ✅ Ejecutar `dotnet build`
4. ✅ Ejecutar `dotnet test`
5. 🎉 ¡Éxito!

---

¡Listo para trabajar en Reqnroll! 🚀

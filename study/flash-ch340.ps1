param(
    [string]$hexTool,              # "${CG_TOOL_HEX}"
    [string]$artifactFileName,     # "${BuildArtifactFileName}"
    [string]$artifactBaseName      # "${BuildArtifactFileBaseName}"
)

#Write-Host "=== Iniciando script CH340 ==="

# -----------------------------
# Validação das variáveis
# -----------------------------

#Write-Host "Valor recebido de CG_TOOL_HEX: '$hexTool'"

#if (-not $hexTool) {
#    Write-Host "ERRO: CG_TOOL_HEX não informado pelo Eclipse!"
#    exit 1
#}
#if (-not (Test-Path $hexTool)) {
 #   Write-Host "ERRO: Caminho CG_TOOL_HEX inválido: $hexTool"
 #   exit 1
#}

#if (-not $artifactFileName) {
#    Write-Host "ERRO: BuildArtifactFileName não informado pelo Eclipse!"
#   exit 1
#}
#if (-not (Test-Path $artifactFileName)) {
#    Write-Host "ERRO: Arquivo .out não encontrado: $artifactFileName"
#    exit 1
#}

#if (-not $artifactBaseName) {
#    Write-Host "ERRO: BuildArtifactFileBaseName não informado!"
#    exit 1
#}

#Write-Host "Arquivo de entrada: $artifactFileName"
#Write-Host "Ferramenta HEX: $hexTool"

# -----------------------------
# Converter arquivo para TI-TXT
# -----------------------------
$txtFile = "$artifactBaseName.txt"

Write-Host "Convertendo para TI-TXT..."
& "$hexTool" --ti_txt "$artifactFileName" -o "$txtFile" -order MS -romwidth 16
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERRO: Falha na conversão TI-TXT!"
    exit 1
}

Write-Host "Arquivo gerado: $txtFile"


# -----------------------------
# Localizar o gravador
# -----------------------------
#Write-Host "Procurando gravador..."
$ch340 = Get-PnpDevice -Class Ports |
    Where-Object { $_.Name -match "CH340|USB-SERIAL CH340" }

if (-not $ch340) {
    Write-Host "ERRO: Nenhum gravador encontrado!"
    exit 1
}




# Extrair COM
#$comPort = ($ch340.Name -replace ".*\((COM\d+)\).*", '$1')



# -----------------------------
# Detectar CH340 e validar porta
# -----------------------------

#Write-Host "Procurando dispositivos CH340 reais..."

$ch340List = Get-PnpDevice -Class Ports | Where-Object {
    $_.InstanceId -match "VID_1A86&PID_7523" -or
    $_.InstanceId -match "VID_1A86&PID_5523"
}

if (-not $ch340List) {
    Write-Host "ERRO: Nenhum dispositivo CH340 detectado!"
    exit 1
}

$comRegex = [regex]"\(COM(\d+)\)"
$comPort = $null

foreach ($dev in $ch340List) {
    $match = $comRegex.Match($dev.Name)
    if ($match.Success) {
        $port = "COM$($match.Groups[1].Value)"

        #Write-Host "Teste funcional da porta $port..."

        try {
            $serial = New-Object System.IO.Ports.SerialPort $port,9600,None,8,one
            $serial.Open()
            $serial.Close()

            $comPort = $port
            break  # primeira porta válida -> OK
        }
        catch {
            #Write-Host "⚠ Porta $port ignorada (porta fantasma ou em uso)"
        }
    }
}

if (-not $comPort) {
    Write-Host "ERRO=> COM inativa!"
Add-Type -AssemblyName System
[System.Media.SystemSounds]::Hand.Play()
Write-Host "Desconectou do terminal serial?"	
	
    exit 1
}

#Write-Host "➡ Porta ativa confirmada: $comPort"


if (-not $comPort) {
    Write-Host "ERRO: Não foi possível identificar a COM!"
    exit 1
}

#Write-Host "Gravador encontrado na $comPort"



# -----------------------------
# Executar o BSLDEMO com COM detectada
# -----------------------------
$bsl = "C:\BSLDEMO-2.01c.exe"

if (-not (Test-Path $bsl)) {
    Write-Host "ERRO: BSLDEMO não encontrado: $bsl"
    exit 1
}

# Funções de som usando Beep do Windows
$Beep = Add-Type -MemberDefinition @"
    [DllImport("kernel32.dll")] public static extern bool Beep(uint freq, uint duration);
"@ -Name "WinBeep" -Namespace "Win32" -PassThru

$playSuccess = { $Beep::Beep(2000, 150) } # Beep mais agudo
$playError   = { $Beep::Beep(800, 300) }  # Beep mais grave


Write-Host "GRAVANDO MSP430 VIA $comPort ..."
& "$bsl" "-c$comPort" -m1 -ij -s2 +epr "$txtFile"
if ($LASTEXITCODE -ne 0) {
    Write-Host ">>>REVEJA AS CONECCOES<<<"
#& $playError

Add-Type -AssemblyName System
[System.Media.SystemSounds]::Hand.Play()
    exit 1
}


#Write-Host "=== GRAVADO COM SUCESSO! ==="
#& $playSuccess 

Add-Type -AssemblyName System
[System.Media.SystemSounds]::Exclamation.Play()

exit 0

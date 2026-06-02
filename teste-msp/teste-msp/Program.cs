using System;
using System.IO.Ports;
using System.Text;

class Programa {
    static SerialPort porta;
    enum Estado {
        EsperandoCabecalho,
        EsperandoId,
        RecebendoTexto,
        RecebendoRaw,
        RecebendoTextoUsuario
    }

    static Estado estado = Estado.EsperandoCabecalho;
    static StringBuilder texto = new StringBuilder();
    static StringBuilder textoUsuario = new StringBuilder();
    static byte[] raw = new byte[4];
    static int indiceRaw = 0;

    static void Main() {
        porta = new SerialPort("COM4", 38400);
        porta.DataReceived += Porta_DataReceived;
        porta.Open();
        Console.WriteLine("Aguardando...");
        Console.ReadLine();
    }

    static void Porta_DataReceived(object sender,
        SerialDataReceivedEventArgs e) {
        while (porta.BytesToRead > 0) {
            byte dado = (byte)porta.ReadByte();
            ProcessaByte(dado);
            Console.WriteLine((char)dado);
        }
    }

    static void ProcessaByte(byte dado) {
        switch (estado) {
            case Estado.EsperandoCabecalho:
                if (dado == 0x55) {
                    estado = Estado.EsperandoId;
                } else {
                    textoUsuario.Clear();
                    textoUsuario.Append((char)dado);
                    estado = Estado.RecebendoTextoUsuario;
                }
                break;
            case Estado.EsperandoId:
                if (dado == 0x01) {
                    texto.Clear();
                    estado = Estado.RecebendoTexto;
                } else if (dado == 0x03) {
                    indiceRaw = 0;
                    estado = Estado.RecebendoRaw;
                } else {
                    // O 0x55 recebido não era um cabeçalho válido
                    textoUsuario.Clear();
                    textoUsuario.Append('U');
                    textoUsuario.Append((char)dado);
                    estado = Estado.RecebendoTextoUsuario;
                }
                break;
            case Estado.RecebendoTexto:
                if (dado == '\n') {
                    Console.WriteLine(
                        $"TEXTO: {texto}");
                    estado = Estado.EsperandoCabecalho;
                } else {
                    texto.Append((char)dado);
                }
                break;
            case Estado.RecebendoTextoUsuario:
                if (dado == '\n') {
                    Console.WriteLine(
                        $"MSG USUARIO: {textoUsuario}");
                    estado = Estado.EsperandoCabecalho;
                } else {
                    textoUsuario.Append((char)dado);
                }
                break;
            case Estado.RecebendoRaw:
                raw[indiceRaw++] = dado;
                if (indiceRaw == 4) {
                    Console.WriteLine(
                        $"RAW1={raw[0]} " +
                        $"RAW2={raw[1]} " +
                        $"RAW3={raw[2]} " +
                        $"RAW4={raw[3]}");
                    estado = Estado.EsperandoCabecalho;
                }
                break;
        }//switch estado
    }//processa byte
}//class Program
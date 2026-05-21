using System;
using System.IO.Ports;

namespace TesteUART {
    class Program {
        static SerialPort serialPort;

        static void Main() {
            // Porta serial
            serialPort = new SerialPort {
                PortName = "COM4",
                BaudRate = 38400,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One
            };

            serialPort.ErrorReceived += SerialPort_ErrorReceived;
            serialPort.PinChanged += SerialPort_PinChanged;
            serialPort.DataReceived += SerialPort_DataReceived;

            try {
                serialPort.Open();

                Console.WriteLine($"Monitorando BREAK na porta {serialPort.PortName}...");
                Console.WriteLine("Pressione ENTER para sair.");

                Console.ReadLine();
            } catch (Exception ex) {
                Console.WriteLine($"Erro ao abrir porta serial: {ex.Message}");
            } finally {
                if (serialPort != null && serialPort.IsOpen)
                    serialPort.Close();
            }
        }

        private static void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e) {
            if (e.EventType == SerialError.Frame) {
                Console.WriteLine("[ERRO] Break detectado.");
            }
        }

        private static void SerialPort_PinChanged(object sender, SerialPinChangedEventArgs e) {
            if (e.EventType == SerialPinChange.Break) {
                Console.WriteLine("[PINCHANGE] Break detectado");
            }
        }

        private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            SerialPort sp = (SerialPort)sender;
            var v = sp.ReadByte();
            Console.WriteLine(v);
        }
    }
}
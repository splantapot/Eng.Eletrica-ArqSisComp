using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gerenciamento_memoria {
    public class Comunicacao {
        string name;
        string message;

        const int baudRate = 38400;
        const int dataBits = 8;
        const Parity parity = Parity.None;
        const StopBits stopBits = StopBits.One;
        const int ReadTimeout = 500;
        const int WriteTimeout = 500;

        public bool success = true;

        static bool _continue;
        static SerialPort _serialPort;

        public Comunicacao() {
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            Thread readThread = new Thread(Read);

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = SetPortName(_serialPort.PortName);
            _serialPort.BaudRate = baudRate;
            _serialPort.Parity = parity;
            _serialPort.DataBits = dataBits;
            _serialPort.StopBits = stopBits;

            // Set the read/write timeouts
            _serialPort.ReadTimeout = ReadTimeout;
            _serialPort.WriteTimeout = WriteTimeout;

            try {
                _serialPort.Open();
                success = true;
            } catch {
                Console.WriteLine("Não foi possível abrir a porta serial. Verifique as configurações e tente novamente.");
                success = false;
                return;
            }

            /*readThread.Start();
            _continue = true;
            Console.Write("Name: ");
            name = Console.ReadLine();

            Console.WriteLine("Type QUIT to exit");

            while (_continue) {
                message = Console.ReadLine();

                if (stringComparer.Equals("quit", message)) {
                    _continue = false;
                } else {
                    _serialPort.WriteLine(
                        String.Format("<{0}>: {1}", name, message));
                }
            }

            readThread.Join();
            _serialPort.Close();*/
        }

        public void SetCallback(SerialDataReceivedEventHandler callback) {
            _serialPort.DataReceived += callback;
        }

        public static void Read() {
            while (_continue) {
                try {
                    string message = _serialPort.ReadLine();
                    Console.WriteLine(message);
                } catch (TimeoutException) { }
            }
        }

        // Display Port values and prompt user to enter a port.
        public static string SetPortName(string defaultPortName) {
            string portName;

            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames()) {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
            portName = Console.ReadLine();

            if (portName == "" || !(portName.ToLower()).StartsWith("com")) {
                portName = defaultPortName;
            }
            return portName;
        }


    }
}

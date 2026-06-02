using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gerenciamento_memoria {

    public enum STATE {
        DONE = 0,
        STRING = 1,
        USER_STRING = 2,
        RAW = 3,
        WAITING_ID = 4
    }

    public class Communication {

        /* Default settings */
        const int baudRate = 38400;
        const int dataBits = 8;
        const Parity parity = Parity.None;
        const StopBits stopBits = StopBits.One;
        const int ReadTimeout = 500;
        const int WriteTimeout = 500;

        public const char dummyByte = (char)0x55; //Dummy byte: "U", start comunication

        static SerialPort _serialPort;

        public Communication() {
            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            // Setup the properties
            _serialPort.BaudRate = baudRate;
            _serialPort.Parity = parity;
            _serialPort.DataBits = dataBits;
            _serialPort.StopBits = stopBits;

            // Set the read/write timeouts
            _serialPort.ReadTimeout = ReadTimeout;
            _serialPort.WriteTimeout = WriteTimeout;

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

        /* ====================================  */
        /* Get all COMx Ports                    */
        /* ====================================  */
        public string[] GetPortList() {
            return SerialPort.GetPortNames();
        }

        /* ====================================  */
        /* Get status from COMx Port             */
        /* ====================================  */
        public bool IsConnected() {
            return _serialPort.IsOpen;
        }

        /* ====================================  */
        /* Open the COMx Port                    */
        /* ====================================  */
        public bool Open(string port) {
            try {
                _serialPort.PortName = port;
                _serialPort.Open();
                return true;
            } catch {
                Console.WriteLine("Não foi possível abrir a porta serial. Verifique as configurações e tente novamente.");
                return false;
            }
        }

        /* ====================================  */
        /* Closes the COMx Port                  */
        /* ====================================  */
        public bool Close(string port) {
            if (String.IsNullOrEmpty(_serialPort.PortName)) {
                Console.WriteLine("Não é possível fechar porta nula.");
                return false;
            }
            try {
                _serialPort.Close();
                return true;
            } catch {
                Console.WriteLine("Não foi possível fechar a porta serial.");
                return false;
            }
        }

        /* ====================================  */
        /* Prepares the COMx Port to read        */
        /* ====================================  */
        public void SetReadCallback(SerialDataReceivedEventHandler callback) {
            _serialPort.DataReceived += callback;
        }

        /* ====================================  */
        /* Removes a COMx Port callback event    */
        /* ====================================  */
        public void RmvReadCallback(SerialDataReceivedEventHandler callback) {
            _serialPort.DataReceived -= callback;
        }

        /* ====================================  */
        /* Writes to COMx Port                   */
        /* ====================================  */
        public void WriteRaw(byte data) {
            _serialPort.Write(new[] { data }, 0, 1);
        }

        public void WriteChar(string str) {
            _serialPort.Write(str);
        }

        public void WriteBreak() {
            _serialPort.BreakState = true;
            System.Threading.Thread.Sleep(1);
            _serialPort.BreakState = false;
        }
    }
}

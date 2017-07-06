using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;



namespace NordCar.Carla.Data.Infrastructure
{
    public class SocketToCarla : IDisposable
    {
        #region Variables
        private const string iso8859 = "iso-8859-1";

        private const long _Ticks1970 = 621355968000000000;
        private const int bufferSize = 8192;//1024 * 16;
        private byte[] m_DataBuffer = new byte[bufferSize];
        //private IAsyncResult m_asynResult;
        //private AsyncCallback pfnCallBack;
        private Socket m_socClient;
        private IPEndPoint _RemoteEndPoint;
        //private bool _autoReconnect;
        //private Timer _ReconnectTimer;
        //private int _reconnectCount = 0;
        private string _ipAdr = string.Empty;
        private int _port = -1;
        private Stopwatch mywatch = null;

        
        #endregion

        public SocketToCarla(string ipAdr, int port, string logfilepath)
        {
            _ipAdr = ipAdr;
            _port = port;

            if (logfilepath != string.Empty)
            {
                FileLog.OpenLog(logfilepath);
            }
        
        }
        static byte[] AppendTwoByteArrays(byte[] arrayA, byte[] arrayB)
        {
            byte[] outputBytes = new byte[arrayA.Length + arrayB.Length];
            Buffer.BlockCopy(arrayA, 0, outputBytes, 0, arrayA.Length);
            Buffer.BlockCopy(arrayB, 0, outputBytes, arrayA.Length, arrayB.Length);
            return outputBytes;
        }
        /// <summary>
        /// Convert a HexAscii encoded string to a byte array
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static byte[] HexAscii2Bytes(string Data)
        {
            if (Data.Length % 2 != 0)
                throw new ArgumentException("Must contain an even number of chars");

            var result = new byte[Data.Length / 2];
            int index = 0;
            while (Data.Length > 0)
            {
                // take two chars and convert into hex value using substring.
                result[index] = (Byte)Convert.ToUInt32(Data.Substring(0, 2), 16);
                index++;
                Data = Data.Substring(2, Data.Length - 2);
            }
            return result;
        }

        /// <summary>
        /// Encode a Byte array to a hesAscii encoded string
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static string Bytes2HexAscii(byte[] Data)
        {
            string result = string.Empty;
            foreach (Byte b in Data)
            {
                result += AsciiCode2Char((b & 0xF0) >> 4);
                result += AsciiCode2Char(b & 0x0F);
            }
            return result;
        }

        /// <summary>
        /// Convert char code 0-15 to Ascii char '0'-'9' and 'A'-'F'
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static Char AsciiCode2Char(int code)
        {
            if (code < 10)
                return (Char)(code + 0x30);
            else
                return (Char)(code + 0x37);
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        public byte[] GetDataRetByteArr(string str)
        {
            Trace.TraceInformation("Start: GetDataFromCarla");
            mywatch = Stopwatch.StartNew();


            //bool receiving = true;
            StringBuilder response = new StringBuilder();
            const int bufferSizeXX = 1024 * 16;
		    byte[] bufferX = new byte[bufferSizeXX];
	
            //byte[] resbyte = null; ;
            //create the end point 
            _RemoteEndPoint = new IPEndPoint(IPAddress.Parse(_ipAdr), _port);

            //create the socket instance...
            m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //connect to the remote host...
            m_socClient.Connect(_RemoteEndPoint);

            // Convert the string data to byte data using ASCII encoding.

            //FileLog.WriteLog(FileLog.Level.DEBUG, "CSK Data to send: " + str);
            //byte[] byteData = Encoding.ASCII.GetBytes(str); //æ,ø,å problems  
            byte[] byteData = Encoding.GetEncoding(iso8859).GetBytes(str.ToCharArray());
            // byte[] byteData = Helpers.HexAscii2Bytes(str); //DO NOT WORK
            //byte[] byteData = Encoding.Unicode.GetBytes(str);
            int bytesRead;
            Write(byteData);

            do//while (receiving)
            {
                //System.Threading.Thread.Sleep(10);
                bytesRead = m_socClient.Receive(m_DataBuffer, 0, m_DataBuffer.Length, SocketFlags.None);

                if (bytesRead == 0)
                    break;

                Debug.WriteLine("Received no of bytes, {0}", bytesRead);


                // if (bytesRead > 1)

                //{
                byte[] buff = new byte[bytesRead];
                Array.Copy(m_DataBuffer, buff, bytesRead);
                //response = Encoding.ASCII.GetString(buff, 0, bytesRead);
                response.Append(System.Text.Encoding.GetEncoding(iso8859).GetString(buff, 0, bytesRead));
                //bufferX = AppendTwoByteArrays(bufferX, buff);
                //long nTime = ((DateTime.UtcNow.Ticks - _Ticks1970) / 10000); // millisec

                //Debug.WriteLine("time, {0}", nTime);

                //    if (bytesRead < bufferSize)
                //        receiving = false;

                FileLog.WriteLog(FileLog.Level.DEBUG, "Data received: " + bytesRead + " : " + response);

                //  }


            } while (bytesRead > 0);
            Close();
            Trace.TraceInformation("End carla time=" + mywatch.Elapsed);
            return Convert.FromBase64String(response.ToString());
        }

        public string GetData(string str)
        {

            Trace.TraceInformation("Start: GetDataFromCarla");
            mywatch = Stopwatch.StartNew();

            //bool receiving = true;
            StringBuilder response = new StringBuilder();
            //create the end point 
            _RemoteEndPoint = new IPEndPoint(IPAddress.Parse(_ipAdr), _port);

            //create the socket instance...
            m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //connect to the remote host...
            m_socClient.Connect(_RemoteEndPoint);

            // Convert the string data to byte data using ASCII encoding.
           
            //FileLog.WriteLog(FileLog.Level.DEBUG, "CSK Data to send: " + str);
            //byte[] byteData = Encoding.ASCII.GetBytes(str); //æ,ø,å problems  
            byte[] byteData = Encoding.GetEncoding(iso8859).GetBytes(str.ToCharArray());
            // byte[] byteData = Helpers.HexAscii2Bytes(str); //DO NOT WORK
            //byte[] byteData = Encoding.Unicode.GetBytes(str);
            int bytesRead;
            Write(byteData);

            do//while (receiving)
            {
                //System.Threading.Thread.Sleep(10);
                 bytesRead = m_socClient.Receive(m_DataBuffer, 0, m_DataBuffer.Length, SocketFlags.None);
                 
                 if (bytesRead == 0)
                 break;

                  Debug.WriteLine("Received no of bytes, {0}", bytesRead);


               // if (bytesRead > 1)
               
                //{
                    byte[] buff = new byte[bytesRead];
                    Array.Copy(m_DataBuffer, buff, bytesRead);
                    //response = Encoding.ASCII.GetString(buff, 0, bytesRead);
                    response.Append(System.Text.Encoding.GetEncoding(iso8859).GetString(buff, 0, bytesRead));
              //      long nTime = ((DateTime.UtcNow.Ticks - _Ticks1970) / 10000); // millisec

              //      Debug.WriteLine("time, {0}", nTime);

                //    if (bytesRead < bufferSize)
                //        receiving = false;

                //    FileLog.WriteLog(FileLog.Level.DEBUG, "Data received: " + bytesRead + " : " + response);

              //  }


            } while (bytesRead > 0);
            Close();
            Trace.TraceInformation("End carla time=" + mywatch.Elapsed);
            return response.ToString();
        }

        private bool Write(byte[] data)
        {
            try
            {
                m_socClient.Send(data);
            }
            catch (SocketException se)
            {
                Debug.WriteLine("Write {0}", se.StackTrace);
                return false;
            }
            return true;
        }

        public bool Close()
        {
            if (m_socClient != null)
            {
                if (m_socClient.IsBound)
                    m_socClient.Shutdown(SocketShutdown.Both);
                m_socClient.Close();
                m_socClient = null;
            }
            FileLog.WriteLog(FileLog.Level.DEBUG, "Closing socket");
            return true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.

        }
    }
}


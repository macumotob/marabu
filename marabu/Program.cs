using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace marabu
{
  class Program
  {

  //  private static readonly string _serverUrl = "ws://localhost:8181/";
    [STAThread]
    static void Main()
    {
            //PrnReader reader = new PrnReader();
            //reader.Load(@"F:\Develops\PrnView\CobaPrnView\data\000(3).prn");// "F:\Develops\marabu\marabu\bin\Debug\data\000(3).prn" );
            //reader.Parse();
            //return;
      Manager.Log.Log("\r\n\r\n\r\n\t\tMARABU start.\r\n\r\n\r\n");
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.ThreadException += Application_ThreadException;

      //CiscoDialogsHistory.CreateHistoryDirectory();

      Application.Run(new frmMain());
      Manager.Log.Log("MARABU stop.");
    }

    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
      Manager.Log.Log(e.Exception, " Application_ThreadException ------- ");
      MessageBox.Show(e.Exception.ToString());
    }
    /*
static void ServersMain(string[] args)
{

 HPWSServer.Run();
// string socketUrl = "ws://echo.websocket.org/tapi";
 StartServer();
 Console.WriteLine("\nType 'exit' to exit.\n");
 while (true)
 {
   Thread.Sleep(1000);
   Console.Write("> ");
   var msg = Console.ReadLine();
   if (msg == "exit")
     break;
 }
}
*/

    /*
    public static void ResponseToClient(ref TcpClient client)
    {
      using (var stream = client.GetStream())
      {
        List<byte> requestList = new List<byte>();

        //wait until there is data in the stream
        while (!stream.DataAvailable) { }

        //read everything in the stream
        while (stream.DataAvailable)
        {
          requestList.Add((byte)stream.ReadByte());
        }

        //send response
        byte[] response = GenerateHTTPResponse(requestList.ToArray());
        stream.Write(response, 0, response.Length);
      }
    }*/
    /*
    public static string decodeMessage(byte[] buffer)
    {
      int payloadStartIndex = 2;

      var firstNibble = (byte)(buffer[0] & 0xF0);
      var secondNibble = (byte)(buffer[0] & 0x0F);

      // When the first bit of the first byte is set,
      // It means that the current frame is the final frame of a message

      //  The opcode consists of the last four bits in the first byte
      //frame.Opcode = (Opcodes)secondNibble;

      // The last bit of the second byte is the masking bit
      bool isMasked = Convert.ToBoolean((buffer[1] & 0x80) >> 7);

      // Payload length is stored in the first seven bits of the second byte
      var payloadLength = (ulong)(buffer[1] & 0x7F);

      // From RFC-6455 - Section 5.2
      // "If 126, the following 2 bytes interpreted as a 16-bit unsigned integer are the payload length
      // (expressed in network byte order)"
      if (payloadLength == 2)//TwoBytesLengthCode)
      {
        Array.Reverse(buffer, payloadStartIndex, 2);
        payloadLength = BitConverter.ToUInt16(buffer, payloadStartIndex);
        payloadStartIndex += 2;
      }

      // From RFC-6455 - Section 5.2
      // "If 127, the following 8 bytes interpreted as a 64-bit unsigned integer (the most significant bit MUST be 0) 
      // are the payload length (expressed in network byte order)"
      else if (payloadLength == 8)// EightBytesLengthCode)
      {
        Array.Reverse(buffer, payloadStartIndex, 8);
        payloadLength = BitConverter.ToUInt64(buffer, payloadStartIndex);
        payloadStartIndex += 8;
      }
      int MaskingKey = 0;
      // From RFC-6455 - Section 5.2
      // "All frames sent from the client to the server are masked by a
      // 32-bit value that is contained within the frame.  This field is
      // present if the mask bit is set to 1 and is absent if the mask bit
      // is set to 0."
      if (isMasked)
      {
        MaskingKey = BitConverter.ToInt32(buffer, payloadStartIndex);
        payloadStartIndex += 4;
      }
      //buffer = new byte[(int)frame.PayloadLength + payloadStartIndex];
      var content = new byte[payloadLength];
      // Array.Copy(buffer, payloadStartIndex, content, 0, (int)frame.PayloadLength);

      if (isMasked)
        UnMask(content, MaskingKey);

      //frame.UnmaskedPayload = content;

      return "";
    }
    private static void UnMask(byte[] payload, int maskingKey)
    {
      int currentMaskIndex = 0;

      byte[] byteKeys = BitConverter.GetBytes(maskingKey);
      for (int index = 0; index < payload.Length; ++index)
      {
        payload[index] = (byte)(payload[index] ^ byteKeys[currentMaskIndex]);
        currentMaskIndex = (++currentMaskIndex) % 4;
      }
    }
    */
    /*
        public static string GetOrigin(string request)
        {

          int i = request.IndexOf("Origin:");

          return Regex.Match(request, @"(?<=Origin:\s).*(?=\r\n)").Value;
        }

        public static string GetKey1(string request)
        {
          return Regex.Match(request, @"(?<=Sec-WebSocket-Key1:\s).*(?=\r\n)").Value;
        }

        public static string GetKey2(string request)
        {
          return Regex.Match(request, @"(?<=Sec-WebSocket-Key2:\s).*(?=\r\n)").Value;
        }

        public static byte[] GenerateResponseToken(string key1, string key2, byte[] request_token)
        {
          int part1 = (int)(ExtractNums(key1) / CountSpaces(key1));
          int part2 = (int)(ExtractNums(key2) / CountSpaces(key2));
          byte[] key1CalcBytes = ReverseBytes(BitConverter.GetBytes(part1));
          byte[] key2CalcBytes = ReverseBytes(BitConverter.GetBytes(part2));
          byte[] sum = key1CalcBytes
                      .Concat(key2CalcBytes)
                      .Concat(request_token).ToArray();

          return new MD5CryptoServiceProvider().ComputeHash(sum);
        }

        public static int CountSpaces(string key)
        {
          return key.Count(c => c == ' ');
        }

        public static long ExtractNums(string key)
        {
          char[] nums = key.Where(c => Char.IsNumber(c)).ToArray();
          return long.Parse(new String(nums));
        }

        //converts to big endian
        private static byte[] ReverseBytes(byte[] inArray)
        {
          byte temp;
          int highCtr = inArray.Length - 1;

          for (int ctr = 0; ctr < inArray.Length / 2; ctr++)
          {
            temp = inArray[ctr];
            inArray[ctr] = inArray[highCtr];
            inArray[highCtr] = temp;
            highCtr -= 1;
          }
          return inArray;
        }
     */
  }

}


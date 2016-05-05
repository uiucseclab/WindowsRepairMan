using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Net.Security;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;

namespace WindowsRepairMan
{
    public class CryptoService
    {
        private Hashtable certificateErrors = new Hashtable();

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPoliciyErrors)
        {
            if (sslPoliciyErrors == SslPolicyErrors.None)
                return true;
            return false;
            
        }

        static string ReadMessage(SslStream sslStream)
        {
            byte[] buf = new byte[2048];
            StringBuilder msgData = new StringBuilder();
            int bytes = -1;

            do
            {
                bytes = sslStream.Read(buf, 0, buf.Length);

            }
        }

        public string GetKey(string serverName)
        {
            try
            {
                string address = ConfigurationSettings.AppSettings["ServerAddress"];
                int port = int.Parse(ConfigurationSettings.AppSettings["Port"]);
                SslStream sslStream = new SslStream( new TcpClient(address, port).GetStream(), false, new RemoteCertificateValidationCallback (ValidateServerCertificate), null);
                try
                {
                    sslStream.AuthenticateAsClient(serverName);
                }
                catch (AuthenticationException ex)
                {
                    sslStream.Close();
                    return "FAILURE";
                }
                byte[] msg = Encoding.UTF8.GetBytes(("Key?"));
                sslStream.Write(msg);
                sslStream.Flush();

                string response = 


            }
            catch (Exception ex) { }
        }
    }
}

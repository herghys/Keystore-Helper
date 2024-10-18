using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Keystore_Extractor.Models;

namespace Keystore_Extractor.Commands
{
    internal class KeytoolCommandRunner
    {
        //keytool -genkeypair -alias mykey -keyalg RSA -keysize 2048 -keystore mykeystore.keystore -validity 365 -dname "CN=Your Name, OU=Your Organizational Unit, O=Your Company, L=Your City, ST=Your State, C=Your Country"

        public static void CreateKeystoreCommand(KeystoreModel keystore, DnameModel dname)
        {

        }


        public static void ExtractKeystoreCommand(KeystoreModel keystore)
        {
            if (string.IsNullOrEmpty(keystore.FilePath))
                return;
            string keystoreFile = keystore.FilePath;

            if (string.IsNullOrEmpty(keystore.Alias))
                return;
            string alias = keystore.Alias;

            if (string.IsNullOrEmpty(keystore.StorePass))
                return;
            string storepass = keystore.StorePass;

            // Build the command to run keytool
            string keytoolCmd = $"-list -v -alias {alias} -keystore \"{keystoreFile}\" -storepass {storepass}";

            // Execute the command and extract SHA1 and SHA256
            using (Process process = CreateKeytoolProcess())
            {
                process.StartInfo.Arguments = keytoolCmd;

                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                keystore.SHA1 = ExtractValue(output, "SHA1");
                keystore.SHA256 = ExtractValue(output, "SHA256");

                process.Dispose();
            }
        }

        public static string ExtractValue(string output, string shaType)
        {
            string shaPattern = $"{shaType}:";
            int shaIndex = output.IndexOf(shaPattern);
            if (shaIndex != -1)
            {
                int startIndex = shaIndex + shaPattern.Length;
                string shaValue = output.Substring(startIndex).Trim();
                return shaValue.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)[0];
            }
            return string.Empty;
        }

        public static Process CreateKeytoolProcess()
        {
            Process process = new Process();
            process.StartInfo.FileName = "keytool";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            return process;
        }
    }
}

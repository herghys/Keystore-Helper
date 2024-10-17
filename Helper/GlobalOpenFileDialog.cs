using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

namespace Keystore_Extractor.Helper
{
    internal class GlobalOpenFileDialog
    {
        private static readonly GlobalOpenFileDialog instance = new GlobalOpenFileDialog();
        private OpenFileDialog openFileDialog;

        private GlobalOpenFileDialog()
        {
            openFileDialog=new OpenFileDialog
            {
                Title="Select a Keystore File",
                Filter="Keystore Files (*.keystore)|*.keystore|All Files (*.*)|*.*",
                Multiselect=false // Only allow single file selection
            };
        }

        public static GlobalOpenFileDialog Instance => instance;

        public bool? ShowDialog()
        {
            return openFileDialog.ShowDialog();
        }

        public string FileName => openFileDialog.FileName;
    }
}

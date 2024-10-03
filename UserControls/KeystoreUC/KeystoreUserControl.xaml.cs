using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.ComponentModel;
using System.Collections.ObjectModel;
using Keystore_Extractor.UserControls.KeystoreUC;
using Microsoft.Win32;
using Keystore_Extractor.Helper;

namespace Keystore_Extractor.UserControls
{
    /// <summary>
    /// Interaction logic for KeystoreUserControl.xaml
    /// </summary>
    public partial class KeystoreUserControl : UserControl
    {
        public KeystoreModel Keystore { get; private set; } = new KeystoreModel();

        public KeystoreUserControl()
        {
            InitializeComponent();
            Keystore=new KeystoreModel(); // Initialize with a new KeystoreModel
            DataContext=Keystore; // Set DataContext to the new model
        }

        public KeystoreUserControl(KeystoreModel model)
        {
            InitializeComponent();
            Keystore=model;
            DataContext=Keystore;

        }

        private void BrowseFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title="Select a Keystore File",
                Filter="Keystore Files (*.keystore)|*.keystore|All Files (*.*)|*.*",
                Multiselect=false // Only allow single file selection
            };

            if (openFileDialog.ShowDialog()==true)
            {
                // Set the FilePath property of the bound model
                if (DataContext is KeystoreModel model)
                {
                    model.FilePath=openFileDialog.FileName;
                }
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            EventsAndActions.OnRemove?.Invoke(((KeystoreModel)DataContext).Id);
        }

        // Helper method to find the ancestor of a specific type
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current!=null)
            {
                if (current is T ancestor)
                {
                    return ancestor;
                }
                current=VisualTreeHelper.GetParent(current);
            }
            return null;
        }
    }
}

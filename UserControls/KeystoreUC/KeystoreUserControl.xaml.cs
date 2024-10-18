using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using Keystore_Extractor.Helper;
using Keystore_Extractor.Models;

namespace Keystore_Extractor.UserControls.KeystoreUC
{
    /// <summary>
    /// Interaction logic for KeystoreUserControl.xaml
    /// </summary>
    public partial class KeystoreUserControl : UserControl
    {
        public KeystoreModel Keystore { get; set; } = new KeystoreModel();

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


        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Toggle the visibility of the details section
            if (Collapsible_Detail.Visibility==Visibility.Visible)
            {
                Collapsible_Detail.Visibility=Visibility.Collapsed;
                RotateArrow(0); // Reset arrow rotation
            }
            else
            {
                Collapsible_Detail.Visibility=Visibility.Visible;
                RotateArrow(90); // Rotate arrow to indicate open state
            }
        }

        private void RotateArrow(double angle)
        {
            // Rotate the arrow based on the toggle state
            var rotateTransform = (RotateTransform)Arrow.RenderTransform;
            rotateTransform.Angle=angle;
        }
    }
}

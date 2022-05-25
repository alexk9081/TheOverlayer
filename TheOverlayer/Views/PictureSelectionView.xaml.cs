﻿using System;
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
using Microsoft.Win32;
using System.IO;

namespace TheOverlayer.Views
{
    /// <summary>
    /// Interaction logic for PictureSelectionView.xaml
    /// </summary>
    public partial class PictureSelectionView : UserControl
    {
        public PictureSelectionView()
        {
            InitializeComponent();
        }

        private void Open_Pic_File_Tku(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImage.Source = new BitmapImage(
                    new Uri(openFileDialog.FileName));
            }
        }
    }
}

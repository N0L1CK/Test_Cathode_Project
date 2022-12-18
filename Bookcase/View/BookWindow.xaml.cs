﻿using Bookcase.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bookcase.View
{
    /// <summary>
    /// Логика взаимодействия для BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        public Book Book { get; private set; }
        private int _noOfErrorsOnScreen = 0;
        public BookWindow(Book book)
        {
            InitializeComponent();
            this.Book = book;
            DataContext = book;
        }
        /// <summary>
        /// Button Accept Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Move Window Mouse Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Count of error text box ValidationError Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _noOfErrorsOnScreen++;
            else
                _noOfErrorsOnScreen--;

            Save.IsEnabled = _noOfErrorsOnScreen > 0 ? false : true;

        }

        private void DateText_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (DateText.Text.Length == 0)
                DateText.Text = "0";
        }

        private void DateText_KeyDown(object sender, KeyEventArgs e)
        {

            if (((int)e.Key >= 34 && (int)e.Key <= 43) || 
               ((int)e.Key >= 74 && (int)e.Key <= 83))
            {
                e.Handled = false;
            }
            else 
            {
                e.Handled = true;
            }
        }
    }
}

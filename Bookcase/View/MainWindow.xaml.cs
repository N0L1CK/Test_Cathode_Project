using Bookcase.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;



namespace Bookcase.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AppViewModel();
            BooksBox.Items.SortDescriptions.Add(new SortDescription("Author", ListSortDirection.Ascending));
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
        /// Close Window Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Minimalize Window Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMinimalize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// Open setting Window Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Sorting Colection Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)SortingBox.SelectedItem;
            string? SortProperty = typeItem.Content.ToString();
            Sort(SortProperty);
        }
        private void Sort(string? SortProperty)
        {
            if (SortProperty != null && BooksBox != null)
            {
                BooksBox.Items.SortDescriptions[0] = new SortDescription(SortProperty, ListSortDirection.Ascending);
            }
        }

    }
}

using Bookcase.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;



namespace Bookcase.View
{
    /// <summary>
    /// Logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
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
            var typeItem = (ComboBoxItem)SortingBox.SelectedItem;
            var sortProperty = typeItem.Content.ToString();
            Sort(sortProperty);
        }
        private void Sort(string? sortProperty)
        {
            if (sortProperty != null && BooksBox != null)
            {
                BooksBox.Items.SortDescriptions[0] = new SortDescription(sortProperty, ListSortDirection.Ascending);
            }
        }

    }
}

using Bookcase.Data;
using Bookcase.Interfaces;
using Bookcase.Model;
using Bookcase.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Bookcase.ViewModel
{
    public class AppViewModel : INotifyPropertyChanged
    {
        readonly ApplicationContext db = new();
        ICommand? addCommand;
        ICommand? editCommand;
        ICommand? deleteCommand;
        private Book? selectedBook;
        public ObservableCollection<Book> Books { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public AppViewModel()
        {
            db.Database.EnsureCreated();
            db.Books.Load();
            Books = db.Books.Local.ToObservableCollection();
        }
        /// <summary>
        /// Command Add Book
        /// </summary>
        
        public ICommand AddCommand
        {
            get
            {
                return addCommand ??= new Command((obj) =>
                {
                    BookWindow bookWindow = new(new Book());
                    if (bookWindow.ShowDialog() == true)
                    {
                        Book book = bookWindow.Book;
                        db.Books.Add(book);
                        db.SaveChanges();
                    }
                });
            }
        }
        /// <summary>
        /// Command Edit Book
        /// </summary>
        public ICommand EditCommand
        {
            get
            {
                return editCommand ??= new Command((selectedItem) =>
                {
                    if (selectedItem is not Book book) return;
                    Book vm = new()
                    {
                        Id = book.Id,
                        Name = book.Name,
                        Author = book.Author,
                        DateEdition = book.DateEdition,
                        Genre = book.Genre
                    };
                    BookWindow bookWindow = new(vm);

                    if (bookWindow.ShowDialog() == true)
                    {
                        book.Name = bookWindow.Book.Name;
                        book.Author = bookWindow.Book.Author;
                        book.DateEdition = bookWindow.Book.DateEdition;
                        book.Genre = bookWindow.Book.Genre;

                        db.Entry(book).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }, (selectedItem) => Books.Count > 0);
            }
        }
        /// <summary>
        /// Command for delete book 
        /// </summary>
        
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??= new Command(obj =>
                {
                    if (obj is not Book book) return;
                    var res = (MessageBox.Show("Are you sure?", "Delete",
                        MessageBoxButton.YesNo, MessageBoxImage.Question,
                        MessageBoxResult.No)).ToString();
                    if (res == "Yes")
                    {
                        db.Books.Remove(book);
                        db.SaveChanges();
                    }
                },
                (obj) => Books.Count > 0);
            }
        }

        /// <summary>
        /// Selection For Delete or Edit
        /// </summary>
        public Book? SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }

        /// <summary>
        /// Declare the event
        /// </summary>

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}

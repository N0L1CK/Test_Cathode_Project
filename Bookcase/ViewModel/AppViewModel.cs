using Bookcase.Data;
using Bookcase.Model;
using Bookcase.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Bookcase.ViewModel
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationContext _db = new();
        private ICommand? _addCommand;
        private ICommand? _editCommand;
        private ICommand? _deleteCommand;
        private Book? _selectedBook;
        public ObservableCollection<Book> Books { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public AppViewModel()
        {
            _db.Database.EnsureCreated();
            _db.Books.Load();
            Books = _db.Books.Local.ToObservableCollection();
        }
        /// <summary>
        /// Command Add Book
        /// </summary> 
        public ICommand AddCommand
        {
            get
            {
                return _addCommand ??= new Command((_) =>
                {
                    BookWindow bookWindow = new(new Book());
                    if (bookWindow.ShowDialog() != true) return;
                    try
                    {
                        var book = bookWindow.Book;
                        _db.Books.Add(book);
                        _db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Error: {e.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
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
                return _editCommand ??= new Command((selectedItem) =>
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

                    if (bookWindow.ShowDialog() != true) return;
                    book.Name = bookWindow.Book.Name;
                    book.Author = bookWindow.Book.Author;
                    book.DateEdition = bookWindow.Book.DateEdition;
                    book.Genre = bookWindow.Book.Genre;
                    try
                    {
                        _db.Entry(book).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Error: {e.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    }
                }, (_) => Books.Count > 0);
            }
        }
        /// <summary>
        /// Command for delete book 
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??= new Command(obj =>
                {
                    if (obj is not Book book) return;
                    var res = (MessageBox.Show("Are you sure?", "Delete",
                        MessageBoxButton.YesNo, MessageBoxImage.Question,
                        MessageBoxResult.No)).ToString();
                    if (res != "Yes") return;
                    try
                    {
                        _db.Books.Remove(book);
                        _db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Error: {e.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    }
                },
                (_) => Books.Count > 0);
            }
        }

        /// <summary>
        /// Selection For Delete or Edit
        /// </summary>
        public Book? SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Declare the event
        /// </summary>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}

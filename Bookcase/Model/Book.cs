using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Bookcase.Model
{
    public class Book : INotifyPropertyChanged, IDataErrorInfo
    {


        public int Id { get; set; }
        private string? _name;
        private int _dateEdition;
        private string? _author;
        private string? _genre;
        private readonly TextInfo _ti = CultureInfo.CurrentCulture.TextInfo;

        public string? Name
        {
            get => _name;
            set
            {
                if (_name != value && value != null)
                    _name = _ti.ToTitleCase(str: value.ToLower());
                OnPropertyChanged();

            }
        }
        public int DateEdition
        {
            get => _dateEdition;
            set
            {

                _dateEdition = value;
                OnPropertyChanged();

            }
        }
        public string? Author
        {
            get => _author;
            set
            {
                if (_author != value && value != null)
                    _author = _ti.ToTitleCase(str: value.ToLower());
                OnPropertyChanged();

            }
        }
        public string? Genre
        {
            get => _genre;
            set
            {
                if (_genre != value && value != null)
                    _genre = _ti.ToTitleCase(str: value.ToLower());
                OnPropertyChanged();

            }
        }

        public string Error => throw new NotImplementedException();

        /// <summary>
        /// Validation Add and Edit Book
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;
                switch (columnName)
                {
                    case "DateEdition":
                        if ((DateEdition < 1564) || (DateEdition > DateTime.Now.Year))
                        {
                            error = "Wrong date of edition";
                        }
                        break;
                    case "Name":

                        if (string.IsNullOrWhiteSpace(Name))
                        {
                            error = "Wrong Name of book";
                        }
                        break;
                    case "Author":
                        if (IsNumeric(Author))
                        {
                            error = "Wrong author";
                        }
                        break;
                    case "Genre":
                        if (IsNumeric(Genre))
                        {
                            error = "Wrong genre";
                        }
                        break;
                }
                return error;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public static bool IsNumeric(string? s)
        {
            return string.IsNullOrWhiteSpace(s) || s.Any(c => c is >= '0' and <= '9');
        }
    }
}

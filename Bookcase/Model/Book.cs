using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;


namespace Bookcase.Model
{
    public class Book : INotifyPropertyChanged, IDataErrorInfo
    {


        public int Id { get; set; }
        private string? name;
        private int dateEdition;
        private string? author;
        private string? genre;
        readonly TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

        public string? Name
        {
            get { return name; }
            set
            {

                name = ti.ToTitleCase(str: value.ToLower()).ToString();
                
                OnPropertyChanged(nameof(Name));

            }
        }
        public int DateEdition
        {
            get { return dateEdition; }
            set
            {

                dateEdition = value;
                OnPropertyChanged(nameof(DateEdition));

            }
        }
        public string? Author
        {
            get { return author; }
            set
            {

                author = ti.ToTitleCase(str: value.ToLower()).ToString();
                OnPropertyChanged(nameof(Author));

            }
        }
        public string? Genre
        {
            get { return genre; }
            set
            {

                genre = ti.ToTitleCase(str: value.ToLower()).ToString();
                OnPropertyChanged(nameof(Genre));

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
                string error = String.Empty;
                switch (columnName)
                {
                    case "DateEdition":
                        if ((DateEdition < 1564) || (DateEdition > (int)DateTime.Now.Year))
                        {
                            error = "Wrong date of edition";
                        }
                        break;
                    case "Name":

                        if (String.IsNullOrWhiteSpace(Name))
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

            if (!String.IsNullOrWhiteSpace(s))
            {
                foreach (char c in s)
                {
                    if (c >= '0' && c <= '9')
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }

            return false;
        }
    }
}

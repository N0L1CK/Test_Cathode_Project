using System;
using System.ComponentModel;
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

       

        public string? Name
        {
            get { return name; }
            set
            {

                name = value;
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

                author = value;
                OnPropertyChanged(nameof(Author));

            }
        }
        public string? Genre
        {
            get { return genre; }
            set
            {

                genre = value;
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
                        if (isNumeric(Author))
                        {
                            error = "Wrong author";
                        }
                        break;
                    case "Genre":
                        if (isNumeric(Genre))
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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public bool isNumeric(string? s)
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

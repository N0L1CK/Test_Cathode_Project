

namespace Bookcase.Interfaces
{
    internal interface IDataErrorInfo
    {
        string Error { get; }
        string this[string columnName] { get; }
    }
}

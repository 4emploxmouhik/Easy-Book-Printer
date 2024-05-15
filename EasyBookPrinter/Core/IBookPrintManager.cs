namespace EasyBookPrinter.Core
{
    public interface IBookPrintManager
    {
        bool IsBookOpened { get; }
        bool IsPagesSorted { get; }

        event EventHandler<string> WorkStatusChanged;

        void OpenBook(string path);
        void SortPages(int paperCount);
        void PrintBook(bool isSavePrintedVersion);
    }
}

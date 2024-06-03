using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Text;

namespace EasyBookPrinter.Core
{
    public class BookPrintManager : IBookPrintManager
    {
        private const string PrintedFilePrefix = "-PRINTED-VERSION";

        private readonly List<BlockOfPages> _bookBlocks;
        private readonly StringBuilder _infoStatusStringBuilder;

        private PdfDocument _bookFile;
        private bool _isBookOpened;
        private bool _isPagesSorted;

        public BookPrintManager()
        {
            _bookBlocks = new List<BlockOfPages>();
            _isBookOpened = false;
            _isPagesSorted = false;
            _infoStatusStringBuilder = new StringBuilder();
        }

        public event EventHandler<string> WorkStatusChanged;

        public bool IsBookOpened => _isBookOpened;
        public bool IsPagesSorted => _isPagesSorted;

        public void OpenBook(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentException($"\"{nameof(source)}\" cannot be null or empty.", nameof(source));
            }
            if (!File.Exists(source))
            {
                throw new FileNotFoundException($"The file \"{source}\" was not found or does not exist.", source);
            }

            _bookFile = PdfReader.Open(source, PdfDocumentOpenMode.Import);

            if (_bookFile.Pages.Count == 0)
            {
                throw new Exception("Opened book contains zero pages.");
            }

            _isBookOpened = true;
            _isPagesSorted = false;

            _infoStatusStringBuilder.Clear();
            _infoStatusStringBuilder.AppendLine($"Book \"{_bookFile.FullPath}\" is opened.");
            _infoStatusStringBuilder.AppendLine();

            WorkStatusChanged?.Invoke(this, _infoStatusStringBuilder.ToString());
        }

        public void PrintBook(bool savePrintedVersionOfBook)
        {
            string filePath = _bookFile.FullPath.Replace(".pdf", $"{PrintedFilePrefix}.pdf");

            PdfDocument printedVersion = CreatePrintingVersionOfBook();

            if (savePrintedVersionOfBook)
            {
                printedVersion.Save(filePath);
            }

            printedVersion.Dispose();

            BlockOfPages printOrder = GetPrintOrder();

            _infoStatusStringBuilder.Clear();
            _infoStatusStringBuilder.AppendLine("Order to print pages:");
            _infoStatusStringBuilder.AppendLine($"> Side 1: [{string.Join(", ", printOrder.Pull(SheetSide.Top))}]");
            _infoStatusStringBuilder.AppendLine($"> Side 2: [{string.Join(", ", printOrder.Pull(SheetSide.Bottom))}]");
            _infoStatusStringBuilder.AppendLine();
            _infoStatusStringBuilder.AppendLine($"Open and print \"{filePath}\" in pdf-viewer.");
            _infoStatusStringBuilder.AppendLine("When printing, specify two page ranges:");
            _infoStatusStringBuilder.AppendLine($"> Side 1: [1-{printOrder.Capacity / 4}]");
            _infoStatusStringBuilder.AppendLine($"> Side 2: [{printOrder.Capacity / 4 + 1}-{printOrder.Capacity / 2}]");
            _infoStatusStringBuilder.AppendLine($"For printing you will need {printOrder.Capacity / 4} sheets of paper.");
            _infoStatusStringBuilder.AppendLine("P.S.: In feathers this process occurs automatically. Thank you.");
            _infoStatusStringBuilder.AppendLine();

            WorkStatusChanged?.Invoke(this, _infoStatusStringBuilder.ToString());
        }

        public void SortPages(int paperCount)
        {
            if (!_isBookOpened || _bookFile == null)
            {
                throw new NullReferenceException($"The book file was not opened.");
            }

            _bookBlocks.Clear();

            _infoStatusStringBuilder.Clear();
            _infoStatusStringBuilder.AppendLine($"Blocks of pages ({paperCount} sheets paper per block) for current book:");

            int pagesCountFromFile = _bookFile.PageCount;
            List<Period> blockPeriods = GetPagesPeriods(pagesCountFromFile, paperCount);

            BlockOfPages block;
            int blockNum = 1;

            foreach (var blockPeriod in blockPeriods)
            {
                if (blockPeriod.IsFull)
                {
                    block = GetSortedBlockOfPages(blockPeriod.From, blockPeriod.To, paperCount);
                }
                else
                {
                    List<int> emptyPageNumbers = new List<int>();

                    int numberOfPagesLeft = blockPeriod.To - (blockPeriod.From - 1);

                    while (numberOfPagesLeft % 4 != 0)
                    {
                        emptyPageNumbers.Add(blockPeriod.From + numberOfPagesLeft);
                        numberOfPagesLeft++;
                    }

                    if (numberOfPagesLeft == 4)
                    {
                        int from = blockPeriods[blockPeriods.Count - 2].From;
                        int to = emptyPageNumbers.Any() ? emptyPageNumbers.Last() : blockPeriod.To;

                        block = GetSortedBlockOfPages(from, to, paperCount + 1);

                        _bookBlocks.Remove(_bookBlocks.Last());
                    }
                    else
                    {
                        int to = emptyPageNumbers.Any() ? emptyPageNumbers.Last() : blockPeriod.To;

                        block = GetSortedBlockOfPages(blockPeriod.From, to, numberOfPagesLeft / 4);
                    }

                    if (emptyPageNumbers.Any())
                    {
                        block.ReplaceToEmptyPages(emptyPageNumbers.ToArray());
                    }
                }

                _bookBlocks.Add(block);

                _infoStatusStringBuilder.AppendLine($"{blockNum++}: " +
                    "[" + string.Join(", ", block.Pull(SheetSide.Top)) + "], " +
                    "[" + string.Join(", ", block.Pull(SheetSide.Bottom)) + "]");
            }

            _isPagesSorted = true;
            _infoStatusStringBuilder.AppendLine();

            WorkStatusChanged?.Invoke(this, _infoStatusStringBuilder.ToString());
        }

        private static BlockOfPages GetSortedBlockOfPages(int from, int to, int paperCount)
        {
            BlockOfPages block = new BlockOfPages(paperCount);

            int pageL = to;
            int pageR = from;
            int middle = to - block.Capacity / 2;
            int edge = middle;
            SheetSide side = SheetSide.Top;

            do
            {
                block.Push(pageL, side);
                block.Push(pageR, side);

                pageL -= 2;
                pageR += 2;

                if (pageR == middle - 1)
                {
                    block.Push(pageL, side);
                    block.Push(pageR, side);

                    edge = to;
                    pageL -= 2;
                    pageR += 2;
                    side = SheetSide.Bottom;
                }
            } while (pageR < edge);

            return block;
        }

        private static List<Period> GetPagesPeriods(int pagesCount, int sheetsCount)
        {
            int regularBlockCapacity = sheetsCount * 4;
            double regularBlocksCount = (double)pagesCount / (double)regularBlockCapacity;

            List<Period> blocksPeriods = new List<Period>();

            for (int from = 1, to = regularBlockCapacity, i = 0; i < (int)regularBlocksCount; i++, from += regularBlockCapacity, to += regularBlockCapacity)
            {
                blocksPeriods.Add(new Period()
                {
                    From = from,
                    To = to,
                    IsFull = true
                });
            }

            if (regularBlocksCount % 1 != 0)
            {
                blocksPeriods.Add(new Period()
                {
                    From = regularBlockCapacity * (int)regularBlocksCount + 1,
                    To = pagesCount,
                    IsFull = false
                });
            }

            return blocksPeriods;
        }

        private BlockOfPages GetPrintOrder()
        {
            return _bookBlocks.First().Join(_bookBlocks.GetRange(1, _bookBlocks.Count - 1));
        }

        private PdfDocument CreatePrintingVersionOfBook()
        {
            PdfDocument tempFile = new PdfDocument();

            foreach (var pageNum in GetPrintOrder().Pull())
            {
                PdfPage page = (pageNum != BlockOfPages.ValueOfEmptyPage) ? _bookFile.Pages[pageNum - 1] : new PdfPage();
                page.Orientation = PageOrientation.Landscape;

                tempFile.AddPage(page);
            }

            string tempFilePath = Directory.GetCurrentDirectory() + "/temp.pdf";

            tempFile.Save(tempFilePath);

            PdfDocument printedVersionOfBook = new PdfDocument()
            {
                PageLayout = PdfPageLayout.SinglePage
            };
            XPdfForm form = XPdfForm.FromFile(tempFilePath);
            XGraphics gfx;
            XRect box;

            for (int idx = 0; idx < form.PageCount; idx += 2)
            {
                PdfPage page = printedVersionOfBook.AddPage();
                page.Orientation = PageOrientation.Landscape;

                double width = page.Width;
                double height = page.Height;

                gfx = XGraphics.FromPdfPage(page);
                form.PageNumber = idx + 1;
                box = new XRect(0, 0, width / 2, height);

                gfx.DrawImage(form, box);

                if (idx + 1 < form.PageCount)
                {
                    form.PageNumber = idx + 2;
                    box = new XRect(width / 2, 0, width / 2, height);

                    gfx.DrawImage(form, box);
                }
            }

            tempFile.Close();
            File.Delete(tempFilePath);

            return printedVersionOfBook;
        }

        private struct Period
        {
            public int From { get; set; }
            public int To { get; set; }
            public bool IsFull { get; set; }
        }
    }
}

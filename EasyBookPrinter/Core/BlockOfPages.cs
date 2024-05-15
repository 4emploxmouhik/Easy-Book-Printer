namespace EasyBookPrinter.Core
{
    public class BlockOfPages : IEquatable<BlockOfPages>
    {
        public const int ValueOfEmptyPage = -1;
        private int[] _pages;

        public BlockOfPages() 
        {
            _pages = Array.Empty<int>();
        }

        public BlockOfPages(int paperCount) 
        {
            _pages = new int[paperCount * 4];

            Array.Fill(_pages, ValueOfEmptyPage);
        }

        public int Capacity => _pages.Length;
        public int PagesOnSideCount => _pages.Length / 2;
                
        public override bool Equals(object? obj)
        {
            return Equals(obj as BlockOfPages);
        }

        public bool Equals(BlockOfPages? other)
        {
            if (other == null) return false;

            for (int i = 0; i < _pages.Length; i++)
            {
                if (_pages[i] != other._pages[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _pages.GetHashCode();
        }

        public BlockOfPages Join(BlockOfPages other)
        {
            List<int> newPages =
            [
                .. Pull(SheetSide.Top),
                .. other.Pull(SheetSide.Top),
                .. Pull(SheetSide.Bottom),
                .. other.Pull(SheetSide.Bottom),
            ];

            return new BlockOfPages() { _pages = newPages.ToArray() };
        }

        public bool Push(int page, SheetSide side)
        {          
            int index = (side == SheetSide.Top) ? 0 : PagesOnSideCount;

            while (_pages[index] != ValueOfEmptyPage)
            {
                index++;

                if (index == _pages.Length)
                {
                    return false;
                }
            }

            _pages[index] = page;
            return true;
        }

        public bool Push(int[] topSide, int[] bottomSide)
        {
            if (topSide.Length > PagesOnSideCount || bottomSide.Length > PagesOnSideCount)
            {
                return false;
            }

            for (int i = 0, j = PagesOnSideCount; i < PagesOnSideCount; i++, j++)
            {
                _pages[i] = (i < topSide.Length) ? topSide[i] : ValueOfEmptyPage;
                _pages[j] = (i < bottomSide.Length) ? bottomSide[i] : ValueOfEmptyPage;
            }

            return true;
        }

        public int[] Pull()
        {
            int[] copy = new int[_pages.Length];

            Array.Copy(_pages, copy, _pages.Length);

            return copy;
        }

        public int[] Pull(SheetSide side)
        {
            int[] pagesOnSide = new int[PagesOnSideCount];

            Array.Copy(_pages, (side == SheetSide.Top) ? 0 : PagesOnSideCount, pagesOnSide, 0, PagesOnSideCount);

            return pagesOnSide;
        }

        public void ReplaceToEmptyPages(int[] pagesToReplace)
        {
            for (int i = 0; i < _pages.Length; i++)
            {
                for (int j = 0; j < pagesToReplace.Length; j++)
                {
                    if (_pages[i] == pagesToReplace[j])
                    {
                        _pages[i] = ValueOfEmptyPage;
                        break;
                    }
                }
            }
        }

        public override string ToString()
        {
            string output = "Top side: [";

            for (int i = 0; i < _pages.Length; i++)
            {
                output += (i == PagesOnSideCount - 1) ? $"{_pages[i]}], Bottom side: [" : $"{_pages[i]}, ";
            }

            output = output.Remove(output.Length - 2) + "]";

            return output;
        }
    }
}
 
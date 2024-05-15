using EasyBookPrinter.Core;

namespace EasyBookPrinterTests.Core
{
    [TestClass]
    public class BlockOfPagesTests
    {

        [TestMethod]
        public void PullBySide()
        {
            int sheetCount = 2;
            int[] expectedPages = [2, 4, 6, 8];

            BlockOfPages blockOfPages = new BlockOfPages(sheetCount);
            blockOfPages.Push([1, 3, 5, 7], [2, 4, 6, 8]);

            Assert.IsTrue(Compare(expectedPages, blockOfPages.Pull(SheetSide.Bottom)));
        }

        [TestMethod]
        public void PushByPageAndSide()
        {
            int sheetCount = 2;
            int[] expectedPages = [1, 3, 5, 7, 2, 4, 6, 8];

            BlockOfPages blockOfPages = new BlockOfPages(sheetCount);

            for (int i = 1; i <= 8; i++)
            {
                blockOfPages.Push(i, (i % 2 == 0) ? SheetSide.Bottom : SheetSide.Top);
            }

            Assert.IsTrue(Compare(expectedPages, blockOfPages.Pull()));
        }

        [TestMethod]
        public void PushWithTwoArraysOfPages()
        {
            int sheetCount = 2;
            int[] expectedPages = [1, 3, 5, 7, 2, 4, 6, 8];

            BlockOfPages blockOfPages = new BlockOfPages(sheetCount);
            blockOfPages.Push([1, 3, 5, 7], [2, 4, 6, 8]);

            Assert.IsTrue(Compare(expectedPages, blockOfPages.Pull()));
        }

        [TestMethod]
        public void PushTwoSmallArrays()
        {
            int sheetCount = 2;
            int[] expectedPages = [1, 3, 5, BlockOfPages.ValueOfEmptyPage, 2, 4, BlockOfPages.ValueOfEmptyPage, BlockOfPages.ValueOfEmptyPage];

            BlockOfPages blockOfPages = new BlockOfPages(sheetCount);
            blockOfPages.Push([1, 3, 5], [2, 4]);

            Assert.IsTrue(Compare(expectedPages, blockOfPages.Pull()));
        }

        [TestMethod]
        public void PushNewPageWithoutFreeSeatsInBlock()
        {
            int sheetCount = 2;

            BlockOfPages blockOfPages = new BlockOfPages(sheetCount);
            blockOfPages.Push([1, 3, 5, 7], [2, 4, 6, 8]);

            Assert.IsFalse(blockOfPages.Push(9, SheetSide.Top));
        }

        [TestMethod]
        public void ReplaceToEmptyPages()
        {
            int sheetCount = 2;
            int[] expectedPages = [1, BlockOfPages.ValueOfEmptyPage, 5, BlockOfPages.ValueOfEmptyPage, 2, 4, BlockOfPages.ValueOfEmptyPage, 8];

            BlockOfPages blockOfPages = new BlockOfPages(sheetCount);
            blockOfPages.Push([1, 3, 5, 7], [2, 4, 6, 8]);

            blockOfPages.ReplaceToEmptyPages([3, 7, 6]);

            Assert.IsTrue(Compare(expectedPages, blockOfPages.Pull()));
        }

        private static bool Compare(int[] first, int[] second)
        {
            if (first.Length != second.Length) return false;

            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}

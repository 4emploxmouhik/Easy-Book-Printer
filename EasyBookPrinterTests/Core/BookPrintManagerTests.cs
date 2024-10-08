﻿using System.Reflection;

namespace EasyBookPrinter.Core.Tests
{
    [TestClass()]
    public class BookPrintManagerTests
    {
        private readonly string _testPdfFilePath = $@"{Directory.GetCurrentDirectory()}\Resources\TestBookOf36Pages.pdf";
        private readonly string _testReourceDirectory = $@"{Directory.GetCurrentDirectory()}\Resources";

        [TestMethod()]
        public void OpenBookTest()
        {
            BookPrintManager manager = new BookPrintManager();
            manager.OpenBook(_testPdfFilePath);

            Assert.IsTrue(manager.IsBookOpened);
        }

        [TestMethod()]
        public void SortPagesTestWith36PagesAnd3PaperSheets()
        {
            int paperCountByBlock = 3;

            BlockOfPages b1 = new BlockOfPages(paperCountByBlock);
            BlockOfPages b2 = new BlockOfPages(paperCountByBlock);
            BlockOfPages b3 = new BlockOfPages(paperCountByBlock);

            b1.Push([12, 1, 10, 3, 8, 5], [2, 11, 4, 9, 6, 7]);
            b2.Push([24, 13, 22, 15, 20, 17], [14, 23, 16, 21, 18, 19]);
            b3.Push([36, 25, 34, 27, 32, 29], [26, 35, 28, 33, 30, 31]);

            List<BlockOfPages> expectedBlocks = new List<BlockOfPages>();
            expectedBlocks.AddRange([b1, b2, b3]);

            // Creating manager for test sorting method
            BookPrintManager bookPrintManager = new BookPrintManager();
            bookPrintManager.OpenBook(_testPdfFilePath);
            bookPrintManager.SortPages(paperCountByBlock);

            // Getting private field of blocks from tested manager
            Type bmpType = typeof(BookPrintManager);
            FieldInfo? fieldInfo = bmpType.GetField("_bookBlocks", BindingFlags.Instance | BindingFlags.NonPublic);
            List<BlockOfPages>? actualBlocks = (List<BlockOfPages>?)fieldInfo?.GetValue(bookPrintManager);

            // Comparing two collection of pages for identity and save the result
            bool isSame = false;

            for (int i = 0; i < paperCountByBlock; i++)
            {
                isSame = expectedBlocks[i].Equals(actualBlocks[i]);
            }

            Assert.IsTrue(isSame);
        }

        [TestMethod]
        public void SortPagesTestWith36PagesAnd4PaperSheets()
        {
            int paperCountByBlock = 4;

            BlockOfPages b1 = new BlockOfPages(paperCountByBlock);
            BlockOfPages b2 = new BlockOfPages(paperCountByBlock + 1);

            b1.Push([16, 1, 14, 3, 12, 5, 10, 7], [2, 15, 4, 13, 6, 11, 8, 9]);
            b2.Push([36, 17, 34, 19, 32, 21, 30, 23, 28, 25], [18, 35, 20, 33, 22, 31, 24, 29, 26, 27]);

            List<BlockOfPages> expectedBlocks = new List<BlockOfPages>();
            expectedBlocks.AddRange([b1, b2]);

            BookPrintManager bookPrintManager = new BookPrintManager();
            bookPrintManager.OpenBook(_testPdfFilePath);
            bookPrintManager.SortPages(paperCountByBlock);

            // Getting private field of blocks from tested manager
            Type bmpType = typeof(BookPrintManager);
            FieldInfo? fieldInfo = bmpType.GetField("_bookBlocks", BindingFlags.Instance | BindingFlags.NonPublic);
            List<BlockOfPages>? actualBlocks = (List<BlockOfPages>?)fieldInfo?.GetValue(bookPrintManager);

            // Comparing two collection of pages for identity and save the result
            bool isSame = false;

            for (int i = 0; i < 2; i++)
            {
                isSame = expectedBlocks[i].Equals(actualBlocks[i]);
            }

            Assert.IsTrue(isSame);
        }

        [TestMethod]
        public void SortPagesWith37Pages()
        {
            string testPath = $@"{_testReourceDirectory}\TestBookOf37Pages.pdf";

            int paperCountByBlock = 3;

            BlockOfPages b1 = new BlockOfPages(paperCountByBlock);
            BlockOfPages b2 = new BlockOfPages(paperCountByBlock);
            BlockOfPages b3 = new BlockOfPages(paperCountByBlock + 1);

            b1.Push([12, 1, 10, 3, 8, 5], [2, 11, 4, 9, 6, 7]);
            b2.Push([24, 13, 22, 15, 20, 17], [14, 23, 16, 21, 18, 19]);
            b3.Push([-1, 25, -1, 27, 36, 29, 34, 31], [26, -1, 28, 37, 30, 35, 32, 33]);

            List<BlockOfPages> expectedBlocks = new List<BlockOfPages>();
            expectedBlocks.AddRange([b1, b2, b3]);

            BookPrintManager bookPrintManager = new BookPrintManager();
            bookPrintManager.OpenBook(testPath);
            bookPrintManager.SortPages(paperCountByBlock);

            Type bmpType = typeof(BookPrintManager);
            FieldInfo? fieldInfo = bmpType.GetField("_bookBlocks", BindingFlags.Instance | BindingFlags.NonPublic);
            List<BlockOfPages>? actualBlocks = (List<BlockOfPages>?)fieldInfo?.GetValue(bookPrintManager);

            // Comparing two collection of pages for identity and save the result
            bool isSame = false;

            for (int i = 0; i < paperCountByBlock; i++)
            {
                isSame = expectedBlocks[i].Equals(actualBlocks[i]);
            }

            Assert.IsTrue(isSame);
        }

        [TestMethod]
        public void SortPagesWith44Pages()
        {
            int paperCountByBlock = 4;
            int bookPagesCount = 44;

            string testFilePath = _testReourceDirectory + $@"\TestBookOf{bookPagesCount}Pages.pdf";

            BlockOfPages b1 = new BlockOfPages(paperCountByBlock);
            BlockOfPages b2 = new BlockOfPages(paperCountByBlock);
            BlockOfPages b3 = new BlockOfPages(paperCountByBlock - 1);

            b1.Push([16, 1, 14, 3, 12, 5, 10, 7], [2, 15, 4, 13, 6, 11, 8, 9]);
            b2.Push([32, 17, 30, 19, 28, 21, 26, 23], [18, 31, 20, 29, 22, 27, 24, 25]);
            b3.Push([44, 33, 42, 35, 40, 37], [34, 43, 36, 41, 38, 39]);

            List<BlockOfPages> expectedBlocks = new List<BlockOfPages>();
            expectedBlocks.AddRange([b1, b2, b3]);

            BookPrintManager bookPrintManager = new BookPrintManager();
            bookPrintManager.OpenBook(testFilePath);
            bookPrintManager.SortPages(paperCountByBlock);

            Type bmpType = typeof(BookPrintManager);
            FieldInfo? fieldInfo = bmpType.GetField("_bookBlocks", BindingFlags.Instance | BindingFlags.NonPublic);
            List<BlockOfPages>? actualBlocks = (List<BlockOfPages>?)fieldInfo?.GetValue(bookPrintManager);

            // Comparing two collection of pages for identity and save the result
            bool isSame = false;

            for (int i = 0; i < 3; i++)
            {
                isSame = expectedBlocks[i].Equals(actualBlocks[i]);
            }

            Assert.IsTrue(isSame);
        }
    }
}
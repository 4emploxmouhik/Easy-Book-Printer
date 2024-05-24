# ![EasyBookPrinter-Icon](/EasyBookPrinter/Resources/book.png) Easy book printer

EasyBookPrinter - is a small desktop solution that allows you to easily print a file in book format.
This app will open the selected PDF-file, sort the pages into notebooks (blocks of pages), prepare a temporary print file that can be saved separately, and print it.

> *At the moment the application cannot print the file from the "box".*

## Features 

- Add implementation of sending a file for printing

## Installation


## Usage

Before launching the application, download the book from any resource in PDF format. 
Let's launch the application by double-clicking on . We are greeted by the main window. 

![EasyBookPrinter-MainForm-Scrennshoot](/EasyBookPrinter/Resources/EasyBookPrinter-MainForm-Scrennshoot.png)

First, indicate the location of the downloaded book file `1` or select its from explorer `2`. Then we set the number of sheets of paper in the notebook (block of pages) `3` and sort the pages by pressing button `4`.

> *Note! If the number of pages in the source file does not correspond to the specified partitioning parameters, then the number of pages in the last notebook (block of pages) will be different.*

In the information field `5` you can view the composition of the pages in each notebook (block of pages), the order of sorting and printing, as well as other information. By clicking on the button `6`, we will save a printed version of the book file, which we will open in any PDF browser and print in two passes according to the instructions, which can be viewed in the information field `5`.

## Tech 

- C# 
- WinForms
- PdfSharp

## License

[MIT](https://choosealicense.com/licenses/mit/)

using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenQA.Selenium.BiDi.Modules.Session.ProxyConfiguration;
using a = DocumentFormat.OpenXml.Drawing; // Это исправляет ошибки с именем "a"
using wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using pic = DocumentFormat.OpenXml.Drawing.Pictures;
using WordProcessingDrawing = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Picture = DocumentFormat.OpenXml.Drawing.Pictures;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
namespace KT4
{
    public class Report
    {
        ////string screenshotPath;
        public string [] ImagePaths;

        public void Parametr(WebDriverWait wait, IWebDriver driver, string[] imagePaths)
        {
            ImagePaths = imagePaths;
            //screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), Filename);

        }





        public void REPORT()
        {
            //Console.WriteLine($"Скриншот сохранён: {screenshotPath}");
            //string excelPath = Path.Combine(Directory.GetCurrentDirectory(), "TestReport.xlsx");
            string wordPath = Path.Combine(Directory.GetCurrentDirectory(), "TestReport.docx");
            //ReportExsel(excelPath, "Main", "");
            ReportWord(wordPath, ImagePaths);
        }

        public void ReportExsel(string excelPath, string pageTitle, string screenshotPath)
        {
            try
            {
                // Создание отчета в Excel
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Тестовый отчет");

                    // Установка заголовков
                    worksheet.Cell(1, 1).Value = "Название теста";
                    worksheet.Cell(1, 2).Value = "Результат";
                    worksheet.Cell(1, 3).Value = "Описание";
                    worksheet.Cell(1, 4).Value = "Скриншот";

                    // Настройка ширины столбцов
                    worksheet.Column(1).Width = 20; // Ширина столбца "Название теста"
                    worksheet.Column(2).Width = 15; // Ширина столбца "Результат"
                    worksheet.Column(3).Width = 30; // Ширина столбца "Описание"
                    worksheet.Column(4).Width = 40; // Ширина столбца "Скриншот"

                    // Настройка высоты строк
                    worksheet.Row(1).Height = 20; // Высота заголовков
                    worksheet.Row(2).Height = 60; // Высота строки для данных

                    // Заполнение строки данных
                    worksheet.Cell(2, 1).Value = "Проверка заголовка страницы";
                    worksheet.Cell(2, 2).Value = pageTitle.Contains("Example Domain") ? "Успешно" : "Неудачно";
                    worksheet.Cell(2, 3).Value = pageTitle.Contains("Example Domain")
                        ? "Заголовок совпал с ожидаемым."
                        : "Заголовок не совпал с ожидаемым.";

                    // Создание области для скриншота
                    var screenshotRange = worksheet.Range(2, 4, 4, 4); // Диапазон для скриншота
                    screenshotRange.Merge(); // Объединение ячеек
                    screenshotRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    screenshotRange.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    screenshotRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Добавление изображения в область
                    var picture = worksheet.AddPicture(screenshotPath)
                                            .MoveTo(screenshotRange.FirstCell()) // Привязка изображения к области
                                            .Scale(0.055); // Масштабирование изображения до 20% от оригинального размера

                    // Сохранение файла
                    workbook.SaveAs(excelPath);
                }

                Console.WriteLine($"Excel: {excelPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ReportWord(string wordPath, string[] imagePaths)
        {
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(wordPath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = new Body();
                    mainPart.Document.Append(body);

                    // Заголовок отчета
                    Paragraph titleParagraph = new Paragraph(
                        new Run(new Text("Тестовый отчет"))
                        {
                            RunProperties = new RunProperties
                            {
                                Bold = new Bold(),
                                FontSize = new FontSize { Val = "36" }
                            }
                        }
                    );
                    titleParagraph.ParagraphProperties = new ParagraphProperties
                    {
                        Justification = new Justification { Val = JustificationValues.Center }
                    };
                    body.Append(titleParagraph);

                    // Подзаголовок
                    Paragraph subtitleParagraph = new Paragraph(
                        new Run(new Text("Результаты тестирования"))
                    );
                    subtitleParagraph.ParagraphProperties = new ParagraphProperties
                    {
                        SpacingBetweenLines = new SpacingBetweenLines { After = "200" }
                    };
                    body.Append(subtitleParagraph);

                    // Создание таблицы
                    Table table = new Table();
                    TableProperties tblProperties = new TableProperties(
                        new TableBorders(
                            new TopBorder { Val = BorderValues.Single, Size = 6 },
                            new BottomBorder { Val = BorderValues.Single, Size = 6 },
                            new LeftBorder { Val = BorderValues.Single, Size = 6 },
                            new RightBorder { Val = BorderValues.Single, Size = 6 },
                            new InsideHorizontalBorder { Val = BorderValues.Single, Size = 6 },
                            new InsideVerticalBorder { Val = BorderValues.Single, Size = 6 }
                        )
                    );
                    table.AppendChild(tblProperties);

                    // Добавление заголовка таблицы
                    TableRow headerRow = new TableRow();
                    headerRow.Append(
                        CreateTableCell("Название теста", true),
                        CreateTableCell("Результат", true),
                        CreateTableCell("Описание", true),
                        CreateTableCell("Скриншот", true)
                    );
                    table.Append(headerRow);

                    // Заполнение таблицы с данными
                    for (int i = 0; i < imagePaths.Length; i++)
                    {
                        string testName = $"Тест {i + 1}";
                        string result = "Успешно";  // Для примера, можно изменить
                        string description = $"Результат теста {i + 1} успешный.";

                        TableRow dataRow = new TableRow();
                        dataRow.Append(
                            CreateTableCell(testName),
                            CreateTableCell(result),
                            CreateTableCell(description),
                            CreateImageCell(mainPart, imagePaths[i])
                        );
                        table.Append(dataRow);
                    }

                    body.Append(table);
                    mainPart.Document.Save();
                }

                Console.WriteLine($"Word-отчет создан: {wordPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка создания отчета: {ex.Message}");
            }
        }

        //public void ReportWord(string wordPath, string pageTitlestring, string screenshotPath)
        //{
        //    try
        //    {
        //        string testName = "Проверка заголовка страницы";
        //        string result = "Успешно"; // или "Неудачно"
        //        string description = "Заголовок совпал с ожидаемым."; // или другой текст

        //        // Создание Word-документа
        //        // Создание Word-документа
        //        using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(wordPath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
        //        {
        //            // Добавляем основную часть документа
        //            MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
        //            mainPart.Document = new Document();
        //            Body body = new Body();
        //            mainPart.Document.Append(body);

        //            // Заголовок отчета
        //            Paragraph titleParagraph = new Paragraph
        //            {
        //                ParagraphProperties = new ParagraphProperties
        //                {
        //                    Justification = new Justification { Val = JustificationValues.Center }
        //                },
        //                RsidParagraphAddition = "00000000"
        //            };

        //            Run titleRun = new Run(new Text("Тестовый отчет"))
        //            {
        //                RunProperties = new RunProperties
        //                {
        //                    Bold = new Bold(),
        //                    FontSize = new FontSize { Val = "36" } // Размер шрифта
        //                }
        //            };


        //            titleParagraph.Append(titleRun);
        //            body.Append(titleParagraph);

        //            // Подзаголовок
        //            Paragraph subtitleParagraph = new Paragraph(new Run(new Text("Результаты теста:")))
        //            {
        //                ParagraphProperties = new ParagraphProperties
        //                {
        //                    SpacingBetweenLines = new SpacingBetweenLines { After = "200" }
        //                }
        //            };
        //            body.Append(subtitleParagraph);

        //            // Таблица
        //            Table table = new Table();

        //            // Свойства таблицы
        //            TableProperties tblProperties = new TableProperties(
        //                new TableBorders(
        //                    new TopBorder { Val = BorderValues.Single, Size = 4 },
        //                    new BottomBorder { Val = BorderValues.Single, Size = 4 },
        //                    new LeftBorder { Val = BorderValues.Single, Size = 4 },
        //                    new RightBorder { Val = BorderValues.Single, Size = 4 },
        //                    new InsideHorizontalBorder { Val = BorderValues.Single, Size = 4 },
        //                    new InsideVerticalBorder { Val = BorderValues.Single, Size = 4 }
        //                )
        //            );
        //            table.AppendChild(tblProperties);

        //            // Добавляем строки в таблицу
        //            TableRow headerRow = new TableRow();
        //            headerRow.Append(
        //                CreateTableCell("Название теста", isBold: true),
        //                CreateTableCell("Результат", isBold: true),
        //                CreateTableCell("Описание", isBold: true),
        //                CreateTableCell("Скриншот", isBold: true)
        //            );
        //            table.Append(headerRow);

        //            TableRow dataRow = new TableRow();
        //            dataRow.Append(
        //                CreateTableCell(testName),
        //                CreateTableCell(result),
        //                CreateTableCell(description),
        //                CreateImageCell(mainPart, screenshotPath) // Вставка изображения
        //            );
        //            table.Append(dataRow);

        //            body.Append(table);

        //            // Сохранение документа
        //            mainPart.Document.Save();
        //        }


        //        Console.WriteLine($"Word-отчет создан: {wordPath}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        // Метод для создания ячейки таблицы
        //static TableCell CreateTableCell(string text, bool isBold = false)
        //{
        //    Run run = new Run(new Text(text));
        //    if (isBold)
        //    {
        //        run.RunProperties = new RunProperties(new Bold());
        //    }

        //    return new TableCell(
        //        new Paragraph(run)
        //    );
        //}

        // Метод для создания ячейки с изображением
        // Метод для создания ячейки с изображением
        //static TableCell CreateImageCell(MainDocumentPart mainPart, string imagePath)
        //{
        //    // Добавляем изображение в MainDocumentPart
        //    ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Png);
        //    using (var stream = new System.IO.FileStream(imagePath, System.IO.FileMode.Open))
        //    {
        //        imagePart.FeedData(stream);
        //    }

        //    string imageId = mainPart.GetIdOfPart(imagePart);

        //    // Создаем Drawing элемент для вставки изображения
        //    Drawing drawing = new Drawing(
        //        new wp.Inline(
        //            new wp.Extent { Cx = 990000L, Cy = 792000L }, // Размер изображения
        //            new wp.DocProperties { Id = (UInt32Value)1U, Name = "Picture" },
        //    new a.Graphic(
        //    new a.GraphicData(
        //                    new pic.Picture(
        //    new pic.NonVisualPictureProperties(
        //                            new pic.NonVisualDrawingProperties { Id = (UInt32Value)0U, Name = "New Bitmap Image" },
        //                            new pic.NonVisualPictureDrawingProperties()),
        //    new pic.BlipFill(
        //                            new a.Blip { Embed = imageId },
        //                            new a.Stretch(new a.FillRectangle())),
        //                        new pic.ShapeProperties(
        //                            new a.Transform2D(
        //                                new a.Offset { X = 0L, Y = 0L },
        //                                new a.Extents { Cx = 990000L, Cy = 792000L }),
        //                            new a.PresetGeometry(new a.AdjustValueList())
        //                            { Preset = a.ShapeTypeValues.Rectangle }))
        //                )
        //                { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
        //        )
        //        {
        //            DistanceFromTop = 0U,
        //            DistanceFromBottom = 0U,
        //            DistanceFromLeft = 0U,
        //            DistanceFromRight = 0U
        //        }
        //    );

        //    // Возвращаем ячейку с изображением
        //    TableCell cell = new TableCell(new Paragraph(new Run(drawing)));
        //    return cell;
        //}
        // Создание ячейки таблицы
        static TableCell CreateTableCell(string text, bool isBold = false)
        {
            Run run = new Run(new Text(text));
            if (isBold)
            {
                run.RunProperties = new RunProperties(new Bold());
            }
            return new TableCell(new Paragraph(run));
        }
        // Создание ячейки с изображением
        static TableCell CreateImageCell(MainDocumentPart mainPart, string imagePath)
        {
            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Png);
            using (var stream = new System.IO.FileStream(imagePath, System.IO.FileMode.Open))
            {
                imagePart.FeedData(stream);
            }

            string imageId = mainPart.GetIdOfPart(imagePart);
            Drawing drawing = new Drawing(
                new wp.Inline(
                    new wp.Extent { Cx = 990000L, Cy = 792000L }, // Размер изображения
                    new wp.DocProperties { Id = (UInt32Value)1U, Name = "Picture" },
                    new a.Graphic(
                        new a.GraphicData(
                            new pic.Picture(
                                new pic.NonVisualPictureProperties(
                                    new pic.NonVisualDrawingProperties { Id = (UInt32Value)0U, Name = "New Bitmap Image" },
                                    new pic.NonVisualPictureDrawingProperties()
                                ),
                                new pic.BlipFill(
                                    new a.Blip { Embed = imageId },
                                    new a.Stretch(new a.FillRectangle())
                                ),
                                new pic.ShapeProperties(
                                    new a.Transform2D(
                                        new a.Offset { X = 0L, Y = 0L },
                                        new a.Extents { Cx = 990000L, Cy = 792000L }
                                    ),
                                    new a.PresetGeometry(new a.AdjustValueList())
                                    { Preset = a.ShapeTypeValues.Rectangle }
                                )
                            )
                        )
                        { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" }
                    )
                )
                {
                    DistanceFromTop = 0U,
                    DistanceFromBottom = 0U,
                    DistanceFromLeft = 0U,
                    DistanceFromRight = 0U
                }
            );

            return new TableCell(new Paragraph(new Run(drawing)));
        }
    }
}

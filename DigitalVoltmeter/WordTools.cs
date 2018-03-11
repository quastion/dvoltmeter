using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;


namespace DigitalVoltmeter
{
    class WordTools
    {
        Word._Application application;
        Word._Document document;
        Object missingObj = Missing.Value;
        Object trueObj = true;
        string cherta = " ̅";
        Object falseObj = false;
        public void someMethod()
        {
            application = new Word.Application();
            application.Visible = true;
            try
            {
                document = application.Documents.Add();
                object start = 0;
                object end = 0;
                Word.Range _currentRange = document.Range(ref start, ref end);
                _currentRange.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
                _currentRange.Text = "a=(b+c) ̅";
                document.OMaths.Add(_currentRange).OMaths.BuildUp();
                //Word.Paragraphs wordparagraphs = document.Paragraphs;
                //String Text = Convert.ToString(wordparagraphs.Count);
                //document.SaveAs2("D:\\test.docx");

            }
            finally
            {
                //Закрывание ворда
                //document.Close();
                //application.Quit();
            }
        }

        /// <summary>
        /// Создание документа docx с формулами ai
        /// </summary>
        /// <param name="b">Массив состовляющих ЕПК</param>
        /// <param name="n">Количество резисторов</param>
        /// <param name="path">Путь к результирующему файлу</param>
        /// Вызов метода
        /// WordTools wt = new WordTools();
        /// wt.createDocumentWithFormules(b, processor.GetN(b.Length + 1),"D:\\test.docx");
        public void createDocumentWithFormules(LongBits[] b, out LongBits[] a, string path)
        {
            application = new Word.Application();
            application.Visible = false;
            try
            {
                document = application.Documents.Add();
                object start = 0;
                object end = 0;
                Word.Range _currentRange = document.Range(ref start, ref end);
                _currentRange.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;

                int n = MathProcessor.GetN(b.Length + 1);
                a = new LongBits[n];

                for (int i = a.Length-1; i >=0; i--)
                {
                    _currentRange.Text += "a_" + i + " = (";
                    a[i] = new LongBits(b[0].Length);
                    a[i] = ~a[i];
                    int kmax = (int)Math.Pow(2, n - 1 - i) - 1;
                    for (int k = 0; k <= kmax; k++)
                    {
                        int tmin = (int)Math.Pow(2, i) * (2 * k + 1);
                        int tmax = (int)Math.Pow(2, i + 1) * (k + 1) - 1;
                        for (int t = tmin; t <= tmax; t++)
                        {
                            _currentRange.Text += " b_" + t + cherta + " ";
                            a[i] &= ~b[t - 1];
                        }
                    }
                    a[i] = ~a[i];
                    _currentRange.Text += ")" + cherta + "\n";
                    document.OMaths.Add(_currentRange).OMaths.BuildUp();
                    _currentRange = document.Range(0,0);

                }
                document.SaveAs2(path);
            }
            finally
            {
                //Закрывание ворда
                document.Close();
                application.Quit();
            }
        }

        /// <summary>
        /// Создает временный файл .rtf из файла .docx для импорта в richTextBox
        /// </summary>
        /// <param name="docxFile">Путь к файлу .docx</param>
        /// <returns>Путь к файлу .rtf</returns>
        public static string GetRTFFromDOCXFile(string docxFile)
        {
            Word.Application app = new Word.Application();
            string rtfFileName;
            object missing = Type.Missing;
            try
            {
                app.Documents.Open(docxFile,
                    missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing);
                string temp = System.IO.Path.GetTempPath();
                int safeNameStartindex = docxFile.LastIndexOf('\\') + 1;
                rtfFileName = docxFile.Substring(safeNameStartindex, docxFile.Length - ".docx".Length - safeNameStartindex);
                rtfFileName += ".rtf";
                rtfFileName = temp + rtfFileName;

                #region permissions
                object lookComments = false;
                object password = string.Empty;
                object AddToRecentFiles = true;
                object WritePassword = string.Empty;
                object ReadOnlyRecommended = false;
                object EmbedTrueTypeFonts = false;
                object SaveFormsData = false;
                object SaveAsAOCELetter = false;
                #endregion

                app.ActiveDocument.SaveAs(rtfFileName, Word.WdSaveFormat.wdFormatRTF,
                    lookComments, password, AddToRecentFiles, WritePassword,
                    ReadOnlyRecommended, EmbedTrueTypeFonts, missing, SaveFormsData,
                    SaveAsAOCELetter, missing, missing, missing, missing, missing);


            }
            finally
            {
                app.ActiveDocument.Close(false, missing, missing);
                app.Quit(false, missing, missing);
            }
            return rtfFileName;
        }
    }
}

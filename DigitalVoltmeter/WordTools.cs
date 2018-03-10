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
                _currentRange.Text = "a_1 = (b_1"+cherta+"b_2 "+"b_3"+cherta+")"+cherta;
                document.OMaths.Add(_currentRange).OMaths.BuildUp();
                document.SaveAs2("D:\\test.docx");

            }
            finally
            {
                //Закрывание ворда
                //document.Close();
                //application.Quit();
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
                    ReadOnlyRecommended,EmbedTrueTypeFonts, missing, SaveFormsData, 
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

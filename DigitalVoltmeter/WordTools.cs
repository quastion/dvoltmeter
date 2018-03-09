﻿using System;
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
    }
}
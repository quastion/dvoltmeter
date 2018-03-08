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
        Object falseObj = false;
        public void someMethod()
        {
            application = new Word.Application();
            application.Visible = true;
            application.Documents.Add();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;

namespace DigitalVoltmeter
{
    public partial class Form1 : Form
    {
        private MathProcessor processor;
        private long[] singleCodes;
        private long[] b;
        private int bitsCount;

        public Form1()
        {
            InitializeComponent();
            ExcelTools.SetProgressBar(this.progressBar1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] singleCodesString = singleCodes.Select(a =>  processor.PrettyPrintBits(a, bitsCount)).ToArray();
            string[] bString = b.Select(a =>  processor.PrettyPrintBits(a, bitsCount)).ToArray();
            string[] BinaryCodes = new string[bitsCount];
            for(int i =0; i < BinaryCodes.Length; i++)
                BinaryCodes[i] = processor.PrettyPrintBits(i, (int)Math.Ceiling(Math.Log(bitsCount, 2)));
            ExcelTools.GenerateExcel(singleCodesString, bString, BinaryCodes);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bitsCount = int.Parse(this.textBox2.Text);
            this.textBox1.Text = "";
            processor = new MathProcessor();
            singleCodes = processor.SingleCodes(bitsCount);
            long[] ec = processor.GetElementsFromSingleCodes(singleCodes);
            b = processor.GetAllBFromE(ec);
            long[] a = processor.GetA(b);
            string[] formules = processor.Formules(b);
            for (int i = 0; i < formules.Length; i++)
                textBox1.Text += "a" + i + "=" + formules[i] + Environment.NewLine;
            progressBar1.Value = 0;
        }
        
    }
}

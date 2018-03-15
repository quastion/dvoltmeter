namespace DigitalVoltmeter
{
    partial class DigitalVoltmeterForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonGetFormules = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSaveToExel = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.checkBoxOutToWord = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxN = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxK = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDK = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDi = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxDUsm = new System.Windows.Forms.TextBox();
            this.buttonGetModel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.listBoxInputX = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxOutputA = new System.Windows.Forms.ListBox();
            this.mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonExpand = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(127, 528);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(710, 21);
            this.progressBar.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(9, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(425, 24);
            this.label2.TabIndex = 12;
            this.label2.Text = "Программа для анализа работы вольтметра параллельного преобразования.\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonGetFormules
            // 
            this.buttonGetFormules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGetFormules.Location = new System.Drawing.Point(10, 358);
            this.buttonGetFormules.Name = "buttonGetFormules";
            this.buttonGetFormules.Size = new System.Drawing.Size(133, 23);
            this.buttonGetFormules.TabIndex = 10;
            this.buttonGetFormules.Text = "Получить уравнения";
            this.buttonGetFormules.UseVisualStyleBackColor = true;
            this.buttonGetFormules.Click += new System.EventHandler(this.buttonGetFormules_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "n:";
            // 
            // buttonSaveToExel
            // 
            this.buttonSaveToExel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveToExel.Location = new System.Drawing.Point(11, 527);
            this.buttonSaveToExel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSaveToExel.Name = "buttonSaveToExel";
            this.buttonSaveToExel.Size = new System.Drawing.Size(110, 23);
            this.buttonSaveToExel.TabIndex = 8;
            this.buttonSaveToExel.Text = "Экспорт в Excel";
            this.buttonSaveToExel.UseVisualStyleBackColor = true;
            this.buttonSaveToExel.Click += new System.EventHandler(this.buttonSaveToExel_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox.Location = new System.Drawing.Point(12, 385);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(825, 137);
            this.richTextBox.TabIndex = 7;
            this.richTextBox.Text = "";
            this.richTextBox.WordWrap = false;
            // 
            // checkBoxOutToWord
            // 
            this.checkBoxOutToWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxOutToWord.AutoSize = true;
            this.checkBoxOutToWord.Location = new System.Drawing.Point(149, 362);
            this.checkBoxOutToWord.Name = "checkBoxOutToWord";
            this.checkBoxOutToWord.Size = new System.Drawing.Size(126, 17);
            this.checkBoxOutToWord.TabIndex = 15;
            this.checkBoxOutToWord.Text = "Вывод в Word файл";
            this.checkBoxOutToWord.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 342);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(308, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Формулы связи между ЕПК и выходным двоичным кодом.\r\n";
            // 
            // textBoxN
            // 
            this.textBoxN.Location = new System.Drawing.Point(30, 50);
            this.textBoxN.Name = "textBoxN";
            this.textBoxN.Size = new System.Drawing.Size(59, 20);
            this.textBoxN.TabIndex = 18;
            this.textBoxN.Text = "8";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "K:";
            // 
            // textBoxK
            // 
            this.textBoxK.Location = new System.Drawing.Point(30, 76);
            this.textBoxK.Name = "textBoxK";
            this.textBoxK.Size = new System.Drawing.Size(59, 20);
            this.textBoxK.TabIndex = 20;
            this.textBoxK.Text = "1000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "ΔK:";
            // 
            // textBoxDK
            // 
            this.textBoxDK.Location = new System.Drawing.Point(30, 107);
            this.textBoxDK.Name = "textBoxDK";
            this.textBoxDK.Size = new System.Drawing.Size(59, 20);
            this.textBoxDK.TabIndex = 22;
            this.textBoxDK.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Δi:";
            // 
            // textBoxDi
            // 
            this.textBoxDi.Location = new System.Drawing.Point(30, 136);
            this.textBoxDi.Name = "textBoxDi";
            this.textBoxDi.Size = new System.Drawing.Size(59, 20);
            this.textBoxDi.TabIndex = 24;
            this.textBoxDi.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "δсм:";
            // 
            // textBoxDUsm
            // 
            this.textBoxDUsm.Location = new System.Drawing.Point(43, 166);
            this.textBoxDUsm.Name = "textBoxDUsm";
            this.textBoxDUsm.Size = new System.Drawing.Size(46, 20);
            this.textBoxDUsm.TabIndex = 26;
            this.textBoxDUsm.Text = "0";
            // 
            // buttonGetModel
            // 
            this.buttonGetModel.Location = new System.Drawing.Point(9, 192);
            this.buttonGetModel.Name = "buttonGetModel";
            this.buttonGetModel.Size = new System.Drawing.Size(80, 36);
            this.buttonGetModel.TabIndex = 27;
            this.buttonGetModel.Text = "Получить модель";
            this.buttonGetModel.UseVisualStyleBackColor = true;
            this.buttonGetModel.Click += new System.EventHandler(this.buttonGetModel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Входные векторы:";
            // 
            // listBoxInputX
            // 
            this.listBoxInputX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxInputX.FormattingEnabled = true;
            this.listBoxInputX.Location = new System.Drawing.Point(3, 17);
            this.listBoxInputX.Name = "listBoxInputX";
            this.listBoxInputX.Size = new System.Drawing.Size(165, 108);
            this.listBoxInputX.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Выходные векторы:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.11236F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.88764F));
            this.tableLayoutPanel1.Controls.Add(this.listBoxOutputA, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.listBoxInputX, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(666, 40);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.59603F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.40398F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(171, 299);
            this.tableLayoutPanel1.TabIndex = 32;
            // 
            // listBoxOutputA
            // 
            this.listBoxOutputA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxOutputA.FormattingEnabled = true;
            this.listBoxOutputA.Location = new System.Drawing.Point(3, 152);
            this.listBoxOutputA.Name = "listBoxOutputA";
            this.listBoxOutputA.Size = new System.Drawing.Size(165, 134);
            this.listBoxOutputA.TabIndex = 33;
            // 
            // mainChart
            // 
            this.mainChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.mainChart.ChartAreas.Add(chartArea1);
            this.mainChart.Location = new System.Drawing.Point(107, 50);
            this.mainChart.Name = "mainChart";
            this.mainChart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mainChart.Size = new System.Drawing.Size(539, 276);
            this.mainChart.TabIndex = 33;
            title1.Name = "Title1";
            title1.Text = "Входное напряжение";
            this.mainChart.Titles.Add(title1);
            // 
            // buttonExpand
            // 
            this.buttonExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExpand.Location = new System.Drawing.Point(512, 332);
            this.buttonExpand.Name = "buttonExpand";
            this.buttonExpand.Size = new System.Drawing.Size(133, 23);
            this.buttonExpand.TabIndex = 34;
            this.buttonExpand.Text = "Раскрыть график";
            this.buttonExpand.UseVisualStyleBackColor = true;
            this.buttonExpand.Click += new System.EventHandler(this.buttonExpand_Click);
            // 
            // DigitalVoltmeterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 561);
            this.Controls.Add(this.buttonExpand);
            this.Controls.Add(this.mainChart);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonGetModel);
            this.Controls.Add(this.textBoxDUsm);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxDi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxDK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxOutToWord);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonGetFormules);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSaveToExel);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "DigitalVoltmeterForm";
            this.Text = "DigitalVoltmeter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DigitalVoltmeterForm_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonGetFormules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSaveToExel;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.CheckBox checkBoxOutToWord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDK;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxDUsm;
        private System.Windows.Forms.Button buttonGetModel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBoxInputX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBoxOutputA;
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
        private System.Windows.Forms.Button buttonExpand;
    }
}


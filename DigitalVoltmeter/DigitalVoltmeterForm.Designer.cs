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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
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
            this.mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonExpand = new System.Windows.Forms.Button();
            this.dataGridViewVect = new System.Windows.Forms.DataGridView();
            this.labelCriticalDK = new System.Windows.Forms.Label();
            this.labelCriticalDi = new System.Windows.Forms.Label();
            this.labelCriticalDsm = new System.Windows.Forms.Label();
            this.buttonCriticalDK = new System.Windows.Forms.Button();
            this.buttonCriticalDi = new System.Windows.Forms.Button();
            this.buttonCritical = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVect)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(190, 812);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1065, 32);
            this.progressBar.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(14, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(636, 36);
            this.label2.TabIndex = 12;
            this.label2.Text = "Программа для анализа работы вольтметра параллельного преобразования.\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonGetFormules
            // 
            this.buttonGetFormules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGetFormules.Location = new System.Drawing.Point(15, 551);
            this.buttonGetFormules.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGetFormules.Name = "buttonGetFormules";
            this.buttonGetFormules.Size = new System.Drawing.Size(200, 35);
            this.buttonGetFormules.TabIndex = 10;
            this.buttonGetFormules.Text = "Получить уравнения";
            this.buttonGetFormules.UseVisualStyleBackColor = true;
            this.buttonGetFormules.Click += new System.EventHandler(this.buttonGetFormules_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "n:";
            // 
            // buttonSaveToExel
            // 
            this.buttonSaveToExel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveToExel.Location = new System.Drawing.Point(16, 811);
            this.buttonSaveToExel.Name = "buttonSaveToExel";
            this.buttonSaveToExel.Size = new System.Drawing.Size(165, 35);
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
            this.richTextBox.Location = new System.Drawing.Point(18, 592);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(1236, 209);
            this.richTextBox.TabIndex = 7;
            this.richTextBox.Text = "";
            this.richTextBox.WordWrap = false;
            // 
            // checkBoxOutToWord
            // 
            this.checkBoxOutToWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxOutToWord.AutoSize = true;
            this.checkBoxOutToWord.Location = new System.Drawing.Point(224, 559);
            this.checkBoxOutToWord.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxOutToWord.Name = "checkBoxOutToWord";
            this.checkBoxOutToWord.Size = new System.Drawing.Size(188, 24);
            this.checkBoxOutToWord.TabIndex = 15;
            this.checkBoxOutToWord.Text = "Вывод в Word файл";
            this.checkBoxOutToWord.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 526);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(451, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Формулы связи между ЕПК и выходным двоичным кодом.\r\n";
            // 
            // textBoxN
            // 
            this.textBoxN.Location = new System.Drawing.Point(45, 77);
            this.textBoxN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxN.Name = "textBoxN";
            this.textBoxN.Size = new System.Drawing.Size(64, 26);
            this.textBoxN.TabIndex = 18;
            this.textBoxN.Text = "8";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 122);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "K:";
            // 
            // textBoxK
            // 
            this.textBoxK.Location = new System.Drawing.Point(45, 117);
            this.textBoxK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxK.Name = "textBoxK";
            this.textBoxK.Size = new System.Drawing.Size(64, 26);
            this.textBoxK.TabIndex = 20;
            this.textBoxK.Text = "1000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 169);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "ΔK:";
            // 
            // textBoxDK
            // 
            this.textBoxDK.Location = new System.Drawing.Point(45, 165);
            this.textBoxDK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxDK.Name = "textBoxDK";
            this.textBoxDK.Size = new System.Drawing.Size(64, 26);
            this.textBoxDK.TabIndex = 22;
            this.textBoxDK.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 214);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 20);
            this.label6.TabIndex = 23;
            this.label6.Text = "Δi:";
            // 
            // textBoxDi
            // 
            this.textBoxDi.Location = new System.Drawing.Point(45, 209);
            this.textBoxDi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxDi.Name = "textBoxDi";
            this.textBoxDi.Size = new System.Drawing.Size(64, 26);
            this.textBoxDi.TabIndex = 24;
            this.textBoxDi.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 260);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 20);
            this.label7.TabIndex = 25;
            this.label7.Text = "δсм:";
            // 
            // textBoxDUsm
            // 
            this.textBoxDUsm.Location = new System.Drawing.Point(64, 255);
            this.textBoxDUsm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxDUsm.Name = "textBoxDUsm";
            this.textBoxDUsm.Size = new System.Drawing.Size(44, 26);
            this.textBoxDUsm.TabIndex = 26;
            this.textBoxDUsm.Text = "0";
            // 
            // buttonGetModel
            // 
            this.buttonGetModel.Location = new System.Drawing.Point(14, 295);
            this.buttonGetModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGetModel.Name = "buttonGetModel";
            this.buttonGetModel.Size = new System.Drawing.Size(201, 37);
            this.buttonGetModel.TabIndex = 27;
            this.buttonGetModel.Text = "Получить модель";
            this.buttonGetModel.UseVisualStyleBackColor = true;
            this.buttonGetModel.Click += new System.EventHandler(this.buttonGetModel_Click);
            // 
            // mainChart
            // 
            this.mainChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea6.Name = "ChartArea1";
            this.mainChart.ChartAreas.Add(chartArea6);
            this.mainChart.Location = new System.Drawing.Point(224, 77);
            this.mainChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mainChart.Name = "mainChart";
            this.mainChart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mainChart.Size = new System.Drawing.Size(663, 425);
            this.mainChart.TabIndex = 33;
            title6.Name = "Title1";
            title6.Text = "Входное напряжение";
            this.mainChart.Titles.Add(title6);
            // 
            // buttonExpand
            // 
            this.buttonExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExpand.Location = new System.Drawing.Point(784, 511);
            this.buttonExpand.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonExpand.Name = "buttonExpand";
            this.buttonExpand.Size = new System.Drawing.Size(102, 35);
            this.buttonExpand.TabIndex = 34;
            this.buttonExpand.Text = "Раскрыть график";
            this.buttonExpand.UseVisualStyleBackColor = true;
            this.buttonExpand.Click += new System.EventHandler(this.buttonExpand_Click);
            // 
            // dataGridViewVect
            // 
            this.dataGridViewVect.AllowUserToAddRows = false;
            this.dataGridViewVect.AllowUserToDeleteRows = false;
            this.dataGridViewVect.AllowUserToOrderColumns = true;
            this.dataGridViewVect.AllowUserToResizeColumns = false;
            this.dataGridViewVect.AllowUserToResizeRows = false;
            this.dataGridViewVect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewVect.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewVect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVect.Location = new System.Drawing.Point(896, 18);
            this.dataGridViewVect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewVect.MultiSelect = false;
            this.dataGridViewVect.Name = "dataGridViewVect";
            this.dataGridViewVect.ReadOnly = true;
            this.dataGridViewVect.RowHeadersVisible = false;
            this.dataGridViewVect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewVect.ShowCellToolTips = false;
            this.dataGridViewVect.Size = new System.Drawing.Size(360, 565);
            this.dataGridViewVect.TabIndex = 35;
            this.dataGridViewVect.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewVect_CellPainting);
            this.dataGridViewVect.SelectionChanged += new System.EventHandler(this.dataGridViewVect_SelectionChanged);
            // 
            // labelCriticalDK
            // 
            this.labelCriticalDK.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labelCriticalDK.Location = new System.Drawing.Point(120, 169);
            this.labelCriticalDK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCriticalDK.Name = "labelCriticalDK";
            this.labelCriticalDK.Size = new System.Drawing.Size(62, 20);
            this.labelCriticalDK.TabIndex = 36;
            // 
            // labelCriticalDi
            // 
            this.labelCriticalDi.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labelCriticalDi.Location = new System.Drawing.Point(120, 214);
            this.labelCriticalDi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCriticalDi.Name = "labelCriticalDi";
            this.labelCriticalDi.Size = new System.Drawing.Size(62, 20);
            this.labelCriticalDi.TabIndex = 37;
            // 
            // labelCriticalDsm
            // 
            this.labelCriticalDsm.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labelCriticalDsm.Location = new System.Drawing.Point(120, 260);
            this.labelCriticalDsm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCriticalDsm.Name = "labelCriticalDsm";
            this.labelCriticalDsm.Size = new System.Drawing.Size(62, 20);
            this.labelCriticalDsm.TabIndex = 38;
            // 
            // buttonCriticalDK
            // 
            this.buttonCriticalDK.BackColor = System.Drawing.Color.Red;
            this.buttonCriticalDK.Location = new System.Drawing.Point(190, 165);
            this.buttonCriticalDK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCriticalDK.Name = "buttonCriticalDK";
            this.buttonCriticalDK.Size = new System.Drawing.Size(24, 31);
            this.buttonCriticalDK.TabIndex = 39;
            this.buttonCriticalDK.UseVisualStyleBackColor = false;
            // 
            // buttonCriticalDi
            // 
            this.buttonCriticalDi.BackColor = System.Drawing.Color.Green;
            this.buttonCriticalDi.Location = new System.Drawing.Point(190, 209);
            this.buttonCriticalDi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCriticalDi.Name = "buttonCriticalDi";
            this.buttonCriticalDi.Size = new System.Drawing.Size(24, 31);
            this.buttonCriticalDi.TabIndex = 40;
            this.buttonCriticalDi.UseVisualStyleBackColor = false;
            // 
            // buttonCritical
            // 
            this.buttonCritical.BackColor = System.Drawing.SystemColors.Highlight;
            this.buttonCritical.Location = new System.Drawing.Point(190, 255);
            this.buttonCritical.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCritical.Name = "buttonCritical";
            this.buttonCritical.Size = new System.Drawing.Size(24, 31);
            this.buttonCritical.TabIndex = 41;
            this.buttonCritical.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 342);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(201, 57);
            this.button1.TabIndex = 42;
            this.button1.Text = "Получить критические значения параметров";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DigitalVoltmeterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 863);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonCritical);
            this.Controls.Add(this.buttonCriticalDi);
            this.Controls.Add(this.buttonCriticalDK);
            this.Controls.Add(this.labelCriticalDsm);
            this.Controls.Add(this.labelCriticalDi);
            this.Controls.Add(this.labelCriticalDK);
            this.Controls.Add(this.dataGridViewVect);
            this.Controls.Add(this.buttonExpand);
            this.Controls.Add(this.mainChart);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(988, 862);
            this.Name = "DigitalVoltmeterForm";
            this.Text = "DigitalVoltmeter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DigitalVoltmeterForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVect)).EndInit();
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
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
        private System.Windows.Forms.Button buttonExpand;
        private System.Windows.Forms.DataGridView dataGridViewVect;
        private System.Windows.Forms.Label labelCriticalDK;
        private System.Windows.Forms.Label labelCriticalDi;
        private System.Windows.Forms.Label labelCriticalDsm;
        private System.Windows.Forms.Button buttonCriticalDK;
        private System.Windows.Forms.Button buttonCriticalDi;
        private System.Windows.Forms.Button buttonCritical;
        private System.Windows.Forms.Button button1;
    }
}


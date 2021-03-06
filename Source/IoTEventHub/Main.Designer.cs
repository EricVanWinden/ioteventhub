﻿
namespace IoTEventHub
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabEventProcessor = new System.Windows.Forms.TabPage();
            this.btnExecuteAction = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabCharts = new System.Windows.Forms.TabPage();
            this.chart = new OxyPlot.WindowsForms.PlotView();
            this.bgwSynchronization = new System.ComponentModel.BackgroundWorker();
            this.comboAction = new System.Windows.Forms.ComboBox();
            this.tabControl.SuspendLayout();
            this.tabEventProcessor.SuspendLayout();
            this.tabCharts.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabEventProcessor);
            this.tabControl.Controls.Add(this.tabCharts);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(933, 519);
            this.tabControl.TabIndex = 0;
            // 
            // tabEventProcessor
            // 
            this.tabEventProcessor.Controls.Add(this.comboAction);
            this.tabEventProcessor.Controls.Add(this.btnExecuteAction);
            this.tabEventProcessor.Controls.Add(this.btnRefresh);
            this.tabEventProcessor.Controls.Add(this.rtbLog);
            this.tabEventProcessor.Controls.Add(this.btnStop);
            this.tabEventProcessor.Controls.Add(this.btnStart);
            this.tabEventProcessor.Location = new System.Drawing.Point(4, 24);
            this.tabEventProcessor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabEventProcessor.Name = "tabEventProcessor";
            this.tabEventProcessor.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabEventProcessor.Size = new System.Drawing.Size(925, 491);
            this.tabEventProcessor.TabIndex = 0;
            this.tabEventProcessor.Text = "Event processor";
            this.tabEventProcessor.UseVisualStyleBackColor = true;
            // 
            // btnExecuteAction
            // 
            this.btnExecuteAction.Location = new System.Drawing.Point(423, 7);
            this.btnExecuteAction.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExecuteAction.Name = "btnExecuteAction";
            this.btnExecuteAction.Size = new System.Drawing.Size(88, 27);
            this.btnExecuteAction.TabIndex = 3;
            this.btnExecuteAction.Text = "Execute";
            this.btnExecuteAction.UseVisualStyleBackColor = true;
            this.btnExecuteAction.Click += new System.EventHandler(this.btnReadFromAzure_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(200, 7);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 27);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.Location = new System.Drawing.Point(9, 40);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(908, 443);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(104, 7);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(88, 27);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(9, 7);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(88, 27);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tabCharts
            // 
            this.tabCharts.Controls.Add(this.chart);
            this.tabCharts.Location = new System.Drawing.Point(4, 24);
            this.tabCharts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabCharts.Name = "tabCharts";
            this.tabCharts.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabCharts.Size = new System.Drawing.Size(925, 491);
            this.tabCharts.TabIndex = 1;
            this.tabCharts.Text = "Charts";
            this.tabCharts.UseVisualStyleBackColor = true;
            // 
            // chart
            // 
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(4, 3);
            this.chart.Name = "chart";
            this.chart.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.chart.Size = new System.Drawing.Size(917, 485);
            this.chart.TabIndex = 0;
            this.chart.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.chart.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.chart.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // bgwSynchronization
            // 
            this.bgwSynchronization.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSynchronization_DoWork);
            this.bgwSynchronization.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwSynchronization_RunWorkerCompleted);
            // 
            // comboAction
            // 
            this.comboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAction.FormattingEnabled = true;
            this.comboAction.Location = new System.Drawing.Point(295, 10);
            this.comboAction.Name = "comboAction";
            this.comboAction.Size = new System.Drawing.Size(121, 23);
            this.comboAction.TabIndex = 4;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Main";
            this.Text = "IoT hub UI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabEventProcessor.ResumeLayout(false);
            this.tabCharts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabEventProcessor;
        private System.Windows.Forms.TabPage tabCharts;
        private OxyPlot.WindowsForms.PlotView chart;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExecuteAction;
        private System.ComponentModel.BackgroundWorker bgwSynchronization;
        private System.Windows.Forms.ComboBox comboAction;
    }
}


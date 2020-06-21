
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
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabCharts = new System.Windows.Forms.TabPage();
            this.chart = new OxyPlot.WindowsForms.PlotView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabEventProcessor.SuspendLayout();
            this.chart.SuspendLayout();
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
            // chart
            //
            this.chart.Location = new System.Drawing.Point(9, 7);
            this.chart.Name = "chart";
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.chart.ResumeLayout(false);
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
    }
}


﻿using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System;
using System.Windows.Forms;

namespace IoTEventHub
{
    /// <summary>
    /// The main form
    /// </summary>
    public partial class Main : Form
    {

        /// <summary>
        /// The IoT event processor
        /// </summary>
        private readonly EventProcessorHost _processor;

        /// <summary>
        /// Constructor
        /// </summary>
        public Main()
        {
            InitializeComponent();

            // Substribe to logging
            StatisticsSingleton.Instance.StatusLogged += PrintToLog;
            StatisticsSingleton.Instance.LogMessage("Subscribed to logger");

            // Create processor
            var config = StatisticsSingleton.Instance.HubConfig;
            var storageContainerName = "messagehost";
            var consumerGroupName = PartitionReceiver.DefaultConsumerGroupName;
            _processor = new EventProcessorHost(config.HubName, consumerGroupName, config.IotHubConnectionString, config.StorageConnectionString, storageContainerName);

            // Enable start and disable stop
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            // Show chart
            UpdateChart();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            // Enable stop and disable start
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            // Register the event processor
            await _processor.RegisterEventProcessorAsync<LoggingEventProcessor>();
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            // Enable start and disable stop
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            // Save data and unregister the event processor
            StatisticsSingleton.Instance.Save();
            await _processor.UnregisterEventProcessorAsync();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateChart();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if event processor is running, close it properly
            if (btnStop.Enabled)
                btnStop_Click(this, EventArgs.Empty);

        }

        private void UpdateChart()
        {
            chart.Model = new OxyPlot.PlotModel()
            {
                PlotType = OxyPlot.PlotType.XY,
                PlotAreaBackground = OxyPlot.OxyColors.White,
                IsLegendVisible = true,
            };

            // Add axes
            chart.Model.Axes.Add(
                new OxyPlot.Axes.LinearAxis()
                {
                    Position = OxyPlot.Axes.AxisPosition.Bottom,
                    Title = "Time",
                    MajorGridlineStyle = OxyPlot.LineStyle.Solid,
                    IsZoomEnabled = false,
                    TickStyle = OxyPlot.Axes.TickStyle.Inside,
                });

            chart.Model.Axes.Add(
                new OxyPlot.Axes.LinearAxis()
                {
                    Position = OxyPlot.Axes.AxisPosition.Left,
                    Title = "Data",
                    MajorGridlineStyle = OxyPlot.LineStyle.Solid,
                    AbsoluteMinimum = 0,
                    TickStyle = OxyPlot.Axes.TickStyle.Inside,
                });

            var curveT = new OxyPlot.Series.LineSeries()
            {
                LineStyle = OxyPlot.LineStyle.Solid,
                Title = "T (°C)",
                Color = OxyPlot.OxyColor.FromRgb(255, 0, 0),
                StrokeThickness = 0.5,
            };
            chart.Model.Series.Add(curveT);

            var curveTmin = new OxyPlot.Series.LineSeries()
            {
                LineStyle = OxyPlot.LineStyle.Solid,
                Title = "T min (°C)",
                Color = OxyPlot.OxyColor.FromRgb(255, 0, 0),
                StrokeThickness = 0.5,
            };
            chart.Model.Series.Add(curveTmin);

            var curveTmax = new OxyPlot.Series.LineSeries()
            {
                LineStyle = OxyPlot.LineStyle.Solid,
                Title = "T max (°C)",
                Color = OxyPlot.OxyColor.FromRgb(255, 0, 0),
                StrokeThickness = 0.5,
            };
            chart.Model.Series.Add(curveTmax);

            var curveRH = new OxyPlot.Series.LineSeries()
            {
                LineStyle = OxyPlot.LineStyle.Solid,
                Title = "RH (%)",
                Color = OxyPlot.OxyColor.FromRgb(0, 255, 0),
                StrokeThickness = 0.5,
            };
            chart.Model.Series.Add(curveRH);

            var curveRHmin = new OxyPlot.Series.LineSeries()
            {
                LineStyle = OxyPlot.LineStyle.Solid,
                Title = "RH min (%)",
                Color = OxyPlot.OxyColor.FromRgb(0, 255, 0),
                StrokeThickness = 0.5,
            };
            chart.Model.Series.Add(curveRHmin);

            var curveRHmax = new OxyPlot.Series.LineSeries()
            {
                LineStyle = OxyPlot.LineStyle.Solid,
                Title = "RH max (%)",
                Color = OxyPlot.OxyColor.FromRgb(0, 255, 0),
                StrokeThickness = 0.5,
            };
            chart.Model.Series.Add(curveRHmax);

            var curveP = new OxyPlot.Series.LineSeries()
            {
                LineStyle = OxyPlot.LineStyle.Solid,
                Title = "p / 20 (mBar)",
                Color = OxyPlot.OxyColor.FromRgb(0, 0, 255),
                StrokeThickness = 0.5,
            };
            chart.Model.Series.Add(curveP);

            var curvePmin = new OxyPlot.Series.LineSeries()
            {
                LineStyle = OxyPlot.LineStyle.Solid,
                Title = "p min / 20 (mBar)",
                Color = OxyPlot.OxyColor.FromRgb(0, 0, 255),
                StrokeThickness = 0.5,
            };
            chart.Model.Series.Add(curvePmin);

            var curvePmax = new OxyPlot.Series.LineSeries()
            {
                LineStyle = OxyPlot.LineStyle.Solid,
                Title = "p max / 20 (mBar)",
                Color = OxyPlot.OxyColor.FromRgb(0, 0, 255),
                StrokeThickness = 0.5,
            };
            chart.Model.Series.Add(curvePmax);

            var curveCount = new OxyPlot.Series.LineSeries()
            {
                LineStyle = OxyPlot.LineStyle.Solid,
                Title = "count / 2",
                Color = OxyPlot.OxyColor.FromRgb(0, 0, 0),
                StrokeThickness = 0.5,
            };
            chart.Model.Series.Add(curveCount);

            var items = StatisticsSingleton.Instance.Statistics;
            chart.Model.Axes[0].Minimum = -1;
            chart.Model.Axes[0].AbsoluteMinimum = -1;
            chart.Model.Axes[0].Maximum = items.Length * 1.1;
            chart.Model.Axes[0].AbsoluteMaximum = items.Length * 1.1;

            var counter = 0;
            foreach (var item in items)
            {
                curveT.Points.Add(new OxyPlot.DataPoint(counter, item.AverageT));
                curveTmin.Points.Add(new OxyPlot.DataPoint(counter, item.MinimumT));
                curveTmax.Points.Add(new OxyPlot.DataPoint(counter, item.MaximumT));

                curveRH.Points.Add(new OxyPlot.DataPoint(counter, item.AverageRH));
                curveRHmin.Points.Add(new OxyPlot.DataPoint(counter, item.MinimumRH));
                curveRHmax.Points.Add(new OxyPlot.DataPoint(counter, item.MaximumRH));

                curveP.Points.Add(new OxyPlot.DataPoint(counter, item.AverageP / 20.0));
                curvePmin.Points.Add(new OxyPlot.DataPoint(counter, item.MinimumP / 20.0));
                curvePmax.Points.Add(new OxyPlot.DataPoint(counter, item.MaximumP / 20.0));

                curveCount.Points.Add(new OxyPlot.DataPoint(counter, item.Count / 2));
                counter++;
            }

            chart.Invalidate();
        }

        internal void PrintToLog(string message)
        {
            var action = new Action(() =>
            {
                rtbLog.AppendText($"{DateTime.Now}:{message}\n");
                rtbLog.ScrollToCaret();
            });

            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}

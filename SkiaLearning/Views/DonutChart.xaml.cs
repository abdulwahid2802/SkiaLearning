using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaLearning.Views
{
    public partial class DonutChart : ContentPage
    {
        class ChartData
        {
            public ChartData(int value, SKColor color)
            {
                Value = value;
                Color = color;
            }

            public int Value { private set; get; }

            public SKColor Color { private set; get; }
        }

        ChartData[] chartData =
        {
            new ChartData(45, SKColors.Red),
            new ChartData(13, SKColors.Green),
            new ChartData(27, SKColors.Blue),
            new ChartData(19, SKColors.Magenta),
            new ChartData(40, SKColors.Cyan),
            new ChartData(22, SKColors.Brown),
            new ChartData(29, SKColors.Gray)
        };

        public DonutChart()
        {
            InitializeComponent();
        }

        void OnSliderValueChanged(object sender, EventArgs e)
        {
            canvasView.InvalidateSurface();
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            int totalValues = 0;

            foreach (ChartData item in chartData)
            {
                totalValues += item.Value;
            }

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float explodeOffset = explodeSwitch.IsToggled ? (float)offsetSlider.Value : 0f;
            float radius = Math.Min(info.Width / 3, info.Height / 3) - 2 * explodeOffset;
            SKRect rect = new SKRect(center.X - radius, center.Y - radius,
                                     center.X + radius, center.Y + radius);

            float startAngle = 0;

            foreach (ChartData item in chartData)
            {
                float sweepAngle = 360f * item.Value / totalValues;

                using (SKPath path = new SKPath())
                using (SKPaint strokePaint = new SKPaint())
                {
                    path.AddArc(rect, startAngle, sweepAngle);

                    strokePaint.Style = SKPaintStyle.Stroke;
                    strokePaint.Color = item.Color;
                    strokePaint.StrokeWidth = radius * .9f;

                    // Calculate "explode" transform
                    float angle = startAngle + 0.5f * sweepAngle;
                    float x = explodeOffset * (float)Math.Cos(Math.PI * angle / 180);
                    float y = explodeOffset * (float)Math.Sin(Math.PI * angle / 180);

                    canvas.Save();
                    canvas.Translate(x, y);

                    // Fill and stroke the path
                    canvas.DrawPath(path, strokePaint);
                    canvas.Restore();


                }

                startAngle += sweepAngle;
            }

        }
    }
}

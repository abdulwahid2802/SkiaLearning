using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaLearning.Views
{
    public partial class PieChartPage : ContentPage
    {
        public class ChartData
        {
            public ChartData(int value, SKColor color)
            {
                Value = value;
                Color = Color;
            }

            public int Value { get; private set; }

            public SKColor Color { get; private set; }
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

        public PieChartPage()
        {
            InitializeComponent();


        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            int totalValues = 0;

            foreach (ChartData item in chartData)
            {
                totalValues += item.Value;
            }

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float explodeOffset = 50;
            float radius = Math.Min(info.Width / 2, info.Height / 2) - 2 * explodeOffset;
            SKRect rect = new SKRect(center.X - radius, center.Y - radius,
                                     center.X + radius, center.Y + radius);

            float startAngle = 0;

            foreach (ChartData item in chartData)
            {
                float sweepAngle = 360f * item.Value / totalValues;

                using (SKPath path = new SKPath())
                using (SKPaint fillPaint = new SKPaint())
                using (SKPaint outlinePaint = new SKPaint())
                {
                    path.MoveTo(center);
                    path.ArcTo(rect, startAngle, sweepAngle, false);
                    path.Close();

                    Random r = new Random();

                    fillPaint.Style = SKPaintStyle.Fill;
                    fillPaint.Color = Color.FromRgb(r.Next(255), r.Next(255), r.Next(255)).ToSKColor();


                    outlinePaint.Style = SKPaintStyle.Stroke;
                    outlinePaint.StrokeWidth = 5;
                    outlinePaint.Color = SKColors.Black;

                    // Calculate "explode" transform
                    float angle = startAngle + 0.5f * sweepAngle;
                    float x = explodeOffset * (float)Math.Cos(Math.PI * angle / 180);
                    float y = explodeOffset * (float)Math.Sin(Math.PI * angle / 180);

                    canvas.Save();
                    canvas.Translate(x, y);

                    // Fill and stroke the path
                    canvas.DrawPath(path, fillPaint);
                    //canvas.DrawPath(path, outlinePaint);
                    canvas.Restore();
                }

                startAngle += sweepAngle;
            }
        }
    }
}

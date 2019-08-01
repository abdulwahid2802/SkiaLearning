using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaLearning.Views
{
    public partial class ArcsAndCurves : ContentPage
    {
        public ArcsAndCurves()
        {
            InitializeComponent();
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;


            canvas.Clear();

            SKRect rect = new SKRect(100, 100, info.Width - 100, info.Height - 100);
            float startAngle = (float)startSlider.Value;
            float sweepAngle = (float)sweepSlider.Value;

            canvas.DrawOval(rect, new SKPaint { Color = Color.DimGray.ToSKColor(), StrokeWidth = 25, Style = SKPaintStyle.Stroke});

            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, startAngle, sweepAngle);
                canvas.DrawPath(path, new SKPaint { Color = Color.AliceBlue.ToSKColor(), StrokeWidth = 50, Style = SKPaintStyle.Stroke });

            }

        }

        void OnSliderValueChanged(object sender, EventArgs e)
        {
            canvasView.InvalidateSurface();
        }
    }
}

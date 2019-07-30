using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaLearning.Views
{
    public partial class OvalFillPage : ContentPage
    {
        SKCanvasView canvasView;

        public OvalFillPage()
        {
            InitializeComponent();

            canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasPaintSurface;
            Content = canvasView;
        }

        private void OnCanvasPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();


            var strokeWidth = 50.0F;
            var xRadius = (info.Width - strokeWidth) / 2;
            var yRadius = (info.Height - strokeWidth) / 2;

            var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = strokeWidth,
                Color = Color.Blue.ToSKColor()
            };

            canvas.DrawOval(info.Width / 2, info.Height / 2, xRadius, yRadius, paint);

            //SKRect rect = new SKRect(strokeWidth / 2,
            //    strokeWidth / 2,
            //    info.Width - strokeWidth / 2,
            //    info.Height - strokeWidth / 2);

            //canvas.DrawOval(rect, paint);

        }
    }
}

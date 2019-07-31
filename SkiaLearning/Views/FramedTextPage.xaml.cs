using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaLearning.Views
{
    public partial class FramedTextPage : ContentPage
    {
        SKCanvasView canvasView;

        public FramedTextPage()
        {
            InitializeComponent();

            canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;

            Content = canvasView;

        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            string str = "Hello SkiaSharp!";

            var textPaint = new SKPaint
            {
                Color = Color.Chocolate.ToSKColor()
            };


            float textWidth = textPaint.MeasureText(str);
            textPaint.TextSize = .9f * info.Width * textPaint.TextSize / textWidth;

            var textBounds = new SKRect();
            textPaint.MeasureText(str, ref textBounds);

            // Calculate offsets to center the text on the screen
            float xText = info.Width / 2 - textBounds.MidX;
            float yText = info.Height / 2 - textBounds.MidY;

            // And draw the text
            canvas.DrawText(str, xText, yText, textPaint);

        }
    }
}

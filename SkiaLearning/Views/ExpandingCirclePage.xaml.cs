using System;
using System.Collections.Generic;
using System.Diagnostics;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaLearning.Views
{
    public partial class ExpandingCirclePage : ContentPage
    {
        const double cycleTime = 1000;

        SKCanvasView canvasView;
        bool pageIsActive;
        Stopwatch stopwatch = new Stopwatch();
        float t;

        SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke
        };


        public ExpandingCirclePage()
        {
            InitializeComponent();

            canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;

            Content = canvasView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            pageIsActive = true;
            stopwatch.Start();

            Device.StartTimer(TimeSpan.FromMilliseconds(33), () =>
            {
                t = (float)(stopwatch.Elapsed.TotalMilliseconds % cycleTime / cycleTime);
                canvasView.InvalidateSurface();

                if(!pageIsActive)
                {
                    stopwatch.Stop();
                }
                return pageIsActive;
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);

            float baseRadius = Math.Min(info.Width, info.Height) / 12;

            for(int circle=0;circle<5;circle++)
            {
                float radius = baseRadius * (circle + t);

                paint.StrokeWidth = baseRadius / 2 * (circle == 0 ? t : 1);
                paint.Color = new SKColor(0, 0, 255,
                (byte)(255 * (circle == 4 ? (1 - t) : 1)));

                canvas.DrawCircle(center.X, center.Y, radius, paint);

            }

        }
    }
}

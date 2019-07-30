using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaLearning.Views
{
    public partial class TapToggleFillPage : ContentPage
    {
        static bool isFilled = false;

        public TapToggleFillPage()
        {
            InitializeComponent();
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();


            var paint = new SKPaint
            {
                Color = Color.Black.ToSKColor(),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 50
            };

            canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);

            if(isFilled)
            {
                paint.Style = SKPaintStyle.Fill;
                paint.Color = Color.HotPink.ToSKColor();
                canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);
            }

        }

        void OnCanvasViewTapped(object sender, EventArgs args)
        {
            isFilled ^= true;

            (sender as SKCanvasView).InvalidateSurface();
        }


    }

}

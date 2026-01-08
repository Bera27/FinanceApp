using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Drawables
{
    public class Drawable : IDrawable
    {
        public Color HeaderColor { get; set; } = Colors.Teal;
        public Color PageBackgroundColor { get; set; } = Colors.White;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();
            canvas.FillColor = HeaderColor;

            float radius = Math.Min(24f, dirtyRect.Height / 2f);

            canvas.FillRoundedRectangle(0, 0, dirtyRect.Width, dirtyRect.Height, radius);

            canvas.FillRectangle(0, 0, dirtyRect.Width, dirtyRect.Height - radius);

            canvas.RestoreState();
        }
    }
}

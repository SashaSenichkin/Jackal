namespace jackal.Paint;


public class EllipsePainter : Painter
{
    protected override void DrawBackground(
        Graphics graphics,
        Rectangle bounds,
        Brush backgroundBrush)
    {
        graphics.FillEllipse(backgroundBrush, bounds);
    }

    protected override void DrawBorder(Graphics graphics, Rectangle bounds, Pen borderPen) => graphics.DrawEllipse(borderPen, bounds);

    protected override void DrawString(
        Graphics graphics,
        string text,
        Brush textBrush,
        Rectangle bounds)
    {
        graphics.DrawString(text, DrawFont, textBrush, new Rectangle(bounds.X + bounds.Width / 6, bounds.Y, bounds.Width, bounds.Height));
    }
}
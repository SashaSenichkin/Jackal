namespace jackal.Paint;

public class RectanglePainter : Painter
{
    protected override void DrawBackground(
        Graphics graphics,
        Rectangle bounds,
        Brush backgroundBrush)
    {
        graphics.FillRectangle(backgroundBrush, bounds);
    }

    protected override void DrawBorder(Graphics graphics, Rectangle bounds, Pen borderPen) => graphics.DrawRectangle(borderPen, bounds);

    protected override void DrawString(
        Graphics graphics,
        string text,
        Brush textBrush,
        Rectangle bounds)
    {
        graphics.DrawString(text, DrawFont, textBrush, bounds);
    }
}

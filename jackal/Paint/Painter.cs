namespace jackal.Paint;

public class Painter
{
    protected readonly Font DrawFont = new ("Arial", 10f);

    internal void Draw(
        Graphics graphics,
        Rectangle bounds,
        Pen borderPen,
        string text,
        Brush textBrush,
        Brush backgroung)
    {
        DrawBackground(graphics, bounds, backgroung);
        DrawBorder(graphics, bounds, borderPen);
        DrawString(graphics, text, textBrush, bounds);
    }

    protected virtual void DrawBorder(Graphics graphics, Rectangle bounds, Pen borderPen)
    {
    }

    protected virtual void DrawBackground(
        Graphics graphics,
        Rectangle bounds,
        Brush backgroundBrush)
    {
    }

    protected virtual void DrawString(
        Graphics graphics,
        string text,
        Brush textBrush,
        Rectangle bounds)
    {
    }
}
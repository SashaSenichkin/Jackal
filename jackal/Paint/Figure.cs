namespace jackal.Paint;

public abstract class Figure
{
    private readonly Painter _Painter;

    protected Figure() => _Painter = CreatePainter();

    private Painter Painter => _Painter;

    public Rectangle Bounds { get; set; }

    public Pen BorderPen { get; set; }

    public Brush TextBrush { get; set; }

    public Brush BackBrush { get; set; } = Brushes.Snow;

    public string Text { get; set; }

    protected abstract Painter CreatePainter();

    public void DrawFigure(Graphics graphics) => Painter.Draw(graphics, Bounds, BorderPen, Text, TextBrush, BackBrush);

    public void DrawEllipse(Graphics graphics) => Painter.Draw(graphics, Bounds, BorderPen, Text, TextBrush, BackBrush);
}
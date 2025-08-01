namespace jackal.Paint;

internal class Paint
{
  public class MyEllipse : Figure
  {
    protected override Painter CreatePainter() => new EllipsePainter();
  }

  public class MyRectangle : Figure
  {
    protected override Painter CreatePainter() => new RectanglePainter();
  }
}

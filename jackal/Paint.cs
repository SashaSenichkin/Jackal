// Decompiled with JetBrains decompiler
// Type: jackal.Paint
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using System.Drawing;

namespace jackal
{
  internal class Paint
  {
    public class MyEllipse : Painter.Figure
    {
      protected override Painter CreatePainter() => new Painter.EllipsePainter();
    }

    public class MyRectangle : Painter.Figure
    {
      protected override Painter CreatePainter() => new Painter.RectanglePainter();
    }

    public class Painter
    {
      private Font drawFont = new Font("Arial", 10f);

      public void Draw(
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
          graphics.DrawString(text, drawFont, textBrush, new Rectangle(bounds.X + bounds.Width / 6, bounds.Y, bounds.Width, bounds.Height));
        }
      }

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
          graphics.DrawString(text, drawFont, textBrush, bounds);
        }
      }

      public class Figure
      {
        private Pen _BorderPen;
        private Rectangle _Bounds;
        private Painter _Painter;
        private Brush _TextBrush;
        private string _text;
        private Brush _defaultBrush = Brushes.Snow;

        public Figure() => _Painter = CreatePainter();

        private Painter Painter => _Painter;

        public Rectangle Bounds
        {
          get => _Bounds;
          set => _Bounds = value;
        }

        public Pen BorderPen
        {
          get => _BorderPen;
          set => _BorderPen = value;
        }

        public Brush TextBrush
        {
          get => _TextBrush;
          set => _TextBrush = value;
        }

        public Brush BackBrush
        {
          get => _defaultBrush;
          set => _defaultBrush = value;
        }

        public string Text
        {
          get => _text;
          set => _text = value;
        }

        protected virtual Painter CreatePainter() => new Painter();

        public void DrawFigure(Graphics graphics) => Painter.Draw(graphics, Bounds, BorderPen, Text, TextBrush, BackBrush);

        public void DrawEllipce(Graphics graphics) => Painter.Draw(graphics, Bounds, BorderPen, Text, TextBrush, BackBrush);
      }
    }
  }
}

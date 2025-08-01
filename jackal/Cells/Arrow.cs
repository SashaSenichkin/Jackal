using System;
using System.Collections.Generic;

namespace jackal;

[Serializable]
internal class Arrow(Image img, Point address) : Cell(img, address)
{
  public int[] directionsToMove = new int[8];

  public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
  {
    var move = base.CellsAbleToMove(unitToMove, withoutSea);
    if (directionsToMove[0] == 0)
    {
      move.RemoveAll(x =>
      {
        var cellAddress1 = x.CellAddress;
        var cellAddress2 = CellAddress;
        var x1 = cellAddress2.X;
        cellAddress2 = CellAddress;
        var y = cellAddress2.Y - 1;
        var point = new Point(x1, y);
        return cellAddress1 == point;
      });
    }

    if (directionsToMove[1] == 0)
    {
      move.RemoveAll(x =>
      {
        var cellAddress3 = x.CellAddress;
        var cellAddress4 = CellAddress;
        var x2 = cellAddress4.X + 1;
        cellAddress4 = CellAddress;
        var y = cellAddress4.Y;
        var point = new Point(x2, y);
        return cellAddress3 == point;
      });
    }

    if (directionsToMove[2] == 0)
    {
      move.RemoveAll(x =>
      {
        var cellAddress5 = x.CellAddress;
        var cellAddress6 = CellAddress;
        var x3 = cellAddress6.X;
        cellAddress6 = CellAddress;
        var y = cellAddress6.Y + 1;
        var point = new Point(x3, y);
        return cellAddress5 == point;
      });
    }

    if (directionsToMove[3] == 0)
    {
      move.RemoveAll(x =>
      {
        var cellAddress7 = x.CellAddress;
        var cellAddress8 = CellAddress;
        var x4 = cellAddress8.X - 1;
        cellAddress8 = CellAddress;
        var y = cellAddress8.Y;
        var point = new Point(x4, y);
        return cellAddress7 == point;
      });
    }

    if (directionsToMove[4] == 0)
    {
      move.RemoveAll(x =>
      {
        var cellAddress9 = x.CellAddress;
        var cellAddress10 = CellAddress;
        var x5 = cellAddress10.X - 1;
        cellAddress10 = CellAddress;
        var y = cellAddress10.Y - 1;
        var point = new Point(x5, y);
        return cellAddress9 == point;
      });
    }

    if (directionsToMove[5] == 0)
    {
      move.RemoveAll(x =>
      {
        var cellAddress11 = x.CellAddress;
        var cellAddress12 = CellAddress;
        var x6 = cellAddress12.X + 1;
        cellAddress12 = CellAddress;
        var y = cellAddress12.Y - 1;
        var point = new Point(x6, y);
        return cellAddress11 == point;
      });
    }

    if (directionsToMove[6] == 0)
    {
      move.RemoveAll(x =>
      {
        var cellAddress13 = x.CellAddress;
        var cellAddress14 = CellAddress;
        var x7 = cellAddress14.X + 1;
        cellAddress14 = CellAddress;
        var y = cellAddress14.Y + 1;
        var point = new Point(x7, y);
        return cellAddress13 == point;
      });
    }

    if (directionsToMove[7] == 0)
    {
      move.RemoveAll(x =>
      {
        var cellAddress15 = x.CellAddress;
        var cellAddress16 = CellAddress;
        var x8 = cellAddress16.X - 1;
        cellAddress16 = CellAddress;
        var y = cellAddress16.Y + 1;
        var point = new Point(x8, y);
        return cellAddress15 == point;
      });
    }

    return move;
  }
}
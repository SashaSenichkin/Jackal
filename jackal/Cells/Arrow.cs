// Decompiled with JetBrains decompiler
// Type: jackal.Arrow
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace jackal
{
  [Serializable]
  internal class Arrow : Cell
  {
    public int[] directionsToMove = new int[8];

    public Arrow(Image img, Point adress)
      : base(img, adress)
    {
    }

    public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
    {
      List<Cell> move = base.CellsAbleToMove(unitToMove, withoutSea);
      if (directionsToMove[0] == 0)
        move.RemoveAll(x =>
        {
          Point cellAdress1 = x.CellAdress;
          Point cellAdress2 = CellAdress;
          int x1 = cellAdress2.X;
          cellAdress2 = CellAdress;
          int y = cellAdress2.Y - 1;
          Point point = new Point(x1, y);
          return cellAdress1 == point;
        });
      if (directionsToMove[1] == 0)
        move.RemoveAll(x =>
        {
          Point cellAdress3 = x.CellAdress;
          Point cellAdress4 = CellAdress;
          int x2 = cellAdress4.X + 1;
          cellAdress4 = CellAdress;
          int y = cellAdress4.Y;
          Point point = new Point(x2, y);
          return cellAdress3 == point;
        });
      if (directionsToMove[2] == 0)
        move.RemoveAll(x =>
        {
          Point cellAdress5 = x.CellAdress;
          Point cellAdress6 = CellAdress;
          int x3 = cellAdress6.X;
          cellAdress6 = CellAdress;
          int y = cellAdress6.Y + 1;
          Point point = new Point(x3, y);
          return cellAdress5 == point;
        });
      if (directionsToMove[3] == 0)
        move.RemoveAll(x =>
        {
          Point cellAdress7 = x.CellAdress;
          Point cellAdress8 = CellAdress;
          int x4 = cellAdress8.X - 1;
          cellAdress8 = CellAdress;
          int y = cellAdress8.Y;
          Point point = new Point(x4, y);
          return cellAdress7 == point;
        });
      if (directionsToMove[4] == 0)
        move.RemoveAll(x =>
        {
          Point cellAdress9 = x.CellAdress;
          Point cellAdress10 = CellAdress;
          int x5 = cellAdress10.X - 1;
          cellAdress10 = CellAdress;
          int y = cellAdress10.Y - 1;
          Point point = new Point(x5, y);
          return cellAdress9 == point;
        });
      if (directionsToMove[5] == 0)
        move.RemoveAll(x =>
        {
          Point cellAdress11 = x.CellAdress;
          Point cellAdress12 = CellAdress;
          int x6 = cellAdress12.X + 1;
          cellAdress12 = CellAdress;
          int y = cellAdress12.Y - 1;
          Point point = new Point(x6, y);
          return cellAdress11 == point;
        });
      if (directionsToMove[6] == 0)
        move.RemoveAll(x =>
        {
          Point cellAdress13 = x.CellAdress;
          Point cellAdress14 = CellAdress;
          int x7 = cellAdress14.X + 1;
          cellAdress14 = CellAdress;
          int y = cellAdress14.Y + 1;
          Point point = new Point(x7, y);
          return cellAdress13 == point;
        });
      if (directionsToMove[7] == 0)
        move.RemoveAll(x =>
        {
          Point cellAdress15 = x.CellAdress;
          Point cellAdress16 = CellAdress;
          int x8 = cellAdress16.X - 1;
          cellAdress16 = CellAdress;
          int y = cellAdress16.Y + 1;
          Point point = new Point(x8, y);
          return cellAdress15 == point;
        });
      return move;
    }
  }
}

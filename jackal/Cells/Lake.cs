// Decompiled with JetBrains decompiler
// Type: jackal.Lake
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using jackal.Properties;
using System;
using System.Drawing;
using System.Linq;

namespace jackal
{
  [Serializable]
  internal class Lake : Cell
  {
    public Lake(Point adress)
      : base(Resources.lake, adress)
    {
    }

    public override void OnCellMove(Unit unitToMove)
    {
      Point adressSave = unitToMove.UnitAdress;
      base.OnCellMove(unitToMove);
      Point cellAdress = CellAdress;
      int x1 = cellAdress.X * 2 - adressSave.X;
      cellAdress = CellAdress;
      int y = cellAdress.Y * 2 - adressSave.Y;
      adressSave = new Point(x1, y);
      if (adressSave.X < 0 || adressSave.Y < 0 || adressSave.X > 13 || adressSave.Y > 13)
        GameData.AllUnits.Remove(unitToMove);
      else
        GameData.AllCells.Single(x => x.CellAdress == adressSave).OnCellMove(unitToMove);
    }
  }
}

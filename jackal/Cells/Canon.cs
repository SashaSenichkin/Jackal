// Decompiled with JetBrains decompiler
// Type: jackal.Canon
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
  internal class Canon : Cell
  {
    public int[] directionsToMove = new int[8];

    public Canon(int direction, Point adress)
      : base(Resources.canon, adress)
    {
      directionsToMove[direction] = 1;
      Spin = direction;
      for (int index = 0; index < direction; ++index)
        CellImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
    }

    public override void OnCellMove(Unit unitToMove)
    {
      if (!IsCellOpen)
        OnCellOpen(unitToMove);
      Point target = new Point();
      if (directionsToMove[0] == 1)
        target = new Point(CellAdress.X, 0);
      else if (directionsToMove[1] == 1)
        target = new Point(12, CellAdress.Y);
      else if (directionsToMove[2] == 1)
        target = new Point(CellAdress.X, 12);
      else if (directionsToMove[3] == 1)
        target = new Point(0, CellAdress.Y);
      GameData.AllCells.Single(x => x.CellAdress == target).OnCellMove(unitToMove);
    }
  }
}

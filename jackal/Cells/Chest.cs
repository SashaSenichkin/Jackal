// Decompiled with JetBrains decompiler
// Type: jackal.Chest
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using System;
using System.Drawing;

namespace jackal
{
  [Serializable]
  internal class Chest : Cell
  {
    public int CoinsInChest;

    public Chest(Image img, Point adress)
      : base(img, adress)
    {
    }

    public override void OnCellOpen(Unit unitToMove)
    {
      base.OnCellOpen(unitToMove);
      CoinsOnCell = CoinsInChest;
      unitToMove.UnitAdress = CellAdress;
    }
  }
}

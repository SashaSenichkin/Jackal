// Decompiled with JetBrains decompiler
// Type: jackal.Castle
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
  internal class Castle : Cell
  {
    public Castle(Point adress)
      : base(Resources.castle, adress)
    {
    }

    public override void OnCellMove(Unit unitToMove)
    {
      if (GameData.AllUnits.Count(x => x.UnitAdress == CellAdress && x.Player != unitToMove.Player) > 0)
        GameData.AllUnits.Remove(unitToMove);
      else
        base.OnCellMove(unitToMove);
    }
  }
}

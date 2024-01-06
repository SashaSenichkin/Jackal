// Decompiled with JetBrains decompiler
// Type: jackal.Pit
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using jackal.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace jackal
{
  [Serializable]
  internal class Pit : Cell
  {
    public Pit(Point adress)
      : base(Resources.pit, adress)
    {
    }

    public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
    {
      List<Point> PlayerUnits = GameData.AllUnits.Where(x => x.Player == GameData.AllUnits.Single(y => y.UnitAdress == CellAdress).Player).Select(x => x.UnitAdress).ToList();
      List<Cell> move = base.CellsAbleToMove(unitToMove, withoutSea);
      move.RemoveAll(x => !PlayerUnits.Contains(x.CellAdress));
      return move;
    }
  }
}

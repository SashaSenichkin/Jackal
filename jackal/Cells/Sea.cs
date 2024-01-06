// Decompiled with JetBrains decompiler
// Type: jackal.Sea
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
  internal class Sea : Cell
  {
    public Sea(Point adress)
      : base(Resources.sea, adress)
    {
    }

    public override void OnCellMove(Unit unitToMove)
    {
      if (GameData.AllCells.Single(x => x.CellAdress == unitToMove.UnitAdress) is Lake)
      {
        GameData.AllUnits.Remove(unitToMove);
      }
      else
      {
        GameData.AllUnits.RemoveAll(x => x.UnitAdress == CellAdress && x.Player != unitToMove.Player);
        base.OnCellMove(unitToMove);
        unitToMove.CoinHold = false;
        CoinsOnCell = 0;
      }
    }

    public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = false)
    {
      List<Cell> move = base.CellsAbleToMove(unitToMove, false);
      move.RemoveAll(x => !(x is Sea) && !(x is Ship));
      return move;
    }
  }
}

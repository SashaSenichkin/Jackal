// Decompiled with JetBrains decompiler
// Type: jackal.Plane
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
  internal class Plane : Cell
  {
    public Unit PlaneOwner;

    public Plane(Point adress)
      : base(Resources.plane, adress)
    {
    }

    public override void OnCellOpen(Unit unitToMove)
    {
      PlaneOwner = unitToMove;
      base.OnCellOpen(unitToMove);
    }

    public override void OnCellMove(Unit unitToMove)
    {
      if (GameData.AllUnits.Count(x => x.UnitAdress == CellAdress) > 0 && PlaneOwner != null)
        PlaneOwner = unitToMove;
      base.OnCellMove(unitToMove);
    }

    public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
    {
      if (unitToMove != PlaneOwner)
        return base.CellsAbleToMove(unitToMove);
      List<Cell> list = GameData.AllCells.Where(x =>
      {
        switch (x)
        {
          case Sea _:
          case Ship _:
            return false;
          default:
            return !(x is Plane);
        }
      }).ToList();
      list.Add(GameData.GetAllShips().Single(x => x.Player == unitToMove.Player));
      return list;
    }

    public void FromCellMove(Unit unitToMove, Cell cellToMove)
    {
      if (PlaneOwner == null || unitToMove != PlaneOwner)
        return;
      cellToMove.CoinsOnCell = CoinsOnCell;
      CoinsOnCell = 0;
    }
  }
}

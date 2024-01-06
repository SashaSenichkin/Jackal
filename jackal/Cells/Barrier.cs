// Decompiled with JetBrains decompiler
// Type: jackal.Barrier
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace jackal
{
  [Serializable]
  internal class Barrier : Cell
  {
    public List<BarrierCell> SubCells = new List<BarrierCell>();

    public Barrier(int turns, Image img, Point adress)
      : base(img, adress)
    {
      for (int index = 0; index <= turns; ++index)
        SubCells.Add(new BarrierCell()
        {
          CellNumber = index
        });
    }

    public override void OnCellMove(Unit unitToMove)
    {
      if (!IsCellOpen)
        OnCellOpen(unitToMove);
      if (unitToMove.CoinHold)
      {
        --GameData.AllCells.Single(x => x.CellAdress == unitToMove.UnitAdress).CoinsOnCell;
        ++SubCells[0].CoinsOnCell;
      }
      unitToMove.UnitAdress = CellAdress;
      foreach (Unit unit in SubCells[0].UnitsOnCell.Where(x => x.Player != unitToMove.Player))
        GameData.ReturnToShip(unit);
      SubCells[0].UnitsOnCell.Add(unitToMove);
    }

    public void OnSubCellMove(Unit unitToMove)
    {
      OnSubCellAttack(unitToMove);
      int cellNumber = SubCells.Single(x => x.UnitsOnCell.Contains(unitToMove)).CellNumber;
      if (unitToMove.CoinHold)
      {
        --SubCells[cellNumber].CoinsOnCell;
        ++SubCells[cellNumber + 1].CoinsOnCell;
      }
      SubCells[cellNumber].UnitsOnCell.Remove(unitToMove);
      SubCells[cellNumber + 1].UnitsOnCell.Add(unitToMove);
    }

    public void OnSubCellAttack(Unit unitToMove)
    {
      int cellNumber = SubCells.Single(x => x.UnitsOnCell.Contains(unitToMove)).CellNumber;
      if (SubCells[cellNumber + 1].UnitsOnCell.Count(x => x.Player != unitToMove.Player) <= 0)
        return;
      foreach (Unit unit in SubCells[cellNumber + 1].UnitsOnCell)
        GameData.ReturnToShip(unit);
    }

    public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true) => base.CellsAbleToMove(unitToMove);
  }
}

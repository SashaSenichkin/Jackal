using System;
using System.Collections.Generic;
using System.Linq;

namespace jackal;

[Serializable]
internal class Barrier : Cell
{
  public List<BarrierCell> SubCells = [];

  public Barrier(int turns, Image img, Point address)
    : base(img, address)
  {
    for (var index = 0; index <= turns; ++index)
    {
      SubCells.Add(new BarrierCell()
      {
        CellNumber = index,
      });
    }
  }

  public override void OnCellMove(Unit unitToMove)
  {
    if (!IsCellOpen)
    {
      OnCellOpen(unitToMove);
    }

    if (unitToMove.CoinHold)
    {
      --GameData.AllCells.Single(x => x.CellAddress == unitToMove.UnitAddress).CoinsOnCell;
      ++SubCells[0].CoinsOnCell;
    }
    unitToMove.UnitAddress = CellAddress;
    foreach (var unit in SubCells[0].UnitsOnCell.Where(x => x.Player != unitToMove.Player))
    {
      GameData.ReturnToShip(unit);
    }

    SubCells[0].UnitsOnCell.Add(unitToMove);
  }

  public void OnSubCellMove(Unit unitToMove)
  {
    OnSubCellAttack(unitToMove);
    var cellNumber = SubCells.Single(x => x.UnitsOnCell.Contains(unitToMove)).CellNumber;
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
    var cellNumber = SubCells.Single(x => x.UnitsOnCell.Contains(unitToMove)).CellNumber;
    if (SubCells[cellNumber + 1].UnitsOnCell.Count(x => x.Player != unitToMove.Player) <= 0)
    {
      return;
    }

    foreach (var unit in SubCells[cellNumber + 1].UnitsOnCell)
    {
      GameData.ReturnToShip(unit);
    }
  }
}
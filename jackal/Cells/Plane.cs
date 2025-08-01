using jackal.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jackal;

[Serializable]
internal class Plane(Point address) : Cell(Resources.plane, address)
{
  public Unit PlaneOwner;

  public override void OnCellOpen(Unit unitToMove)
  {
    PlaneOwner = unitToMove;
    base.OnCellOpen(unitToMove);
  }

  public override void OnCellMove(Unit unitToMove)
  {
    if (GameData.AllUnits.Count(x => x.UnitAddress == CellAddress) > 0 && PlaneOwner != null)
    {
      PlaneOwner = unitToMove;
    }

    base.OnCellMove(unitToMove);
  }

  public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
  {
    if (unitToMove != PlaneOwner)
    {
      return base.CellsAbleToMove(unitToMove, withoutSea);
    }

    var list = GameData.AllCells.Where(x =>
    {
      return x switch
      {
        Sea _ or Ship _ => false,
        _ => x is not Plane,
      };
    }).ToList();
    
    list.Add(GameData.GetAllShips().Single(x => x.Player == unitToMove.Player));
    return list;
  }

  public void FromCellMove(Unit unitToMove, Cell cellToMove)
  {
    if (PlaneOwner == null || unitToMove != PlaneOwner)
    {
      return;
    }

    cellToMove.CoinsOnCell = CoinsOnCell;
    CoinsOnCell = 0;
  }
}
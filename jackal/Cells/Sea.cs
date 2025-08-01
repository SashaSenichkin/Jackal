using jackal.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jackal;

[Serializable]
internal class Sea(Point address) : Cell(Resources.sea, address)
{
  public override void OnCellMove(Unit unitToMove)
  {
    if (GameData.AllCells.Single(x => x.CellAddress == unitToMove.UnitAddress) is Lake)
    {
      GameData.AllUnits.Remove(unitToMove);
    }
    else
    {
      GameData.AllUnits.RemoveAll(x => x.UnitAddress == CellAddress && x.Player != unitToMove.Player);
      base.OnCellMove(unitToMove);
      unitToMove.CoinHold = false;
      CoinsOnCell = 0;
    }
  }

  public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
  {
    var move = base.CellsAbleToMove(unitToMove, false);
    move.RemoveAll(x => x is not Sea && x is not Ship);
    return move;
  }
}
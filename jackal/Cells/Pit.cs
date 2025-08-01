using jackal.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jackal;

[Serializable]
internal class Pit(Point address) : Cell(Resources.pit, address)
{
  public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
  {
    var PlayerUnits = GameData.AllUnits
      .Where(x => x.Player == GameData.AllUnits
        .Single(y => y.UnitAddress == CellAddress).Player)
      .Select(x => x.UnitAddress)
      .ToList();
    
    var move = base.CellsAbleToMove(unitToMove, withoutSea);
    move.RemoveAll(x => !PlayerUnits.Contains(x.CellAddress));
    return move;
  }
}
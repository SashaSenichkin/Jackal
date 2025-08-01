using jackal.Properties;
using System;
using System.Linq;

namespace jackal;

[Serializable]
internal class Castle(Point address) : Cell(Resources.castle, address)
{
  public override void OnCellMove(Unit unitToMove)
  {
    if (GameData.AllUnits.Count(x => x.UnitAddress == CellAddress && x.Player != unitToMove.Player) > 0)
    {
      GameData.AllUnits.Remove(unitToMove);
    }
    else
    {
      base.OnCellMove(unitToMove);
    }
  }
}
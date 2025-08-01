using jackal.Properties;
using System.Linq;

namespace jackal;

internal class Healer(Point address) : Cell(Resources.healer, address)
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
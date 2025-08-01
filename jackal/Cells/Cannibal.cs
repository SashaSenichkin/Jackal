using jackal.Properties;
using System;

namespace jackal;

[Serializable]
internal class Cannibal(Point address) : Cell(Resources.canibal, address)
{
  public override void OnCellMove(Unit unitToMove)
  {
    if (!IsCellOpen)
    {
      OnCellOpen(unitToMove);
    }

    GameData.AllUnits.Remove(unitToMove);
  }
}
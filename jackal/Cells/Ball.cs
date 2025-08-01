using jackal.Properties;
using System;
using System.Linq;

namespace jackal;

[Serializable]
internal class Ball(Point address) : Cell(Resources.ball, address)
{
  public override void OnCellMove(Unit unitToMove)
  {
    if (!IsCellOpen)
    {
      OnCellOpen(unitToMove);
    }

    GameData.GetAllShips().Single(x => x.Player == unitToMove.Player).OnCellMove(unitToMove);
  }
}
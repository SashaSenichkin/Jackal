using jackal.Properties;
using System;

namespace jackal;

[Serializable]
internal class Rum(Point address) : Cell(Resources.rum, address)
{
  public int DelayTurn;

  public override void OnCellMove(Unit unitToMove)
  {
    DelayTurn = GameData.TurnCount + GameData.Players;
    base.OnCellMove(unitToMove);
  }
}
using jackal.Properties;
using System;

namespace jackal;

[Serializable]
internal class Crocodile(Point address) : Cell(Resources.crocodile, address)
{
  public override void OnCellMove(Unit unitToMove)
  {
    if (IsCellOpen)
    {
      return;
    }

    OnCellOpen(unitToMove);
  }
}
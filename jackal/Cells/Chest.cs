using System;

namespace jackal;

[Serializable]
internal class Chest(Image img, Point address) : Cell(img, address)
{
  public int CoinsInChest;

  public override void OnCellOpen(Unit unitToMove)
  {
    base.OnCellOpen(unitToMove);
    CoinsOnCell = CoinsInChest;
    unitToMove.UnitAddress = CellAddress;
  }
}
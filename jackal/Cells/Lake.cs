using jackal.Properties;
using System;
using System.Linq;

namespace jackal;

[Serializable]
internal class Lake(Point address) : Cell(Resources.lake, address)
{
  public override void OnCellMove(Unit unitToMove)
  {
    var AddressSave = unitToMove.UnitAddress;
    base.OnCellMove(unitToMove);
    var cellAddress = CellAddress;
    var x1 = cellAddress.X * 2 - AddressSave.X;
    cellAddress = CellAddress;
    var y = cellAddress.Y * 2 - AddressSave.Y;
    AddressSave = new Point(x1, y);
    if (AddressSave.X < 0 || AddressSave.Y < 0 || AddressSave.X > 13 || AddressSave.Y > 13)
    {
      GameData.AllUnits.Remove(unitToMove);
    }
    else
    {
      GameData.AllCells.Single(x => x.CellAddress == AddressSave).OnCellMove(unitToMove);
    }
  }
}
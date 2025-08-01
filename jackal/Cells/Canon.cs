using jackal.Properties;
using System;
using System.Linq;

namespace jackal;

[Serializable]
internal class Canon : Cell
{
  private const int LastSeaCellIndex = 12;
  public int[] directionsToMove = new int[8];

  public Canon(int direction, Point address)
    : base(Resources.canon, address)
  {
    directionsToMove[direction] = 1;
    Spin = direction;
    for (var index = 0; index < direction; ++index)
    {
      CellImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
    }
  }

  public override void OnCellMove(Unit unitToMove)
  {
    if (!IsCellOpen)
    {
      OnCellOpen(unitToMove);
    }

    var target = new Point();
    if (directionsToMove[0] == 1)
    {
      target = new Point(CellAddress.X, 0);
    }
    else if (directionsToMove[1] == 1)
    {
      target = new Point(12, CellAddress.Y);
    }
    else if (directionsToMove[2] == 1)
    {
      target = new Point(CellAddress.X, LastSeaCellIndex);
    }
    else if (directionsToMove[3] == 1)
    {
      target = new Point(0, CellAddress.Y);
    }

    GameData.AllCells.Single(x => x.CellAddress == target).OnCellMove(unitToMove);
  }
}
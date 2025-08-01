using jackal.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jackal;

[Serializable]
internal class Horse(Point address) : Cell(Resources.horse, address)
{
  public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
  {
    var move = new List<Cell>();
    var checkMateHorse = new[,]
    {
      { 1, 2, },
      { 2, 1, },
      { 2, -1, },
      { 1, -2, },
      { -1, 2, },
      { -2, 1, },
      { -2, -1, },
      { -1, -2, },
    };
    for (var index = 0; index < 8; ++index)
    {
      var cellAddress = CellAddress;
      var x1 = cellAddress.X + checkMateHorse[index, 0];
      cellAddress = CellAddress;
      var y = cellAddress.Y + checkMateHorse[index, 1];
      var CyclePoint = new Point(x1, y);
      cellAddress = CellAddress;
      int num;
      if (cellAddress.X + checkMateHorse[index, 0] >= 0)
      {
        cellAddress = CellAddress;
        if (cellAddress.Y + checkMateHorse[index, 1] >= 0)
        {
          cellAddress = CellAddress;
          if (cellAddress.X + checkMateHorse[index, 0] < 13)
          {
            cellAddress = CellAddress;
            num = cellAddress.Y + checkMateHorse[index, 1] < 13 ? 1 : 0;
            goto label_6;
          }
        }
      }
      num = 0;
      label_6:
      if (num != 0 && GameData.AllCells.Single(x => x.CellAddress == CyclePoint) is not Sea)
      {
        move.Add(GameData.AllCells.Single(x => x.CellAddress == CyclePoint));
      }
    }
    if (unitToMove.CoinHold)
    {
      move.RemoveAll(x => !x.IsCellOpen);
    }

    return move;
  }
}
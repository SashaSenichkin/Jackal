using System;
using System.Collections.Generic;
using System.Linq;

namespace jackal;

[Serializable]
public class Ship(Image img, Point address) : Cell(img, address)
{
  private int _player;

  public int Player
  {
    get => _player;
    set => _player = value;
  }

  public override int CoinsOnCell
  {
    get => 0;
    set => GameData.GameScore[Player] += value;
  }

  public override void OnCellMove(Unit unitToMove)
  {
    base.OnCellMove(unitToMove);
    if (unitToMove.CoinHold)
    {
      unitToMove.CoinHold = false;
    }

    if (unitToMove.Player == Player)
    {
      return;
    }

    GameData.AllUnits.Remove(unitToMove);
  }

  public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
  {
    var move = base.CellsAbleToMove(unitToMove, false);
    move.RemoveAll(x =>
    {
      var cellAddress1 = x.CellAddress;
      var x1 = cellAddress1.X;
      cellAddress1 = x.CellAddress;
      var y1 = cellAddress1.Y;
      var num1 = x1 - y1;
      cellAddress1 = CellAddress;
      var x2 = cellAddress1.X;
      cellAddress1 = CellAddress;
      var y2 = cellAddress1.Y;
      var num2 = x2 - y2;
      if (num1 == num2)
      {
        return true;
      }

      var cellAddress2 = x.CellAddress;
      var x3 = cellAddress2.X;
      cellAddress2 = x.CellAddress;
      var y3 = cellAddress2.Y;
      var num3 = x3 + y3;
      cellAddress2 = CellAddress;
      var x4 = cellAddress2.X;
      cellAddress2 = CellAddress;
      var y4 = cellAddress2.Y;
      var num4 = x4 + y4;
      return num3 == num4;
    });
    for (var index = 0; index < 4; ++index)
    {
      var num = 11;
      var corner = new List<Point>()
      {
        new(index / 2 * num, index % 2 * num),
        new(index / 2 * num, 1 + index % 2 * num),
        new(1 + index / 2 * num, index % 2 * num),
        new(1 + index / 2 * num, 1 + index % 2 * num),
      };
      move.RemoveAll(x => corner.Contains(x.CellAddress));
    }
    return move;
  }

  public void MoveShipToCell(Cell Target)
  {
    var cellAddress = Target.CellAddress;
    GameData.AllUnits.RemoveAll(x => x.UnitAddress == Target.CellAddress && x.Player != Player);
    foreach (var unit in GameData.AllUnits.Where(x => x.Player == Player && x.UnitAddress == CellAddress))
    {
      unit.UnitAddress = cellAddress;
    }

    Target.SetNewAddress(CellAddress);
    CellAddress = cellAddress;
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace jackal;

[Serializable]
public class Cell
{
  private int _coinsOnCell;
  public int Spin;
  public bool IsCellOpen;
  public bool IsTarget;

  public Image CellImage { get; private set; }

  public Point CellAddress { get; protected set; }

  public virtual int CoinsOnCell
  {
    get => _coinsOnCell;
    set => _coinsOnCell = value;
  }

  public Cell(Image img, Point address)
  {
    for (var index = 0; index < Spin; ++index)
    {
      img.RotateFlip(RotateFlipType.Rotate90FlipNone);
    }

    CellImage = img;
    CellAddress = address;
  }

  public virtual void OnCellOpen(Unit unitToMove) => IsCellOpen = true;

  public virtual void OnCellMove(Unit unitToMove)
  {
    if (!IsCellOpen)
    {
      OnCellOpen(unitToMove);
    }

    if (unitToMove.CoinHold)
    {
      --GameData.AllCells.Single(x => x.CellAddress == unitToMove.UnitAddress).CoinsOnCell;
      ++CoinsOnCell;
    }
    unitToMove.UnitAddress = CellAddress;
    GameData.UnitAttack(unitToMove);
  }

  public virtual List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
  {
    var move = new List<Cell>();
    var x1 = CellAddress.X - 1;
    while (true)
    {
      var num1 = x1;
      var cellAddress = CellAddress;
      var num2 = cellAddress.X + 1;
      if (num1 <= num2)
      {
        if (x1 >= 0 && x1 < 13)
        {
          cellAddress = CellAddress;
          var y = cellAddress.Y - 1;
          while (true)
          {
            var num3 = y;
            cellAddress = CellAddress;
            var num4 = cellAddress.Y + 1;
            if (num3 <= num4)
            {
              if (y >= 0 && y < 13)
              {
                var cell = GameData.AllCells.Single(p => p.CellAddress == new Point(x1, y));
                if (cell is not Sea || !withoutSea)
                {
                  move.Add(cell);
                }
              }
              y++;
            }
            else
            {
              break;
            }
          }
        }
        x1++;
      }
      else
      {
        break;
      }
    }
    if (unitToMove.CoinHold)
    {
      move.RemoveAll(x => !x.IsCellOpen || GameData.AllUnits.Where(y => y.Player != unitToMove.Player).Select(z => z.UnitAddress).Contains(x.CellAddress));
    }

    return move;
  }

  public virtual XElement XmlSave() => new("Card", [
    new XAttribute("Address", CellAddress),
    new XElement("Spin", Spin),
    new XElement("CoinsOnCell", CoinsOnCell),
    new XElement("IsCellOpen", IsCellOpen),
    new XElement("IsTarget", IsTarget),
  ]);

  public void SetNewAddress(Point newAddress) => CellAddress = newAddress;
    
}
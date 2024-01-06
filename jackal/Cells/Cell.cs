// Decompiled with JetBrains decompiler
// Type: jackal.Cell
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;

namespace jackal
{
  [Serializable]
  public class Cell
  {
    private int _coinsOnCell;
    public int Spin;
    public bool IsCellOpen;
    public bool IsTarget;

    public Image CellImage { get; private set; }

    public Point CellAdress { get; protected set; }

    public virtual int CoinsOnCell
    {
      get => _coinsOnCell;
      set => _coinsOnCell = value;
    }

    public Cell(Image img, Point adress)
    {
      for (int index = 0; index < Spin; ++index)
        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
      CellImage = img;
      CellAdress = adress;
    }

    public virtual void OnCellOpen(Unit unitToMove) => IsCellOpen = true;

    public virtual void OnCellMove(Unit unitToMove)
    {
      if (!IsCellOpen)
        OnCellOpen(unitToMove);
      if (unitToMove.CoinHold)
      {
        --GameData.AllCells.Single(x => x.CellAdress == unitToMove.UnitAdress).CoinsOnCell;
        ++CoinsOnCell;
      }
      unitToMove.UnitAdress = CellAdress;
      GameData.UnitAttack(unitToMove);
    }

    public virtual List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
    {
      List<Cell> move = new List<Cell>();
      int x1 = CellAdress.X - 1;
      while (true)
      {
        int num1 = x1;
        Point cellAdress = CellAdress;
        int num2 = cellAdress.X + 1;
        if (num1 <= num2)
        {
          if (x1 >= 0 && x1 < 13)
          {
            cellAdress = CellAdress;
            int y = cellAdress.Y - 1;
            while (true)
            {
              int num3 = y;
              cellAdress = CellAdress;
              int num4 = cellAdress.Y + 1;
              if (num3 <= num4)
              {
                if (y >= 0 && y < 13)
                {
                  Cell cell = GameData.AllCells.Single(p => p.CellAdress == new Point(x1, y));
                  if (!(cell is Sea) || !withoutSea)
                    move.Add(cell);
                }
                y++;
              }
              else
                break;
            }
          }
          x1++;
        }
        else
          break;
      }
      if (unitToMove.CoinHold)
        move.RemoveAll(x => !x.IsCellOpen || GameData.AllUnits.Where(y => y.Player != unitToMove.Player).Select(z => z.UnitAdress).Contains(x.CellAdress));
      return move;
    }

    public virtual XElement XmlSave() => new XElement("Card", new object[5]
    {
      new XAttribute("Adress", CellAdress),
      new XElement("Spin", Spin),
      new XElement("CoinsOnCell", CoinsOnCell),
      new XElement("IsCellOpen", IsCellOpen),
      new XElement("IsTarget", IsTarget)
    });

    public void SetNewAdress(Point newAdress) => CellAdress = newAdress;
    
  }
}

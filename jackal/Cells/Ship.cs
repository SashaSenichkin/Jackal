// Decompiled with JetBrains decompiler
// Type: jackal.Ship
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace jackal
{
  [Serializable]
  public class Ship : Cell
  {
    private int _player;

    public Ship(Image img, Point adress)
      : base(img, adress)
    {
    }

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
        unitToMove.CoinHold = false;
      if (unitToMove.Player == Player)
        return;
      GameData.AllUnits.Remove(unitToMove);
    }

    public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = false)
    {
      List<Cell> move = base.CellsAbleToMove(unitToMove, false);
      move.RemoveAll(x =>
      {
        Point cellAdress1 = x.CellAdress;
        int x1 = cellAdress1.X;
        cellAdress1 = x.CellAdress;
        int y1 = cellAdress1.Y;
        int num1 = x1 - y1;
        cellAdress1 = CellAdress;
        int x2 = cellAdress1.X;
        cellAdress1 = CellAdress;
        int y2 = cellAdress1.Y;
        int num2 = x2 - y2;
        if (num1 == num2)
          return true;
        Point cellAdress2 = x.CellAdress;
        int x3 = cellAdress2.X;
        cellAdress2 = x.CellAdress;
        int y3 = cellAdress2.Y;
        int num3 = x3 + y3;
        cellAdress2 = CellAdress;
        int x4 = cellAdress2.X;
        cellAdress2 = CellAdress;
        int y4 = cellAdress2.Y;
        int num4 = x4 + y4;
        return num3 == num4;
      });
      for (int index = 0; index < 4; ++index)
      {
        int num = 11;
        List<Point> corner = new List<Point>()
        {
          new Point(index / 2 * num, index % 2 * num),
          new Point(index / 2 * num, 1 + index % 2 * num),
          new Point(1 + index / 2 * num, index % 2 * num),
          new Point(1 + index / 2 * num, 1 + index % 2 * num)
        };
        move.RemoveAll(x => corner.Contains(x.CellAdress));
      }
      return move;
    }

    public void MoveShipToCell(Cell Target)
    {
      Point cellAdress = Target.CellAdress;
      GameData.AllUnits.RemoveAll(x => x.UnitAdress == Target.CellAdress && x.Player != Player);
      foreach (Unit unit in GameData.AllUnits.Where(x => x.Player == Player && x.UnitAdress == CellAdress))
        unit.UnitAdress = cellAdress;
      Target.SetNewAdress(CellAdress);
      CellAdress = cellAdress;
    }
  }
}

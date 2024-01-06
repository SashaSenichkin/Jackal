// Decompiled with JetBrains decompiler
// Type: jackal.Horse
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using jackal.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace jackal
{
  [Serializable]
  internal class Horse : Cell
  {
    public Horse(Point adress)
      : base(Resources.horse, adress)
    {
    }

    public override List<Cell> CellsAbleToMove(Unit unitToMove, bool withoutSea = true)
    {
      List<Cell> move = new List<Cell>();
      int[,] numArray = new int[8, 2]
      {
        {
          1,
          2
        },
        {
          2,
          1
        },
        {
          2,
          -1
        },
        {
          1,
          -2
        },
        {
          -1,
          2
        },
        {
          -2,
          1
        },
        {
          -2,
          -1
        },
        {
          -1,
          -2
        }
      };
      for (int index = 0; index < 8; ++index)
      {
        Point cellAdress = CellAdress;
        int x1 = cellAdress.X + numArray[index, 0];
        cellAdress = CellAdress;
        int y = cellAdress.Y + numArray[index, 1];
        Point CyclePoint = new Point(x1, y);
        cellAdress = CellAdress;
        int num;
        if (cellAdress.X + numArray[index, 0] >= 0)
        {
          cellAdress = CellAdress;
          if (cellAdress.Y + numArray[index, 1] >= 0)
          {
            cellAdress = CellAdress;
            if (cellAdress.X + numArray[index, 0] < 13)
            {
              cellAdress = CellAdress;
              num = cellAdress.Y + numArray[index, 1] < 13 ? 1 : 0;
              goto label_6;
            }
          }
        }
        num = 0;
label_6:
        if (num != 0 && !(GameData.AllCells.Single(x => x.CellAdress == CyclePoint) is Sea))
          move.Add(GameData.AllCells.Single(x => x.CellAdress == CyclePoint));
      }
      if (unitToMove.CoinHold)
        move.RemoveAll(x => !x.IsCellOpen);
      return move;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: jackal.Ball
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using jackal.Properties;
using System;
using System.Drawing;
using System.Linq;

namespace jackal
{
  [Serializable]
  internal class Ball : Cell
  {
    public Ball(Point adress)
      : base(Resources.ball, adress)
    {
    }

    public override void OnCellMove(Unit unitToMove)
    {
      if (!IsCellOpen)
        OnCellOpen(unitToMove);
      GameData.GetAllShips().Single(x => x.Player == unitToMove.Player).OnCellMove(unitToMove);
    }
  }
}

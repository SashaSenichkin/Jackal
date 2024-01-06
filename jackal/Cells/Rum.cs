// Decompiled with JetBrains decompiler
// Type: jackal.Rum
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using jackal.Properties;
using System;
using System.Drawing;

namespace jackal
{
  [Serializable]
  internal class Rum : Cell
  {
    public int DelayTurn;

    public Rum(Point adress)
      : base(Resources.rum, adress)
    {
    }

    public override void OnCellMove(Unit unitToMove)
    {
      DelayTurn = GameData.TurnCount + GameData.Players;
      base.OnCellMove(unitToMove);
    }
  }
}

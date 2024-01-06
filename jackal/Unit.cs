// Decompiled with JetBrains decompiler
// Type: jackal.Unit
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using System;
using System.Drawing;

namespace jackal
{
  [Serializable]
  public class Unit
  {
    public Point UnitAdress;
    public int TurnDelay;
    public bool CoinHold;
    private int _player;

    public int Player
    {
      get => _player;
      set => _player = value;
    }
  }
}

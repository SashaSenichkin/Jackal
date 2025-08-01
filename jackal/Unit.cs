using System;

namespace jackal;

[Serializable]
public class Unit
{
  public Point UnitAddress;
  public int TurnDelay;
  public bool CoinHold;
  private int _player;

  public int Player
  {
    get => _player;
    set => _player = value;
  }
}
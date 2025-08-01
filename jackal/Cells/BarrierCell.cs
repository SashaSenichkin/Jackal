using System;
using System.Collections.Generic;

namespace jackal;

[Serializable]
public class BarrierCell
{
  public int CellNumber;
  public List<Unit> UnitsOnCell = [];
  public int CoinsOnCell = 0;
}
// Decompiled with JetBrains decompiler
// Type: jackal.BarrierCell
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using System;
using System.Collections.Generic;

namespace jackal
{
  [Serializable]
  public class BarrierCell
  {
    public int CellNumber;
    public List<Unit> UnitsOnCell = new List<Unit>();
    public int CoinsOnCell;
  }
}

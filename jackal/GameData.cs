// Decompiled with JetBrains decompiler
// Type: jackal.GameData
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using jackal.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace jackal
{
  [Serializable]
  public static class GameData
  {
    public const int GameFieldSize = 13;
    public static int Players = 4;
    public static int TurnCount;
    public static List<int> GameScore = new List<int>()
    {
      0,
      0,
      0,
      0
    };
    public static List<Cell> AllCells = new List<Cell>();
    public static List<Unit> AllUnits = new List<Unit>();
    [NonSerialized]
    private static Random rand = new Random();
    [NonSerialized]
    private static int randomNumber;
    [NonSerialized]
    private static int tempSpin = 0;
    [NonSerialized]
    private static List<Point> spareAdress = new List<Point>();

    public static void SetAllCells()
    {
      AllCells.Clear();
      for (int index = 0; index < 169; ++index)
        spareAdress.Add(new Point(index % 13, index / 13));
      AddSea();
      SetValueCells();
      AddShips();
      if (spareAdress.Count > 0)
        throw new Exception("miss some cells");
    }

    private static void SetValueCells()
    {
      AddCanibal(1);
      AddHealer(1);
      AddPlane(1);
      AddArrows(21);
      AddCanons(2);
      AddFields(40);
      AddCastles(2);
      AddRum(4);
      AddPits(3);
      AddHorses(2);
      AddBalls(2);
      AddCrocodiles(4);
      AddLakes(6);
      AddBarriers(12);
      AddChests(16);
    }

    private static void AddSea()
    {
      IEnumerable<Point> borderCells = spareAdress.Where(x =>
      {
        if (x.X == 0 || x.X == 12 || x.Y == 0 || x.Y == 12 || x.X == 1 && x.Y == 1 || x.X == 11 && x.Y == 1 || x.X == 1 && x.Y == 11)
          return true;
        return x.X == 11 && x.Y == 11;
      });
      foreach (Point adress in borderCells)
      {
        List<Cell> allCells = AllCells;
        Sea sea = new Sea(adress);
        sea.IsCellOpen = true;
        sea.Spin = randomNumber % 4;
        allCells.Add(sea);
      }
      spareAdress.RemoveAll(x => borderCells.Contains(x));
    }

    private static void AddShips()
    {
      Dictionary<Point, Image> AdressAndImage = new Dictionary<Point, Image>()
      {
        {
          new Point(0, 6),
          Resources.shipY
        },
        {
          new Point(6, 0),
          Resources.shipW
        },
        {
          new Point(12, 6),
          Resources.shipR
        },
        {
          new Point(6, 12),
          Resources.shipB
        }
      };
      for (int i = 0; i < Players; i++)
      {
        AllCells.RemoveAll(x => x.CellAdress == AdressAndImage.Keys.ToList()[i]);
        List<Cell> allCells = AllCells;
        Ship ship = new Ship(AdressAndImage.Values.ToList()[i], AdressAndImage.Keys.ToList()[i]);
        ship.Player = i;
        ship.IsCellOpen = true;
        allCells.Add(ship);
      }
    }

    private static void AddPlane(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        AllCells.Add(new Plane(spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddCanibal(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        AllCells.Add(new Cannibal(spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddHealer(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        AllCells.Add(new Healer(spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddCastles(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        AllCells.Add(new Castle(spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddRum(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        AllCells.Add(new Rum(spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddHorses(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        AllCells.Add(new Horse(spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddBalls(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        AllCells.Add(new Ball(spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddCrocodiles(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        AllCells.Add(new Crocodile(spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddLakes(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        AllCells.Add(new Lake(spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddBarriers(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        Image img = Resources.field;
        randomNumber = rand.Next() % spareAdress.Count;
        int turns = 0;
        if (index < 5)
        {
          turns = 1;
          img = Resources.barr2;
        }
        else if (index > 4 && index < 9)
        {
          turns = 2;
          img = Resources.barr3;
        }
        else if (index > 8 && index < 11)
        {
          turns = 3;
          img = Resources.barr4;
        }
        else if (index >= 11)
        {
          turns = 4;
          img = Resources.barr5;
        }
        AllCells.Add(new Barrier(turns, img, spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddChests(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        Image img = Resources.chest1;
        randomNumber = rand.Next() % spareAdress.Count;
        int num = 0;
        if (index < 5)
          num = 1;
        else if (index > 4 && index < 10)
        {
          num = 2;
          img = Resources.chest2;
        }
        else if (index > 9 && index < 13)
        {
          num = 3;
          img = Resources.chest3;
        }
        else if (index > 12 && index < 15)
        {
          num = 4;
          img = Resources.chest4;
        }
        else if (index >= 15)
        {
          num = 5;
          img = Resources.chest5;
        }
        AllCells.Add(new Chest(img, spareAdress[randomNumber])
        {
          CoinsInChest = num
        });
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddPits(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        AllCells.Add(new Pit(spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddFields(int numberOfCells)
    {
      for (int index1 = 0; index1 < numberOfCells; ++index1)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        Image field = Resources.field;
        for (int index2 = 0; index2 < randomNumber % 4; ++index2)
          field.RotateFlip(RotateFlipType.Rotate90FlipNone);
        AllCells.Add(new Cell(field, spareAdress[randomNumber])
        {
          Spin = randomNumber % 4
        });
        spareAdress.RemoveAt(randomNumber);
      }
    }

    private static void AddCanons(int numberOfCells)
    {
      for (int index = 0; index < numberOfCells; ++index)
      {
        randomNumber = rand.Next() % spareAdress.Count;
        int num = rand.Next();
        AllCells.Add(new Canon(num % 4, spareAdress[randomNumber]));
        spareAdress.RemoveAt(randomNumber);
        Thread.Sleep(randomNumber % 30);
      }
    }

    private static void AddArrows(int numberOfCells)
    {
      for (int index1 = 0; index1 < numberOfCells; ++index1)
      {
        Image img = Resources.field;
        int[] numArray = new int[8];
        if (index1 < 3)
        {
          numArray = new int[8]{ 1, 1, 1, 1, 0, 0, 0, 0 };
          img = Resources.arrow4;
        }
        else if (index1 > 2 && index1 < 6)
        {
          numArray = new int[8]{ 0, 0, 0, 0, 1, 1, 1, 1 };
          img = Resources.arrow7;
        }
        else if (index1 > 5 && index1 < 9)
        {
          img = Resources.arrow1;
          randomNumber = rand.Next();
          tempSpin = randomNumber % 2;
          numArray[randomNumber % 2] = 1;
          numArray[randomNumber % 2 + 2] = 1;
        }
        else if (index1 > 8 && index1 < 12)
        {
          img = Resources.arrow3;
          randomNumber = rand.Next();
          tempSpin = randomNumber % 2;
          numArray[randomNumber % 2 + 4] = 1;
          numArray[randomNumber % 2 + 6] = 1;
        }
        else if (index1 > 11 && index1 < 15)
        {
          img = Resources.arrow6;
          randomNumber = rand.Next();
          tempSpin = randomNumber % 4;
          numArray[randomNumber % 4 + 4] = 1;
        }
        else if (index1 > 14 && index1 < 18)
        {
          img = Resources.arrow2;
          randomNumber = rand.Next();
          tempSpin = randomNumber % 4;
          numArray[randomNumber % 4] = 1;
        }
        else if (index1 > 17)
        {
          img = Resources.arrow5;
          randomNumber = rand.Next();
          numArray = new List<int[]>()
          {
            new int[8]{ 0, 1, 1, 0, 1, 0, 0, 0 },
            new int[8]{ 0, 0, 1, 1, 0, 1, 0, 0 },
            new int[8]{ 1, 0, 0, 1, 0, 0, 1, 0 },
            new int[8]{ 1, 1, 0, 0, 0, 0, 0, 1 }
          }[randomNumber % 4];
          tempSpin = randomNumber % 4;
        }
        randomNumber = rand.Next() % spareAdress.Count;
        for (int index2 = 0; index2 < tempSpin; ++index2)
          img.RotateFlip(RotateFlipType.Rotate90FlipNone);
        List<Cell> allCells = AllCells;
        Arrow arrow = new Arrow(img, spareAdress[randomNumber]);
        arrow.directionsToMove = numArray;
        arrow.Spin = tempSpin;
        allCells.Add(arrow);
        spareAdress.RemoveAt(randomNumber);
        Thread.Sleep(randomNumber % 20);
      }
    }

    public static void SetAllUnits()
    {
      AllUnits.Clear();
      for (int i = 0; i < Players; i++)
      {
        for (int index = 0; index < 3; ++index)
          AllUnits.Add(new Unit()
          {
            Player = i,
            UnitAdress = GetAllShips().Single(x => x.Player == i).CellAdress
          });
      }
    }

    public static List<Ship> GetAllShips()
    {
      List<Ship> allShips = new List<Ship>();
      foreach (Cell cell in AllCells.Where(x => x is Ship))
        allShips.Add((Ship) cell);
      return allShips;
    }

    public static void ReturnToShip(Unit unit) => unit.UnitAdress = GetAllShips().Single(x => x.Player == unit.Player).CellAdress;

    public static void UnitAttack(Unit agressor)
    {
      foreach (Unit unit in AllUnits.Where(x => x.UnitAdress == agressor.UnitAdress && x.Player != agressor.Player))
      {
        unit.CoinHold = false;
        ReturnToShip(unit);
      }
    }

    public static void Save()
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      using (Stream serializationStream = new FileStream("sells.dat", FileMode.Create, FileAccess.Write, FileShare.None))
        binaryFormatter.Serialize(serializationStream, AllCells);
      using (Stream serializationStream = new FileStream("units.dat", FileMode.Create, FileAccess.Write, FileShare.None))
        binaryFormatter.Serialize(serializationStream, AllUnits);
      object[] graph = new object[3]
      {
        TurnCount,
        Players,
        GameScore
      };
      using (Stream serializationStream = new FileStream("data.dat", FileMode.Create, FileAccess.Write, FileShare.None))
        binaryFormatter.Serialize(serializationStream, graph);
    }

    public static void Load()
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      using (Stream serializationStream = new FileStream("sells.dat", FileMode.Open))
        AllCells = (List<Cell>) binaryFormatter.Deserialize(serializationStream);
      using (Stream serializationStream = new FileStream("units.dat", FileMode.Open))
        AllUnits = (List<Unit>) binaryFormatter.Deserialize(serializationStream);
      using (Stream serializationStream = new FileStream("data.dat", FileMode.Open))
      {
        object[] objArray = (object[]) binaryFormatter.Deserialize(serializationStream);
        TurnCount = (int) objArray[0];
        Players = (int) objArray[1];
        GameScore = (List<int>) objArray[2];
      }
    }
  }
}

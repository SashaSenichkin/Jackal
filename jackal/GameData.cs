using jackal.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace jackal;

[Serializable]
public static class GameData
{
  public const int GameFieldSize = 13;
  public static int Players = 4;
  public static int TurnCount;
  public static List<int> GameScore =
  [
    0,
    0,
    0,
    0,
  ];
  public static List<Cell> AllCells = [];
  public static List<Unit> AllUnits = [];
  [NonSerialized]
  private static Random rand = new();
  [NonSerialized]
  private static int randomNumber;
  [NonSerialized]
  private static int tempSpin;
  [NonSerialized]
  private static List<Point> spareAddress = [];

  public static void SetAllCells()
  {
    AllCells.Clear();
    for (var index = 0; index < 169; ++index)
    {
      spareAddress.Add(new Point(index % GameFieldSize, index / GameFieldSize));
    }

    AddSea();
    SetValueCells();
    AddShips();
    if (spareAddress.Count > 0)
    {
      throw new Exception("miss some cells");
    }
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
    var borderCells = spareAddress.Where(x =>
    {
      if (x.X == 0 || x.X == 12 || x.Y == 0 || x.Y == 12 || x.X == 1 && x.Y == 1 || x.X == 11 && x.Y == 1 || x.X == 1 && x.Y == 11)
      {
        return true;
      }

      return x.X == 11 && x.Y == 11;
    }).ToList();
    
    foreach (var Address in borderCells)
    {
      var allCells = AllCells;
      var sea = new Sea(Address);
      sea.IsCellOpen = true;
      sea.Spin = randomNumber % 4;
      allCells.Add(sea);
    }
    spareAddress.RemoveAll(x => borderCells.Contains(x));
  }

  private static void AddShips()
  {
    var AddressAndImage = new Dictionary<Point, Image>()
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
      },
    };
    for (var i = 0; i < Players; i++)
    {
      AllCells.RemoveAll(x => x.CellAddress == AddressAndImage.Keys.ToList()[i]);
      var allCells = AllCells;
      var ship = new Ship(AddressAndImage.Values.ToList()[i], AddressAndImage.Keys.ToList()[i]);
      ship.Player = i;
      ship.IsCellOpen = true;
      allCells.Add(ship);
    }
  }

  private static void AddPlane(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      AllCells.Add(new Plane(spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddCanibal(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      AllCells.Add(new Cannibal(spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddHealer(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      AllCells.Add(new Healer(spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddCastles(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      AllCells.Add(new Castle(spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddRum(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      AllCells.Add(new Rum(spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddHorses(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      AllCells.Add(new Horse(spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddBalls(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      AllCells.Add(new Ball(spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddCrocodiles(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      AllCells.Add(new Crocodile(spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddLakes(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      AllCells.Add(new Lake(spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddBarriers(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      Image img = Resources.field;
      randomNumber = rand.Next() % spareAddress.Count;
      var turns = 0;
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
      AllCells.Add(new Barrier(turns, img, spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddChests(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      Image img = Resources.chest1;
      randomNumber = rand.Next() % spareAddress.Count;
      var num = 0;
      if (index < 5)
      {
        num = 1;
      }
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
      AllCells.Add(new Chest(img, spareAddress[randomNumber])
      {
        CoinsInChest = num,
      });
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddPits(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      AllCells.Add(new Pit(spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddFields(int numberOfCells)
  {
    for (var index1 = 0; index1 < numberOfCells; ++index1)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      Image field = Resources.field;
      for (var index2 = 0; index2 < randomNumber % 4; ++index2)
      {
        field.RotateFlip(RotateFlipType.Rotate90FlipNone);
      }

      AllCells.Add(new Cell(field, spareAddress[randomNumber])
      {
        Spin = randomNumber % 4,
      });
      spareAddress.RemoveAt(randomNumber);
    }
  }

  private static void AddCanons(int numberOfCells)
  {
    for (var index = 0; index < numberOfCells; ++index)
    {
      randomNumber = rand.Next() % spareAddress.Count;
      var num = rand.Next();
      AllCells.Add(new Canon(num % 4, spareAddress[randomNumber]));
      spareAddress.RemoveAt(randomNumber);
      Thread.Sleep(randomNumber % 30);
    }
  }

  private static void AddArrows(int numberOfCells)
  {
    for (var index1 = 0; index1 < numberOfCells; ++index1)
    {
      Image img = Resources.field;
      var numArray = new int[8];
      if (index1 < 3)
      {
        numArray = [1, 1, 1, 1, 0, 0, 0, 0];
        img = Resources.arrow4;
      }
      else if (index1 > 2 && index1 < 6)
      {
        numArray = [0, 0, 0, 0, 1, 1, 1, 1];
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
          new []{ 0, 1, 1, 0, 1, 0, 0, 0 },
          new []{ 0, 0, 1, 1, 0, 1, 0, 0 },
          new []{ 1, 0, 0, 1, 0, 0, 1, 0 },
          new []{ 1, 1, 0, 0, 0, 0, 0, 1 },
        }[randomNumber % 4];
        tempSpin = randomNumber % 4;
      }
      randomNumber = rand.Next() % spareAddress.Count;
      for (var index2 = 0; index2 < tempSpin; ++index2)
      {
        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
      }

      var allCells = AllCells;
      var arrow = new Arrow(img, spareAddress[randomNumber])
      {
        directionsToMove = numArray,
        Spin = tempSpin,
      };
      
      allCells.Add(arrow);
      spareAddress.RemoveAt(randomNumber);
      Thread.Sleep(randomNumber % 20);
    }
  }

  public static void SetAllUnits()
  {
    AllUnits.Clear();
    for (var i = 0; i < Players; i++)
    {
      for (var index = 0; index < 3; ++index)
      {
        AllUnits.Add(new Unit()
        {
          Player = i,
          UnitAddress = GetAllShips().Single(x => x.Player == i).CellAddress,
        });
      }
    }
  }

  public static List<Ship> GetAllShips() => AllCells.Where(x => x is Ship).Cast<Ship>().ToList();

  public static void ReturnToShip(Unit unit) => unit.UnitAddress = GetAllShips().Single(x => x.Player == unit.Player).CellAddress;

  public static void UnitAttack(Unit agressor)
  {
    foreach (var unit in AllUnits.Where(x => x.UnitAddress == agressor.UnitAddress && x.Player != agressor.Player))
    {
      unit.CoinHold = false;
      ReturnToShip(unit);
    }
  }

  public static void Save()
  {
    var binaryFormatter = new BinaryFormatter();
    using (Stream serializationStream = new FileStream("sells.dat", FileMode.Create, FileAccess.Write, FileShare.None))
      binaryFormatter.Serialize(serializationStream, AllCells);
    using (Stream serializationStream = new FileStream("units.dat", FileMode.Create, FileAccess.Write, FileShare.None))
      binaryFormatter.Serialize(serializationStream, AllUnits);
    var graph = new object[]
    {
      TurnCount,
      Players,
      GameScore,
    };
    using (Stream serializationStream = new FileStream("data.dat", FileMode.Create, FileAccess.Write, FileShare.None))
      binaryFormatter.Serialize(serializationStream, graph);
  }

  public static void Load()
  {
    var binaryFormatter = new BinaryFormatter();
    using (Stream serializationStream = new FileStream("sells.dat", FileMode.Open))
      AllCells = (List<Cell>) binaryFormatter.Deserialize(serializationStream);
    using (Stream serializationStream = new FileStream("units.dat", FileMode.Open))
      AllUnits = (List<Unit>) binaryFormatter.Deserialize(serializationStream);
    using (Stream serializationStream = new FileStream("data.dat", FileMode.Open))
    {
      var objArray = (object[]) binaryFormatter.Deserialize(serializationStream);
      TurnCount = (int) objArray[0];
      Players = (int) objArray[1];
      GameScore = (List<int>) objArray[2];
    }
  }
}
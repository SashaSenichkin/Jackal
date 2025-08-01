using jackal.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace jackal;

public class GameField : UserControl
{
  private Unit unitSave;
  public bool DrawAll = true;
  public string[] TeamNames =
  [
    "Белые",
    "Зеленые",
    "Красные",
    "Черные",
  ];
  public SolidBrush[] TeamColors =
  [
    (SolidBrush) Brushes.White,
    (SolidBrush) Brushes.Green,
    (SolidBrush) Brushes.Red,
    (SolidBrush) Brushes.Black,
  ];
  public SolidBrush[] TeamPens =
  [
    (SolidBrush) Brushes.DarkBlue,
    (SolidBrush) Brushes.DarkBlue,
    (SolidBrush) Brushes.Snow,
    (SolidBrush) Brushes.Snow,
  ];
  private List<Rectangle> UnitsAreas = [];
  private List<Point> CoinsAddresses = [];
  private List<Paint.Paint.MyEllipse> UnitsPaint = [];
  private List<Paint.Paint.MyEllipse> SubUnitCoinPaint = [];
  private List<Paint.Paint.MyEllipse> CoinPaint = [];
  public int scale = 55;
  private IContainer components = null;
  private Button button1;
  private Label label1;
  private Label label2;

  public GameField()
  {
    DoubleBuffered = true;
    InitializeComponent();
  }

  private Rectangle GetCoinArea(Point Address) => new(Address.X * scale + 50, Address.Y * scale + 80, (int) (scale * 0.25), (int) (scale * 0.25));

  private Rectangle GetCellArea(Point Address) => new(Address.X * scale + 10, Address.Y * scale + 40, scale, scale);

  private Rectangle GetUnitBarrierArea(Unit unit)
  {
    var num = GameData.AllUnits.Where(x => x.UnitAddress == unit.UnitAddress).ToList().IndexOf(unit);
    return new Rectangle(unit.UnitAddress.X * scale + 12 + num % 3 * ((int) (scale * 0.25) + 5), unit.UnitAddress.Y * scale + 45 + 20 * (num / 3), (int) (scale * 0.25), (int) (scale * 0.25));
  }

  private void CreateUnits()
  {
    UnitsAreas.Clear();
    UnitsPaint.Clear();
    SubUnitCoinPaint.Clear();
    foreach (var allUnit in GameData.AllUnits)
    {
      var unit = allUnit;
      Rectangle rectangle;
      var str = string.Empty;
      if (GameData.AllCells.Count(x => x.CellAddress == unit.UnitAddress && x is Barrier) == 1)
      {
        rectangle = GetUnitBarrierArea(unit);
        str = (((Barrier) GameData.AllCells.Single(x => x.CellAddress == unit.UnitAddress)).SubCells.FindIndex(x => x.UnitsOnCell.Contains(unit)) + 1).ToString();
      }
      else
      {
        rectangle = GetUnitBarrierArea(unit);
      }

      UnitsAreas.Add(rectangle);
      if (unit.CoinHold)
      {
        var subUnitCoinPaint = SubUnitCoinPaint;
        var myEllipse = new Paint.Paint.MyEllipse();
        myEllipse.Bounds = new Rectangle(rectangle.X, rectangle.Y + scale / 8, rectangle.Width, rectangle.Height);
        myEllipse.Text = string.Empty;
        myEllipse.BackBrush = Brushes.Goldenrod;
        myEllipse.BorderPen = Pens.Black;
        myEllipse.TextBrush = TeamPens[unit.Player];
        subUnitCoinPaint.Add(myEllipse);
      }
      var unitsPaint = UnitsPaint;
      var myEllipse1 = new Paint.Paint.MyEllipse();
      myEllipse1.Bounds = rectangle;
      myEllipse1.Text = str;
      myEllipse1.BackBrush = TeamColors[unit.Player];
      myEllipse1.BorderPen = Pens.Black;
      myEllipse1.TextBrush = TeamPens[unit.Player];
      unitsPaint.Add(myEllipse1);
    }
  }

  private void CreateCoins()
  {
    CoinPaint.Clear();
    CoinsAddresses.Clear();
    foreach (var allCell in GameData.AllCells)
    {
      if (allCell.CoinsOnCell != 0)
      {
        CoinsAddresses.Add(allCell.CellAddress);
        var coinPaint = CoinPaint;
        var myEllipse = new Paint.Paint.MyEllipse();
        myEllipse.Bounds = GetCoinArea(allCell.CellAddress);
        myEllipse.Text = allCell.CoinsOnCell.ToString();
        myEllipse.BackBrush = Brushes.Goldenrod;
        myEllipse.BorderPen = Pens.Black;
        myEllipse.TextBrush = Brushes.White;
        coinPaint.Add(myEllipse);
      }
    }
  }

  private void Field_Paint(object sender, PaintEventArgs e)
  {
    if (DrawAll)
    {
      foreach (var cell in GameData.AllCells.Where(x => x.IsCellOpen))
      {
        e.Graphics.DrawImage(cell.CellImage, GetCellArea(cell.CellAddress));
      }

      foreach (var cell in GameData.AllCells.Where(x => !x.IsCellOpen))
      {
        e.Graphics.DrawImage(Resources.fog, GetCellArea(cell.CellAddress));
      }
    }
    else
    {
      foreach (var allCell in GameData.AllCells)
      {
        e.Graphics.DrawImage(allCell.CellImage, GetCellArea(allCell.CellAddress));
      }
    }
    foreach (var figure in SubUnitCoinPaint)
    {
      figure.DrawFigure(e.Graphics);
    }

    foreach (var figure in UnitsPaint)
    {
      figure.DrawFigure(e.Graphics);
    }

    foreach (var figure in CoinPaint)
    {
      figure.DrawFigure(e.Graphics);
    }

    foreach (var cell in GameData.AllCells.Where(x => x.IsTarget))
    {
      e.Graphics.DrawRectangle(Pens.Blue, GetCellArea(cell.CellAddress));
    }
  }

  public void RePaint()
  {
    CreateUnits();
    CreateCoins();
    Invalidate();
    button1.Visible = false;
    label2.Visible = false;
    if (unitSave != null)
    {
      button1.Visible = true;
      if (unitSave.CoinHold)
      {
        label2.Visible = true;
      }
    }
    label1.Text = GetLabelText();
  }

  private string GetLabelText()
  {
    var str1 = string.Empty;
    switch (GameData.TurnCount % GameData.Players)
    {
      case 0:
        str1 = TeamNames[0] + ": " + GameData.GameScore[0] + " монет. " + TeamNames[1] + ": " + GameData.GameScore[1] + " монет. ";
        if (GameData.Players > 2)
        {
          str1 = str1 + TeamNames[2] + ": " + GameData.GameScore[2] + " монет. ";
        }

        if (GameData.Players > 3)
        {
          str1 = str1 + TeamNames[3] + ": " + GameData.GameScore[3] + " монет. ";
        }
        break;
      case 1:
        var str2 = TeamNames[1] + ": " + GameData.GameScore[1] + " монет. ";
        if (GameData.Players > 2)
        {
          str2 = str2 + TeamNames[2] + ": " + GameData.GameScore[2] + " монет. ";
        }

        if (GameData.Players > 3)
        {
          str2 = str2 + TeamNames[3] + ": " + GameData.GameScore[3] + " монет. ";
        }

        str1 = str2 + TeamNames[0] + ": " + GameData.GameScore[0] + " монет. ";
        break;
      case 2:
        var str3 = TeamNames[2] + ": " + GameData.GameScore[2] + " монет. ";
        if (GameData.Players > 3)
        {
          str3 = str3 + TeamNames[3] + ": " + GameData.GameScore[3] + " монет. ";
        }

        str1 = str3 + TeamNames[0] + ": " + GameData.GameScore[0] + " монет. " + TeamNames[1] + ": " + GameData.GameScore[1] + " монет. ";
        break;
      case 3:
        str1 = TeamNames[3] + ": " + GameData.GameScore[3] + " монет. " + TeamNames[0] + ": " + GameData.GameScore[0] + " монет. " + TeamNames[1] + ": " + GameData.GameScore[1] + " монет. " + TeamNames[2] + ": " + GameData.GameScore[2] + " монет. ";
        break;
    }
    return str1 + "  " + GameData.TurnCount + " Ход";
  }

  protected override void OnMouseDown(MouseEventArgs e)
  {
    var isClickOnUnit = false;
    UnitChoose(ref isClickOnUnit);
    if (unitSave != null)
    {
      coinChoose();
      if (GameData.AllCells.Single(x => x.CellAddress == unitSave.UnitAddress) is Barrier)
      {
        BarrierEvent();
      }
      else
      {
        HightLightCells();
      }

      if (!isClickOnUnit)
      {
        UnitMove();
      }
    }
    RePaint();
    WinCheck();
  }

  private void WinCheck()
  {
    if (GameData.AllCells.Count(x => x is Chest && !x.IsCellOpen) != 0 || CoinsAddresses.Count != 0)
    {
      return;
    }

    MessageBox.Show("Победила команда, под названием: " + TeamNames[GameData.GameScore.FindIndex(x => x == GameData.GameScore.Max())]);
  }

  private void coinChoose()
  {
    foreach (var coinsAddress in CoinsAddresses)
    {
      var point = coinsAddress;
      if (GetCoinArea(point).Contains(PointToClient(MousePosition)) && point == unitSave.UnitAddress)
      {
        if (unitSave.CoinHold)
        {
          unitSave.CoinHold = false;
        }
        else if (GameData.AllUnits.Count(x => x.UnitAddress == point && x.CoinHold) < GameData.AllCells.Single(x => x.CellAddress == point).CoinsOnCell)
        {
          unitSave.CoinHold = true;
        }
      }
    }
  }

  private void UnitMove()
  {
    if (unitSave == null)
    {
      return;
    }
    
    foreach (var cellToMove in GameData.AllCells.Where(x => x.IsTarget))
    {
      var shipMove = new Point();
      if (GetCellArea(cellToMove.CellAddress).Contains(PointToClient(MousePosition)))
      {
        if (GameData.AllCells.Single(x => x.CellAddress == unitSave.UnitAddress) is Plane)
        {
          var plane = (Plane) GameData.AllCells.Single(x => x.CellAddress == unitSave.UnitAddress);
          plane.FromCellMove(unitSave, cellToMove);
          if (unitSave == plane.PlaneOwner)
          {
            plane.PlaneOwner = null;
          }
        }
        if (GameData.AllCells.Single(x => x.CellAddress == unitSave.UnitAddress) is Ship && cellToMove is Sea)
        {
          shipMove = cellToMove.CellAddress;
        }
        else if (unitSave.UnitAddress == cellToMove.CellAddress && cellToMove is Healer && GameData.AllUnits.Count(x => x.Player == unitSave.Player) < 3)
        {
          GameData.AllUnits.Add(new Unit()
          {
            Player = unitSave.Player,
            UnitAddress = unitSave.UnitAddress,
          });
        }
        else
        {
          cellToMove.OnCellMove(unitSave);
        }

        if (!shipMove.IsEmpty)
        {
          GameData.GetAllShips().Single(x => x.Player == unitSave.Player).MoveShipToCell(GameData.AllCells.Single(x => x.CellAddress == shipMove));
        }

        ++GameData.TurnCount;
        unitSave = null;
        foreach (var cell in GameData.AllCells.Where(x => x.IsTarget = true))
        {
          cell.IsTarget = false;
        }
      }
    }
  }

  private void BarrierEvent()
  {
    var barrier = (Barrier) GameData.AllCells.Single(x => x.CellAddress == unitSave.UnitAddress);
    if (barrier.SubCells.Count(x => x.UnitsOnCell.Contains(unitSave)) != 1)
    {
      return;
    }

    if (barrier.SubCells.Single(x => x.UnitsOnCell.Contains(unitSave)).CellNumber + 1 < barrier.SubCells.Count)
    {
      barrier.OnSubCellMove(unitSave);
      unitSave = null;
      ++GameData.TurnCount;
    }
    else
    {
      barrier.SubCells.Single(x => x.UnitsOnCell.Contains(unitSave)).UnitsOnCell.Remove(unitSave);
      HightLightCells();
    }
  }

  private void UnitChoose(ref bool isClickOnUnit)
  {
    for (var i = 0; i < UnitsAreas.Count; i++)
    {
      if (UnitsAreas[i].Contains(PointToClient(MousePosition)) && GameData.AllUnits[i].Player == GameData.TurnCount % GameData.Players)
      {
        if (GameData.AllCells.Single(x => x.CellAddress == GameData.AllUnits[i].UnitAddress) is Rum && ((Rum) GameData.AllCells.Single(x => x.CellAddress == GameData.AllUnits[i].UnitAddress)).DelayTurn == GameData.TurnCount)
        {
          break;
        }

        unitSave = GameData.AllUnits[i];
        isClickOnUnit = true;
      }
    }
  }

  private void HightLightCells()
  {
    foreach (var cell in GameData.AllCells.Where(x => x.IsTarget = true))
    {
      cell.IsTarget = false;
    }

    foreach (var cell1 in GameData.AllCells.Single(x => x.CellAddress == unitSave.UnitAddress).CellsAbleToMove(unitSave))
    {
      var cell = cell1;
      if (!(cell is Ship) || ((Ship) cell).Player == unitSave.Player)
      {
        cell.IsTarget = true;
      }

      if (cell.CellAddress == unitSave.UnitAddress)
      {
        cell.IsTarget = cell is Healer;
      }

      if ((cell is Healer || cell is Castle) && (unitSave.CoinHold || GameData.AllUnits.Count(x => x.UnitAddress == cell.CellAddress && x.Player != unitSave.Player) > 0))
      {
        cell.IsTarget = false;
      }
    }
  }

  private void button1_Click(object sender, EventArgs e)
  {
    GameData.AllUnits.Remove(unitSave);
    RePaint();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && components != null)
    {
      components.Dispose();
    }

    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    button1 = new Button();
    label1 = new Label();
    label2 = new Label();
    SuspendLayout();
    button1.Location = new Point(4, 4);
    button1.Name = "button1";
    button1.Size = new Size(75, 23);
    button1.TabIndex = 0;
    button1.Text = "kill me!";
    button1.UseVisualStyleBackColor = true;
    button1.Click += button1_Click;
    label1.AutoSize = true;
    label1.Location = new Point(198, 10);
    label1.Name = "label1";
    label1.Size = new Size(35, 13);
    label1.TabIndex = 1;
    label1.Text = "Score";
    label2.AutoSize = true;
    label2.Location = new Point(98, 10);
    label2.Name = "label2";
    label2.Size = new Size(91, 13);
    label2.TabIndex = 2;
    label2.Text = "Монетка у меня!";
    AutoScaleDimensions = new SizeF(6f, 13f);
    AutoScaleMode = AutoScaleMode.Font;
    Controls.Add(label2);
    Controls.Add(label1);
    Controls.Add(button1);
    Name = nameof (GameField);
    Size = new Size(300, 150);
    Paint += Field_Paint;
    ResumeLayout(false);
    PerformLayout();
  }
}
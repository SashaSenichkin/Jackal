// Decompiled with JetBrains decompiler
// Type: jackal.Settings
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace jackal
{
  public class Settings : Form
  {
    public SolidBrush[] TempColors = new SolidBrush[4];
    public SolidBrush[] TempPens = new SolidBrush[4];
    public string[] TempNames = new string[4];
    public bool Restart = false;
    private List<Rectangle> ColorRects = new List<Rectangle>();
    private List<Rectangle> PensRects = new List<Rectangle>();
    private List<Paint.MyRectangle> PensObj = new List<Paint.MyRectangle>();
    private List<Paint.MyRectangle> ColorObj = new List<Paint.MyRectangle>();
    private IContainer components = null;
    private Label label1;
    private NumericUpDown numericUpDown1;
    private TextBox textBox1;
    private TextBox textBox2;
    private TextBox textBox3;
    private TextBox textBox4;
    private Button button1;
    private Label label2;
    private Label label3;
    private Label label4;

    public Settings() => InitializeComponent();

    private void CreateColors()
    {
      ColorObj.Clear();
      ColorRects.Clear();
      for (int index = 0; index < 4; ++index)
      {
        Rectangle rectangle = new Rectangle(215, 80 + 40 * index, 40, 20);
        ColorRects.Add(rectangle);
        List<Paint.MyRectangle> colorObj = ColorObj;
        Paint.MyRectangle myRectangle = new Paint.MyRectangle();
        myRectangle.Bounds = rectangle;
        myRectangle.Text = "";
        myRectangle.BackBrush = TempColors[index];
        myRectangle.BorderPen = Pens.Black;
        myRectangle.TextBrush = Brushes.White;
        colorObj.Add(myRectangle);
      }
    }

    private void CreatePens()
    {
      PensObj.Clear();
      PensRects.Clear();
      for (int index = 0; index < 4; ++index)
      {
        Rectangle rectangle = new Rectangle(285, 80 + 40 * index, 40, 20);
        PensRects.Add(rectangle);
        List<Paint.MyRectangle> pensObj = PensObj;
        Paint.MyRectangle myRectangle = new Paint.MyRectangle();
        myRectangle.Bounds = rectangle;
        myRectangle.Text = "";
        myRectangle.BackBrush = TempPens[index];
        myRectangle.BorderPen = Pens.Black;
        myRectangle.TextBrush = Brushes.White;
        pensObj.Add(myRectangle);
      }
    }

    private void Dial_Paint(object sender, PaintEventArgs e)
    {
      foreach (Paint.Painter.Figure figure in ColorObj)
        figure.DrawFigure(e.Graphics);
      foreach (Paint.Painter.Figure figure in PensObj)
        figure.DrawFigure(e.Graphics);
    }

    private void colorChangeDialog(SolidBrush objToChange)
    {
      ColorDialog colorDialog = new ColorDialog();
      colorDialog.AllowFullOpen = false;
      colorDialog.Color = objToChange.Color;
      if (colorDialog.ShowDialog() != DialogResult.OK)
        return;
      objToChange.Color = colorDialog.Color;
    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
      GameData.Players = (int) numericUpDown1.Value;
      Restart = true;
    }

    public void SettingsUpDate()
    {
      textBox1.Text = TempNames[0];
      textBox2.Text = TempNames[1];
      textBox3.Text = TempNames[2];
      textBox4.Text = TempNames[3];
      numericUpDown1.Value = GameData.Players;
      CreateColors();
      CreatePens();
      Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      foreach (Rectangle colorRect in ColorRects)
      {
        if (colorRect.Contains(PointToClient(MousePosition)))
          colorChangeDialog(TempColors[ColorRects.IndexOf(colorRect)]);
      }
      foreach (Rectangle pensRect in PensRects)
      {
        if (pensRect.Contains(PointToClient(MousePosition)))
          colorChangeDialog(TempPens[PensRects.IndexOf(pensRect)]);
      }
      Invalidate();
    }

    private void textBox1_TextChanged(object sender, EventArgs e) => TempNames[0] = textBox1.Text;

    private void textBox2_TextChanged(object sender, EventArgs e) => TempNames[1] = textBox2.Text;

    private void textBox3_TextChanged(object sender, EventArgs e) => TempNames[2] = textBox3.Text;

    private void textBox4_TextChanged(object sender, EventArgs e) => TempNames[3] = textBox4.Text;

    private void button1_Click(object sender, EventArgs e) => Close();

    private void Settings_Shown(object sender, EventArgs e) => Restart = false;

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      label1 = new Label();
      numericUpDown1 = new NumericUpDown();
      textBox1 = new TextBox();
      textBox2 = new TextBox();
      textBox3 = new TextBox();
      textBox4 = new TextBox();
      button1 = new Button();
      label2 = new Label();
      label3 = new Label();
      label4 = new Label();
      numericUpDown1.BeginInit();
      SuspendLayout();
      label1.AutoSize = true;
      label1.Location = new Point(12, 52);
      label1.Name = "label1";
      label1.Size = new Size(98, 13);
      label1.TabIndex = 0;
      label1.Text = "Названия команд";
      numericUpDown1.Location = new Point(13, 13);
      numericUpDown1.Maximum = new Decimal(new int[4]
      {
        4,
        0,
        0,
        0
      });
      numericUpDown1.Minimum = new Decimal(new int[4]
      {
        2,
        0,
        0,
        0
      });
      numericUpDown1.Name = "numericUpDown1";
      numericUpDown1.Size = new Size(120, 20);
      numericUpDown1.TabIndex = 1;
      numericUpDown1.Value = new Decimal(new int[4]
      {
        2,
        0,
        0,
        0
      });
      numericUpDown1.ValueChanged += new EventHandler(numericUpDown1_ValueChanged);
      textBox1.Location = new Point(12, 80);
      textBox1.Name = "textBox1";
      textBox1.Size = new Size(171, 20);
      textBox1.TabIndex = 2;
      textBox1.TextChanged += new EventHandler(textBox1_TextChanged);
      textBox2.Location = new Point(12, 120);
      textBox2.Name = "textBox2";
      textBox2.Size = new Size(171, 20);
      textBox2.TabIndex = 3;
      textBox2.TextChanged += new EventHandler(textBox2_TextChanged);
      textBox3.Location = new Point(12, 160);
      textBox3.Name = "textBox3";
      textBox3.Size = new Size(171, 20);
      textBox3.TabIndex = 4;
      textBox3.TextChanged += new EventHandler(textBox3_TextChanged);
      textBox4.Location = new Point(12, 200);
      textBox4.Name = "textBox4";
      textBox4.Size = new Size(171, 20);
      textBox4.TabIndex = 5;
      textBox4.TextChanged += new EventHandler(textBox4_TextChanged);
      button1.Location = new Point(110, 226);
      button1.Name = "button1";
      button1.Size = new Size(120, 23);
      button1.TabIndex = 6;
      button1.Text = "Готово";
      button1.UseVisualStyleBackColor = true;
      button1.Click += new EventHandler(button1_Click);
      label2.Location = new Point(148, 13);
      label2.Name = "label2";
      label2.Size = new Size(201, 37);
      label2.TabIndex = 7;
      label2.Text = "Изменение количества игроков приведет к перезапуску игры";
      label3.AutoSize = true;
      label3.Location = new Point(214, 52);
      label3.Name = "label3";
      label3.Size = new Size(38, 13);
      label3.TabIndex = 8;
      label3.Text = "Пират";
      label4.AutoSize = true;
      label4.Location = new Point(280, 52);
      label4.Name = "label4";
      label4.Size = new Size(43, 13);
      label4.TabIndex = 9;
      label4.Text = "Цифры";
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(361, 252);
      Controls.Add(label4);
      Controls.Add(label3);
      Controls.Add(label2);
      Controls.Add(button1);
      Controls.Add(textBox4);
      Controls.Add(textBox3);
      Controls.Add(textBox2);
      Controls.Add(textBox1);
      Controls.Add(numericUpDown1);
      Controls.Add(label1);
      Name = nameof (Settings);
      Text = nameof (Settings);
      Shown += new EventHandler(Settings_Shown);
      Paint += new PaintEventHandler(Dial_Paint);
      numericUpDown1.EndInit();
      ResumeLayout(false);
      PerformLayout();
    }
  }
}

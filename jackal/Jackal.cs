// Decompiled with JetBrains decompiler
// Type: jackal.Jackal
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace jackal
{
  public class Jackal : Form
  {
    private Settings SettingsScreen = new Settings();
    private IContainer components = null;
    private GameField GameField1;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem помощь;
    private ToolStripMenuItem сохранить;
    private ToolStripMenuItem загрузить;
    private ToolStripMenuItem настрорйкиToolStripMenuItem;

    public Jackal()
    {
      DoubleBuffered = true;
      InitializeComponent();
      KeyPreview = true;
      GameStart();
    }

    private void GameStart()
    {
      GameData.SetAllCells();
      GameData.SetAllUnits();
      GameField gameField1 = GameField1;
      Size size1 = Size;
      int width = size1.Width - 25;
      size1 = Size;
      int height = size1.Height - 70;
      Size size2 = new Size(width, height);
      gameField1.Size = size2;
      GameField1.RePaint();
      playSound(Application.StartupPath + "\\corsars.wav");
    }

    private void playSound(string path)
    {
      SoundPlayer soundPlayer = new SoundPlayer();
      soundPlayer.SoundLocation = path;
      soundPlayer.Load();
      soundPlayer.Play();
    }

    private void Form1_SizeChanged(object sender, EventArgs e)
    {
      GameField gameField1 = GameField1;
      Size size1 = Size;
      int width1 = size1.Width - 25;
      size1 = Size;
      int height1 = size1.Height - 70;
      Size size2 = new Size(width1, height1);
      gameField1.Size = size2;
      Size size3 = GameField1.Size;
      int height2 = size3.Height;
      size3 = GameField1.Size;
      int width2 = size3.Width;
      GameField1.scale = height2 <= width2 ? (GameField1.Size.Height - 40) / 13 : (GameField1.Size.Width - 20) / 13;
      GameField1.RePaint();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (e.KeyCode != Keys.A || !e.Control)
        return;
      GameField1.DrawAll = !GameField1.DrawAll;
      GameField1.RePaint();
    }

    private void помощь_Click(object sender, EventArgs e) => Process.Start(Application.StartupPath + "\\rules.docx");

    private void сохранить_Click(object sender, EventArgs e) => GameData.Save();

    private void загрузить_Click(object sender, EventArgs e)
    {
      GameData.Load();
      GameField1.RePaint();
    }

    private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SettingsScreen.TempColors = GameField1.TeamColors;
      SettingsScreen.TempPens = GameField1.TeamPens;
      SettingsScreen.TempNames = GameField1.TeamNames;
      SettingsScreen.SettingsUpDate();
      int num = (int) SettingsScreen.ShowDialog();
      if (SettingsScreen.Restart)
        GameStart();
      GameField1.TeamColors = SettingsScreen.TempColors;
      SettingsScreen.TempPens = GameField1.TeamPens;
      GameField1.TeamNames = SettingsScreen.TempNames;
      GameField1.RePaint();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Jackal));
      menuStrip1 = new MenuStrip();
      настрорйкиToolStripMenuItem = new ToolStripMenuItem();
      помощь = new ToolStripMenuItem();
      сохранить = new ToolStripMenuItem();
      загрузить = new ToolStripMenuItem();
      GameField1 = new GameField();
      menuStrip1.SuspendLayout();
      SuspendLayout();
      menuStrip1.Items.AddRange(new ToolStripItem[4]
      {
        настрорйкиToolStripMenuItem,
        помощь,
        сохранить,
        загрузить
      });
      menuStrip1.Location = new Point(0, 0);
      menuStrip1.Name = "menuStrip1";
      menuStrip1.Size = new Size(735, 24);
      menuStrip1.TabIndex = 1;
      menuStrip1.Text = "menuStrip1";
      настрорйкиToolStripMenuItem.Name = "настрорйкиToolStripMenuItem";
      настрорйкиToolStripMenuItem.Size = new Size(79, 20);
      настрорйкиToolStripMenuItem.Text = "Настройки";
      настрорйкиToolStripMenuItem.Click += new EventHandler(настройкиToolStripMenuItem_Click);
      помощь.Name = "помощь";
      помощь.Size = new Size(68, 20);
      помощь.Text = "Помощь";
      помощь.Click += new EventHandler(помощь_Click);
      сохранить.Name = "сохранить";
      сохранить.Size = new Size(75, 20);
      сохранить.Text = "сохранить";
      сохранить.Click += new EventHandler(сохранить_Click);
      загрузить.Name = "загрузить";
      загрузить.Size = new Size(71, 20);
      загрузить.Text = "загрузить";
      загрузить.Click += new EventHandler(загрузить_Click);
      GameField1.Dock = DockStyle.Fill;
      GameField1.Location = new Point(0, 24);
      GameField1.Name = "GameField1";
      GameField1.Size = new Size(735, 761);
      GameField1.TabIndex = 0;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = SystemColors.GradientActiveCaption;
      ClientSize = new Size(735, 785);
      Controls.Add(GameField1);
      Controls.Add(menuStrip1);
      Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      Name = nameof (Jackal);
      Text = nameof (Jackal);
      SizeChanged += new EventHandler(Form1_SizeChanged);
      menuStrip1.ResumeLayout(false);
      menuStrip1.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }
  }
}

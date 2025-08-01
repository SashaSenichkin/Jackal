using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace jackal;

public class Jackal : Form
{
  private Settings SettingsScreen = new();
  private IContainer components = null;
  private GameField GameField1;
  private MenuStrip menuStrip1;
  private ToolStripMenuItem helpToolStripMenuItem;
  private ToolStripMenuItem saveToolStripMenuItem;
  private ToolStripMenuItem loadToolStripMenuItem;
  private ToolStripMenuItem settingsToolStripMenuItem;

  private const int FiledSizeSquares = 13;

  private const int WidthMargin = 25;
  private const int HeightMargin = 70;

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
    var gameField1 = GameField1;
    var width = Size.Width - WidthMargin;
    var height = Size.Height - HeightMargin;
    var size2 = new Size(width, height);
    gameField1.Size = size2;
    GameField1.RePaint();
    PlaySound(Path.Combine(Application.StartupPath, "corsars.wav"));
  }

  private static void PlaySound(string path)
  {
    if (!File.Exists(path))
    {
      return;
    }
      
    var soundPlayer = new SoundPlayer();
    soundPlayer.SoundLocation = path;
    soundPlayer.Load();
    soundPlayer.Play();
  }

  private void Form1_SizeChanged(object sender, EventArgs e)
  {
    var gameField1 = GameField1;
    var width1 = Size.Width - WidthMargin;
    var height1 = Size.Height - HeightMargin;
    gameField1.Size =  new Size(width1, height1);
    var size3 = GameField1.Size;
    var height2 = size3.Height;
    size3 = GameField1.Size;
    var width2 = size3.Width;
    GameField1.scale = height2 <= width2 ? (GameField1.Size.Height - 40) / FiledSizeSquares : (GameField1.Size.Width - 20) / FiledSizeSquares;
    GameField1.RePaint();
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    if (e.KeyCode != Keys.A || !e.Control)
    {
      return;
    }

    GameField1.DrawAll = !GameField1.DrawAll;
    GameField1.RePaint();
  }

  private void help_Click(object sender, EventArgs e) => Process.Start(Path.Combine(Application.StartupPath, "rules.docx"));

  private void save_Click(object sender, EventArgs e) => GameData.Save();

  private void load_Click(object sender, EventArgs e)
  {
    GameData.Load();
    GameField1.RePaint();
  }

  private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    SettingsScreen.TempColors = GameField1.TeamColors;
    SettingsScreen.TempPens = GameField1.TeamPens;
    SettingsScreen.TempNames = GameField1.TeamNames;
    SettingsScreen.SettingsUpDate();
    SettingsScreen.ShowDialog();
    if (SettingsScreen.Restart)
    {
      GameStart();
    }

    GameField1.TeamColors = SettingsScreen.TempColors;
    SettingsScreen.TempPens = GameField1.TeamPens;
    GameField1.TeamNames = SettingsScreen.TempNames;
    GameField1.RePaint();
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
    var componentResourceManager = new ComponentResourceManager(typeof (Jackal));
    menuStrip1 = new MenuStrip();
    settingsToolStripMenuItem = new ToolStripMenuItem();
    helpToolStripMenuItem = new ToolStripMenuItem();
    saveToolStripMenuItem = new ToolStripMenuItem();
    loadToolStripMenuItem = new ToolStripMenuItem();
    GameField1 = new GameField();
    menuStrip1.SuspendLayout();
    SuspendLayout();
    menuStrip1.Items.AddRange([
      settingsToolStripMenuItem,
      helpToolStripMenuItem,
      saveToolStripMenuItem,
      loadToolStripMenuItem,
    ]);
      
    menuStrip1.Location = Point.Empty;
    menuStrip1.Name = "menuStrip1";
    menuStrip1.Size = new Size(735, 24);
    menuStrip1.TabIndex = 1;
    menuStrip1.Text = "menuStrip1";
    settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
    settingsToolStripMenuItem.Size = new Size(79, 20);
    settingsToolStripMenuItem.Text = "Настройки";
    settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
    helpToolStripMenuItem.Name = "helpToolStripMenuItem";
    helpToolStripMenuItem.Size = new Size(68, 20);
    helpToolStripMenuItem.Text = "Помощь";
    helpToolStripMenuItem.Click += help_Click;
    saveToolStripMenuItem.Name = "saveToolStripMenuItem";
    saveToolStripMenuItem.Size = new Size(75, 20);
    saveToolStripMenuItem.Text = "сохранить";
    saveToolStripMenuItem.Click += save_Click;
    loadToolStripMenuItem.Name = "loadToolStripMenuItem";
    loadToolStripMenuItem.Size = new Size(71, 20);
    loadToolStripMenuItem.Text = "загрузить";
    loadToolStripMenuItem.Click += load_Click;
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
    SizeChanged += Form1_SizeChanged;
    menuStrip1.ResumeLayout(false);
    menuStrip1.PerformLayout();
    ResumeLayout(false);
    PerformLayout();
  }
}
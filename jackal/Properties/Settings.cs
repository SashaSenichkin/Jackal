// Decompiled with JetBrains decompiler
// Type: jackal.Properties.Settings
// Assembly: jackal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DB49CD4A-B69F-4765-B90A-02396B5F5C89
// Assembly location: D:\Projects\Jackal\jackal.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace jackal.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) Synchronized(new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marabu
{
  using System.Configuration;
  using System.Windows.Forms;

  public class ServerSettings
  {
    public const string MARABU_HOST_NAME = "marabu_host";
    public const string MARABU_PORT_NAME = "marabu_port";
    public const string MENUITEM_ID_NAME = "menuItemId";
    public const string FIELD_ID_NAME = "fieldId";
    public const string FULL_LOG_MODE_NAME = "full_log_mode";

    public const string SERVER_LOG_FOLDER_NAME = "server_log_folder";

    public string MarabuHost { get; set; }

    private string _marabuPort;
    public string MarabuPort
    {
      get
      {
        return _marabuPort;
      }
      set
      {
        _marabuPort = value;
        iMarabuPort = int.Parse(_marabuPort);
      }
    }
    public int iMarabuPort;

    private string _serverLogFolder;

    public string ServerLogFolder
    {
      get
      {
        return _serverLogFolder;
      }
      set
      {
        _serverLogFolder = value;
        if(string.IsNullOrEmpty(_serverLogFolder))
        {
          _serverLogFolder = AppDomain.CurrentDomain.BaseDirectory;
        }
        else
        {
          char c = value[value.Length - 1];
          if (c != '\\' && c != '/')
          {
            _serverLogFolder += '\\';
          }
        }
      }
    }
    
    
    public int iCiscoNotifierPort { get; set; }

    public string MenuItemId { get; set; }
    public string FieldId { get; set; }
    public bool FullLogMode { get; set; }

    public void LoadConfig()
    {
      Configuration config = null;

      config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

      MarabuHost = config.AppSettings.Settings[MARABU_HOST_NAME].Value;
      MarabuPort = config.AppSettings.Settings[MARABU_PORT_NAME].Value;

      MenuItemId = config.AppSettings.Settings[MENUITEM_ID_NAME].Value;
      FieldId = config.AppSettings.Settings[FIELD_ID_NAME].Value;
      ServerLogFolder = config.AppSettings.Settings[SERVER_LOG_FOLDER_NAME].Value;
      string s = config.AppSettings.Settings[FULL_LOG_MODE_NAME].Value;
      bool mode;
      bool.TryParse(s, out mode);
      FullLogMode = mode;

    }
    public void Save()
    {
      Configuration config = null;
      config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

      config.AppSettings.Settings[MARABU_HOST_NAME].Value = MarabuHost;
      config.AppSettings.Settings[MARABU_PORT_NAME].Value = MarabuPort;

      config.AppSettings.Settings[MENUITEM_ID_NAME].Value = MenuItemId;
      config.AppSettings.Settings[FIELD_ID_NAME].Value = FieldId;

      config.AppSettings.Settings[SERVER_LOG_FOLDER_NAME].Value = ServerLogFolder;
      config.AppSettings.Settings[FULL_LOG_MODE_NAME].Value = FullLogMode.ToString();

      config.Save(ConfigurationSaveMode.Modified);
    }
  }
}

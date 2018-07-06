using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marabu
{
  public class HpsmCisco
  {
    public static HpsmCisco Instance = new HpsmCisco();

    private Dictionary<string, string> _users = new Dictionary<string, string>();
    private Exception _exception;
    private object _lockObject = new object();

    private static string _usersFileName
    {
      get
      {
        return AppDomain.CurrentDomain.BaseDirectory + "users.txt";
      }
    }

    public string UsersFileName
    {
      get
      {
        lock (_lockObject)
        {
          return AppDomain.CurrentDomain.BaseDirectory + "users.txt";
        }
      }
    }

    private string _validateUser(string user)
    {
      return user.Trim().ToLower();
    }
    private void _add(string hpsmUser,string ciscoUser)
    {
      try
      {
        _users[_validateUser(hpsmUser)] = _validateUser(ciscoUser);
      }
      catch(Exception ex)
      {
        _exception = ex;
      }
    }
    private void _loadFromFile(string file)
    {
      if (!System.IO.File.Exists(file)) return;
      string[] lines = System.IO.File.ReadAllLines(file, Encoding.UTF8);
      foreach (string line in lines)
      {
        string s = line.Trim();
        if (s.Length > 0 && s[0] != '#')
        {
          string[] data = s.Split(':');
          if (data.Length == 2)
          {
            _add(data[0], data[1]);
          }
        }
      }
    }

    public void Load()
    {
      lock (_lockObject)
      {
        _users.Clear();
        _loadFromFile(_usersFileName);
      }
    }
    private void _saveToFile(string content)
    {
      try
      {
        if (System.IO.File.Exists(_usersFileName))
        {
          System.IO.File.Delete(_usersFileName);
        }
        System.IO.File.WriteAllText(_usersFileName, content, Encoding.UTF8);
      }
      catch(Exception ex)
      {
        _exception = ex;
      }
    }

    public void Save(string content)
    {
      lock (_lockObject)
      {
        _saveToFile(content);
        _loadFromFile(_usersFileName);
      }
    }

    public string FindCiscoUser(string hpsmUser)
    {
      lock (_lockObject)
      {
        hpsmUser = _validateUser(hpsmUser);
        foreach(string key in _users.Keys)
        {
          if (key.Equals(hpsmUser))
          {
            string value = _users[key];
            return value;
          }
        }
        //if (_users.ContainsKey(hpsmUser))
        //{
        //  return _users[hpsmUser];
        //}
        return null;
      }
    }
    public string _getFileContent()
    {
      if (System.IO.File.Exists(_usersFileName))
      {
        return System.IO.File.ReadAllText(_usersFileName, Encoding.UTF8);
      }
      else
      {
        return "#ERROR file not found : " + _usersFileName;
      }
    }
    public string GetFileContent()
    {
      lock (_lockObject)
      {
        return _getFileContent();
      }
      }
    }//end of class
}

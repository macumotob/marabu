using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace marabu
{
  public class jabber
  {
    public string name;
    public string value;
    public Dictionary<string, string> attributes = new Dictionary<string, string>();
    public List<jabber> childs = new List<jabber>();
    public bool error;
    public override string ToString()
    {
      return name + " " + value;
    }
  }

  class BoshParser
  {
    private void _skipws(string s, ref int i)
    {
      while (i < s.Length && (s[i] == ' ' || s[i] == '\t' || s[i] == '\r' || s[i] == '\n')) i++;
    }

    enum termtype
    {
      tagendbegin, tegcloseend, tag, tagend, str, none, assign, ws, word, attribute
    }
    class token
    {
      public termtype type;
      public string value;
      public string attrvalue;
      public override string ToString()
      {
        return type + " " + value + " " + attrvalue;
      }
    }
    private bool _isterm(string s, int i, ref int len, out termtype type)
    {
      type = termtype.none;
      string[] terms = { "</", "/>", "<", ">", "=", "\"", "\'", " ", "\t", "\r", "\n" };
      termtype[] types = { termtype.tagendbegin, termtype.tegcloseend, termtype.tag, termtype.tagend, termtype.assign, termtype.str, termtype.str,
      termtype.ws,termtype.ws,termtype.ws,termtype.ws};
      for (int n = 0; n < terms.Length; n++)
      {
        string term = terms[n];
        type = types[n];
        len = term.Length;
        if (len == 2 && i + 1 < s.Length && term[0] == s[i] && term[1] == s[i + 1])
        {
          return true;
        }
        if (len == 1 && term[0] == s[i])
        {
          return true;
        }
      }
      type = termtype.none;
      return false;
    }
    private void _add(List<token> toks, token tok)
    {
      if (toks.Count > 1 && toks[toks.Count - 1].type == termtype.assign)
      {
        toks[toks.Count - 2].type = termtype.attribute;
        toks[toks.Count - 2].attrvalue = tok.value;
        toks.RemoveAt(toks.Count - 1);
        return;
      }
      if (tok.type == termtype.tagend && toks.Count > 2 && toks[toks.Count - 1].type == termtype.word && toks[toks.Count - 2].type == termtype.tag)
      {
        toks[toks.Count - 2].value = toks[toks.Count - 1].value;
        toks.RemoveAt(toks.Count - 1);
        return;
      }
      if (tok.type == termtype.tagend && toks.Count > 2 && toks[toks.Count - 1].type == termtype.word && toks[toks.Count - 2].type == termtype.tagendbegin)
      {
        toks[toks.Count - 2].value = toks[toks.Count - 1].value;
        toks.RemoveAt(toks.Count - 1);
        return;
      }

      if (tok.type == termtype.tagendbegin && toks.Count > 2 && toks[toks.Count - 1].type == termtype.word && toks[toks.Count - 2].type == termtype.tag)
      {
        toks[toks.Count - 2].type = termtype.attribute;
        toks[toks.Count - 2].attrvalue = toks[toks.Count - 1].value;
        toks.RemoveAt(toks.Count - 1);
        return;
      }
      if (tok.type == termtype.tagendbegin && toks.Count > 1 && toks[toks.Count - 1].type == termtype.tag)
      {
        toks[toks.Count - 1].type = termtype.attribute;
        toks[toks.Count - 1].attrvalue = "";
        return;
      }
      toks.Add(tok);
    }
    private jabber _parseResponse(string s)
    {
      int i = 0;


      List<token> toks = new List<BoshParser.token>();
      string name;

      while (i < s.Length)
      {
        _skipws(s, ref i);
        name = "";
        char c = s[i];
        int len = 0;
        termtype term;

        if (_isterm(s, i, ref len, out term))
        {
          i += len;
          if (term == termtype.ws)
          {
            continue;
          }
          if (term == termtype.str)
          {
            while (!_isterm(s, i, ref len, out term))
            {
              name += s[i++];
            }
            if (_isterm(s, i, ref len, out term) && term == termtype.str)
            {
              i += len;
              _add(toks, new token() { type = term, value = name });
              continue;
            }
            throw new Exception("todo");
          }
          if (term == termtype.tagendbegin)
          {
            while (!_isterm(s, i, ref len, out term))
            {
              name += s[i++];
            }
            if (_isterm(s, i, ref len, out term) && term == termtype.tagend)
            {
              i += len;
              _add(toks, new token() { type = termtype.tagendbegin, value = name });
              continue;
            }
            throw new Exception("todo");
          }
          _add(toks, new token() { type = term, value = s.Substring(i - len, len) });
          continue;
        }
        while (!_isterm(s, i, ref len, out term))
        {
          name += s[i++];
        }
        _add(toks, new token() { type = termtype.word, value = name });
        continue;
      }
      i = 0;

      name = "";
      token tok = toks[i];
      if (tok.type == termtype.tag && toks[i + 1].type == termtype.word)
      {
        name = toks[i + 1].value;
        i++;
      }
      jabber root = new jabber() { name = name };
      i++;
      jabber j = root;
      Stack stack = new Stack();

      while (i < toks.Count)
      {
        tok = toks[i];
        switch (tok.type)
        {
          case termtype.attribute:
            j.attributes.Add(tok.value, tok.attrvalue);
            i++;
            break;
          case termtype.tag:
            i++;
            if (toks[i].type != termtype.word)
            {
              throw new Exception("todo");
            }
            name = toks[i].value;
            i++;
            stack.Push(j);

            jabber tmp = new jabber() { name = name };
            j.childs.Add(tmp);
            j = tmp;
            break;
          case termtype.tagend:
            i++;
            break;
          case termtype.tagendbegin:
            i++;
            if (toks[i].type == termtype.word)
            {
              if (toks[i].value != j.name)
              {
                throw new Exception("todo");
              }
              i++;
            }
            if (stack.Count > 0)
            {
              j = (jabber)stack.Pop();
            }
            else
            {
            }
            break;
          default:
            throw new Exception("todo");
        }
      }

      if (stack.Count > 0)
      {
        j = (jabber)stack.Pop();
      }
      if (j != root)
      {
        throw new Exception("todo");
      }
      return root;
    }
    private string _parseDialogs(string s)
    {
      CiscoDialogs dialogs = new CiscoDialogs(text.XML_HEADER + s);
#if CISCO_HISTORY1
      foreach (CiscoDialog dialog in dialogs.Items)
      {
        history.SaveDialog(dialog);
      }
#endif
      JavaScriptSerializer serializer = new JavaScriptSerializer();
      return serializer.Serialize(dialogs);

    }

    private string _gettag(string s, string tag, string tagend)
    {
      int i = s.IndexOf(tag);
      if (i > -1)
      {
        int i2 = s.IndexOf(tagend);
        return s.Substring(i, i2 - i + tagend.Length);
      }
      return null;
    }
  }
}

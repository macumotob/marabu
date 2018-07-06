using System;
using System.Collections.Generic;

namespace marabu
{

  public class CallVariable
  {
    public string name { get; set; }
    public string value { get; set; }
    public CallVariable(CiscoParser parser)
    {
      while (parser.Read())
      {
        if (parser.state == CiscoParser.ParserState.Begin)
        {
          switch (parser.name)
          {
            case "name":
            case "value":
              parser.Read(this, parser.name);
              break;
            default:
              throw new Exception("parser error");
          }
        }
        else if (parser.state == CiscoParser.ParserState.End)
        {
          if (parser.name == this.GetType().Name)
          {
            return;
          }
        }
        else
        {
          throw new Exception("parser error");
        }
      }
    }
  }
  /// <summary>
  /// /////////
  /// </summary>
  public class Participant
  {
    public List<string> actions = new List<string>();
    public string mediaAddress { get; set; }
    public string mediaAddressType { get; set; }
    public string startTime { get; set; }
    public string state { get; set; }
    public string stateCause { get; set; }
    public string stateChangeTime { get; set; }

    public Participant(CiscoParser parser)
    {
      while (parser.Read())
      {
        if (parser.state == CiscoParser.ParserState.Begin)
        {
          switch (parser.name)
          {
            case "actions":
              actions = new List<string>();
              break;
            case "action":
              actions.Add(parser.ReadSimple(this));
              break;
            case "mediaAddress":
            case "mediaAddressType":
            case "startTime":
            case "state":
            case "stateCause":
            case "stateChangeTime":
              parser.Read(this, parser.name);
              break;
            default:
              throw new Exception("parser error");
          }
        }
        else if (parser.state == CiscoParser.ParserState.End)
        {
          if (parser.name == this.GetType().Name)
          {
            return;
          }
        }
        else
        {
          throw new Exception("parser error");
        }
      }
    }
  }
  /// <summary>
  /// ////
  /// </summary>
  public class mediaProperties
  {
    public string DNIS { get; set; }
    public string callType { get; set; }
    public List<CallVariable> callvariables;
    public string dialedNumber { get; set; }
    public string outboundClassification { get; set; }

    public mediaProperties(CiscoParser parser)
    {
      while (parser.Read())
      {
        if (parser.state == CiscoParser.ParserState.Begin)
        {
          switch (parser.name)
          {
            case "DNIS":  
            case "callType":
            case "dialedNumber":
            case "outboundClassification":
              parser.Read(this, parser.name);
              break;
            case "callvariables":
              callvariables = new List<CallVariable>();
              break;
            case "CallVariable":
              callvariables.Add(new CallVariable(parser));
              break;
            default:
              throw new Exception("parser error");
          }
        }
        else if (parser.state == CiscoParser.ParserState.End)
        {
          if (parser.name == this.GetType().Name)
          {
            return;
          }
        }
        else
        {
          throw new Exception("parser error");
        }

      }
    }
  }

}
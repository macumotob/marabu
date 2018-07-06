using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marabu
{
  public class CiscoDialog
  {
    public string associatedDialogUri { get; set; }
    public string fromAddress { get; set; }
    public string id { get; set; }
    //public List<MediaProperty> mediaProperties { get; set; }
    public mediaProperties mediaProperties { get; set; }
    public string mediaType { get; set; }
    public List<Participant> participants { get; set; }
    public string state { get; set; }
    public string toAddress { get; set; }
    public string uri { get; set; }

    public CiscoDialog(CiscoParser parser)
    {
      while (parser.Read())
      {
        if (parser.state == CiscoParser.ParserState.Begin)
        {
          switch (parser.name)
          {
            case "associatedDialogUri":
            case "fromAddress":
            case "id":
            case "mediaType":
            case "state":
            case "toAddress":
            case "uri":
              parser.Read(this, parser.name);
              break;
            case "mediaProperties":
              mediaProperties = new mediaProperties(parser);
              break;
            case "participants":
              participants = new List<Participant>();
              break;
            case "Participant":
              participants.Add(new Participant(parser));
              break;
            default:
              throw new Exception("parser error");
          }
        }
        else if (parser.state == CiscoParser.ParserState.End)
        {
          if (parser.name == "Dialog") //this.GetType().Name)
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
  //

  public class CiscoDialogs
  {

    public List<CiscoDialog> Items;
    public CiscoDialogs(string xml)
    {
      CiscoParser _parser = new CiscoParser(xml);
      while (_parser.Read())
      {
        if (_parser.state == CiscoParser.ParserState.Begin)
        {
          switch (_parser.name)
          {
            case "dialogs":
              Items = new List<CiscoDialog>();
              break;
            case "Dialog":
              Items.Add(new CiscoDialog(_parser));
              break;
            default:
              throw new Exception("parse dialogs error");
          }
        }
      }

    }
  }
  //
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marabu
{
  public class text
  {
    public const string XML_HEADER = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

    public const string STATE_LOGOUT = "LOGOUT";
    public const string STATE_NOT_READY = "NOT_READY";
    public const string STATE_READY = "READY";

    public const string STATE_TAPI = "tapi";

    public const string CISCO_COMMAND_ERROR = "error";
    public const string CISCO_ERROR_TEXT = "-CISCO ERROR";
    public const string CISCO_ERROR_GET_STATE = "Ошибка получения статуса пользователя.";

    public const string CISCO_JABBER_DIALOGS = "dialogs";
    public const string CISCO_JABBER_MESSAGE = "message";

    public const string BOSH_TERMINATE = "<body xmlns=\"http://jabber.org/protocol/httpbind\" type=\"terminate\"></body>";
    public const string BOSH_HEART_BIT = "<body xmlns=\"http://jabber.org/protocol/httpbind\"></body>";

    public const string FINESSE_LOGIN = "login";
    public const string FINESSE_LOGOUT = "logout";
    public const string FINESSE_USER_STATE_CHANGE = "userstatus";
    public const string FINESSE_MAKE_CALL = "makecall";
    public const string FINESSE_DIALOG_ACTION = "dialog";
    public const string FINESSE_CONSULTANT_CALL = "consultant";

    public const string HPSM_HEART_BIT= "tapi";
    public const string HPSM_REGISTER_USER = "reg";
    public const string HPSM_UNREGISTER_USER = "unreg";
    public const string HPSM_CHANGE_USER_STATE = "status";
    public const string HPSM_MAKE_CALL = "call";
    public const string HPSM_DIALOG_ACTION = "action";
    public const string HPSM_REDIRECT = "redirect";
    public const string HPSM_HISTORY =  "history";
  }
}

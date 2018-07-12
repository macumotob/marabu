using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace marabu
{
    public static class Utils
    {
        private static IDictionary<string, string> _mimeTypeMappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
			#region extension to MIME type list
			{".asf", "video/x-ms-asf"},
      {".asx", "video/x-ms-asf"},
      {".avi", "video/x-msvideo"},
      {".bin", "application/octet-stream"},
      {".cco", "application/x-cocoa"},
      {".crt", "application/x-x509-ca-cert"},
      {".css", "text/css"},
      {".deb", "application/octet-stream"},
      {".der", "application/x-x509-ca-cert"},
      {".dll", "application/octet-stream"},
      {".dmg", "application/octet-stream"},
      {".ear", "application/java-archive"},
      {".eot", "application/octet-stream"},
      {".exe", "application/octet-stream"},
      {".flv", "video/x-flv"},
      {".gif", "image/gif"},
      {".hqx", "application/mac-binhex40"},
      {".htc", "text/x-component"},
      {".htm", "text/html"},
      {".html", "text/html"},
      {".ico", "image/x-icon"},
      {".img", "application/octet-stream"},
      {".iso", "application/octet-stream"},
      {".jar", "application/java-archive"},
      {".jardiff", "application/x-java-archive-diff"},
      {".jng", "image/x-jng"},
      {".jnlp", "application/x-java-jnlp-file"},
      {".jpeg", "image/jpeg"},
      {".jpg", "image/jpeg"},
      {".js", "application/x-javascript"},
      {".mml", "text/mathml"},
      {".mng", "video/x-mng"},
      {".mov", "video/quicktime"},
      {".mp3", "audio/mpeg"},
      {".mpeg", "video/mpeg"},
      {".mpg", "video/mpeg"},
      {".msi", "application/octet-stream"},
      {".msm", "application/octet-stream"},
      {".msp", "application/octet-stream"},
      {".pdb", "application/x-pilot"},
      {".pdf", "application/pdf"},
      {".pem", "application/x-x509-ca-cert"},
      {".pl", "application/x-perl"},
      {".pm", "application/x-perl"},
      {".png", "image/png"},
      {".prc", "application/x-pilot"},
      {".ra", "audio/x-realaudio"},
      {".rar", "application/x-rar-compressed"},
      {".rpm", "application/x-redhat-package-manager"},
      {".rss", "text/xml"},
      {".run", "application/x-makeself"},
      {".sea", "application/x-sea"},
      {".shtml", "text/html"},
      {".sit", "application/x-stuffit"},
      {".swf", "application/x-shockwave-flash"},
      {".tcl", "application/x-tcl"},
      {".tk", "application/x-tcl"},
      {".txt", "text/plain"},
      {".war", "application/java-archive"},
      {".wbmp", "image/vnd.wap.wbmp"},
      {".wmv", "video/x-ms-wmv"},
      {".xml", "text/xml"},
      {".xpi", "application/x-xpinstall"},
      {".zip", "application/zip"},
			#endregion
		};
        public static string FileNameToMime(string filename)
        {
            string mime;
            return _mimeTypeMappings.TryGetValue(System.IO.Path.GetExtension(filename), out mime) ? mime : "application/octet-stream";
        }

        public static dynamic Xml2Dynamic(string xml)
        {
            var document = XDocument.Parse(xml);
            dynamic dobj = new ExpandoObject();
            _xml_to_dynamic(dobj, document.Root);

            return dobj;
        }
        private static void _xml_to_dynamic(dynamic parent, XElement node)
        {
            if (node.HasElements)
            {
                if (node.Elements(node.Elements().First().Name.LocalName).Count() > 1)
                {
                    var item = new ExpandoObject();
                    var list = new List<dynamic>();
                    foreach (var element in node.Elements())
                    {
                        _xml_to_dynamic(list, element);
                    }
                    _add_dynamic_property(item, node.Elements().First().Name.LocalName, list);
                    _add_dynamic_property(parent, node.Name.ToString(), item);
                }
                else
                {
                    var item = new ExpandoObject();

                    foreach (var attribute in node.Attributes())
                    {
                        _add_dynamic_property(item, attribute.Name.ToString(), attribute.Value.Trim());
                    }
                    foreach (var element in node.Elements())
                    {
                        _xml_to_dynamic(item, element);
                    }
                    _add_dynamic_property(parent, node.Name.ToString(), item);
                }
            }
            else
            {
                _add_dynamic_property(parent, node.Name.ToString(), node.Value.Trim());
            }
        }

        private static void _add_dynamic_property(dynamic parent, string name, object value)
        {
            if (parent is List<dynamic>)
            {
                (parent as List<dynamic>).Add(value);
            }
            else
            {
                (parent as IDictionary<String, object>)[name] = value;
            }
        }
        // Reverses bits in a byte
        public static byte Reverse(byte inByte)
        {
            byte result = 0x00;

            for (byte mask = 0x80; Convert.ToInt32(mask) > 0; mask >>= 1)
            {
                // shift right current result
                result = (byte)(result >> 1);

                // tempbyte = 1 if there is a 1 in the current position
                var tempbyte = (byte)(inByte & mask);
                if (tempbyte != 0x00)
                {
                    // Insert a 1 in the left
                    result = (byte)(result | 0x80);
                }
            }

            return (result);
        }
    }
}

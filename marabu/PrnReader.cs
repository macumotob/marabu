using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marabu
{
    public enum PrnCommands
    {
        FeedLines,
        PrintLine,
        PrintComprLine,
        BytesPerLine,
        EndOfFile,
        LeftMargin,
        EndJob,
        Error
    }
    public class PrnReader
    {
        public delegate void EventCommandReaded(PrnCommands command, byte[] data, int count);
        public event EventCommandReaded OnCommandReaded;
        public enum PrnVersion
        {
            V2,
            V3,
            Unknown
        };
        const byte ESC = 0x1B;
        const byte LENGTH_COMMAND = 0x4C; //LCMD
        const byte WIDTH_COMMAND = 0x77; // wCMD
        const byte RESOLUTION_HIGH = 0x69;
        const byte RESOLUTION_NORMAL = 0x68;
        const byte FEED_LINES_COMMAND = 0x66;
        const byte FEED_LINES_COMMAND_V3 = 0x46;

        const byte BYTES_PER_LINE = 0x44;
        const byte LEFT_MARGIN = 0x42;
        const byte BEGIN_DATA = 0x17;
        const byte BEGIN_VALIDE_DATA = 0x16;
        const byte RESET = 0x40;
        const byte DOT_TAB_OFFSET = 0x42;
        const byte SHORT_FORM_FEED = 0x47;
        const byte CUT_PAPER = 0x45;
        const byte END_JOB = 0x5A;

        const byte RESOLUTION_HIGH_V3 = 0x49;

        const int V2_HEADER_LENGTH = 120;
        //v2
        //120 byte - 0x1B
        //2 byte - 0x1B 0x40
        //3 byte - 0x1B 0x42 (dot tab offset)
        //4 byte - ESC, LCmd, (byte) (length >> 8), (byte) length }; - label len
        //4 byte = { ESC, wCmd, (byte) (width >> 8), (byte) width }; - label width

        //2 byte - ESC 0x69 - use high resolution || ESC 0x68 - normal
        // write data
        private int _index = -1;
        private byte[] _bytes;
        public void Load(string prnFile)
        {
            if (string.IsNullOrEmpty(prnFile)) return;

            if (System.IO.File.Exists(prnFile))
            {
                _index = 0;
                _bytes = System.IO.File.ReadAllBytes(prnFile);
                _ValidateData();
            }
            else
            {
                throw new Exception("Error : File :" + prnFile + " not found!");
            }
        }
        private byte _bt
        {
            get
            {
                if(_index == 3070)
                {

                }
                return (_index < 0 || _bytes.Length <= 0 || _index >= _bytes.Length) ? (byte)0x00 : _bytes[_index];
            }
        }
        private byte _next()
        {
            _index++;
            return _bt;
        }
        private byte _prev()
        {
            _index--;
            if(_index < 0)
            {
                _index = 0;
            }
            return _bt;
        }
        public PrnVersion _version = PrnVersion.Unknown;
        private byte _dotTabOffset;
        private int _labelLen = 0;
        private int _labelWidth = 0;
        private bool _use_high_resolution;
        private int _dataOffset;

        public int LabelLength
        {
            get
            {
                return _labelLen;
            }
        }
        public int LabelWidth
        {
            get
            {
                return _labelWidth;
            }
        }
        public bool UseHighResolution
        {
            get
            {
                return _use_high_resolution;
            }
        }
        public byte DotTabOffset
        {
            get
            {
                return _dotTabOffset;
            }
        }
        private int _feedLines;
        private int _leftMarginLines;
        private void _ValidateData()
        {
            int dataLen = _bytes.Length;
            if (dataLen <= 0) return;
            _version = PrnVersion.V2;
            while (_bt == ESC && _index < 120)
            {
                _next();
            }
            if(_index == V2_HEADER_LENGTH)
            {

            }
            else
            {
                _version = PrnVersion.V3;
                _ValidateDataV3();
                return;
            }
            if(!(_bt == ESC && _next() == RESET))
            {
                return;
            }
            if(_next() == ESC && _next() == DOT_TAB_OFFSET)
            {
                _dotTabOffset = _next();
            }
            if(_next() == ESC && _next() ==LENGTH_COMMAND)
            {
              //  _labelLen = _next() + _next() * 255;
                _labelLen = _next() * 256 + _next();
                //ESC, LCmd, (byte) (length >> 8), (byte) length }; - label len

            }
            if (_next() == ESC && _next() == WIDTH_COMMAND)
            {
                _labelWidth = _next() * 256 + _next();
            }
            if(_next() == ESC)
            {
                _next();
                if(_bt != RESOLUTION_HIGH && _bt != RESOLUTION_NORMAL)
                {
                    throw new Exception("Invalide format");
                }
                _use_high_resolution = _bt == RESOLUTION_HIGH;
            }
            //if(_next() == ESC && _next() == FEED_LINES_COMMAND)
            //{
            //    _feedLines = _next() * 256 + _next();
            //}
            //int i = 0;
            //while(i< _labelWidth)
            //{
            //    i++;
            //    _next();
            //}
            _next();
            _dataOffset = _index;
        }
        private void _ValidateDataV3()
        {
            _index = 0;
            int dataLen = _bytes.Length;
            if (dataLen <= 0) return;
            
            if (!(_bt == ESC && _next() == RESET))
            {
                return;
            }
            if (_next() == ESC && _next() == DOT_TAB_OFFSET)
            {
                _dotTabOffset = _next();
            }
            if (_next() == ESC)
            {
                _next();
                if (_bt == RESOLUTION_HIGH_V3)
                {
                   // throw new Exception("Invalide format");
                }
                else
                {
                    _index--;
                }
                _use_high_resolution = _bt == RESOLUTION_HIGH_V3;
            }
            //if(_next() == ESC && _next() == FEED_LINES_COMMAND)
            //{
            //    _feedLines = _next() * 256 + _next();
            //}
            //int i = 0;
            //while(i< _labelWidth)
            //{
            //    i++;
            //    _next();
            //}
            //_next();
            _dataOffset = _index;
        }
        public void ResetData()
        {
            _index = _dataOffset;
        }
        List<PrnCommands> _commands = new List<PrnCommands>();
        private void RaiseCommandReaded(PrnCommands command, byte [] data = null, int count = -1)
        {
            if(OnCommandReaded != null)
            {
                _commands.Add(command);
                OnCommandReaded(command, data,count);
                
            }
        }
        public bool EndJob = false;
        public bool EOF
        {
            get
            {
                return _index <= 0 || _index >= _bytes.Length;
            }
        }
        private byte _bytesPerLine;
        private int _currentLine = 0;
        public void Read_V3()
        {
            if (_index >= _bytes.Length)
            {
                EndJob = true;
                RaiseCommandReaded(PrnCommands.EndOfFile);
                return;
            }

            if (_bt != ESC && _bt != BEGIN_DATA && _bt != BEGIN_VALIDE_DATA)
            {
                EndJob = true;
                return;
            }
            if (_bt == ESC)
            {
                _next();
            }
            if (_bt == FEED_LINES_COMMAND_V3)
            {
                _feedLines = _next() * 256 + _next();
                RaiseCommandReaded(PrnCommands.FeedLines, null, _feedLines);
                _next();
                return;
            }
            if (_bt == BYTES_PER_LINE)
            {
                _bytesPerLine = _next();
               // _bytesPerLine = 90;
                RaiseCommandReaded(PrnCommands.BytesPerLine, null, _bytesPerLine);
                _next();
                return;
            }
            if (_bt == LEFT_MARGIN)
            {
                int leftMargin = _next();
                RaiseCommandReaded(PrnCommands.LeftMargin, null, leftMargin);
                _next();
                return;
            }
            if (_bt == BEGIN_DATA)
            {
                try
                {
                    _currentLine++;
                    _next();
                    int i = 0;
                    byte[] data = new byte[_bytesPerLine * 1024];
                    while (_index < _bytes.Length
                         // _bt != ESC 
                         //&& _bt != BEGIN_DATA   
                         //&& _bt != BEGIN_VALIDE_DATA  
                         && !(_bt == FEED_LINES_COMMAND && _bytes[_index - 1] == ESC)
                         && !(_bt == LEFT_MARGIN && _bytes[_index - 1] == ESC)
                         && !(_bt == END_JOB && _bytes[_index - 1] == ESC)
                        )
                    {
                        data[i] = _bt;
                        _next();
                        i++;

                    }
                    if (_bytes[_index - 1] == ESC)
                    {
                        _index--;
                    }
                    RaiseCommandReaded(PrnCommands.PrintComprLine, data, i);
                }
                catch (Exception ex)
                {

                }
                return;

            }
            if (_bt == BEGIN_VALIDE_DATA)
            {
                _currentLine++;
                _next();
                int i = 0;
                byte[] data = new byte[_bytesPerLine];
                while (i < _bytesPerLine )
                {
                    data[i] = _bt;
                    _next();
                    i++;
                    //if (i >= _bytesPerLine)
                    //{
                    //    break;
                    //}
                }
                RaiseCommandReaded(PrnCommands.PrintLine, data, i);

                return;

            }
            if (_bt == SHORT_FORM_FEED)
            {
                _next();
                return;
            }
            if (_bt == CUT_PAPER)
            {
                _next();
                return;
            }
            if (_bt == RESET)
            {
                _next();
                return;
            }
            if (_bt == END_JOB)
            {
                RaiseCommandReaded(PrnCommands.EndJob);
                EndJob = true;
                _next();
                return;
            }
            else
            {
                while(_bt != ESC && _bt != BEGIN_DATA && _bt != BEGIN_VALIDE_DATA)
                {
                    _next();
                }

            }
        }
        
        public void Read()
        {
            
            if(_index >= _bytes.Length)
            {
                EndJob = true;
                //RaiseCommandReaded(PrnCommands.EndOfFile);
                RaiseCommandReaded(PrnCommands.Error, null, _currentLine);
                return;
            }
            if (_bt != ESC && _bt != BEGIN_DATA && _bt != BEGIN_VALIDE_DATA && _bt != FEED_LINES_COMMAND)
            {
                EndJob = true;
                RaiseCommandReaded(PrnCommands.Error, null, _currentLine);
                return;
            }
            if(_bt == ESC)
            {
                _next();
            }
            if (_bt == FEED_LINES_COMMAND)
            {
                _feedLines = _next() * 256 + _next();
                RaiseCommandReaded(PrnCommands.FeedLines,null, _feedLines);
                _next();
                return;
            }
            if (_bt == BYTES_PER_LINE)
            {
                _bytesPerLine =  _next();
                RaiseCommandReaded(PrnCommands.BytesPerLine);
                _next();
                return;
            }
            if(_bt == LEFT_MARGIN)
            {
                int leftMargin = _next();
                RaiseCommandReaded(PrnCommands.LeftMargin, null, leftMargin);
                _next();
                return;
            }
            if(_bt == BEGIN_DATA)
            {
                try
                {
                    _currentLine++;
                    _next();
                    int i = 0;
                    byte[] data = new byte[_bytesPerLine * 1024];
                    while ( _index < _bytes.Length
                         // _bt != ESC 
                         //&& _bt != BEGIN_DATA   
                         //&& _bt != BEGIN_VALIDE_DATA  
                         && !( _bt == FEED_LINES_COMMAND  && _bytes[_index - 1] == ESC)
                         && !(_bt == LEFT_MARGIN && _bytes[_index - 1] == ESC)
                         && !(_bt == END_JOB && _bytes[_index - 1] == ESC)
                        )
                    {
                        data[i] = _bt;
                        _next();
                        i++;

                    }
                    if (_bytes[_index - 1] == ESC)
                    {
                        _index--;
                        i--;
                    }
                    RaiseCommandReaded(PrnCommands.PrintComprLine, data, i);
                }
                catch(Exception ex)
                {

                }
                return;

            }
            if (_bt == BEGIN_VALIDE_DATA)
            {
                _currentLine++;
                _next();
                int i = 0;
                byte[] data = new byte[_bytesPerLine];
                while (i < _bytesPerLine)// && _bt != ESC && _bt != BEGIN_DATA)
                {
                    data[i] = _bt;
                    _next();
                    i++;

                }
                RaiseCommandReaded(PrnCommands.PrintLine, data,i);

                return;

            }
            if(_bt == SHORT_FORM_FEED)
            {
                _next();
                return;
            }
            if(_bt == CUT_PAPER)
            {
                _next();
                return;
            }
            if(_bt == RESET)
            {
                _next();
                return;
            }
            if(_bt == END_JOB)
            {
                EndJob = true;
                RaiseCommandReaded(PrnCommands.EndJob);
                _next();
                return;
            }
            else
            {
                RaiseCommandReaded(PrnCommands.Error, null, _currentLine);
                EndJob = true;
            }
        }
        public void Parse()
        {
            _index = 0;
            while(_bt != END_JOB)
            {
                switch (_bt)
                {
                    case ESC:
                        _next();
                        switch (_bt)
                        {
                            case RESET:
                                _next();
                                break;
                            case LEFT_MARGIN:
                                {
                                    int leftMargin = _next();
                                    _next();
                                }
                                break;
                            case FEED_LINES_COMMAND_V3:
                                {
                                    _feedLines = _next() * 256 + _next();
                                    _next();
                                }
                                break;
                            case BYTES_PER_LINE:
                                {
                                    _bytesPerLine = _next();
                                    _next();

                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case BEGIN_VALIDE_DATA:
                        {
                            _next();
                            int i = 0;
                            byte[] data = new byte[_bytesPerLine];
                            //while (i < _bytesPerLine)// && _bt != ESC && _bt != BEGIN_DATA)
                            while ( _bt != ESC && _bt != BEGIN_DATA && _bt != BEGIN_VALIDE_DATA)
                            {
                                data[i] = _bt;
                                _next();
                                i++;
                                if(i >= _bytesPerLine)
                                {

                                }
                            }

                        }
                        break;
                    case BEGIN_DATA:
                        {
                            _next();
                            int i = 0;
                            byte[] data = new byte[_bytesPerLine];
                            //while (i < _bytesPerLine && _bt != ESC && _bt != BEGIN_DATA)
                            while (_bt != ESC && _bt != BEGIN_DATA && _bt != BEGIN_VALIDE_DATA)
                            {
                                data[i] = _bt;
                                _next();
                                i++;
                                if (i >= _bytesPerLine)
                                {

                                }

                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            if (_index >= _bytes.Length)
            {
                EndJob = true;
                RaiseCommandReaded(PrnCommands.EndOfFile);
                return;
            }
            if (_bt != ESC && _bt != BEGIN_DATA && _bt != BEGIN_VALIDE_DATA)
            {
                EndJob = true;
                return;
            }
            if (_bt == ESC)
            {
                _next();
            }
            if (_bt == FEED_LINES_COMMAND)
            {
                _feedLines = _next() * 256 + _next();
                RaiseCommandReaded(PrnCommands.FeedLines, null, _feedLines);
                _next();
                return;
            }
            if (_bt == BYTES_PER_LINE)
            {
                _bytesPerLine = _next();
                RaiseCommandReaded(PrnCommands.BytesPerLine);
                _next();
                return;
            }
            if (_bt == BEGIN_DATA)
            {
                _next();
                int i = 0;
                byte[] data = new byte[_bytesPerLine];
                while (i < _bytesPerLine && _bt != ESC && _bt != BEGIN_DATA)
                {
                    data[i] = _bt;
                    _next();
                    i++;

                }
                RaiseCommandReaded(PrnCommands.PrintComprLine, data, i);

                return;

            }
            if (_bt == SHORT_FORM_FEED)
            {
                _next();
                return;
            }
            if (_bt == CUT_PAPER)
            {
                _next();
                return;
            }
            if (_bt == RESET)
            {
                _next();
                return;
            }
            if (_bt == END_JOB)
            {
                RaiseCommandReaded(PrnCommands.EndJob);
                _next();
                return;
            }
            else
            {

            }
        }

        public BitArray CurrentByte()
        {
            byte[] data = { _bt };
            return  new BitArray(data);
        }
    }
}

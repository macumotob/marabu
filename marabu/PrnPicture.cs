using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marabu
{

    public class PrnPicture
    {
        private Bitmap _bitmap;
        private Graphics _g;
        private int _width;
        private int _height;
        private int __x = 0;
        private int _x
        {
            get
            {
                return __x;
            }
            set
            {
                __x = value;
                if (__x < 0 && __x >= _width)
                {

                }
            }
        }
        private int _y = 0;
        private int _step = 1;
        PrnReader _viewer = new PrnReader();

        public PrnPicture()
        {

        }
        public PrnPicture(int width, int height)
        {
            _width = width;
            _height = height;
            _bitmap = new Bitmap(_width * _step, _height * _step, System.Drawing.Imaging.PixelFormat.Format24bppRgb); // or some other format
            _g = Graphics.FromImage(_bitmap);
            _y = 0;// -1 * _step;
            _Clear();
            _viewer.OnCommandReaded += _viewer_OnCommandReaded;
        }
        public void Init(int width, int height)
        {
            _width = width;
            _height = height;
            _bitmap = new Bitmap(_width * _step, _height * _step, System.Drawing.Imaging.PixelFormat.Format24bppRgb); // or some other format
            _g = Graphics.FromImage(_bitmap);
            _y = 0;// -1 * _step;
            _Clear();
            _viewer.OnCommandReaded += _viewer_OnCommandReaded;
        }

        public Image Image
        {
            get
            {
                return _bitmap;
            }
        }
        System.Drawing.SolidBrush _blackBush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        System.Drawing.SolidBrush _whiteBush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        System.Drawing.SolidBrush _blueBush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
        System.Drawing.SolidBrush _greenBush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
        public void _Clear()
        {
            using (System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red))
            {
                _g.FillRectangle(myBrush, new Rectangle(0, 0, _width * _step, _height * _step)); // whatever
            }
        }
        private byte _leftMargin = 0;
        public void ResetX()
        {
            _x = _leftMargin;
            _y += _step;
            if (_x != 1)
            {

            }
        }
        private byte[] _bytes = new byte[1];
        public void FeedLines(int count)
        {
            _g.FillRectangle(_greenBush, new Rectangle(0, _y, _width * _step, _y + (count + 1) * _step)); // whatever
            _y += count * _step;
        }
        public void Draw(byte data)
        {
            _bytes[0] = data;

            var bits = new BitArray(_bytes);
            List<bool> x = new List<bool>();
            foreach (bool bit in bits)
            {
                x.Add(bit);
            }
            x.Reverse();
            foreach (bool bit in x)
            {
                if (bit)
                {
                    _g.FillRectangle(_blackBush, new Rectangle(_x, _y, _step, _step)); // whatever
                }
                else
                {
                    _g.FillRectangle(_whiteBush, new Rectangle(_x, _y, _step, _step)); // whatever
                }
                _x += _step;
            }
        }
        const byte ETB_BLACK_MASK = 0x80;
        const byte ETB_COUNT_MASK = 0x7F;

        private int _ParseCompressedByte(byte etb_data, ref bool isblack)
        {
            if (etb_data != 255)
            {

            }

            byte res = (byte)((etb_data & ETB_COUNT_MASK) + 1);
            var x = etb_data & ETB_BLACK_MASK;
            //if (etb_data & ETB_BLACK_MASK)
            //{
            //    byte_extract_black_pixels(channel, etb_data);
            //}
            //else
            //{
            //    channel->pixels_done += res;
            //}
            if (x != 0) // black
            {
                isblack = true;
                //byte_extract_black_pixels(channel, etb_data);
                return res;
            }
            else // white
            {
                isblack = false;
                return res;
            }
        }
        static void byte_extract_black_pixels(int[] pe_data, byte etb_data, ref int pixels_done)
        {
            uint i;
            int byte_offset = pixels_done >> 3;
            int bit_offset = pixels_done & 0x0007;
            int pixel_cnt = (etb_data & ETB_COUNT_MASK) + 1;
            uint bit_cnt = 0;
            int data = 0xff;

            if (pixel_cnt < 8)
            {
                data = data >> (8 - pixel_cnt);
                data = data << (8 - pixel_cnt);
            }

            pe_data[byte_offset] |= (data >> bit_offset);
            pixel_cnt -= (8 - bit_offset);

            if (pixel_cnt <= 0)
            {
                pixels_done += (etb_data & ETB_COUNT_MASK) + 1;
                return;
            }
            byte_offset++;
            while (pixel_cnt >= 8)
            {
                pe_data[byte_offset] = 0xFF;
                pixel_cnt -= 8;
                byte_offset++;
            }
            if (pixel_cnt != 0)
            {
                pe_data[byte_offset] = 0xff << (8 - pixel_cnt);
            }
            pixels_done += (etb_data & ETB_COUNT_MASK) + 1;
        }
        private void DrawCompressedLine(byte[] data, int count)
        {
            int[] pe_data = new int[720];
            int pixels_done = 0;
            bool isBlack = false;

            for (int i = 0; i < count; i++)
            {
                byte drawCount = (byte)_ParseCompressedByte(data[i], ref isBlack);
                if (isBlack)
                {
                    int offset = pixels_done;
                    byte_extract_black_pixels(pe_data, data[i], ref pixels_done);
                    for (var j = 0; j < pixels_done - offset; j++)
                    {
                        _g.FillRectangle(_blackBush, new Rectangle(_x, _y, _step, _step)); // whatever
                        _x += _step;
                    }
                }
                else
                {
                    pixels_done += drawCount;
                    for (var j = 0; j < drawCount; j++)
                    {
                        _g.FillRectangle(_whiteBush, new Rectangle(_x, _y, _step, _step));
                        _x += _step;
                    }

                }

            }
        }
        public void DrawCompressed(byte data)
        {

            int[] pe_data = new int[720];
            int pixels_done = 0;
            //_bytes[0] = data;
            //data = Utils.Reverse( data);
            //byte count = 0;
            bool isBlack = false;
            byte count = (byte)_ParseCompressedByte(data, ref isBlack);
            byte_extract_black_pixels(pe_data, data, ref pixels_done);

            if (data < 0x7F) // white
            {
                //count = data;
                for (var i = 0; i < count; i++)
                {
                    _g.FillRectangle(_whiteBush, new Rectangle(_x, _y, _step, _step)); // whatever
                    _x += _step;
                }
            }
            else // black
            {
                //count =  (byte)(data - 0x7F - 1);
                for (var i = 0; i < count; i++)
                {
                    _g.FillRectangle(_blackBush, new Rectangle(_x, _y, _step, _step)); // whatever
                    _x += _step;
                }
                //_x++;
            }

        }
        private int _bytesPerLine = 0;
        private void _viewer_OnCommandReaded(PrnCommands command, byte[] data, int count)
        {
            switch (command)
            {
                case PrnCommands.FeedLines:
                    FeedLines(count);
                    return;
                case PrnCommands.LeftMargin:
                    if (count > 0)
                    {
                        int offset = (960 - (_bytesPerLine * 4)) / 16;
                        //_x += _step * count;
                        _leftMargin = (byte)(count * _step);
                    }
                    return;
                case PrnCommands.BytesPerLine:
                    _bytesPerLine = count;

                    return;
                case PrnCommands.EndOfFile:
                    return;
                case PrnCommands.EndJob:

                    //_pic.Image.Save(AppDomain.CurrentDomain.BaseDirectory + "\\data\\pic.png", ImageFormat.Png);
                    return;
                case PrnCommands.PrintComprLine:
                    ResetX();
                    DrawCompressedLine(data, count);
                    //for (int i = 0; i < count; i++)
                    //{
                    //    DrawCompressed(data[i]);
                    //}
                    return;

                case PrnCommands.PrintLine:
                    ResetX();
                    for (int i = 0; i < count; i++)
                    {
                        Draw(data[i]);
                    }
                    return;
                case PrnCommands.Error:
                    _g.DrawString(string.Format("Current line {0}", count), new Font("Tahoma", 24), _blackBush, 0, 120 * _step);
                    _g.FillRectangle(_blueBush, new Rectangle(0, _y, _width, 10)); // whatever
                    return;
                default:
                    return;
            }
        }
        public string MakePng(string prnFile)
        {
            try
            {
                _viewer.Load(prnFile);
                if (_viewer._version == PrnReader.PrnVersion.V3)
                {
                    Init(960, 1400);
                }
                else
                {
                    Init(_viewer.LabelWidth, _viewer.LabelLength);
                }
                if (_viewer._version == PrnReader.PrnVersion.V3)
                {
                    while (!_viewer.EndJob)
                    {
                        _viewer.Read_V3();
                    }

                }
                else
                {
                    while (!_viewer.EndJob)
                    {

                        _viewer.Read();
                    }
                }
                string output = prnFile.Replace(".prn", ".png");
                Image.Save(output);
                return output;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

    }
}

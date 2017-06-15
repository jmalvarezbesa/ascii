







using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;

namespace DoubleBuffer
{
    #region NativeBuffer
    /* 
     * Copyright [2012] [Jeff R Baker] 
 
     * Licensed under the Apache License, Version 2.0 (the "License"); 
     * you may not use this file except in compliance with the License. 
     * You may obtain a copy of the License at     
     *  
     *          http://www.apache.org/licenses/LICENSE-2.0 
     *  
     * Unless required by applicable law or agreed to in writing, software 
     * distributed under the License is distributed on an "AS IS" BASIS, 
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
     * See the License for the specific language governing permissions and 
     * limitations under the License. 
     *  
     * v 1.2.0 
     */

    enum CharAttributes
    {
        FOREGROUND_BLUE = 0x0001,
        FOREGROUND_GREEN = 0x0002,
        FOREGROUND_RED = 0x0004,
        FOREGROUND_INTENSITY = 0x0008,
        BACKGROUND_BLUE = 0x0010,
        BACKGROUND_GREEN = 0x0020,
        BACKGROUND_RED = 0x0040,
        BACKGROUND_INTENSITY = 0x0080
    }

    ///<summary> 
    ///This class allows for a double buffer in Visual C# cmd promt.  
    ///The buffer is persistent between frames. 
    ///</summary> 
    class Buffer
    {
        private int width;
        private int height;
        private int windowWidth;
        private int windowHeight;
        private SafeFileHandle h;
        private CharInfo[] buf;
        private SmallRect rect;

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            private short X;
            private short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)]
            public char UnicodeChar;
            [FieldOffset(0)]
            public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)]
            public CharUnion Char;
            [FieldOffset(2)]
            public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            private short Left;
            private short Top;
            private short Right;
            private short Bottom;
            public void setDrawCord(short l, short t)
            {
                Left = l;
                Top = t;
            }
            public void setWindowSize(short r, short b)
            {
                Right = r;
                Bottom = b;
            }
        }

        /// <summary> 
        /// Consctructor class for the buffer. Pass in the width and height you want the buffer to be. 
        /// </summary> 
        /// <param name="Width"></param> 
        /// <param name="Height"></param> 
        public Buffer(int Width, int Height, int wWidth, int wHeight) // Create and fill in a multideminsional list with blank spaces. 
        {
            if (Width > wWidth || Height > wHeight)
            {
                throw new System.ArgumentException("The buffer width and height can not be greater than the window width and height.");
            }
            h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            width = Width;
            height = Height;
            windowWidth = wWidth;
            windowHeight = wHeight;
            buf = new CharInfo[width * height];
            rect = new SmallRect();
            rect.setDrawCord(0, 0);
            rect.setWindowSize((short)windowWidth, (short)windowHeight);
            Clear();
        }
        /// <summary> 
        /// This method draws any text to the buffer with given color. 
        /// To chance the color, pass in a value above 0. (0 being black text, 15 being white text). 
        /// Put in the starting width and height you want the input string to be. 
        /// </summary> 
        /// <param name="str"></param> 
        /// <param name="Width"></param> 
        /// <param name="Height"></param> 
        /// <param name="attribute"></param> 
        public void Draw(String str, int Width, int Height, short attribute) //Draws the image to the buffer 
        {
            if (Width > windowWidth - 1 || Height > windowHeight - 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (str != null)
            {
                Char[] temp = str.ToCharArray();
                int tc = 0;
                foreach (Char le in temp)
                {
                    buf[(Width + tc) + (Height * width)].Char.AsciiChar = (byte)(int)le; //Height * width is to get to the correct spot (since this array is not two dimensions). 
                    if (attribute != 0)
                        buf[(Width + tc) + (Height * width)].Attributes = attribute;
                    tc++;
                }
            }


        }
        /// <summary> 
        /// Prints the buffer to the screen. 
        /// </summary> 
        public void Print() //Paint the image 
        {
            if (!h.IsInvalid)
            {

                bool b = WriteConsoleOutput(h, buf, new Coord((short)width, (short)height), new Coord((short)0, (short)0), ref rect);
            }
        }
        /// <summary> 
        /// Clears the buffer and resets all character values back to 32, and attribute values to 1. 
        /// </summary> 
        public void Clear()
        {
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i].Attributes = 1;
                buf[i].Char.AsciiChar = 32;
            }
        }
        /// <summary> 
        /// Pass in a buffer to change the current buffer. 
        /// </summary> 
        /// <param name="b"></param> 
        public void setBuf(CharInfo[] b)
        {
            if (b == null)
            {
                throw new System.ArgumentNullException();
            }

            buf = b;
        }

        /// <summary> 
        /// Set the x and y cordnants where you wish to draw your buffered image. 
        /// </summary> 
        /// <param name="x"></param> 
        /// <param name="y"></param> 
        public void setDrawCord(short x, short y)
        {
            rect.setDrawCord(x, y);
        }

        /// <summary> 
        /// Clear the designated row and make all attribues = 1. 
        /// </summary> 
        /// <param name="row"></param> 
        public void clearRow(int row)
        {
            for (int i = (row * width); i < ((row * width + width)); i++)
            {
                if (row > windowHeight - 1)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                buf[i].Attributes = 0;
                buf[i].Char.AsciiChar = 32;
            }
        }

        /// <summary> 
        /// Clear the designated column and make all attribues = 1. 
        /// </summary> 
        /// <param name="col"></param> 
        public void clearColumn(int col)
        {
            if (col > windowWidth - 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            for (int i = col; i < windowHeight * windowWidth; i += windowWidth)
            {
                buf[i].Attributes = 0;
                buf[i].Char.AsciiChar = 32;
            }
        }

        /// <summary> 
        /// This function return the character and attribute at given location. 
        /// </summary> 
        /// <param name="x"></param> 
        /// <param name="y"></param> 
        /// <returns> 
        /// byte character 
        /// byte attribute 
        /// </returns> 
        public KeyValuePair<byte, byte> getCharAt(int x, int y)
        {
            if (x > windowWidth || y > windowHeight)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            return new KeyValuePair<byte, byte>((byte)buf[((y * width + x))].Char.AsciiChar, (byte)buf[((y * width + x))].Attributes);
        }
    }
    #endregion

    public enum BufferForegroundColor
    {
        Black = 0x0000,
        Blue = CharAttributes.FOREGROUND_BLUE | CharAttributes.FOREGROUND_INTENSITY,
        Cyan = CharAttributes.FOREGROUND_BLUE | CharAttributes.FOREGROUND_GREEN | CharAttributes.FOREGROUND_INTENSITY,
        DarkBlue = CharAttributes.FOREGROUND_BLUE,
        DarkCyan = CharAttributes.FOREGROUND_BLUE | CharAttributes.FOREGROUND_GREEN,
        DarkGray = CharAttributes.FOREGROUND_BLUE | CharAttributes.FOREGROUND_GREEN | CharAttributes.FOREGROUND_RED,
        DarkGreen = CharAttributes.FOREGROUND_GREEN,
        DarkMagenta = CharAttributes.FOREGROUND_RED | CharAttributes.FOREGROUND_BLUE,
        DarkRed = CharAttributes.FOREGROUND_RED,
        DarkYellow = CharAttributes.FOREGROUND_RED | CharAttributes.FOREGROUND_GREEN,
        Gray = CharAttributes.FOREGROUND_BLUE | CharAttributes.FOREGROUND_GREEN | CharAttributes.FOREGROUND_RED,
        Green = CharAttributes.FOREGROUND_GREEN | CharAttributes.FOREGROUND_INTENSITY,
        Magenta = CharAttributes.FOREGROUND_RED | CharAttributes.FOREGROUND_BLUE | CharAttributes.FOREGROUND_INTENSITY,
        Red = CharAttributes.FOREGROUND_RED | CharAttributes.FOREGROUND_INTENSITY,
        White = CharAttributes.FOREGROUND_BLUE | CharAttributes.FOREGROUND_GREEN | CharAttributes.FOREGROUND_RED | CharAttributes.FOREGROUND_INTENSITY,
        Yellow = CharAttributes.FOREGROUND_RED | CharAttributes.FOREGROUND_GREEN | CharAttributes.FOREGROUND_INTENSITY,
    }

    public enum BufferBackgroundColor
    {
        Black = 0x0000,
        Blue = CharAttributes.BACKGROUND_BLUE | CharAttributes.BACKGROUND_INTENSITY,
        Cyan = CharAttributes.BACKGROUND_BLUE | CharAttributes.BACKGROUND_GREEN | CharAttributes.BACKGROUND_INTENSITY,
        DarkBlue = CharAttributes.BACKGROUND_BLUE,
        DarkCyan = CharAttributes.BACKGROUND_BLUE | CharAttributes.BACKGROUND_GREEN,
        DarkGray = CharAttributes.BACKGROUND_BLUE | CharAttributes.BACKGROUND_GREEN | CharAttributes.BACKGROUND_RED,
        DarkGreen = CharAttributes.BACKGROUND_GREEN,
        DarkMagenta = CharAttributes.BACKGROUND_RED | CharAttributes.BACKGROUND_BLUE,
        DarkRed = CharAttributes.BACKGROUND_RED,
        DarkYellow = CharAttributes.BACKGROUND_RED | CharAttributes.BACKGROUND_GREEN,
        Gray = CharAttributes.BACKGROUND_BLUE | CharAttributes.BACKGROUND_GREEN | CharAttributes.BACKGROUND_RED,
        Green = CharAttributes.BACKGROUND_GREEN | CharAttributes.BACKGROUND_INTENSITY,
        Magenta = CharAttributes.BACKGROUND_RED | CharAttributes.BACKGROUND_BLUE | CharAttributes.BACKGROUND_INTENSITY,
        Red = CharAttributes.BACKGROUND_RED | CharAttributes.BACKGROUND_INTENSITY,
        White = CharAttributes.BACKGROUND_BLUE | CharAttributes.BACKGROUND_GREEN | CharAttributes.BACKGROUND_RED | CharAttributes.BACKGROUND_INTENSITY,
        Yellow = CharAttributes.BACKGROUND_RED | CharAttributes.BACKGROUND_GREEN | CharAttributes.BACKGROUND_INTENSITY,
    }

    public class DoubleGraphicsBuffer
    {
        private int width;
        private int height;
        private Buffer myBuf;
        private BufferForegroundColor foreground = BufferForegroundColor.White;
        private BufferBackgroundColor background = BufferBackgroundColor.Blue;

        private static BufferForegroundColor ConvertToForegroundColor(ConsoleColor color)
        {
            switch(color)
            {
                case ConsoleColor.Blue:
                    return BufferForegroundColor.Blue;
                case ConsoleColor.Cyan:
                    return BufferForegroundColor.Cyan;
                case ConsoleColor.DarkBlue:
                    return BufferForegroundColor.DarkBlue;
                case ConsoleColor.DarkCyan:
                    return BufferForegroundColor.DarkCyan;
                case ConsoleColor.DarkGray:
                    return BufferForegroundColor.DarkGray;
                case ConsoleColor.DarkGreen:
                    return BufferForegroundColor.DarkGreen;
                case ConsoleColor.DarkMagenta:
                    return BufferForegroundColor.DarkMagenta;
                case ConsoleColor.DarkRed:
                    return BufferForegroundColor.DarkRed;
                case ConsoleColor.DarkYellow:
                    return BufferForegroundColor.DarkYellow;
                case ConsoleColor.Gray:
                    return BufferForegroundColor.Gray;
                case ConsoleColor.Green:
                    return BufferForegroundColor.Green;
                case ConsoleColor.Magenta:
                    return BufferForegroundColor.Magenta;
                case ConsoleColor.Red:
                    return BufferForegroundColor.Red;
                case ConsoleColor.White:
                    return BufferForegroundColor.White;
                case ConsoleColor.Yellow:
                    return BufferForegroundColor.Yellow;
                default:
                    return BufferForegroundColor.Black;
            }
        }

        private static BufferBackgroundColor ConvertToBackgroundColor(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Blue:
                    return BufferBackgroundColor.Blue;
                case ConsoleColor.Cyan:
                    return BufferBackgroundColor.Cyan;
                case ConsoleColor.DarkBlue:
                    return BufferBackgroundColor.DarkBlue;
                case ConsoleColor.DarkCyan:
                    return BufferBackgroundColor.DarkCyan;
                case ConsoleColor.DarkGray:
                    return BufferBackgroundColor.DarkGray;
                case ConsoleColor.DarkGreen:
                    return BufferBackgroundColor.DarkGreen;
                case ConsoleColor.DarkMagenta:
                    return BufferBackgroundColor.DarkMagenta;
                case ConsoleColor.DarkRed:
                    return BufferBackgroundColor.DarkRed;
                case ConsoleColor.DarkYellow:
                    return BufferBackgroundColor.DarkYellow;
                case ConsoleColor.Gray:
                    return BufferBackgroundColor.Gray;
                case ConsoleColor.Green:
                    return BufferBackgroundColor.Green;
                case ConsoleColor.Magenta:
                    return BufferBackgroundColor.Magenta;
                case ConsoleColor.Red:
                    return BufferBackgroundColor.Red;
                case ConsoleColor.White:
                    return BufferBackgroundColor.White;
                case ConsoleColor.Yellow:
                    return BufferBackgroundColor.Yellow;
                default:
                    return BufferBackgroundColor.Black;
            }
        }

        public DoubleGraphicsBuffer(int width, int height, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            this.width = width;
            this.height = height;
            this.myBuf = new Buffer(width, height, width, height);
            this.foreground = DoubleGraphicsBuffer.ConvertToForegroundColor(foregroundColor);
            this.background = DoubleGraphicsBuffer.ConvertToBackgroundColor(backgroundColor);

            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.CursorVisible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Render()
        {
            myBuf.Print();
            myBuf.Clear();
            Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    myBuf.Draw(" ", i, j, (short)background);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Draw(char character, int posX, int posY, ConsoleColor foregroundColor)
        {
            if (posX >= 0 && posX < width && posY >= 0 && posY < height)
            {
                BufferForegroundColor newForeground = DoubleGraphicsBuffer.ConvertToForegroundColor(foregroundColor);
                int color = (short)newForeground | ((short)background);
                myBuf.Draw(character.ToString(), posX, posY, (short)color);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Draw(char character, int posX, int posY)
        {
            if (posX >= 0 && posX < width && posY >= 0 && posY < height)
            {
                int color = (short)foreground | ((short)background);
                myBuf.Draw(character.ToString(), posX, posY, (short)color);
            }
        }

        internal void SetBackgroundColor(ConsoleColor consoleColor)
        {
            throw new NotImplementedException();
        }
    }
}
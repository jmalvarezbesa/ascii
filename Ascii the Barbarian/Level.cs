using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_the_Barbarian
{
    enum MapSymbol
    {
        Player = 'P',
        Gazer = 'G',
        Skeleton = 'S',
        Arrow = 'A',
        VerticalWall = '|',
        HorizontalWall = '-',
        Space = ' ',
        Exit = 'E',
        Rat = 'R',
        Zombie = 'Z'
    }

    /// <summary>
    /// 
    /// </summary>
    public class Level
    {
        List<List<MapSymbol>> map = new List<List<MapSymbol>>();
        int width = 0;
        int height = 0;

        public int Width {  get { return width;  } }
        public int Height { get { return height; } }

        public MapSymbol GetMapSymbol(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
                throw new Exception("Invalid map coordinates.");

            if(y < map.Count)
            {
                if(x < map[y].Count)
                    return map[y][x];
            }

            return MapSymbol.Space;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public void LoadFromFile(string filepath)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(filepath);
            string line = file.ReadLine();
            if (line == null)
                return;

            string[] parts = line.Split(" ".ToCharArray());
            this.width = Int32.Parse(parts[0]);
            this.height = Int32.Parse(parts[1]);

            while ((line = file.ReadLine()) != null)
            {
                List<MapSymbol> mapLine = new List<MapSymbol>();
                foreach(char part in line)
                {
                    mapLine.Add((MapSymbol) part);
                }
                map.Add(mapLine);
            }
            file.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Print()
        {
            for(int i=0; i<map.Count; i++)
            {
                for(int j=0; j<map[i].Count; j++)
                {
                    Console.Write((char) map[i][j]);
                }
                Console.Write('\n');
            }
        }

    }
}

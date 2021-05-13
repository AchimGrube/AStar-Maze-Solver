using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_Maze_Solver
{
    public abstract class CellBase
    {
        protected readonly SpriteBatch spriteBatch;
        protected readonly int size;
        protected readonly Dictionary<string, Rectangle> coords = new();


        public int Row { get; protected set; }
        public int Col { get; protected set; }
        public Dictionary<string, bool> Walls { get; set; }

        public CellBase(int i, int j, int rows, int cols, SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            Row = i;
            Col = j;
            this.spriteBatch = spriteBatch;

            int height = graphics.Viewport.Bounds.Height / rows;
            int width = graphics.Viewport.Bounds.Width / cols;

            size = Math.Min(width, height);

            Walls = new()
            {
                { Constants.MAZE_WALL_TOP, true },
                { Constants.MAZE_WALL_RIGHT, true },
                { Constants.MAZE_WALL_BOTTOM, true },
                { Constants.MAZE_WALL_LEFT, true }
            };

            int y = Row * size;
            int x = Col * size;

            coords.Add(Constants.MAZE_WALL_TOP, new Rectangle(x, y, size, 2));
            coords.Add(Constants.MAZE_WALL_RIGHT, new Rectangle(x + size, y, 2, size));
            coords.Add(Constants.MAZE_WALL_BOTTOM, new Rectangle(x, y + size, size, 2));
            coords.Add(Constants.MAZE_WALL_LEFT, new Rectangle(x, y, 2, size));

            coords.Add(Constants.MAZE_CELL, new Rectangle(x, y, size, size));

            coords.Add(Constants.ASTAR_PATH, new Rectangle(x + (size / 3), y + (size / 3), size / 4, size / 4));
        }

        public abstract void Show();

        protected void Draw(string textureKey, string coordsKey)
        {
            spriteBatch.Draw(Textures.GetTexture(textureKey), coords[coordsKey], Textures.GetColor(textureKey));
        }
    }
}

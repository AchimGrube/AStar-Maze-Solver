using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_Maze_Solver.MazeGen
{
    public class Cell : CellBase
    {
        public bool HasBeenVisited { get; set; }
        public bool IsCurrent { get; set; }

        public Cell(int i, int j, int rows, int cols, SpriteBatch spriteBatch, GraphicsDevice graphics) : base(i, j, rows, cols, spriteBatch, graphics) { }

        public override void Show()
        {
            if (IsCurrent)
                Draw(Constants.MAZE_CURRENT, Constants.MAZE_CELL);
            else if (HasBeenVisited)
                Draw(Constants.MAZE_VISITED, Constants.MAZE_CELL);

            if (Walls[Constants.MAZE_WALL_TOP])
                Draw(Constants.MAZE_LINE, Constants.MAZE_WALL_TOP);
            if (Walls[Constants.MAZE_WALL_RIGHT])
                Draw(Constants.MAZE_LINE, Constants.MAZE_WALL_RIGHT);
            if (Walls[Constants.MAZE_WALL_BOTTOM])
                Draw(Constants.MAZE_LINE, Constants.MAZE_WALL_BOTTOM);
            if (Walls[Constants.MAZE_WALL_LEFT])
                Draw(Constants.MAZE_LINE, Constants.MAZE_WALL_LEFT);
        }
    }
}

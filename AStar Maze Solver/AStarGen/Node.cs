using AStar_Maze_Solver.MazeGen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_Maze_Solver.AStarGen
{
    public class Node : CellBase
    {
        private bool isPath;

        public float F { get; set; }
        public float G { get; set; }
        public float H { get; private set; }
        public HashSet<Node> Neighbors { get; private set; } = new();
        public Node PreviousNode { get; set; }
        public bool IsOpen { get; set; }
        public bool IsClosed { get; set; }
        public bool IsPath
        {
            get { return isPath; }
            set
            {
                IsOpen = false;
                IsClosed = false;
                isPath = value;
            }
        }

        public Node(int i, int j, int rows, int cols, Cell[,] grid, SpriteBatch spriteBatch, GraphicsDevice graphics) : base(i, j, rows, cols, spriteBatch, graphics)
        {
            Dictionary<string, bool> newWalls = new();

            newWalls.Add(Constants.ASTAR_WALL_NORTH, grid[Row, Col].Walls[Constants.MAZE_WALL_TOP]);
            newWalls.Add(Constants.ASTAR_WALL_EAST, grid[Row, Col].Walls[Constants.MAZE_WALL_RIGHT]);
            newWalls.Add(Constants.ASTAR_WALL_SOUTH, grid[Row, Col].Walls[Constants.MAZE_WALL_BOTTOM]);
            newWalls.Add(Constants.ASTAR_WALL_WEST, grid[Row, Col].Walls[Constants.MAZE_WALL_LEFT]);

            //bool northeast = grid[Row, Col].Walls[Constants.MAZE_WALL_TOP] && grid[Row, Col].Walls[Constants.MAZE_WALL_RIGHT];
            //bool southeast = grid[Row, Col].Walls[Constants.MAZE_WALL_RIGHT] && grid[Row, Col].Walls[Constants.MAZE_WALL_BOTTOM];
            //bool southwest = grid[Row, Col].Walls[Constants.MAZE_WALL_BOTTOM] && grid[Row, Col].Walls[Constants.MAZE_WALL_LEFT];
            //bool northwest = grid[Row, Col].Walls[Constants.MAZE_WALL_LEFT] && grid[Row, Col].Walls[Constants.MAZE_WALL_TOP];

            //newWalls.Add(Constants.ASTAR_WALL_NORTHEAST, northeast);
            //newWalls.Add(Constants.ASTAR_WALL_SOUTHEAST, southeast);
            //newWalls.Add(Constants.ASTAR_WALL_SOUTHWEST, southwest);
            //newWalls.Add(Constants.ASTAR_WALL_NORTHWEST, northwest);

            Walls = newWalls;

            coords.Add(Constants.ASTAR_CELL, coords[Constants.MAZE_CELL]);
            coords.Remove(Constants.MAZE_CELL);
        }

        public override void Show()
        {
            if (IsOpen)
                Draw(Constants.ASTAR_OPEN, Constants.ASTAR_CELL);
            else if (IsClosed)
                Draw(Constants.ASTAR_CLOSED, Constants.ASTAR_CELL);
            if (IsPath)
                Draw(Constants.ASTAR_PATH, Constants.ASTAR_PATH);

            if (Walls[Constants.ASTAR_WALL_NORTH])
                Draw(Constants.MAZE_LINE, Constants.MAZE_WALL_TOP);
            if (Walls[Constants.ASTAR_WALL_EAST])
                Draw(Constants.MAZE_LINE, Constants.MAZE_WALL_RIGHT);
            if (Walls[Constants.ASTAR_WALL_SOUTH])
                Draw(Constants.MAZE_LINE, Constants.MAZE_WALL_BOTTOM);
            if (Walls[Constants.ASTAR_WALL_WEST])
                Draw(Constants.MAZE_LINE, Constants.MAZE_WALL_LEFT);
        }

        public void AddNeighbors(Node[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            if (!Walls[Constants.ASTAR_WALL_NORTH])
                Neighbors.Add(Row > 0 ? grid[Row - 1, Col] : null);
            //if (Walls[Constants.ASTAR_WALL_NORTHEAST])
            //    Neighbors.Add(Row > 0 && Col < cols - 1 ? grid[Row - 1, Col + 1] : null);
            if (!Walls[Constants.ASTAR_WALL_EAST])
                Neighbors.Add(Col < cols - 1 ? grid[Row, Col + 1] : null);
            //if (Walls[Constants.ASTAR_WALL_SOUTHEAST])
            //    Neighbors.Add(Row < rows - 1 && Col < cols - 1 ? grid[Row + 1, Col + 1] : null);
            if (!Walls[Constants.ASTAR_WALL_SOUTH])
                Neighbors.Add(Row < rows - 1 ? grid[Row + 1, Col] : null);
            //if (Walls[Constants.ASTAR_WALL_SOUTHWEST])
            //    Neighbors.Add(Row < rows - 1 && Col > 0 ? grid[Row + 1, Col - 1] : null);
            if (!Walls[Constants.ASTAR_WALL_WEST])
                Neighbors.Add(Col > 0 ? grid[Row, Col - 1] : null);
            //if (Walls[Constants.ASTAR_WALL_NORTHWEST])
            //    Neighbors.Add(Row > 0 && Col > 0 ? grid[Row - 1, Col - 1] : null);

            Neighbors.RemoveWhere(n => n == null);
        }

        public void SetH(IHeuristic heuristic) => H = heuristic.GetH();
    }
}

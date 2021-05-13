using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_Maze_Solver.MazeGen
{
    public class Maze
    {
        private readonly SpriteBatch spriteBatch;
        private readonly GraphicsDevice graphics;

        private readonly Random rnd = new();

        private readonly int rows;
        private readonly int cols;
        private Cell[,] grid;
        private readonly List<Cell> allCells = new();
        private readonly Stack<Cell> stack = new();

        private Cell startCell;
        private Cell endCell;
        private Cell currentCell;

        public bool IsFinished { get; private set; }

        public Maze(int rows, int cols, SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            this.rows = rows;
            this.cols = cols;

            grid = new Cell[rows, cols];

            this.spriteBatch = spriteBatch;
            this.graphics = graphics;

            Init();
        }

        private void Init()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Cell cell = new(i, j, rows, cols, spriteBatch, graphics);
                    grid[i, j] = cell;
                    allCells.Add(cell);
                }
            }

            startCell = grid[0, 0];
            startCell.Walls[Constants.MAZE_WALL_LEFT] = false;
            endCell = grid[rows - 1, cols - 1];
            endCell.Walls[Constants.MAZE_WALL_RIGHT] = false;

            currentCell = startCell;
            currentCell.HasBeenVisited = true;
            currentCell.IsCurrent = true;
        }

        public void Run()
        {
            currentCell.IsCurrent = false;
            Cell nextCell = GetRandomNeighbor(currentCell);
            if (nextCell != null)
            {
                stack.Push(nextCell);

                nextCell.HasBeenVisited = true;
                RemoveWalls(currentCell, nextCell);

                currentCell = nextCell;
                currentCell.IsCurrent = true;
            }
            else if (stack.Count > 0)
            {
                currentCell = stack.Pop();
                currentCell.IsCurrent = true;
            }

            if (allCells.All(c => c.HasBeenVisited))
            {
                currentCell.IsCurrent = false;
                IsFinished = true;
            }
        }

        public void Generate()
        {
            while (!IsFinished)
                Run();
        }

        public Cell[,] GetGrid() => grid;

        public void Show()
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    grid[i, j].Show();
        }

        private Cell GetRandomNeighbor(Cell cell)
        {
            List<Cell> neighbors = new();

            int y = cell.Row;
            int x = cell.Col;

            Cell top = y > 0 ? grid[y - 1, x] : null;
            Cell right = x < cols - 1 ? grid[y, x + 1] : null;
            Cell bottom = y < rows - 1 ? grid[y + 1, x] : null;
            Cell left = x > 0 ? grid[y, x - 1] : null;

            if (top != null && !top.HasBeenVisited)
                neighbors.Add(top);
            if (right != null && !right.HasBeenVisited)
                neighbors.Add(right);
            if (bottom != null && !bottom.HasBeenVisited)
                neighbors.Add(bottom);
            if (left != null && !left.HasBeenVisited)
                neighbors.Add(left);

            if (neighbors.Count > 0)
                return neighbors.ElementAt(rnd.Next(neighbors.Count));
            else
                return null;
        }

        private static void RemoveWalls(Cell current, Cell next)
        {
            int x = current.Row - next.Row;
            int y = current.Col - next.Col;

            if (x == 1)
            {
                current.Walls[Constants.MAZE_WALL_TOP] = false;
                next.Walls[Constants.MAZE_WALL_BOTTOM] = false;
            }
            else if (x == -1)
            {
                current.Walls[Constants.MAZE_WALL_BOTTOM] = false;
                next.Walls[Constants.MAZE_WALL_TOP] = false;
            }

            if (y == 1)
            {
                current.Walls[Constants.MAZE_WALL_LEFT] = false;
                next.Walls[Constants.MAZE_WALL_RIGHT] = false;
            }
            else if (y == -1)
            {
                current.Walls[Constants.MAZE_WALL_RIGHT] = false;
                next.Walls[Constants.MAZE_WALL_LEFT] = false;
            }
        }
    }
}

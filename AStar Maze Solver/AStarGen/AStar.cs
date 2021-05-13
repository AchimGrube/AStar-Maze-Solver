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
    public class AStar
    {
        private readonly SpriteBatch spriteBatch;
        private readonly GraphicsDevice graphics;

        private readonly int rows;
        private readonly int cols;
        private Node[,] grid;

        private readonly List<Node> openList = new();
        private readonly List<Node> closedList = new();
        private readonly List<Node> uncheckedList = new();
        private Node startNode;
        private Node endNode;
        private Node currentNode;

        public bool IsLoaded { get; private set; }
        public bool IsFinished { get; private set; }

        public AStar(Cell[,] mazeGrid, SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;

            rows = mazeGrid.GetLength(0);
            cols = mazeGrid.GetLength(1);

            grid = ConvertMazeGrid(mazeGrid);

            Init();
            IsLoaded = true;
        }

        private void Init()
        {
            if (IsLoaded)
                return;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    Node node = grid[i, j];
                    uncheckedList.Add(node);
                    grid[i, j] = node;
                }

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    grid[i, j].AddNeighbors(grid);

            startNode = grid[0, 0];
            endNode = grid[rows - 1, cols - 1];

            openList.Add(startNode);
            startNode.IsOpen = true;
            startNode.IsPath = true;
        }

        private Node[,] ConvertMazeGrid(Cell[,] mazeGrid)
        {
            Node[,] newGrid = new Node[rows, cols];

            foreach (Cell mazeCell in mazeGrid)
                newGrid[mazeCell.Row, mazeCell.Col] = new Node(mazeCell.Row, mazeCell.Col, rows, cols, mazeGrid, spriteBatch, graphics);

            return newGrid;
        }

        public void Run()
        {
            if (openList.Count > 0)
            {
                int lowestIndex = 0;
                lowestIndex = openList.FindIndex(n => n.F == openList.Min(n => n.F));

                currentNode = openList.ElementAt(lowestIndex);
                uncheckedList.Remove(currentNode);

                if (currentNode == endNode)
                {
                    IsFinished = true;
                    return;
                }

                openList.Remove(currentNode);
                currentNode.IsOpen = false;
                closedList.Add(currentNode);
                currentNode.IsClosed = true;

                HashSet<Node> neighbors = currentNode.Neighbors;

                for (int i = 0; i < neighbors.Count; i++)
                {
                    Node neighborNode = neighbors.ElementAt(i);

                    if (!closedList.Contains(neighborNode))
                    {
                        float tempG = currentNode.G + 1;

                        if (openList.Contains(neighborNode))
                        {
                            if (tempG < neighborNode.G)
                            {
                                neighborNode.G = tempG;
                            }
                        }
                        else
                        {
                            neighborNode.G = tempG;
                            openList.Add(neighborNode);
                            neighborNode.IsOpen = true;
                        }

                        neighborNode.SetH(new HeuristicDist(neighborNode, endNode));
                        //neighborNode.SetH(new HeuristicNone());
                        neighborNode.F = neighborNode.G + neighborNode.H;
                        neighborNode.PreviousNode = currentNode;
                    }
                }
            }
            else
                return;
        }

        public void Generate()
        {
            while (!IsFinished)
                Run();
        }

        public void Show()
        {
            //foreach (Node node in openList)
            //    node.Show();

            //foreach (Node node in closedList)
            //    node.Show();

            Node temp = currentNode;
            while (temp != null)
            {
                temp.IsPath = true;
                temp.Show();
                temp.IsPath = false;
                temp = temp.PreviousNode;
            }
            startNode.IsPath = true;
            startNode.Show();
        }
    }
}

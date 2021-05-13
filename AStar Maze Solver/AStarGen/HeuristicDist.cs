using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_Maze_Solver.AStarGen
{
    public class HeuristicDist : IHeuristic
    {
        private readonly Node a;
        private readonly Node b;

        public HeuristicDist(Node a, Node b)
        {
            this.a = a;
            this.b = b;
        }

        public float GetH() => Vector2.Distance(new Vector2(a.Row, a.Col), new Vector2(b.Row, b.Col));
    }
}

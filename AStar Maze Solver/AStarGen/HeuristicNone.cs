using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_Maze_Solver.AStarGen
{
    public class HeuristicNone : IHeuristic
    {
        public float GetH() => 0;
    }
}

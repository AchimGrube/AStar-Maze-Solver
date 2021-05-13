using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_Maze_Solver.AStarGen
{
    public interface IHeuristic
    {
        float GetH();
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStar_Maze_Solver
{
    public static class Textures
    {
        private static readonly Dictionary<string, Texture2D> textures = new();
        private static readonly Dictionary<string, Color> colors = new();
        private static GraphicsDevice _graphics;

        public static void Init(GraphicsDevice graphics)
        {
            _graphics = graphics;

            CreateTexture(Constants.MAZE_CELL, Color.White);

            CreateTexture(Constants.MAZE_LINE, Color.Black);
            CreateTexture(Constants.MAZE_VISITED, Color.Purple);
            CreateTexture(Constants.MAZE_CURRENT, Color.Magenta);

            CreateTexture(Constants.ASTAR_CELL, Color.White);
            CreateTexture(Constants.ASTAR_CLOSED, Color.Red);
            CreateTexture(Constants.ASTAR_OPEN, Color.Green);
            CreateTexture(Constants.ASTAR_SEARCH, Color.Yellow);
            CreateTexture(Constants.ASTAR_PATH, Color.Cyan);
        }

        private static void CreateTexture(string key, Color color)
        {
            Texture2D texture = new(_graphics, 1, 1);
            texture.SetData(new[] { color });
            textures.Add(key, texture);

            colors.Add(key, color);
        }

        public static Texture2D GetTexture(string key) => textures[key];

        public static Color GetColor(string key) => colors[key];
    }
}

using AStar_Maze_Solver.AStarGen;
using AStar_Maze_Solver.MazeGen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace AStar_Maze_Solver
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private AStar aStar;
        private Maze maze;

        const int rows = 36;
        const int cols = 64;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            Window.Title = "A* Maze Solver";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Textures.Init(graphics.GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                Reset();

            // TODO: Add your update logic here

            if (maze == null)
                maze = new(rows, cols, spriteBatch, graphics.GraphicsDevice);

            if (!maze.IsFinished)
                maze.Run();

            if (maze.IsFinished && aStar == null)
                aStar = new(maze.GetGrid(), spriteBatch, graphics.GraphicsDevice);

            if (maze.IsFinished && aStar != null)
                aStar.Run();

            if (aStar != null && aStar.IsFinished)
                Reset();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (maze != null && !maze.IsFinished)
                maze.Show();

            if (aStar != null && maze.IsFinished)
            {
                maze.Show();
                aStar.Show();
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void Reset()
        {
            maze = null;
            aStar = null;
        }
    }
}

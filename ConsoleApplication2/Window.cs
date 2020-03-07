using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace ConsoleApplication2
{
    class Window
    {
        [STAThread]
        public static void Main()
        {

            const int windowWidth = 800;
            const int windowHeight = 800;
         
           
            SquareGrid square = new SquareGrid(windowWidth, windowHeight, 50, 100);     
            

            using (var game = new GameWindow())
            {
                game.Load += (sender, e) => { game.VSync = VSyncMode.On; game.Width = windowWidth; game.Height = windowHeight; };

                game.Resize += (sender, e) => { GL.Viewport(0, 0, game.Width, game.Height); };

                game.Title = "Conway's Game of Life";

                game.UpdateFrame += (sender, e) =>
                {
                    //if (game.Keyboard[Key.N])

                    square.update();                                     
                    

                    if (game.Keyboard[Key.R])
                        square.createGrid(true);


                    if (game.Keyboard[Key.Escape])
                        game.Exit();
                };

                game.RenderFrame += (sender, e) =>
                {                    
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    GL.Ortho(0, windowHeight, windowWidth, 0, 0, 1);
                    
                    square.drawGrid();

                    game.SwapBuffers();                    
                };

                game.Run(5, 10);                       
            }
        }
    }
}

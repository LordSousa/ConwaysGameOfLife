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
    class SquareGrid
    {
        public Color aliveColor;
        public Color deadColor;

        public int windowWidth;
        public int windowHeight;
        public int numberOfSquareWidth;
        public int numberOfSquareHeight;
        public int squareSide;
        
        Square[,] grid;

        public Color currentColor;
        

        public SquareGrid(int windowWidthIN, int windowHeightIN, int numberOfSquaresWidthIN, int numberOfSquaresHeightIN)
        {
            windowWidth = windowWidthIN;
            windowHeight = windowHeightIN;

            numberOfSquareWidth = numberOfSquaresWidthIN;
            numberOfSquareHeight = numberOfSquaresHeightIN;

            squareSide = windowWidth / numberOfSquareWidth;    
      
            aliveColor = Color.Green;
            deadColor = Color.Black;            

            grid = new Square[numberOfSquareWidth, numberOfSquareHeight];
            createGrid(false);
        }

        public void createGrid(bool alive)
        {
            Random r = new Random();

            for (int x = 0; x < numberOfSquareWidth; x++)
            {
                for (int y = 0; y < numberOfSquareHeight; y++)
                {                    
                    int life = r.Next(0,9);
                    
                    if (life == 1 && alive)
                        grid[x,y] = new Square(new Point(x*squareSide, y*squareSide), true);
                    else
                        grid[x,y] = new Square(new Point(x*squareSide, y*squareSide), false);
                }
            }
        }

        public void drawGrid()
        {
            for (int x = 0; x < numberOfSquareWidth; x++)
            {
                for (int y = 0; y < numberOfSquareHeight; y++)
                {

                    if (grid[x, y].life)
                        currentColor = aliveColor;
                    else
                        currentColor = deadColor;

                    drawSquare(grid[x,y].ponto.X, grid[x,y].ponto.Y, squareSide);
                }
            }
        }

        public void update()
        {
            Square[,] newGrid = new Square[numberOfSquareWidth, numberOfSquareHeight];

            for (int y = 0; y < numberOfSquareHeight; y++)
            {
                for (int x = 0; x < numberOfSquareWidth; x++)
                {
                    int aliveNeighbours = 0;

                    if (x - 1 >= 0 && y - 1 >= 0)
                        if (grid[x - 1, y - 1].life)
                            aliveNeighbours++;
                    
                    if (y - 1 >= 0)
                        if(grid[x, y - 1].life)
                            aliveNeighbours++;                    

                    if (x + 1 < numberOfSquareWidth && y - 1 >= 0)
                        if(grid[x + 1, y - 1].life)
                            aliveNeighbours++;                    

                    if (x - 1 >= 0)
                        if(grid[x - 1, y].life)
                            aliveNeighbours++;                  

                    if (x + 1 < numberOfSquareWidth)
                        if(grid[x + 1, y].life)
                            aliveNeighbours++;
                    
                    if (x - 1 >= 0 && y + 1 < numberOfSquareHeight)
                        if(grid[x - 1, y + 1].life)
                            aliveNeighbours++;                    

                    if (y + 1 < numberOfSquareHeight)
                        if(grid[x, y + 1].life)
                            aliveNeighbours++;                    

                    if (x + 1 < numberOfSquareWidth && y + 1 < numberOfSquareHeight)
                        if(grid[x + 1, y + 1].life)
                            aliveNeighbours++;                    
                    
                    int numberOfAliveCells = aliveNeighbours;
                    int numberOfDeadCells = 8 - aliveNeighbours;

                    //Todas as celulas vivas com menos de 2 vizinhos vivos, morre
                    newGrid[x, y] = new Square(grid[x, y].ponto, grid[x, y].life);

                    if (grid[x, y].life && numberOfAliveCells < 2)
                    {                        
                        newGrid[x, y].life = false;
                    } //Todas as celulas vivas com 2 ou 3 vizinhos mantem-se viva
                    else if (grid[x, y].life && (numberOfAliveCells == 2 || numberOfAliveCells == 3))
                    {
                        newGrid[x, y].life = true;
                    }//Todas as celulas vivas com mais de 3 vizinhos morre
                    else if (grid[x, y].life && numberOfAliveCells > 3)
                    {
                        newGrid[x, y].life = false;
                    }//Todas as celulas mortas com com 3 vizinhos vivos vive
                    else if (!grid[x, y].life && numberOfAliveCells == 3)
                    {
                        newGrid[x, y].life = true;
                    }

                }            
            }

            grid = newGrid;            
        }
    

        public void drawSquare(int x, int y, int size)
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(currentColor);
            GL.Vertex2(x, y);
            GL.Color3(currentColor);
            GL.Vertex2(x + size, y);
            GL.Color3(currentColor);
            GL.Vertex2(x + size, y + size);
            GL.Color3(currentColor);
            GL.Vertex2(x, y + size);

            GL.End();
        }
    }
}

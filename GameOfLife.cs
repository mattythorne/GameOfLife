/*
 * Any live cell with fewer than two live neighbours dies, as if caused by under-population.
 * Any live cell with two or three live neighbours lives on to the next generation.
 * Any live cell with more than three live neighbours dies, as if by overcrowding.
 * Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
 * 
**/
using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApplication1
{
    public class GameOfLife
    {
        private bool[,] gameMap = new bool[1, 1];
        

        // Zero argument constructor
        public GameOfLife()
        {
        }

        // Constructor set gamemap size
        public GameOfLife(int gameMapWidth, int gameMapHeight)
        {
            setGameMapSize(gameMapWidth, gameMapHeight);
        }

        // Set the gamemap size
        public void setGameMapSize(int gameMapWidth, int gameMapHeight)
        {
            // Create the game map
            gameMap = new bool[gameMapWidth, gameMapHeight];
        }

        // Create random start point for game
        public void randomiseInitialState(int percentage)
        {
            int x = 0;
            int y = 0;
            Random random = new Random();

            int numberOfCells = (gameMap.Length / 100) * percentage;
            for (int i = 0; i < numberOfCells; i++)
            {                  
                    x = random.Next(0, gameMap.GetUpperBound(0));
                    y = random.Next(0, gameMap.GetUpperBound(1));
                    gameMap[x, y] = true;    
            }
        }

        // Returns a boolean array representing the map
        public bool[,] getGameMap()
        {
            return gameMap;
        }

        // Generate the next state of the map
        public void nextState()
        {
            // Buffer to store the new state
            bool[,] gameMapBuffer;
            gameMapBuffer = new bool[gameMap.GetLength(0), gameMap.GetLength(1)];

            for (int x = 0; x < gameMap.GetLength(0); ++x)
            {
                for (int y = 0; y < gameMap.GetLength(1); ++y)
                {
                    gameMapBuffer[x, y] = cellDecision(gameMap[x, y], countCellNeighbours(x, y)); 
                }
            }

            gameMap = gameMapBuffer;
        }

        // Returns the number of neighbours a given cell has
        private int countCellNeighbours(int x, int y)
        {
            // Define the containing coordinates of the surrounding cells
            int xMin = x-1;
            int xMax = x+1;
            int yMin = y-1;
            int yMax = y+1;

            int count = 0;

            // Adjust values if we are on the boundaries of the map
            if (xMin < 0) xMin = 0;
            if (yMin < 0) yMin = 0;
            if (xMax >= gameMap.GetLength(0)) xMax = gameMap.GetLength(0)-1;
            if (yMax >= gameMap.GetLength(1)) yMax = gameMap.GetLength(1)-1;

            for (int ix = xMin; ix <= xMax; ++ix)
            {
                for (int iy = yMin; iy <= yMax; ++iy)
                {
                    if (gameMap[ix, iy] == true) count += 1;
                }
                
            }
            if (gameMap[x,y]==true) count = count-1;
            return count;
        }

        // Returns a boolean representing whether a cell should live or die in the next generation
        private bool cellDecision(bool cellState, int numberOfNeighbours)
        {
            bool decision = false;
            if (cellState == true)
            {
                switch (numberOfNeighbours)
                {
                    // Any live cell with fewer than two live neighbours dies
                    case 0:
                    case 1:
                        decision = false;
                        break;
                    // Any live cell with two or three live neighbours lives on
                    case 2:
                    case 3:
                        decision = true;
                        break;
                    // Any live cell with more than three live neighbours dies
                    default: 
                        decision = false;
                        break;
                }
            }

            // Any dead cell with exactly three live neighbours becomes a live cell
            if ((cellState == false) && (numberOfNeighbours == 3)) decision = true;
            return decision;
        }

        
        

    }
}

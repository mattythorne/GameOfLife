using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private GameOfLife gameOfLife = new GameOfLife();
        //private int mapWidth = 100;
        //private int mapHeight = 100;
        private int generation = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeForm_Resize(this, EventArgs.Empty);
            
        }

        private void ResizeForm_Resize(object sender, System.EventArgs e)
        {
            Invalidate();
            pictureBox1.Width = this.Width - 28;
            pictureBox1.Height = this.Height - 95;
            
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(toolStripTextBox2.Text);
            if (startToolStripMenuItem.Text == "Start")
            {
                timer1.Enabled = true;
                startToolStripMenuItem.Text = "Stop";
            }
            else
            {
                timer1.Enabled = false;
                startToolStripMenuItem.Text = "Start";
            }
        }

        private void drawGameMap(bool[,] gameMap)
        {
            int x = 0;
            int y = 0;
            Bitmap bmp = new Bitmap(gameMap.GetLength(0), gameMap.GetLength(1));

            for (x = 0; x < gameMap.GetLength(0); x++)
            {
                for (y = 0; y < gameMap.GetLength(1); y++)
                {
                    if (gameMap[x, y] == true) bmp.SetPixel(x, y, Color.Blue);
                }
            }

            
        

            pictureBox1.Image = bmp;
        }

        private void randomiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameOfLife.setGameMapSize(Convert.ToInt32(toolStripTextBox1.Text), Convert.ToInt32(toolStripTextBox1.Text));
            gameOfLife.randomiseInitialState(Convert.ToInt32(toolStripTextBox3.Text));
            drawGameMap(gameOfLife.getGameMap());
            resetGenerationCounter();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            nextIteration();
        }

        private void resetGenerationCounter()
        {
            generation = 0;
            toolStripStatusLabel2.Text = generation.ToString();
        }

        private void incrementGenerationCounter()
        {
            generation += 1;
            toolStripStatusLabel2.Text = generation.ToString();
        }

        private void nextIteration()
        {
            gameOfLife.nextState();
            drawGameMap(gameOfLife.getGameMap());
            incrementGenerationCounter();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            nextIteration();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void speedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(toolStripTextBox2.Text);
        }
    }
}

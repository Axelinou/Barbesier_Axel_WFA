using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_AB
{
    public partial class StartGame : Form
    {
        public StartGame()
        {
            InitializeComponent();
        }

        private void LoadGame(object sender, EventArgs e)
        {
            frmSnake gameWindow = new frmSnake();

            gameWindow.Show();
        }
    }
}

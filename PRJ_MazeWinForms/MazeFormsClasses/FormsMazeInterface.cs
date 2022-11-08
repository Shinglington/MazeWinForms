using System;
using System.Windows.Forms;

using MazeConsole;
namespace PRJ_MazeWinForms.MazeFormsClasses
{
    public enum SolutionVisibility
    {
        None,
        Partial,
        Full
    }

    class FormsMazeInterface 
    {
        private readonly Char[] MOVE_CONTROLS = { 'w', 'd', 's', 'a' };

        private TableLayoutPanel _container;
        private MenuStrip _menuStrip;

        private FormsMaze _maze;
        private FormsPlayer _player;
        private SolutionVisibility _solutionVis;

        public FormsMazeInterface(MazeSettings Settings, TableLayoutPanel Container, MenuStrip MenuStrip)
        {
            _container = Container;
            _menuStrip = MenuStrip;

            _maze = new FormsMaze(Settings, _container);

            _player = new FormsPlayer(_maze);
            _solutionVis = SolutionVisibility.None;
            AddEventsToMenu();

            _container.Parent.Parent.KeyPress += new KeyPressEventHandler(KeyPressed);
            _maze.DisplayForms(_player.CurrentNode);
        }


        private void AddEventsToMenu()
        {
            // hints
            ToolStripMenuItem HintStrip = (ToolStripMenuItem)_menuStrip.Items[(int)MyMenuItem.Hint];
            HintStrip.DropDownItems.Add("Show Full Solution").Click += new EventHandler(ShowFullSolution);
            HintStrip.DropDownItems.Add("Hint").Click += new EventHandler(ShowPartialSolution);
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            Console.WriteLine("{0} pressed", key);
            for(int i = 0; i < 4; i++)
            {
                if (MOVE_CONTROLS[i] == key)
                {
                    MakeMove((Direction)i);
                    break;
                }
            }
        }

        private void MakeMove(Direction direction)
        {
            Node NextNode = null;
            switch (direction)
            {
                case Direction.North:
                    NextNode = _player.CurrentNode.NorthNode;
                    break;
                case Direction.East:
                    NextNode = _player.CurrentNode.EastNode;
                    break;
                case Direction.South:
                    NextNode = _player.CurrentNode.SouthNode;
                    break;
                case Direction.West:
                    NextNode = _player.CurrentNode.WestNode;
                    break;
            }
            if (NextNode != null)
            {
                _player.Move(NextNode);
                _maze.DisplayForms(_player.CurrentNode);
                CheckFinished();
            }
        }

        private bool CheckFinished()
        {
            bool finished = false;
            if (_player.CurrentNode == _maze.EndNode)
            {
                finished = true;
                // Stop movement
                _container.Parent.Parent.KeyPress -= KeyPressed;
                MessageBox.Show("You finished the maze!");
            }

            return finished;
        }



        private void ShowFullSolution(object sender, EventArgs e)
        {
            if (_solutionVis != SolutionVisibility.Full)
            {
                _maze.DisplayForms(null, true);
                _solutionVis = SolutionVisibility.Full;
            }
            else
            {
                MessageBox.Show("Solution already being shown");
            }
        }
        
        private void ShowPartialSolution(object sender, EventArgs e)
        {
            if (_solutionVis != SolutionVisibility.Full)
            {
                _maze.DisplayForms(_player.CurrentNode, false, true) ;
                _solutionVis = SolutionVisibility.Partial;
            }
            else
            {
                MessageBox.Show("Solution already being shown");
            }
        }


    }
}

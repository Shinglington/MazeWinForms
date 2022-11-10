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

        // Forms attributes
        private TableLayoutPanel _container;
        private MenuStrip _menuStrip;

        // Classes
        private WinFormsMaze _maze;
        private Player _player;

        // Enums
        private SolutionVisibility _solutionVis;


        // Finished event
        public event MazeFinishedEventHandler OnMazeFinish;
        public event MazeErrorEventHandler OnMazeError;

        public FormsMazeInterface(MazeSettings Settings, TableLayoutPanel Container, MenuStrip MenuStrip)
        {
            _container = Container;
            _menuStrip = MenuStrip;

            _maze = new WinFormsMaze(Settings, _container);

            _player = new Player(_maze);
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
                // Call finished event
                if (OnMazeFinish != null)
                {
                    OnMazeFinish(this, new MazeFinishedEventArgs(finished, _player));

                }
                else
                {
                    Console.WriteLine("Nothing is assigned to this event");
                }


            }

            return finished;
        }



        private void ShowFullSolution(object sender, EventArgs e)
        {
            if (_solutionVis != SolutionVisibility.Full)
            {
                _player.UseSolution();
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
                _player.UseHint();
                _maze.DisplayForms(_player.CurrentNode, false, true) ;
                _solutionVis = SolutionVisibility.Partial;
            }
            else
            {
                MessageBox.Show("Solution already being shown");
            }
        }


    }


    // Event args for completed maze

    public delegate void MazeFinishedEventHandler(object source, MazeFinishedEventArgs e);

    // Event arguments about player stats when maze finished
    public class MazeFinishedEventArgs : EventArgs 
    {
        private bool _mazeFinished;
        private Player _player;
        public MazeFinishedEventArgs(bool finished, Player player)
        {
            _mazeFinished = finished;
            _player = player;
        }

        public bool Finished { get { return _mazeFinished; } }
        public int MoveCount { get { return _player.MoveCount; } }
        public int HintCount { get { return _player.HintsUsed; } }
        public bool SolutionUsed { get { return _player.SolutionUsed; } }


    }



}

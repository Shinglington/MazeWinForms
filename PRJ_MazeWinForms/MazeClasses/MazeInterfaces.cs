using MazeConsole.MyDataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole
{
    public interface IMazeDisplayer
    {
        void DisplayMaze();

        void DisplaySolution();
    }

    public interface IMazeInterface
    {
        bool TryMove(Direction moveDirection);

        void SetupControls(char[] movementKeys);
    }
}

using PRJ_MazeWinForms.Authentication;
using PRJ_MazeWinForms.Logging;
using System;
using System.Data;
using System.Windows.Forms;

namespace PRJ_MazeWinForms
{
    public partial class HighscoresForm : Form
    {
        // Form to show highscores, either global or personal
        enum viewMode
        {
            global,
            personal
        }

        private MenuForm _menu;
        private DatabaseHelper _dbHelper;
        private viewMode databaseVisibility;

        private DataSet _allScores;
        private DataSet _personalScores;

        private DataSet _allSortedScores;
        private DataSet _personalSortedScores;

        private bool _showingSorted;

        public HighscoresForm(MenuForm menu)
        {
            InitializeComponent();
            _menu = menu;
            _dbHelper = new DatabaseHelper();
            UpdateDataCache();

            SetupEvents();
            ShowAllScores();
            databaseVisibility = viewMode.global;
            _showingSorted = false;

        }

        private void SetupEvents()
        {
            this.FormClosed += new FormClosedEventHandler(ReturnToMenu);
            this.btn_back.Click += new EventHandler(ReturnToMenu);
            this.btn_toggleGlobal.Click += new EventHandler((sender, e) => SwitchVisibilityMode());
            this.btn_sort.Click += new EventHandler((sender, e) => SwitchSortedView());
        }

        private void ReturnToMenu(object sender, EventArgs e)
        {
            this.Hide();
            _menu.Show();
        }

        private void ShowAllScores()
        {
            ShowScores(_allScores);
        }
        private void ShowPersonalScores()
        {
            ShowScores(_personalScores);
        }

        private void ShowSortedScores()
        {
            if (databaseVisibility == viewMode.global)
            {
                ShowScores(_allSortedScores);
            }
            else
            {
                ShowScores(_personalSortedScores);
            }
        }

        private void UpdateSortedScores()
        {
            // all scores
            _allSortedScores = SortDataSet(_allScores);


            // personal scores
            _personalSortedScores = SortDataSet(_personalScores);
        }

        private DataSet SortDataSet(DataSet data)
        {
            DataSet sorted = new DataSet();
            DataTable table = data.Tables[0].Clone();
            // Create array of tuples which will have the original index and the score.
            // We will sort this and then put the rows back in a new sorted datasaet in the sorted order
            (int, int)[] indexScorePairs = new (int, int)[data.Tables[0].Rows.Count];
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                DataRow row = data.Tables[0].Rows[i];
                // 3rd row (so index 2) of data will be the score
                indexScorePairs[i] = (i, (int)row[2]);
            }

            // Sorting
            indexScorePairs = MergeSortData(indexScorePairs);

            // Putting sorted data in table
            for (int i = 0; i < indexScorePairs.Length; i++)
            {
                (int, int) pair = indexScorePairs[i];
                int originalIndex = pair.Item1;
                table.ImportRow(data.Tables[0].Rows[originalIndex]);
            }

            sorted.Tables.Add(table);
            return sorted;
        }


        private (int, int)[] MergeSortData((int, int)[] array)
        {
            if (array.Length <= 1)
                return array;

            (int, int)[] left = new (int, int)[Convert.ToInt32(array.Length / 2)];
            (int, int)[] right = new (int, int)[array.Length - Convert.ToInt32(array.Length / 2)];

            for (int i = 0; i < array.Length; i++)
            {
                if (i < left.Length)
                {
                    left[i] = array[i];
                }
                else
                {
                    right[i - left.Length] = array[i];
                }
            }



            (int, int)[] leftSorted = MergeSortData(left);
            (int, int)[] rightSorted = MergeSortData(right);

            return MergeArrays(leftSorted, rightSorted);
        }
        private (int, int)[] MergeArrays((int, int)[] leftArray, (int, int)[] rightArray)
        {
            (int, int)[] mergedArrays = new (int, int)[leftArray.Length + rightArray.Length];

            int leftIndex = 0;
            int rightIndex = 0;

            while (leftIndex + rightIndex < mergedArrays.Length)
            {
                // check if one of the index markers is the end of the array
                if (leftIndex >= leftArray.Length)
                {
                    mergedArrays[leftIndex + rightIndex] = rightArray[rightIndex];
                    rightIndex += 1;
                }
                else if (rightIndex >= rightArray.Length)
                {
                    mergedArrays[leftIndex + rightIndex] = leftArray[leftIndex];
                    leftIndex += 1;
                }


                // otherwise compare indices
                else if (leftArray[leftIndex].Item2 <= rightArray[rightIndex].Item2)
                {
                    mergedArrays[leftIndex + rightIndex] = leftArray[leftIndex];
                    leftIndex += 1;
                }
                else
                {
                    mergedArrays[leftIndex + rightIndex] = rightArray[rightIndex];
                    rightIndex += 1;
                }
            }
            return mergedArrays;

        }



        private DataSet GetPersonalScores(DataSet data)
        {
            DataSet PersonalScores = new DataSet();
            // Clone() copies the schema (columns)
            DataTable table = data.Tables[0].Clone();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                if ((string)row[1] == _menu.LoginForm.CurrentUser.Username)
                {
                    table.ImportRow(row);
                }
            }
            PersonalScores.Tables.Add(table);
            return PersonalScores;
        }



        private void UpdateDataCache()
        {
            _allScores = _dbHelper.GetAllScores();
            _personalScores = GetPersonalScores(_allScores);
            UpdateSortedScores();
        }

        private void ShowScores(DataSet ds)
        {
            highscoresGrid.DataSource = null;
            highscoresGrid.DataSource = ds.Tables[0];
        }



        private void SwitchVisibilityMode()
        {
            switch (databaseVisibility)
            {
                case viewMode.global:
                    SwitchToPersonalView();
                    break;
                case viewMode.personal:
                    SwitchToGlobalView();
                    break;
                default:
                    LogHelper.ErrorLog("Could not identify viewmode in Highscore form");
                    break;
            }

        }
        private void UpdateScoreView()
        {
            if (_showingSorted)
            {
                ShowSortedScores();

            }
            else
            {
                switch (databaseVisibility)
                {
                    case viewMode.global:
                        ShowAllScores();
                        break;
                    case viewMode.personal:
                        ShowPersonalScores();
                        break;
                }
            }
        }
        private void SwitchToGlobalView()
        {
            databaseVisibility = viewMode.global;
            btn_toggleGlobal.Text = "Switch to personal view";
            UpdateScoreView();
        }

        private void SwitchToPersonalView()
        {
            databaseVisibility = viewMode.personal;
            btn_toggleGlobal.Text = "Switch to global view";
            UpdateScoreView();
        }

        private void SwitchSortedView()
        {
            if (_showingSorted)
            {
                _showingSorted = false;
            }
            else
            {
                _showingSorted = true;
            }
            UpdateScoreView();
        }
    }
}

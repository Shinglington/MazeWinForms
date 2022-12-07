using System;

namespace PRJ_MazeWinForms.Authentication
{
    public class Matrix
    {
        // Class to handle storing of matrices and support for Matrix Multiplication


        private int[,] _matrixArray;
        public Matrix(int[,] matrixArray)
        {
            _matrixArray = matrixArray;
        }

        public int Rows { get { return _matrixArray.GetLength(0); } }
        public int Columns { get { return _matrixArray.GetLength(1); } }


        public int[] GetColumn(int colNum)
        {
            // A row will have the same number of items as there are rows.
            int[] column = new int[Rows];
            for (int i = 0; i < Rows; i++)
            {
                column[i] = _matrixArray[i, colNum];
            }
            return column;
        }

        public int[] GetRow(int rowNum)
        {
            // A row will have the same number of items as there are columns
            int[] row = new int[Columns];
            for (int i = 0; i < Columns; i++)
            {
                row[i] = _matrixArray[rowNum, i];
            }
            return row;

        }

        public int GetItem(int rowNum, int colNum)
        {
            return _matrixArray[rowNum, colNum];
        }


        public int GetDeterminant()
        {
            if (Columns != Rows)
            {
                throw new Exception("Determinant only exists for square matrix");
            }

            int det = 0;
            if (Columns == 2)
            {
                det = (GetItem(0, 0) * GetItem(1, 1)) - (GetItem(0, 1) * GetItem(1, 0));
            }
            else
            {
                Matrix Cofactors = GetCofactors();
                Console.WriteLine(Cofactors.ToString());
                for (int i = 0; i < Rows; i++)
                {
                    det += GetRow(0)[i] * Cofactors.GetRow(0)[i];
                }
            }

            return det;
        }

        public Matrix GetModuloInverse(int Modulo)
        {
            int determinant = GetDeterminant();
            Console.WriteLine(determinant.ToString());
            int inv_det = 0;
            // If using modulo multiplication, the (inverse * determinant)mod M = 1
            for (int x = 1; x < Modulo; x++)
            {
                if ((determinant * x) % Modulo == 1)
                {
                    inv_det = x;
                }
            }

            if (inv_det == 0)
            {
                throw new Exception(this.ToString() + " has no inverse");
            }

            Matrix adjugate = new Matrix(new int[,] { { GetItem(1, 1), -GetItem(0, 1) }, { -GetItem(1, 0), GetItem(0, 0) } });
            if (Rows > 2)
            {
                adjugate = GetCofactors().GetTransposedMatrix();
            }
            return (inv_det * adjugate) % Modulo;
        }

        private Matrix GetCofactors()
        {
            int[,] cofactorArray = new int[Rows, Columns];
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    // Cofactor of element in matrix is the minor multiplied by -1 or 1.
                    // Since the -1 or 1 alternates, (-1)^row+col makes this work
                    cofactorArray[row, col] = GetMinor(row, col) * Convert.ToInt32(Math.Pow(-1, row + col));
                }
            }
            return new Matrix(cofactorArray);
        }

        public Matrix GetTransposedMatrix()
        {
            int[,] matrixArray = new int[Columns, Rows];
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    // transposing swaps the rows and columns around
                    matrixArray[col, row] = GetItem(row, col);
                }
            }
            return new Matrix(matrixArray);
        }

        private int GetMinor(int rowNum, int colNum)
        {
            // The minor is the determinant of a smaller square matrix formed by removing the row and column the item is in
            int[,] minorArray = new int[Rows - 1, Columns - 1];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    // Assigns matrix item to minor array only if item isn't in same row or column
                    // as the item we are finding the minor of.
                    if (r < rowNum && c < colNum)
                        minorArray[r, c] = GetItem(r, c);
                    else if (r < rowNum && c > colNum)
                        minorArray[r, c - 1] = GetItem(r, c);
                    else if (r > rowNum && c < colNum)
                        minorArray[r - 1, c] = GetItem(r, c);
                    else if (r > rowNum && c > colNum)
                        minorArray[r - 1, c - 1] = GetItem(r, c);
                }
            }
            return new Matrix(minorArray).GetDeterminant();
        }


        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.Columns != B.Rows)
            {
                throw new Exception("Number of columns in A must be equal to number of rows in B");
            }


            int[,] arrayProduct = new int[A.Rows, B.Columns];
            for (int r = 0; r < arrayProduct.GetLength(0); r++)
            {
                for (int c = 0; c < arrayProduct.GetLength(1); c++)
                {
                    int newItem = 0;
                    int[] A_Row = A.GetRow(r);
                    int[] B_Col = B.GetColumn(c);
                    for (int i = 0; i < A_Row.Length; i++)
                    {
                        newItem += A_Row[i] * B_Col[i];
                    }
                    arrayProduct[r, c] = newItem;
                }
            }
            return new Matrix(arrayProduct);
        }


        public static Matrix operator *(int x, Matrix A)
        {

            int[,] arrayProduct = new int[A.Rows, A.Columns];
            for (int r = 0; r < arrayProduct.GetLength(1); r++)
            {
                for (int c = 0; c < arrayProduct.GetLength(0); c++)
                {
                    arrayProduct[r, c] = A.GetItem(r, c) * x;
                }
            }
            return new Matrix(arrayProduct);
        }

        public static Matrix operator %(Matrix A, int x)
        {
            int[,] arrayProduct = new int[A.Rows, A.Columns];
            for (int r = 0; r < arrayProduct.GetLength(1); r++)
            {
                for (int c = 0; c < arrayProduct.GetLength(0); c++)
                {
                    arrayProduct[r, c] = mod(A.GetItem(r, c), x);
                }
            }
            return new Matrix(arrayProduct);
        }

        public override string ToString()
        {
            string s = "";
            for (int r = 0; r < Rows; r++)
            {
                s += "[";
                foreach (int i in GetRow(r))
                {
                    s += i.ToString() + " ";
                }
                s += "] ";
            }
            return s;
        }


        private static int mod(int x, int modulo)
        {
            // Need my own mod implementation since c# does mod of negatives strangely
            int remainder = x % modulo;

            while (remainder < 0)
            {
                remainder += modulo;
            }
            return remainder;

        }
    }
}

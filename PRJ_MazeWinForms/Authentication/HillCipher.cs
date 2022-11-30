using PRJ_MazeWinForms.Logging;
using System;

namespace PRJ_MazeWinForms.Authentication
{
    class HillCipher
    {
        private string _alphabet;
        private int _matrixSize;
        private Matrix _encryptMatrix;
        private Matrix _decryptMatrix;
        public HillCipher(string key, string alphabet)
        {
            _alphabet = alphabet;
            _matrixSize = (int)Math.Sqrt(key.Length);
            if (_matrixSize * _matrixSize != key.Length)
            {
                LogHelper.ErrorLog(string.Format("The length of key {0} is not square", key));
                return;
            }

            _encryptMatrix = GetEncryptionMatrix(key);
            _decryptMatrix = GetDecryptionMatrix();
        }


        public string Encrypt(string plaintext)
        {
            while (plaintext.Length % _matrixSize != 0)
            {
                // Pad plaintext with last letter in alphabet so the length is multiple of matrix size.
                plaintext += _alphabet[_alphabet.Length - 1];
            }
            string ciphertext = "";
            for (int i = 0; i < plaintext.Length; i += _matrixSize)
            {
                string substring = "";
                for (int j = 0; j < _matrixSize; j++)
                {
                    substring += plaintext[i + j];
                }
                ciphertext += EncryptSubstring(substring);

            }
            return ciphertext;
        }


        public string Decrypt(string ciphertext)
        {
            if (ciphertext.Length % _matrixSize != 0)
            {
                LogHelper.ErrorLog("ciphertext length is not a multiple of matrix size, so can't decrypt");
                throw new Exception("ciphertext length is not a multiple of matrix size, so can't decrypt");
            }
            string plaintext = "";
            for (int i = 0; i < ciphertext.Length; i += _matrixSize)
            {
                string substring = "";
                for (int j = 0; j < _matrixSize; j++)
                {
                    substring += ciphertext[i + j];
                }
                ciphertext += DecryptSubstring(substring);

            }
            return ciphertext;
        }

        private string EncryptSubstring(string s)
        {
            int[,] textMatrix = new int[_matrixSize, 1];
            for (int j = 0; j < _matrixSize; j++)
            {
                // Populate text matrix with indexes of each char in substring
                textMatrix[j, 0] = GetIntFromChar(s[j]);
            }

            Matrix product = _encryptMatrix * new Matrix(textMatrix);

            string encrypted = "";
            foreach (int i in product.GetColumn(0))
            {
                encrypted += GetCharFromInt(i);
            }

            return encrypted;
        }
        private string DecryptSubstring(string s)
        {
            int[,] textMatrix = new int[_matrixSize, 1];
            for (int j = 0; j < _matrixSize; j++)
            {
                // Populate text matrix with indexes of each char in substring
                textMatrix[j, 0] = GetIntFromChar(s[j]);
            }

            Matrix product = _decryptMatrix * new Matrix(textMatrix);

            string decrypted = "";
            foreach (int i in product.GetColumn(0))
            {
                decrypted += GetCharFromInt(i);
            }

            return decrypted;

        }



        private Matrix GetEncryptionMatrix(string key)
        {
            int[,] matrixArray = new int[_matrixSize, _matrixSize];
            for (int i = 0; i < key.Length; i++)
            {
                int row = i / _matrixSize;
                int column = i % _matrixSize;
                matrixArray[row, column] = GetIntFromChar(key[i]);
            }
            return new Matrix(matrixArray);

        }

        private Matrix GetDecryptionMatrix()
        {

            return _encryptMatrix.GetModuloInverse(_alphabet.Length);
        }

        private int GetIntFromChar(char c)
        {
            return _alphabet.IndexOf(c);
        }

        private char GetCharFromInt(int i)
        {
            return _alphabet[i % _alphabet.Length];
        } 
    }
}

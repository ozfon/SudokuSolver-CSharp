namespace sudoku_solver
{
    public class NodeClass
    {
        public int[,] sudoku = { { 0, 0, 0, 8, 0, 0, 0, 0, 0 },
                                 { 7, 8, 9, 0, 1, 0, 0, 0, 6 },
                                 { 0, 0, 0, 0, 0, 6, 1, 0, 0 },
                                 { 0, 0, 7, 0, 0, 0, 0, 5, 0 },
                                 { 5, 0, 8, 7, 0, 9, 3, 0, 4 },
                                 { 0, 4, 0, 0, 0, 0, 2, 0, 0 },
                                 { 0, 0, 3, 2, 0, 0, 0, 0, 0 },
                                 { 8, 0, 0, 0, 7, 0, 4, 3, 9 },
                                 { 0, 0, 0, 0, 0, 1, 0, 0, 0 }, };

        public List<int> x = new List<int>();
        public List<int> y = new List<int>();

        public int delay = 1;

        public void MatrixScan()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i, j] != 0)
                    {
                        this.x.Add(i);
                        this.y.Add(j);
                    }
                }
            }
        }

        public void MatrixWrite()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(this.sudoku[i, j].ToString() + " ");
                }
                Console.WriteLine();
            }
        }

        public bool CheckStarterCells(int i, int j)
        {
            for (int k = 0; k < this.x.Count; k++)
            {
                if (i == this.x[k] && j == this.y[k])
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckCriteriaMet(int i, int j)
        {

            for (int k = 0; k < 9; k++)
            {
                if (this.sudoku[k, j] == this.sudoku[i, j] && i != k)
                {
                    return false;

                }

                if (this.sudoku[i, k] == this.sudoku[i, j] && j != k)
                {
                    return false;
                }
            }

            int column = j - j % 3;
            int row = i - i % 3;

            for (int k = 0; k < 3; k++)
            {
                for (int l = 0; l < 3; l++)
                {
                    if (this.sudoku[i, j] == this.sudoku[k + row, l + column] && i != k + row && j != l + column)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public (int, int) DecreaseIndex(int i, int j)
        {
            bool flag = true;

            while (flag)
            {
                if (j != 0)
                {
                    j -= 1;
                }
                else
                {
                    i -= 1;
                    j = 8;
                }
                flag = CheckStarterCells(i, j);
            }
            j -= 1;
            return (i, j);
        }

        public void SudokuSolve()
        {
            MatrixWrite();
            MatrixScan();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    bool StarterFlag = CheckStarterCells(i, j);

                    if (!StarterFlag)
                    {
                        bool CheckCorrect = false;
                        while (!(this.sudoku[i, j] == 10 || CheckCorrect))
                        {
                            this.sudoku[i, j] += 1;
                            Console.Clear();
                            MatrixWrite();
                            Thread.Sleep(delay);

                            CheckCorrect = CheckCriteriaMet(i, j);
                        }

                        if (this.sudoku[i, j] == 10)
                        {
                            this.sudoku[i, j] = 0;
                            Console.Clear();
                            MatrixWrite();
                            Thread.Sleep(delay);
                            (i, j) = DecreaseIndex(i, j);
                        }
                    }
                }
            }
        }

    }
}
﻿using System.Collections.Generic;

namespace Q_Learning
{
    /// <summary>
    /// Samples of mazes.
    /// </summary>
    public static class MazeExamples
    {
        public static IEnumerable<MazeModel> GetMazeModels() => new List<MazeModel>
        {
            Example_1(),
            Example_2(),
            Example_3()
        };

        private static MazeModel Example_1()
        {
            var maze = new MazeModel();

            maze.Id = 1;
            maze.QuantityOfSquares = 30;
            maze.Start = 24;
            maze.Goal = 29;
            maze.QuantityOfColumns = 6;
            maze.QuantityOfRows = 5;
            maze.SizeOfCell = 40;
            maze.Matrix = CreateMatrix();
            maze.Reward = CreateReward();
            maze.Quality = CreateQuality(maze.QuantityOfSquares);

            int[][] CreateMatrix()
            {
                int[][] FT = new int[maze.QuantityOfSquares][];
                for (int i = 0; i < maze.QuantityOfSquares; ++i) FT[i] = new int[maze.QuantityOfSquares];
                FT[0][1] = FT[0][6] = FT[1][0] = FT[1][2] = FT[2][1] = 1;
                FT[2][8] = FT[2][3] = FT[3][2] = FT[3][9] = 1;
                FT[4][5] = FT[4][10] = FT[5][4] = FT[5][11] = 1;
                FT[6][0] = FT[6][12] = FT[7][8] = FT[8][7] = FT[8][2] = FT[8][14] = 1;
                FT[9][3] = FT[9][10] = FT[9][15] = FT[10][4] = 1;
                FT[10][9] = FT[11][5] = FT[11][17] = FT[12][6] = 1;
                FT[12][13] = FT[12][18] = FT[13][12] = FT[13][19] = FT[14][8] = 1;
                FT[15][9] = FT[15][21] = FT[16][17] = FT[17][16] = FT[17][11] = 1;
                FT[17][23] = FT[18][12] = FT[18][24] = FT[19][13] = FT[20][26] = FT[20][21] = 1;
                FT[21][20] = FT[21][15] = FT[22][28] = FT[23][17] = FT[24][18] = 1;
                FT[25][26] = FT[26][25] = FT[26][27] = FT[26][20] = FT[27][26] = FT[27][28] = 1;
                FT[28][22] = FT[28][27] = FT[28][29] = FT[29][28] = 1;
                FT[29][29] = 1;  // Goal
                return FT;
            };

            double[][] CreateReward()
            {
                double[][] R = new double[maze.QuantityOfSquares][];
                for (int i = 0; i < maze.QuantityOfSquares; ++i) R[i] = new double[maze.QuantityOfSquares];
                R[0][1] = R[0][6] = R[1][0] = R[1][2] = R[2][1] = -0.01;
                R[2][8] = R[2][3] = R[3][2] = R[3][9] = -0.01;
                R[4][5] = R[4][10] = R[5][4] = R[5][11] = -0.01;
                R[6][0] = R[6][12] = R[7][8] = R[8][7] = R[8][2] = R[8][14] = -0.01;
                R[9][3] = R[9][10] = R[9][15] = R[10][4] = -0.01;
                R[10][9] = R[11][5] = R[11][17] = R[12][6] = -0.01;
                R[12][13] = R[12][18] = R[13][12] = R[13][19] = R[14][8] = -0.01;
                R[15][9] = R[15][21] = R[16][17] = R[17][16] = R[17][11] = -0.01;
                R[17][23] = R[18][12] = R[18][24] = R[19][13] = R[20][26] = R[20][21] = -0.01;
                R[21][20] = R[21][15] = R[22][28] = R[23][17] = R[24][18] = -0.01;
                R[25][26] = R[26][25] = R[26][27] = R[26][20] = R[27][26] = R[27][28] = -0.01;
                R[28][22] = R[28][27] = R[28][29] = -0.01;
                R[28][29] = 10000.0;  // Goal
                return R;
            }

            return maze;
        }

        private static MazeModel Example_2()
        {
            var maze = new MazeModel();

            maze.Id = 2;
            maze.QuantityOfSquares = 60;
            maze.Start = 0;
            maze.Goal = 9;
            maze.QuantityOfColumns = 10;
            maze.QuantityOfRows = 6;
            maze.SizeOfCell = 40;
            maze.Matrix = CreateMatrix();
            maze.Reward = CreateReward();
            maze.Quality = CreateQuality(maze.QuantityOfSquares);

            int[][] CreateMatrix()
            {
                int[][] FT = new int[maze.QuantityOfSquares][];
                for (int i = 0; i < maze.QuantityOfSquares; ++i) FT[i] = new int[maze.QuantityOfSquares];
                FT[0][1] = FT[1][0] = FT[1][2] = FT[2][1] = FT[2][12] = FT[3][4] = 1;
                FT[3][13] = FT[4][3] = FT[4][5] = FT[5][4] = FT[5][6] = 1;
                FT[6][5] = FT[6][7] = FT[7][6] = FT[8][18] = FT[9][19] = FT[10][20] = 1;
                FT[11][12] = FT[12][11] = FT[12][2] = FT[12][13] = FT[13][12] = 1;
                FT[13][3] = FT[13][23] = FT[14][15] = FT[14][24] = FT[15][14] = 1;
                FT[15][16] = FT[15][25] = FT[16][15] = FT[16][17] = FT[17][16] = 1;
                FT[17][18] = FT[18][17] = FT[18][8] = FT[19][9] = FT[19][29] = 1;
                FT[20][10] = FT[20][21] = FT[21][20] = FT[21][22] = FT[22][21] = 1;
                FT[22][23] = FT[22][32] = FT[23][22] = FT[23][13] = FT[23][24] = 1;
                FT[24][23] = FT[24][14] = FT[24][34] = FT[25][15] = FT[25][26] = 1;
                FT[25][35] = FT[26][25] = FT[26][27] = FT[27][26] = FT[27][28] = 1;
                FT[28][27] = FT[28][38] = FT[29][19] = FT[29][39] = FT[30][31] = 1;
                FT[30][40] = FT[31][30] = FT[31][32] = FT[32][31] = FT[32][22] = 1;
                FT[32][42] = FT[33][34] = FT[34][33] = FT[34][24] = FT[34][44] = 1;
                FT[35][25] = FT[35][45] = FT[36][37] = FT[36][46] = FT[37][36] = 1;
                FT[37][47] = FT[38][28] = FT[38][48] = FT[39][29] = FT[39][49] = 1;
                FT[40][30] = FT[40][50] = FT[41][51] = FT[42][32] = FT[42][52] = 1;
                FT[43][53] = FT[44][34] = FT[44][54] = FT[45][35] = FT[45][55] = 1;
                FT[46][36] = FT[47][37] = FT[47][57] = FT[48][38] = FT[48][49] = 1;
                FT[49][48] = FT[49][39] = FT[50][40] = FT[50][51] = FT[51][50] = 1;
                FT[51][41] = FT[52][42] = FT[52][53] = FT[53][52] = FT[53][43] = 1;
                FT[54][44] = FT[55][45] = FT[55][56] = FT[56][55] = FT[56][57] = 1;
                FT[57][56] = FT[57][47] = FT[57][58] = FT[58][57] = FT[58][59] = 1;
                FT[59][58] = 1;
                FT[9][9] = 1; //Goal
                return FT;
            };

            double[][] CreateReward()
            {
                double[][] R = new double[maze.QuantityOfSquares][];
                for (int i = 0; i < maze.QuantityOfSquares; ++i) R[i] = new double[maze.QuantityOfSquares];
                R[0][1] = R[1][2] = R[2][1] = R[2][12] = R[3][4] = -0.01;
                R[3][13] = R[4][3] = R[4][5] = R[5][4] = R[5][6] = -0.01;
                R[6][5] = R[6][7] = R[7][6] = R[8][18] = R[10][20] = -0.01;
                R[11][12] = R[12][11] = R[12][2] = R[12][13] = R[13][12] = -0.01;
                R[13][3] = R[13][23] = R[14][15] = R[14][24] = R[15][14] = -0.01;
                R[15][16] = R[15][25] = R[16][15] = R[16][17] = R[17][16] = -0.01;
                R[17][18] = R[18][17] = R[18][8] = R[19][9] = R[19][29] = -0.01;
                R[20][10] = R[20][21] = R[21][20] = R[21][22] = R[22][21] = -0.01;
                R[22][23] = R[22][32] = R[23][22] = R[23][13] = R[23][24] = -0.01;
                R[24][23] = R[24][14] = R[24][34] = R[25][15] = R[25][26] = -0.01;
                R[25][35] = R[26][25] = R[26][27] = R[27][26] = R[27][28] = -0.01;
                R[28][27] = R[28][38] = R[29][19] = R[29][39] = R[30][31] = -0.01;
                R[30][40] = R[31][30] = R[31][32] = R[32][31] = R[32][22] = -0.01;
                R[32][42] = R[33][34] = R[34][33] = R[34][24] = R[34][44] = -0.01;
                R[35][25] = R[35][45] = R[36][37] = R[36][46] = R[37][36] = -0.01;
                R[37][47] = R[38][28] = R[38][48] = R[39][29] = R[39][49] = -0.01;
                R[40][30] = R[40][50] = R[41][51] = R[42][32] = R[42][52] = -0.01;
                R[43][53] = R[44][34] = R[44][54] = R[45][35] = R[45][55] = -0.01;
                R[46][36] = R[47][37] = R[47][57] = R[48][38] = R[48][49] = -0.01;
                R[49][48] = R[49][39] = R[50][40] = R[50][51] = R[51][50] = -0.01;
                R[51][41] = R[52][42] = R[52][53] = R[53][52] = R[53][43] = -0.01;
                R[54][44] = R[55][45] = R[55][56] = R[56][55] = R[56][57] = -0.01;
                R[57][56] = R[57][47] = R[57][58] = R[58][57] = R[58][59] = -0.01;
                R[59][58] = -0.1;
                R[19][9] = 100000; //Goal
                return R;
            }

            return maze;
        }

        private static MazeModel Example_3()
        {
            var maze = new MazeModel();

            maze.Id = 3;
            maze.QuantityOfSquares = 48;
            maze.Start = 0;
            maze.Goal = 47;
            maze.QuantityOfColumns = 8;
            maze.QuantityOfRows = 6;
            maze.SizeOfCell = 40;
            maze.Matrix = CreateMatrix();
            maze.Reward = CreateReward();
            maze.Quality = CreateQuality(maze.QuantityOfSquares);

            int[][] CreateMatrix()
            {
                int[][] FT = new int[maze.QuantityOfSquares][];
                for (int i = 0; i < maze.QuantityOfSquares; ++i) FT[i] = new int[maze.QuantityOfSquares];
                FT[0][8] = FT[1][2] = FT[1][9] = FT[2][1] = FT[2][10] = 1;
                FT[3][11] = FT[3][4] = FT[4][3] = FT[4][12] = 1;
                FT[5][13] = FT[5][6] = FT[6][5] = FT[6][7] = 1;
                FT[7][6] = FT[7][15] = FT[8][0] = FT[8][16] = FT[9][10] = 1;
                FT[9][17] = FT[9][1] = FT[10][9] = FT[10][18] = 1;
                FT[10][2] = FT[11][19] = FT[11][3] = FT[11][12] = 1;
                FT[12][11] = FT[12][4] = FT[12][20] = FT[13][5] = FT[13][21] = 1;
                FT[14][15] = FT[14][22] = FT[15][14] = FT[15][7] = FT[15][23] = 1;
                FT[16][8] = FT[16][17] = FT[16][24] = FT[17][16] = FT[17][9] = 1;
                FT[17][18] = FT[18][17] = FT[18][19] = FT[18][10] = FT[19][18] = 1;
                FT[19][20] = FT[19][27] = FT[20][12] = FT[20][19] = FT[20][28] = 1;
                FT[21][13] = FT[21][29] = FT[22][14] = FT[22][30] = FT[23][15] = 1;
                FT[23][31] = FT[24][16] = FT[24][32] = FT[25][26] = FT[25][33] = 1;
                FT[26][27] = FT[26][25] = FT[27][26] = FT[27][28] = FT[27][19] = 1;
                FT[27][35] = FT[28][27] = FT[28][20] = FT[28][36] = FT[29][21] = 1;
                FT[30][22] = FT[30][38] = FT[31][23] = FT[31][39] = FT[32][24] = 1;
                FT[32][40] = FT[33][25] = FT[34][35] = FT[34][42] = FT[35][34] = 1;
                FT[35][27] = FT[35][43] = FT[36][28] = FT[36][37] = 1;
                FT[37][36] = FT[37][38] = FT[38][37] = FT[38][30] = 1;
                FT[38][46] = FT[39][31] = FT[39][47] = FT[40][32] = FT[41][42] = 1;
                FT[41][33] = FT[42][41] = FT[42][43] = FT[42][34] = FT[43][42] = 1;
                FT[43][44] = FT[43][35] = FT[44][45] = FT[44][43] = 1;
                FT[45][44] = FT[46][38] = FT[47][39] = 1;
                FT[47][47] = 1;  // Goal
                return FT;
            };

            double[][] CreateReward()
            {
                double[][] R = new double[maze.QuantityOfSquares][];
                for (int i = 0; i < maze.QuantityOfSquares; ++i) R[i] = new double[maze.QuantityOfSquares];
                R[0][8] = R[1][2] = R[1][9] = R[2][1] = R[2][10] = -0.01;
                R[3][11] = R[3][4] = R[4][3] = R[4][12] = -0.01;
                R[5][13] = R[5][6] = R[6][5] = R[6][7] = -0.01;
                R[7][6] = R[7][15] = R[8][0] = R[8][16] = R[9][10] = -0.01;
                R[9][17] = R[9][1] = R[10][9] = R[10][18] = -0.01;
                R[10][2] = R[11][19] = R[11][3] = R[11][12] = -0.01;
                R[12][11] = R[12][4] = R[12][20] = R[13][5] = R[13][21] = -0.01;
                R[14][15] = R[14][22] = R[15][14] = R[15][7] = R[15][23] = -0.01;
                R[16][8] = R[16][17] = R[16][24] = R[17][16] = R[17][9] = -0.01;
                R[17][18] = R[18][17] = R[18][19] = R[18][10] = R[19][18] = -0.01;
                R[19][20] = R[19][27] = R[20][12] = R[20][19] = R[20][28] = -0.01;
                R[21][13] = R[21][29] = R[22][14] = R[22][30] = R[23][15] = -0.01;
                R[23][31] = R[24][16] = R[24][32] = R[25][26] = R[25][33] = -0.01;
                R[26][27] = R[26][25] = R[27][26] = R[27][28] = R[27][19] = -0.01;
                R[27][35] = R[28][27] = R[28][20] = R[28][36] = R[29][21] = -0.01;
                R[30][22] = R[30][38] = R[31][23] = R[31][39] = R[32][24] = -0.01;
                R[32][40] = R[33][25] = R[34][35] = R[34][42] = R[35][34] = -0.01;
                R[35][27] = R[35][43] = R[36][28] = R[36][37] = -0.01;
                R[37][36] = R[37][38] = R[38][37] = R[38][30] = -0.01;
                R[38][46] = R[39][31] = R[39][47] = R[40][32] = R[41][42] = -0.01;
                R[41][33] = R[42][41] = R[42][43] = R[42][34] = R[43][42] = -0.01;
                R[43][44] = R[43][35] = R[44][45] = R[44][43] = -0.01;
                R[45][44] = R[46][38] = -0.01;
                R[39][47] = 100000;  // Goal
                return R;
            }

            return maze;
        }

        private static double[][] CreateQuality(int numberOfSquares)
        {
            double[][] Q = new double[numberOfSquares][];
            for (int i = 0; i < numberOfSquares; ++i)
                Q[i] = new double[numberOfSquares];
            return Q;
        }
    }
}

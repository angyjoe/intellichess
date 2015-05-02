using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xunit;
using System.Diagnostics;
using System.Security.Cryptography;
/*
 * Author: Sari Haj Hussein
 */
namespace P5
{
    public class BayesValuesTest
    {
        /*        //Base Piece Values & constans
                private static double _basePawnValue = 100;
                private static double _baseKnightValue = 350;
                private static double _baseBishopValue = 350;
                private static double _baseRookValue = 525;
                private static double _baseQueenValue = 1000;
                private static double _baseKingValue = 10000;
                private static double _checkMateConstant = 50000;
                //Factors
                private static double _lateGameBonusRooks = 0.10;
                private static double _lateGameBonusKnights = 0.05;
                private static double _lateGameBonusBishop = 0.08;
                private static double _factorRook = 1.25;
                private static double _factorBishop = 1.25;
                private static double _factorKnight = 1.25;
        */
        [Fact]
        public void CreateEvalConfigurationStringLogFormat_IsStringCorrect_Equal()
        {
            #region ResultString
            ChessBoard board = new ChessBoard();
            board.InitializeGame();
            string result;
            result = Eval.BasePawnValue.ToString() + " " + Eval.BaseBishopValue.ToString() + " " + Eval.BaseKnightValue.ToString() + " " + Eval.BaseRookValue + " " +
                     Eval.BaseQueenValue.ToString() + " " + Eval.BaseKingValue.ToString() + " " + Eval.CheckMateConstant.ToString() + " " + Eval.LateGameBonusRooks.ToString() + " " +
                     Eval.LateGameBonusKnights.ToString() + " " + Eval.LateGameBonusBishop.ToString() + " " + Eval.FactorRook.ToString() + " " + Eval.FactorBishop.ToString() + " " +
                     Eval.FactorKnight.ToString() + " ";


            foreach (int i in board.WhitePawn.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteKnight.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteBishop.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteRook.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteQueen.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteKing.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackPawn.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackKnight.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackBishop.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackRook.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackQueen.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackKing.PositionValues)
            {
                result += i + " ";
            }
            #endregion
            HashAlgorithm algorithm = SHA256.Create();
            byte[] resultHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(result));
            string resultHashString = BitConverter.ToString( resultHash ).Replace( "-", string.Empty );
            bool won = true;

            string expected = resultHashString + " " + won.ToString() + " " + result;


            string outputString;
            outputString = Bayes.CreateEvalConfigurationStringLogFormat(won, result);

            Assert.Equal(expected, outputString);
        }

        [Fact]
        public void WriteLineToTxtFile_HasWrittenToFileCorrectly_Equal()
        {

            #region ResultString
            ChessBoard board = new ChessBoard();
            board.InitializeGame();
            string result;
            result = Eval.BasePawnValue.ToString() + " " + Eval.BaseBishopValue.ToString() + " " + Eval.BaseKnightValue.ToString() + " " + Eval.BaseRookValue + " " +
                     Eval.BaseQueenValue.ToString() + " " + Eval.BaseKingValue.ToString() + " " + Eval.CheckMateConstant.ToString() + " " + Eval.LateGameBonusRooks.ToString() + " " +
                     Eval.LateGameBonusKnights.ToString() + " " + Eval.LateGameBonusBishop.ToString() + " " + Eval.FactorRook.ToString() + " " + Eval.FactorBishop.ToString() + " " +
                     Eval.FactorKnight.ToString() + " ";


            foreach (int i in board.WhitePawn.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteKnight.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteBishop.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteRook.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteQueen.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteKing.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackPawn.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackKnight.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackBishop.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackRook.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackQueen.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackKing.PositionValues)
            {
                result += i + " ";
            }
            #endregion
            string fileName = "TxtFile.txt";
            string path = Path.GetFullPath(fileName);
            using(File.Create(fileName)){}
            Bayes.WriteLineToTxtFile(path, result);

            string[] readText = File.ReadAllLines(path);
            File.Delete(fileName);

            Assert.Equal(result, readText.Last<string>());
        }

        [Fact]
        public void ReadLinesFromTxtFile_HasReadFromFileCorreclty_Equal()
        {
            string fileName = "TxtFile.txt";
            using (File.Create(fileName)) { }
            string path = Path.GetFullPath(fileName);
            string[] expected = { "1 2 3 4", "5 6 7 8", "9 10 11 12", "13 14 15 16" };
            File.WriteAllLines(path, expected);

            string[] result = Bayes.ReadLinesFromTxtFile(path);
            File.Delete(fileName);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ModifyValues_HasModifiedValuesCorrectly_Equal()
        {
            ChessBoard board = new ChessBoard();
            board.InitializeGame();
            string inputValues = null;
            string result;

            for (int i = 0; i < 781; i++)
            {
                inputValues += "1 ";
            }

            Bayes.ModifyValues(inputValues, board);


            result = Eval.BasePawnValue.ToString() + " " + Eval.BaseBishopValue.ToString() + " " + Eval.BaseKnightValue.ToString() + " " + Eval.BaseRookValue + " " +
                     Eval.BaseQueenValue.ToString() + " " + Eval.BaseKingValue.ToString() + " " + Eval.CheckMateConstant.ToString() + " " + Eval.LateGameBonusRooks.ToString() + " " +
                     Eval.LateGameBonusKnights.ToString() + " " + Eval.LateGameBonusBishop.ToString() + " " + Eval.FactorRook.ToString() + " " + Eval.FactorBishop.ToString() + " " +
                     Eval.FactorKnight.ToString() + " ";


            foreach (int i in board.WhitePawn.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteKnight.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteBishop.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteRook.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteQueen.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteKing.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackPawn.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackKnight.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackBishop.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackRook.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackQueen.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackKing.PositionValues)
            {
                result += i + " ";
            }

            Assert.Equal(inputValues, result);
        }

        [Fact]
        public void HandleEvalConfig_HasHandledEvalConfigCorrect_Equal()
        {
            string fileName = "EvalConfigTest.txt";
            using (File.Create(fileName)) { }
            string path = Path.GetFullPath(fileName);
            string[] textLines = { "false", "20", "1337", "10", "15" };
            File.WriteAllLines(fileName, textLines);
            int Depth;
            Bayes.HandleEvalConfig(out Depth, path);

            File.Delete(path);

            Assert.Equal(Convert.ToBoolean(textLines[0]), Bayes.TuneEvalValues);
            Assert.Equal(textLines[1], Bayes.TuningBounderies.ToString());
            Assert.Equal(textLines[2], Bayes.Seed.ToString());
            Assert.Equal(textLines[3], Bayes.RepetetionPerEvalConfiguration.ToString());
            Assert.Equal(textLines[4], Depth.ToString());
        }

        [Fact]
        public void CreateHashFromEvalConfigurationString_HasCreatedTheRightHash_Equal()
        {
            #region ResultString
            ChessBoard board = new ChessBoard();
            board.InitializeGame();
            string result;
            result = Eval.BasePawnValue.ToString() + " " + Eval.BaseBishopValue.ToString() + " " + Eval.BaseKnightValue.ToString() + " " + Eval.BaseRookValue + " " +
                     Eval.BaseQueenValue.ToString() + " " + Eval.BaseKingValue.ToString() + " " + Eval.CheckMateConstant.ToString() + " " + Eval.LateGameBonusRooks.ToString() + " " +
                     Eval.LateGameBonusKnights.ToString() + " " + Eval.LateGameBonusBishop.ToString() + " " + Eval.FactorRook.ToString() + " " + Eval.FactorBishop.ToString() + " " +
                     Eval.FactorKnight.ToString() + " ";


            foreach (int i in board.WhitePawn.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteKnight.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteBishop.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteRook.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteQueen.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteKing.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackPawn.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackKnight.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackBishop.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackRook.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackQueen.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackKing.PositionValues)
            {
                result += i + " ";
            }
            #endregion
            HashAlgorithm algorithm = SHA256.Create();
            byte[] resultHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(result));
            string resultHashString = BitConverter.ToString( resultHash ).Replace( "-", string.Empty );

            Assert.Equal(resultHashString, Bayes.CreateHashFromEvalConfigurationString(result));
        }

        [Fact]
        public void FindBestEvalConfiguration_HasFoundTheBest_Equal()
        {
            string fileName = "TxtFile.txt";
            using (File.Create(fileName)) { }
            #region ResultString
            ChessBoard board = new ChessBoard();
            board.InitializeGame();
            string result;
            result = Eval.BasePawnValue.ToString() + " " + Eval.BaseBishopValue.ToString() + " " + Eval.BaseKnightValue.ToString() + " " + Eval.BaseRookValue + " " +
                     Eval.BaseQueenValue.ToString() + " " + Eval.BaseKingValue.ToString() + " " + Eval.CheckMateConstant.ToString() + " " + Eval.LateGameBonusRooks.ToString() + " " +
                     Eval.LateGameBonusKnights.ToString() + " " + Eval.LateGameBonusBishop.ToString() + " " + Eval.FactorRook.ToString() + " " + Eval.FactorBishop.ToString() + " " +
                     Eval.FactorKnight.ToString() + " ";


            foreach ( int i in board.WhitePawn.PositionValues ) {
              result += i + " ";
            }

            foreach ( int i in board.WhiteKnight.PositionValues ) {
              result += i + " ";
            }

            foreach ( int i in board.WhiteBishop.PositionValues ) {
              result += i + " ";
            }

            foreach ( int i in board.WhiteRook.PositionValues ) {
              result += i + " ";
            }

            foreach ( int i in board.WhiteQueen.PositionValues ) {
              result += i + " ";
            }

            foreach ( int i in board.WhiteKing.PositionValues ) {
              result += i + " ";
            }

            foreach ( int i in board.BlackPawn.PositionValues ) {
              result += i + " ";
            }

            foreach ( int i in board.BlackKnight.PositionValues ) {
              result += i + " ";
            }

            foreach ( int i in board.BlackBishop.PositionValues ) {
              result += i + " ";
            }

            foreach ( int i in board.BlackRook.PositionValues ) {
              result += i + " ";
            }

            foreach ( int i in board.BlackQueen.PositionValues ) {
              result += i + " ";
            }

            foreach ( int i in board.BlackKing.PositionValues ) {
              result += i + " ";
            }
            #endregion
            string path = Path.GetFullPath(fileName);
            string[] textLines = { "111 False " + result, "111 False " + result, "111 False " + result, "111 False " + result, "111 False " + result, "111 True " + result, "111 True " + result, "111 True " + result, "111 True " + result, "111 True " + result,
                             "222 False " + result, "222 False " + result, "222 False " + result, "222 False " + result, "222 True " + result, "222 True " + result, "222 True " + result, "222 True " + result, "222 True " + result, "222 True " + result };
            
            File.WriteAllLines(path, textLines);

            Assert.Equal( result, Bayes.FindBestEvalConfiguration( path ) );
            File.Delete(fileName);
        }

        [Fact]
        public void RandomizeValue_HasRandomizedCorrectly_Equal()
        {
            int modifyProcentage = 20;
            int seed = 1337;
            Random randomNumber = new Random(seed);

            double expectedNumber = ((double)(randomNumber.Next(-modifyProcentage, modifyProcentage)) / 100);

            double resultNumber = Bayes.RandomizeValue( modifyProcentage, seed );

            Assert.Equal(expectedNumber, resultNumber);

        }

      [Fact]
        public void RandomizeEvalConfiguration_HasRandomizedCorrectly_Equal()
        {
          string fileName = "EvalConfigTest.txt";
          using ( File.Create( fileName ) ) { }
          string path = Path.GetFullPath( fileName );
          string[] textLines = { "true", "20", "1337", "10", "15" };
          File.WriteAllLines( fileName, textLines );
          int Depth;
          Bayes.HandleEvalConfig( out Depth, path );
          
          Assert.True( Bayes.TuneEvalValues );
          Assert.Equal( 20, Bayes.TuningBounderies );
          Assert.Equal(1337, Bayes.Seed);
          Assert.Equal(10, Bayes.RepetetionPerEvalConfiguration);
          Assert.Equal( 15, Depth );

          Random randomGenerator = new Random( Bayes.Seed );

          #region resultString
          ChessBoard board = new ChessBoard();
          board.InitializeGame();
          string result;
          result = Eval.BasePawnValue.ToString() + " " + Eval.BaseBishopValue.ToString() + " " + Eval.BaseKnightValue.ToString() + " " + Eval.BaseRookValue + " " +
                   Eval.BaseQueenValue.ToString() + " " + Eval.BaseKingValue.ToString() + " " + Eval.CheckMateConstant.ToString() + " " + Eval.LateGameBonusRooks.ToString() + " " +
                   Eval.LateGameBonusKnights.ToString() + " " + Eval.LateGameBonusBishop.ToString() + " " + Eval.FactorRook.ToString() + " " + Eval.FactorBishop.ToString() + " " +
                   Eval.FactorKnight.ToString() + " ";


          foreach ( int i in board.WhitePawn.PositionValues ) {
            result += i + " ";
          }

          foreach ( int i in board.WhiteKnight.PositionValues ) {
            result += i + " ";
          }

          foreach ( int i in board.WhiteBishop.PositionValues ) {
            result += i + " ";
          }

          foreach ( int i in board.WhiteRook.PositionValues ) {
            result += i + " ";
          }

          foreach ( int i in board.WhiteQueen.PositionValues ) {
            result += i + " ";
          }

          foreach ( int i in board.WhiteKing.PositionValues ) {
            result += i + " ";
          }

          foreach ( int i in board.BlackPawn.PositionValues ) {
            result += i + " ";
          }

          foreach ( int i in board.BlackKnight.PositionValues ) {
            result += i + " ";
          }

          foreach ( int i in board.BlackBishop.PositionValues ) {
            result += i + " ";
          }

          foreach ( int i in board.BlackRook.PositionValues ) {
            result += i + " ";
          }

          foreach ( int i in board.BlackQueen.PositionValues ) {
            result += i + " ";
          }

          foreach ( int i in board.BlackKing.PositionValues ) {
            result += i + " ";
          }
          #endregion

          string[] ToBeModifiedConfigurationString = result.Split( ' ' );
          string modifiedString = null;

          foreach ( string valueString in ToBeModifiedConfigurationString ) {
            if ( valueString != string.Empty) {
              double value = Convert.ToDouble( valueString );
              value = value * ( 1 - ( (double)( randomGenerator.Next( -Bayes.TuningBounderies, Bayes.TuningBounderies ) ) / 100 ) );
              modifiedString += value + " ";
            }
          }

          Assert.Equal( modifiedString, Bayes.RandomizeEvalConfiguration( result ) );
        }            
    }

}

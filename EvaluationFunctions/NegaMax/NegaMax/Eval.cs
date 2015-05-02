using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5
{
    public abstract class Eval
    { 
        private static GameState _currentGameState = GameState.MidGame;
        //Base Piece Values & constans
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

        public enum GameState {MidGame = 32, LateGame = 10};
        public static GameState CurrentGameState { get { return _currentGameState; } }
        public static double LateGameBonusRooks { get { return _lateGameBonusRooks; } set { _lateGameBonusRooks = value; } }
        public static double LateGameBonusKnights { get { return _lateGameBonusKnights; } set { _lateGameBonusKnights = value; } }
        public static double LateGameBonusBishop { get { return _lateGameBonusBishop; } set { _lateGameBonusBishop = value; } }
        public static double CheckMateConstant { get { return _checkMateConstant; } set { _checkMateConstant = value; } }
        //Piece Values
        public static double BasePawnValue { get { return _basePawnValue; } set { _basePawnValue = value; } }
        public static double BaseKnightValue { get { return _baseKnightValue; } set { _baseKnightValue = value; } }
        public static double BaseBishopValue { get { return _baseBishopValue; } set { _baseBishopValue = value; } }
        public static double BaseRookValue { get { return _baseRookValue; } set { _baseRookValue = value; } }
        public static double BaseQueenValue { get { return _baseQueenValue; } set { _baseQueenValue = value; } }
        public static double BaseKingValue { get { return _baseKingValue; } set { _baseKingValue = value; } }

        public static double FactorRook { get { return _factorRook; } set { _factorRook = value; } }
        public static double FactorBishop { get { return _factorBishop; } set { _factorBishop = value; } }
        public static double FactorKnight { get { return _factorKnight; } set { _factorKnight = value; } }

        public static double EvaluateState(ChessBoard board)
        {
            RetrieveCurrentGameState(CountPieces(board));

            double result = 0;
            result += GetMaterialValue(board);
            result += EvaluatePosition(board);
            result += EvaluateKingMobility(board);
            return result;

        }

        private static double GetMaterialValue(ChessBoard board)
        {
            double resultBlack = 0;
            double resultWhite = 0;
            double result = 0;

            Tuple<double, double> bishopFactors = EvaluateBishopFactors(board);
            Tuple<double, double> rookFactors = EvaluateRookFactors(board);
            Tuple<double, double> knightFactors = EvaluateKnightFactors(board);

            resultWhite = (board.WhitePawn.Count * _basePawnValue
                        + board.WhiteBishop.Count * _baseBishopValue * bishopFactors.Item1
                        + board.WhiteKnight.Count * _baseKnightValue * knightFactors.Item1
                        + board.WhiteRook.Count * _baseRookValue * rookFactors.Item1
                        + board.WhiteQueen.Count * _baseQueenValue
                        + board.WhiteKing.Count * _baseKingValue);

            resultBlack = (board.BlackPawn.Count * _basePawnValue
                        + board.BlackBishop.Count * _baseBishopValue * bishopFactors.Item2
                        + board.BlackKnight.Count * _baseKnightValue * knightFactors.Item2
                        + board.BlackRook.Count * _baseRookValue * rookFactors.Item2
                        + board.BlackQueen.Count * _baseQueenValue
                        + board.BlackKing.Count * _baseKingValue);

            result = resultWhite - resultBlack;
            return result;
        }

        #region Piece Evals
        private static double EvaluatePawnValues(ChessBoard board)
        {
            return -1;
        }

        private static Tuple<double, double> EvaluateBishopFactors(ChessBoard board)
        {
            Tuple<double, double> result;
            double factorWhite = 1.0;
            double factorBlack = 1.0;

            //Baysian Network Evaluates Factor !
            if (board.WhiteBishop.Count == 2 && _currentGameState == GameState.MidGame)
            {
                factorWhite = _factorBishop;
            }
            if (board.BlackBishop.Count == 2 && _currentGameState == GameState.MidGame)
            {
                factorBlack = _factorBishop;
            }

            if (board.WhiteBishop.Count == 2 && _currentGameState == GameState.LateGame)
            {
                factorWhite = _factorBishop + _lateGameBonusBishop;
            }
            if (board.BlackBishop.Count == 2 && _currentGameState == GameState.LateGame)
            {
                factorBlack = _factorBishop + _lateGameBonusBishop;
            }

            result = new Tuple<double, double>(factorWhite, factorBlack);
            return result;
        }

        //Knights with late game bonus
        private static Tuple<double, double> EvaluateKnightFactors(ChessBoard board)
        {
            Tuple<double, double> result;
            double factorWhite = 1.0;
            double factorBlack = 1.0;

            //Baysian Network Evaluates Factor !
            if (board.WhiteKnight.Count == 2 && _currentGameState == GameState.MidGame)
            {
                factorWhite = _factorKnight;
            }
            if (board.BlackKnight.Count == 2 && _currentGameState == GameState.MidGame)
            {
                factorBlack = _factorKnight;
            }
            //Baysian Network Evaluates Factor !
            if (board.WhiteKnight.Count == 2 && _currentGameState == GameState.LateGame)
            {
                factorWhite = _factorKnight + _lateGameBonusKnights;
            }
            if (board.BlackKnight.Count == 2 && _currentGameState == GameState.LateGame)
            {
                factorBlack = _factorKnight + _lateGameBonusKnights;
            }

            result = new Tuple<double, double>(factorWhite, factorBlack);
            return result;
        }

        private static Tuple<double, double> EvaluateRookFactors(ChessBoard board)
        {
            Tuple<double, double> result;
            double factorWhite = 1.0;
            double factorBlack = 1.0;

            //Baysian Network Evaluates Factor !
            if (board.WhiteRook.Count == 2 && _currentGameState == GameState.MidGame)
            {
                factorWhite = _factorRook;
            }
            if (board.BlackRook.Count == 2 && _currentGameState == GameState.MidGame)
            {
                factorBlack = _factorRook;
            }
            //Baysian Network Evaluates Factor !
            if (board.WhiteRook.Count == 2 && _currentGameState == GameState.LateGame)
            {
                factorWhite = _factorRook + _lateGameBonusRooks;
            }
            if (board.BlackRook.Count == 2 && _currentGameState == GameState.LateGame)
            {
                factorBlack = _factorRook + _lateGameBonusRooks;
            }

            result = new Tuple<double, double>(factorWhite, factorBlack);
            return result;
        }

        private static double EvaluateQueenValues(ChessBoard board)
        {
            return -1;
        }

        #endregion

        private static double EvaluatePosition(ChessBoard board)
        {
            List<int> indexes;
            double blackResult = 0;
            double whiteResult = 0;
            double result = 0;
            List<BitBoard> blackEnumerator = new List<BitBoard>()
        {
            board.BlackBishop, board.BlackKing, board.BlackKnight, 
            board.BlackPawn, board.BlackQueen,board.BlackRook,
        };
            List<BitBoard> whiteEnumerator = new List<BitBoard>()
        {
            board.WhiteBishop, board.WhiteKing, board.WhiteKnight, 
            board.WhitePawn, board.WhiteQueen,board.WhiteRook,
        };


            foreach (ColoredBitBoard bb in blackEnumerator)
            {
              indexes = BitBoard.PositionIndexFromBoardSquare( ColoredBitBoard.SplitBitBoardToBoardSquares( bb ).ToList() );
                foreach (int index in indexes)
                {
                    blackResult += bb.PositionValues[index - 1];
                }
            }

            foreach (ColoredBitBoard bb in whiteEnumerator)
            {
                indexes = BitBoard.PositionIndexFromBoardSquare(ColoredBitBoard.SplitBitBoardToBoardSquares(bb).ToList());
                foreach (int index in indexes)
                {
                    whiteResult += bb.PositionValues[index - 1];
                }
            }
            result = whiteResult - blackResult;
            return result;
        }

        private static double EvaluateMobilty(ChessBoard board)
        {
            return -1;
        }
#if DEBUG
        public static double GetMaterialValue_DEBUG(ChessBoard board)
        {
            return GetMaterialValue(board);
        }

        public static double EvaluatePosition_DEBUG(ChessBoard board)
        {
            return EvaluatePosition(board);
        }
#endif

        private static double EvaluateKingMobility(ChessBoard board)
        {
            double WhiteMoves;
            double BlackMoves;
            double result;
            bool clampable = true;
            BlackMoves = KingMoveGen.NumberOfBlackKingMoves;
            WhiteMoves = KingMoveGen.NumberOfWhiteKingMoves;

            if (board.WhiteKing.IsChecked && WhiteMoves <= 0)
            {
                clampable = false;
                return (WhiteMoves - CheckMateConstant) - (BlackMoves);
            }

            if (board.BlackKing.IsChecked && BlackMoves <= 0)
            {
                clampable = false;
                return WhiteMoves - (BlackMoves - CheckMateConstant);
            }


            //Clamping value here to avoid pawn suicides for a bad restrictions on a king.
            result = WhiteMoves * 10 - BlackMoves * 10;
            if(clampable)
                Clamp(result, -99, 99);
            return result;

        }

        public static double Clamp(double value, double minValue, double maxValue)
        {
            double result;
            result = value;
            if (value.CompareTo(minValue) < 0)
                result = minValue;
            if (value.CompareTo(maxValue) > 0)
                result = maxValue;
            return result;

        }

        private static int CountPieces(ChessBoard board)
        {
            int result;
            result = board.BlackBishop.Count + board.WhiteBishop.Count + board.BlackKing.Count + board.WhiteKing.Count + board.BlackKnight.Count + board.WhiteKnight.Count + board.BlackPawn.Count + board.WhitePawn.Count + board.BlackQueen.Count + board.WhiteQueen.Count + board.BlackRook.Count + board.WhiteRook.Count;
            return result;
        }
        
        private static void RetrieveCurrentGameState(int pieceCount)
        {
            if (pieceCount > (int)GameState.LateGame)
                _currentGameState = GameState.MidGame;
            if (pieceCount <= (int)GameState.LateGame)
            {
                _currentGameState = GameState.LateGame;
            }
        }
    } 
}
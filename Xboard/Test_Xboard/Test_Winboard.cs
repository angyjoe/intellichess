using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
/*
 * Author: Sari Haj Hussein
 */
namespace P5
{
    public class Test_Winboard
    {
        [Fact]
        public void Handler_IsProtoverHandledCorrectly_Equal()
        {
            Winboard winboard = new Winboard();
            winboard.Handler("protover 2");

            Assert.Equal(2,winboard.protover);
        }

        [Fact]
        public void Handler_IsNewHandledCorrectly_Equal()
        {
            ChessBoard testChessBoard = new ChessBoard();
            testChessBoard.InitializeGame();
            Winboard winboard = new Winboard();
            winboard.Handler("new");

            Assert.Equal(testChessBoard.WhiteKing.Bits, winboard.chessBoard.WhiteKing.Bits);
            Assert.Equal(testChessBoard.WhiteQueen.Bits, winboard.chessBoard.WhiteQueen.Bits);
            Assert.Equal(testChessBoard.WhiteRook.Bits, winboard.chessBoard.WhiteRook.Bits);
            Assert.Equal(testChessBoard.WhiteBishop.Bits, winboard.chessBoard.WhiteBishop.Bits);
            Assert.Equal(testChessBoard.WhiteKnight.Bits, winboard.chessBoard.WhiteKnight.Bits);
            Assert.Equal(testChessBoard.WhitePawn.Bits, winboard.chessBoard.WhitePawn.Bits);

            Assert.Equal(testChessBoard.BlackKing.Bits, winboard.chessBoard.BlackKing.Bits);
            Assert.Equal(testChessBoard.BlackQueen.Bits, winboard.chessBoard.BlackQueen.Bits);
            Assert.Equal(testChessBoard.BlackRook.Bits, winboard.chessBoard.BlackRook.Bits);
            Assert.Equal(testChessBoard.BlackBishop.Bits, winboard.chessBoard.BlackBishop.Bits);
            Assert.Equal(testChessBoard.BlackKnight.Bits, winboard.chessBoard.BlackKnight.Bits);
            Assert.Equal(testChessBoard.BlackPawn.Bits, winboard.chessBoard.BlackPawn.Bits);
        }

        [Fact]
        public void Handler_IsLevelHandledCorrectly_Equal()
        {
            Winboard winboard = new Winboard();
            winboard.Handler("level 40 5 0");

            Assert.Equal(40, winboard.movesPerMinutes);
            Assert.Equal(5, winboard.minutes);
        }

        [Fact]
        public void Handler_IsFirstMoveGoHandledCorrectly_NotEqual()
        {
            Winboard winboard = new Winboard();

            winboard.Handler("new");
            EmptyBitBoard WhitePieceStartPlacement = new EmptyBitBoard();
            WhitePieceStartPlacement.Bits = 
                winboard.chessBoard.WhitePawn.Bits | 
                winboard.chessBoard.WhiteKnight.Bits | 
                winboard.chessBoard.WhiteBishop.Bits | 
                winboard.chessBoard.WhiteRook.Bits | 
                winboard.chessBoard.WhiteQueen.Bits | 
                winboard.chessBoard.WhiteKing.Bits;

            winboard.Handler("go");
            EmptyBitBoard WhitePieceAfterMovePlacement = new EmptyBitBoard();
            WhitePieceAfterMovePlacement.Bits = 
                winboard.chessBoard.WhitePawn.Bits |
                winboard.chessBoard.WhiteKnight.Bits |
                winboard.chessBoard.WhiteBishop.Bits |
                winboard.chessBoard.WhiteRook.Bits |
                winboard.chessBoard.WhiteQueen.Bits |
                winboard.chessBoard.WhiteKing.Bits;

            Assert.NotEqual(WhitePieceStartPlacement.Bits, WhitePieceAfterMovePlacement.Bits);
        }

        [Fact]
        public void Handler_IsBlackCorrectlyPlaced_NotEqual()
        {
            Winboard winboard = new Winboard();

            winboard.Handler("new");
            EmptyBitBoard BlackPieceStartPlacement = new EmptyBitBoard();
            BlackPieceStartPlacement.Bits =
                winboard.chessBoard.BlackPawn.Bits |
                winboard.chessBoard.BlackKnight.Bits |
                winboard.chessBoard.BlackBishop.Bits |
                winboard.chessBoard.BlackRook.Bits |
                winboard.chessBoard.BlackQueen.Bits |
                winboard.chessBoard.BlackKing.Bits;

            winboard.Handler("e2e4");
            winboard.Handler( "e4e5" );
            EmptyBitBoard BlackPieceAfterMovePlacement = new EmptyBitBoard();
            BlackPieceAfterMovePlacement.Bits =
                winboard.chessBoard.BlackPawn.Bits |
                winboard.chessBoard.BlackKnight.Bits |
                winboard.chessBoard.BlackBishop.Bits |
                winboard.chessBoard.BlackRook.Bits |
                winboard.chessBoard.BlackQueen.Bits |
                winboard.chessBoard.BlackKing.Bits;

            Assert.NotEqual(BlackPieceStartPlacement.Bits, BlackPieceAfterMovePlacement.Bits);
        
        }

        [Fact]
        public void Handler_IsResultWhiteWinHandledCorrectly_Equal()
        {
            Winboard winboard = new Winboard();
            winboard.Handler("new");
            winboard.Handler("result 1-0 {Random comment}");  

            Assert.Equal(ChessBoardGameState.WhiteMate, winboard.chessBoard.State);
        }

        [Fact]
        public void Handler_IsResultBlackWinHandledCorrectly_Equal()
        {
            Winboard winboard = new Winboard();
            winboard.Handler("new");
            winboard.Handler("result 0-1 {Random comment}");

            Assert.Equal(ChessBoardGameState.BlackMate, winboard.chessBoard.State);
        }

        [Fact]
        public void Handler_IsResultDrawHandledCorrectly_Equal()
        {
            Winboard winboard = new Winboard();
            winboard.Handler("new");
            winboard.Handler("result 1/2-1/2 {Random comment}");

            Assert.Equal(ChessBoardGameState.Draw, winboard.chessBoard.State);
        }

        [Fact]
        public void Handler_IsMoveHandledCorrectly_Equal()
        {
            Winboard winboard = new Winboard();
            winboard.Handler("new");
            winboard.Handler("e2e4");
            PawnBitBoard correctWhitePawnPlacement = new PawnBitBoard(ChessPieceColors.White);
            correctWhitePawnPlacement.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

            Assert.Equal(correctWhitePawnPlacement.Bits, winboard.chessBoard.WhitePawn.Bits);
        }

        [Fact]
        public void Handler_IsForceHandledCorrectly_Equal() {
          Winboard winboard = new Winboard();
          PawnBitBoard correctWhitePawnPlacement = new PawnBitBoard( ChessPieceColors.White );
          PawnBitBoard correctBlackPawnPlacement = new PawnBitBoard( ChessPieceColors.Black );

          correctWhitePawnPlacement.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
          correctBlackPawnPlacement.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E6 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

          winboard.Handler( "new" );
          winboard.Handler( "force" );
          winboard.Handler( "e2e4" );
          winboard.Handler( "e7e6" );

          Assert.Equal( correctWhitePawnPlacement.Bits, winboard.chessBoard.WhitePawn.Bits );
          Assert.Equal( correctBlackPawnPlacement.Bits, winboard.chessBoard.BlackPawn.Bits );
        }

        [Fact]
        public void Handler_IsForceAndThenWhiteGoHandledCorrectly_Equal() {
          Winboard winboard = new Winboard();
          ChessBoard correctBoardBeforeGo = new ChessBoard();
          correctBoardBeforeGo.InitializeGame();
          correctBoardBeforeGo.WhitePawn.Bits =
              BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D4 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
          correctBoardBeforeGo.BlackPawn.Bits =
             BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D5 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;


          winboard.Handler( "new" );
          winboard.Handler( "force" );
          winboard.Handler( "d2d4" );
          winboard.Handler( "d7d5" );
          winboard.Handler( "go" );

          Assert.NotEqual( correctBoardBeforeGo.WhiteKing.Bits |
                           correctBoardBeforeGo.WhiteQueen.Bits |
                           correctBoardBeforeGo.WhiteRook.Bits |
                           correctBoardBeforeGo.WhiteBishop.Bits |
                           correctBoardBeforeGo.WhiteKnight.Bits |
                           correctBoardBeforeGo.WhitePawn.Bits,
                           winboard.chessBoard.WhiteKing.Bits |
                           winboard.chessBoard.WhiteQueen.Bits |
                           winboard.chessBoard.WhiteRook.Bits |
                           winboard.chessBoard.WhiteBishop.Bits |
                           winboard.chessBoard.WhiteKnight.Bits |
                           winboard.chessBoard.WhitePawn.Bits
                           );
        }

        [Fact]
        public void Handler_IsForceAndThenBlackGoHandledCorrectly_Equal() {
          Winboard winboard = new Winboard();
          ChessBoard correctBoardBeforeGo = new ChessBoard();
          correctBoardBeforeGo.InitializeGame();
          correctBoardBeforeGo.WhitePawn.Bits =
                 BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C4 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
          correctBoardBeforeGo.WhitePawn.Bits =
                BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C4 | BoardSquare.D4 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
          correctBoardBeforeGo.BlackPawn.Bits =
             BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D5 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

          winboard.Handler( "new" );
          winboard.Handler( "force" );
          winboard.Handler( "d2d4" );
          winboard.Handler( "d7d5" );
          winboard.Handler( "c2c4" );
          winboard.Handler( "go" );

          Assert.NotEqual( correctBoardBeforeGo.BlackKing.Bits | 
                           correctBoardBeforeGo.BlackQueen.Bits | 
                           correctBoardBeforeGo.BlackRook.Bits | 
                           correctBoardBeforeGo.BlackBishop.Bits | 
                           correctBoardBeforeGo.BlackKnight.Bits | 
                           correctBoardBeforeGo.BlackPawn.Bits, 
                           winboard.chessBoard.BlackKing.Bits |
                           winboard.chessBoard.BlackQueen.Bits |
                           winboard.chessBoard.BlackRook.Bits |
                           winboard.chessBoard.BlackBishop.Bits |
                           winboard.chessBoard.BlackKnight.Bits |
                           winboard.chessBoard.BlackPawn.Bits
                           );
        }

        [Fact]
        public void Handler_IsForceAndThenWhiteGoHandledCorrectlyV2_Equal() {
          Winboard winboard = new Winboard();
          ChessBoard correctBoardBeforeGo = new ChessBoard();
          correctBoardBeforeGo.InitializeGame();
          correctBoardBeforeGo.WhitePawn.Bits =
             BoardSquare.A2 | BoardSquare.B2 | BoardSquare.D4 | BoardSquare.E3 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
          correctBoardBeforeGo.BlackPawn.Bits =
             BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C6 | BoardSquare.C4 | BoardSquare.E6 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H6;
          correctBoardBeforeGo.WhiteQueen.Bits =
             BoardSquare.D1;
          correctBoardBeforeGo.BlackQueen.Bits =
             BoardSquare.F6;
          correctBoardBeforeGo.WhiteKnight.Bits =
             BoardSquare.C3 | BoardSquare.F3;
          correctBoardBeforeGo.BlackKnight.Bits =
             BoardSquare.D7;
          correctBoardBeforeGo.WhiteBishop.Bits =
            BoardSquare.D3;
          correctBoardBeforeGo.BlackBishop.Bits =
             BoardSquare.C7 | BoardSquare.F8;

          winboard.Handler( "new" );
          winboard.Handler( "force" );
          winboard.Handler( "d2d4" );
          winboard.Handler( "d7d5" );
          winboard.Handler( "c2c4" );
          winboard.Handler( "e7e6" );
          winboard.Handler( "g1f3" );
          winboard.Handler( "g8f6" );
          winboard.Handler( "b1c3" );
          winboard.Handler( "c7c6" );
          winboard.Handler( "c1g5" );
          winboard.Handler( "h7h6" );
          winboard.Handler( "g5f6" );
          winboard.Handler( "d8f6" );
          winboard.Handler( "e2e3" );
          winboard.Handler( "b8d7" );
          winboard.Handler( "f1d3" );
          winboard.Handler( "d5c4" );
          winboard.Handler( "go" );

          Assert.NotEqual( correctBoardBeforeGo.WhiteKing.Bits |
                           correctBoardBeforeGo.WhiteQueen.Bits |
                           correctBoardBeforeGo.WhiteRook.Bits |
                           correctBoardBeforeGo.WhiteBishop.Bits |
                           correctBoardBeforeGo.WhiteKnight.Bits |
                           correctBoardBeforeGo.WhitePawn.Bits,
                           winboard.chessBoard.WhiteKing.Bits |
                           winboard.chessBoard.WhiteQueen.Bits |
                           winboard.chessBoard.WhiteRook.Bits |
                           winboard.chessBoard.WhiteBishop.Bits |
                           winboard.chessBoard.WhiteKnight.Bits |
                           winboard.chessBoard.WhitePawn.Bits
                           );
        }

        [Fact]
        public void Handler_IsForceAndThenBlackGoHandledCorrectlyV2_Equal() {
          Winboard winboard = new Winboard();
          ChessBoard correctBoardBeforeGo = new ChessBoard();
          correctBoardBeforeGo.InitializeGame();
          correctBoardBeforeGo.WhitePawn.Bits =
             BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C4 | BoardSquare.D2 | BoardSquare.E3 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
          correctBoardBeforeGo.WhitePawn.Bits =
             BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C4 | BoardSquare.D4 | BoardSquare.E3 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
          correctBoardBeforeGo.BlackPawn.Bits =
             BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C6 | BoardSquare.D5 | BoardSquare.E6 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H6;
          correctBoardBeforeGo.WhiteQueen.Bits =
             BoardSquare.D1;
          correctBoardBeforeGo.BlackQueen.Bits =
             BoardSquare.F6;
          correctBoardBeforeGo.WhiteKnight.Bits =
             BoardSquare.C3 | BoardSquare.F3;
          correctBoardBeforeGo.BlackKnight.Bits =
             BoardSquare.D7;
          correctBoardBeforeGo.WhiteBishop.Bits =
            BoardSquare.D3;
          correctBoardBeforeGo.BlackBishop.Bits =
             BoardSquare.C7 | BoardSquare.F8;


          winboard.Handler( "new" );
          winboard.Handler( "force" );
          winboard.Handler( "d2d4" );
          winboard.Handler( "d7d5" );
          winboard.Handler( "c2c4" );
          winboard.Handler( "e7e6" );
          winboard.Handler( "g1f3" );
          winboard.Handler( "g8f6" );
          winboard.Handler( "b1c3" );
          winboard.Handler( "c7c6" );
          winboard.Handler( "c1g5" );
          winboard.Handler( "h7h6" );
          winboard.Handler( "g5f6" );
          winboard.Handler( "d8f6" );
          winboard.Handler( "e2e3" );
          winboard.Handler( "b8d7" );
          winboard.Handler( "f1d3" );
          winboard.Handler( "go" );

          Assert.NotEqual( correctBoardBeforeGo.BlackKing.Bits |
                           correctBoardBeforeGo.BlackQueen.Bits |
                           correctBoardBeforeGo.BlackRook.Bits |
                           correctBoardBeforeGo.BlackBishop.Bits |
                           correctBoardBeforeGo.BlackKnight.Bits |
                           correctBoardBeforeGo.BlackPawn.Bits,
                           winboard.chessBoard.BlackKing.Bits |
                           winboard.chessBoard.BlackQueen.Bits |
                           winboard.chessBoard.BlackRook.Bits |
                           winboard.chessBoard.BlackBishop.Bits |
                           winboard.chessBoard.BlackKnight.Bits |
                           winboard.chessBoard.BlackPawn.Bits
                           );
        }
    }
}

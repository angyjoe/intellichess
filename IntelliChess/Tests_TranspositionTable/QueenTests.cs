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
  partial class Tests_TranspositionTable
  {
    #region WHITE
    [Fact]
    public void WhiteQueenMoveNorthUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard move2 = new PawnBitBoard(ChessPieceColors.Black);
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.White);
      ulong expectedHash;

      move1.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.D2) | BoardSquare.D4;
      move2.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.H7) | BoardSquare.H6;
      move3.Bits = (testBoard.WhiteQueen.Bits ^ BoardSquare.D1) | BoardSquare.D3;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      Assert.Equal(expectedHash, testBoard.BoardHash.Key);
      testBoard.Update( move1 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move2 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move2 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move3 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
    }

    [Fact]
    public void WhiteQueenMoveSouthUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard move2 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.White);
      PawnBitBoard move4 = new PawnBitBoard(ChessPieceColors.Black);
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.White);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.D2) | BoardSquare.D4;
      move2.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D6;
      move3.Bits = (testBoard.WhiteQueen.Bits ^ BoardSquare.D1) | BoardSquare.D3;
      move4.Bits = (move2.Bits ^ BoardSquare.D6) | BoardSquare.D5;
      move5.Bits = (move3.Bits ^ BoardSquare.D3) | BoardSquare.D1;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move3);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move4);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move4);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move5);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
    }

    [Fact]
    public void WhiteQueenMoveEastUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard move2 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.White);
      PawnBitBoard move4 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.White);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.D2) | BoardSquare.D4;
      move2.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D6;
      move3.Bits = (testBoard.WhiteQueen.Bits ^ BoardSquare.D1) | BoardSquare.D3;
      move4.Bits = (move2.Bits ^ BoardSquare.D6) | BoardSquare.D5;
      move5.Bits = (move3.Bits ^ BoardSquare.D3) | BoardSquare.H3;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move3);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move4);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move4);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move5);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
    }

    [Fact]
    public void WhiteQueenMoveWestUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard move2 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.White);
      PawnBitBoard move4 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.White);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.D2) | BoardSquare.D4;
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.D7 ) | BoardSquare.D6;
      move3.Bits = (testBoard.WhiteQueen.Bits ^ BoardSquare.D1) | BoardSquare.D3;
      move4.Bits = ( move2.Bits ^ BoardSquare.D6 ) | BoardSquare.D5;
      move5.Bits = (move3.Bits ^ BoardSquare.D3) | BoardSquare.A3;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move2 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move2 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move3);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move5);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
    }

    [Fact]
    public void WhiteQueenMoveNEUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard move2 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.White);
      PawnBitBoard move4 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.White);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.D2) | BoardSquare.D4;
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.E7 ) | BoardSquare.E6;
      move3.Bits = (testBoard.WhiteQueen.Bits ^ BoardSquare.D1) | BoardSquare.D3;
      move4.Bits = ( move2.Bits ^ BoardSquare.E6 ) | BoardSquare.E5;
      move5.Bits = (move3.Bits ^ BoardSquare.D3) | BoardSquare.G6;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }

    [Fact]
    public void WhiteQueenMoveNWUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard move2 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.White);
      PawnBitBoard move4 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.White);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.D2) | BoardSquare.D4;
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.E7 ) | BoardSquare.E6;
      move3.Bits = (testBoard.WhiteQueen.Bits ^ BoardSquare.D1) | BoardSquare.D3;
      move4.Bits = ( move2.Bits ^ BoardSquare.E6 ) | BoardSquare.E5;
      move5.Bits = (move3.Bits ^ BoardSquare.D3) | BoardSquare.A6;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }

    [Fact]
    public void WhiteQueenMoveSEUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard move2 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.White);
      PawnBitBoard move4 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.White);
      PawnBitBoard move6 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move7 = new QueenBitBoard(ChessPieceColors.White);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.D2) | BoardSquare.D4;
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.E7 ) | BoardSquare.E6;
      move3.Bits = (testBoard.WhiteQueen.Bits ^ BoardSquare.D1) | BoardSquare.D3;
      move4.Bits = ( move2.Bits ^ BoardSquare.E6 ) | BoardSquare.E5;
      move5.Bits = (move3.Bits ^ BoardSquare.D3) | BoardSquare.A6;
      move6.Bits = ( move4.Bits ^ BoardSquare.E5 ) | BoardSquare.E4;
      move7.Bits = (move5.Bits ^ BoardSquare.A6) | BoardSquare.D3;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move3);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move4);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move5 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move6 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move6 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move7 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }

    [Fact]
    public void WhiteQueenMoveSWUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard move2 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.White);
      PawnBitBoard move4 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.White);
      PawnBitBoard move6 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move7 = new QueenBitBoard(ChessPieceColors.White);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.D2) | BoardSquare.D4;
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.E7 ) | BoardSquare.E6;
      move3.Bits = (testBoard.WhiteQueen.Bits ^ BoardSquare.D1) | BoardSquare.D3;
      move4.Bits = ( move2.Bits ^ BoardSquare.E6 ) | BoardSquare.E5;
      move5.Bits = (move3.Bits ^ BoardSquare.D3) | BoardSquare.G6;
      move6.Bits = ( move4.Bits ^ BoardSquare.E5 ) | BoardSquare.E4;
      move7.Bits = (move5.Bits ^ BoardSquare.G6) | BoardSquare.D3;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move3);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move4);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move5 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move6 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move6 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move7 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }

    [Fact]
    public void WhiteQueenCaptureUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard move2 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.White);
      PawnBitBoard move4 =  new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.White);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.D2) | BoardSquare.D4;
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.E7 ) | BoardSquare.E6;
      move3.Bits = (testBoard.WhiteQueen.Bits ^ BoardSquare.D1) | BoardSquare.D3;
      move4.Bits = ( move2.Bits ^ BoardSquare.E6 ) | BoardSquare.E5;
      move5.Bits = (move3.Bits ^ BoardSquare.D3) | BoardSquare.H7;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move3);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move4);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
    #endregion
    #region BLACK
    [Fact]
    public void BlackQueenMoveNorthUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.Black);
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.Black);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D5;
      move2.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      move3.Bits = (testBoard.BlackQueen.Bits ^ BoardSquare.D8) | BoardSquare.D6;
      move4.Bits = (move2.Bits ^ BoardSquare.E3 ) | BoardSquare.E4;
      move5.Bits = (move3.Bits ^ BoardSquare.D6) | BoardSquare.D8;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );

    }
    [Fact]
    public void BlackQueenMoveSouthUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.Black);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D5;
      move2.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      move3.Bits = (testBoard.BlackQueen.Bits ^ BoardSquare.D8) | BoardSquare.D6;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move2 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move3 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
    [Fact]
    public void BlackQueenMoveEastUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.Black);
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.Black);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D5;
      move2.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      move3.Bits = (testBoard.BlackQueen.Bits ^ BoardSquare.D8) | BoardSquare.D6;
      move4.Bits = ( move2.Bits ^ BoardSquare.E3 ) | BoardSquare.E4;
      move5.Bits = (move3.Bits ^ BoardSquare.D6) | BoardSquare.H6;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
    [Fact]
    public void BlackQueenMoveWestUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.Black);
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.Black);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D5;
      move2.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      move3.Bits = (testBoard.BlackQueen.Bits ^ BoardSquare.D8) | BoardSquare.D6;
      move4.Bits = ( move2.Bits ^ BoardSquare.E3 ) | BoardSquare.E4;
      move5.Bits = (move3.Bits ^ BoardSquare.D6) | BoardSquare.A6;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
    [Fact]
    public void BlackQueenMoveNEUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.Black);
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.Black);
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move7 = new QueenBitBoard(ChessPieceColors.Black);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D5;
      move2.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      move3.Bits = (testBoard.BlackQueen.Bits ^ BoardSquare.D8) | BoardSquare.D6;
      move4.Bits = ( move2.Bits ^ BoardSquare.E3 ) | BoardSquare.E4;
      move5.Bits = (move3.Bits ^ BoardSquare.D6) | BoardSquare.A3;
      move6.Bits = ( move4.Bits ^ BoardSquare.E4 ) | BoardSquare.E5;
      move7.Bits = (move5.Bits ^ BoardSquare.A3) | BoardSquare.D6;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move3);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move4);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move5 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move6 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move6 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move7 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
    [Fact]
    public void BlackQueenMoveNWUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.Black);
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.Black);
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move7 = new QueenBitBoard(ChessPieceColors.Black);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D5;
      move2.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      move3.Bits = (testBoard.BlackQueen.Bits ^ BoardSquare.D8) | BoardSquare.D6;
      move4.Bits = ( move2.Bits ^ BoardSquare.E3 ) | BoardSquare.E4;
      move5.Bits = (move3.Bits ^ BoardSquare.D6) | BoardSquare.G3;
      move6.Bits = ( move4.Bits ^ BoardSquare.E4 ) | BoardSquare.E5;
      move7.Bits = (move5.Bits ^ BoardSquare.G3) | BoardSquare.D6;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move3);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move4);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move5 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move6 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move6 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move7 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
    [Fact]
    public void BlackQueenMoveSEUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.Black);
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.Black);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D5;
      move2.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      move3.Bits = (testBoard.BlackQueen.Bits ^ BoardSquare.D8) | BoardSquare.D6;
      move4.Bits = ( move2.Bits ^ BoardSquare.E3 ) | BoardSquare.E4;
      move5.Bits = (move3.Bits ^ BoardSquare.D6) | BoardSquare.G3;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
    [Fact]
    public void BlackQueenMoveSWUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.Black);
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.Black);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D5;
      move2.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      move3.Bits = (testBoard.BlackQueen.Bits ^ BoardSquare.D8) | BoardSquare.D6;
      move4.Bits = ( move2.Bits ^ BoardSquare.E3 ) | BoardSquare.E4;
      move5.Bits = (move3.Bits ^ BoardSquare.D6) | BoardSquare.A3;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
    [Fact]
    public void BlackQueenCaptureUndo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move3 = new QueenBitBoard(ChessPieceColors.Black);
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard move5 = new QueenBitBoard(ChessPieceColors.Black);
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D5;
      move2.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      move3.Bits = (testBoard.BlackQueen.Bits ^ BoardSquare.D8) | BoardSquare.D6;
      move4.Bits = ( move2.Bits ^ BoardSquare.E3 ) | BoardSquare.E4;
      move5.Bits = (move3.Bits ^ BoardSquare.D6) | BoardSquare.H2;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move1);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(move2);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal(expectedHash, actualHash);
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
    #endregion
    [Fact]
    public void RandomPeterTest1_equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      KnightBitBoard move3 = new KnightBitBoard( ChessPieceColors.White );
      KnightBitBoard move4 = new KnightBitBoard( ChessPieceColors.Black );
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.Black );
      QueenBitBoard move7 = new QueenBitBoard( ChessPieceColors.White );
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E4;
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.C7 ) | BoardSquare.C5;
      move3.Bits = ( testBoard.WhiteKnight.Bits ^ BoardSquare.B1 ) | BoardSquare.C3;
      move4.Bits = ( testBoard.BlackKnight.Bits ^ BoardSquare.B8 ) | BoardSquare.C6;
      move5.Bits = ( move1.Bits ^ BoardSquare.F2 ) | BoardSquare.F3;
      move6.Bits = ( move2.Bits ^ BoardSquare.H7 ) | BoardSquare.H5;
      move7.Bits = ( testBoard.WhiteQueen.Bits ^ BoardSquare.D1) | BoardSquare.E2;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move1 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move1 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move2 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move2 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move3 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move5 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move6 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move6 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move7 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );

    }
    [Fact]
    public void RandomPeterTest2_equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      KnightBitBoard move1 = new KnightBitBoard( ChessPieceColors.White );
      KnightBitBoard move2 = new KnightBitBoard( ChessPieceColors.Black );
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      KnightBitBoard move5 = new KnightBitBoard( ChessPieceColors.White );
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.Black );
      KnightBitBoard move7 = new KnightBitBoard( ChessPieceColors.White );
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = ( testBoard.WhiteKnight.Bits ^ BoardSquare.G1 ) | BoardSquare.F3;
      move2.Bits = ( testBoard.BlackKnight.Bits ^ BoardSquare.G8 ) | BoardSquare.F6;
      move3.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.D2 ) | BoardSquare.D4;
      move4.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.E7 ) | BoardSquare.E6;
      move5.Bits = ( move1.Bits ^ BoardSquare.B1 ) | BoardSquare.C3;
      move6.Bits = ( move4.Bits ^ BoardSquare.H7 ) | BoardSquare.H6;
      move7.Bits = ( move5.Bits ^ BoardSquare.F3 ) | BoardSquare.E5;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move1 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move1 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move2 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move2 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move3 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move5 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move6 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move6 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move7 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );

    }
    [Fact]
    public void RandomPeterTest3_equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      BishopBitBoard move3 = new BishopBitBoard( ChessPieceColors.White );
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      BishopBitBoard move5 = new BishopBitBoard( ChessPieceColors.White );
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.Black );
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.B2 ) | BoardSquare.B3;
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.D7 ) | BoardSquare.D5;
      move3.Bits = ( testBoard.WhiteBishop.Bits ^ BoardSquare.C1 ) | BoardSquare.A3;
      move4.Bits = ( move2.Bits ^ BoardSquare.A7 ) | BoardSquare.A5;
      move5.Bits = ( move3.Bits ^ BoardSquare.A3 ) | BoardSquare.E7;
      move6.Bits = ( move4.Bits ^ BoardSquare.H7 ^ BoardSquare.E7 ) | BoardSquare.H5;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move1 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move1 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move2 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move2 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move3 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move4 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move5 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move6 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
    [Fact]
    public void RandomPeterTest4_equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      KnightBitBoard move1 = new KnightBitBoard( ChessPieceColors.White );
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = ( testBoard.WhiteKnight.Bits ^ BoardSquare.B1 ) | BoardSquare.A3;
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.G7 ) | BoardSquare.G5;
      move3.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.H2 ) | BoardSquare.H3;
      move4.Bits = ( move2.Bits ^ BoardSquare.H7 ) | BoardSquare.H5;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move1 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move1 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move2 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move2 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move3 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
    [Fact]
    public void RandomPeterTest5_equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      testBoard.Update(move1);
      ulong hash1 = testBoard.BoardHash.Key;
      ColoredBitBoard move2 = null;
      move2 = NegaMax.GetBestMove( testBoard, 3, ChessPieceColors.Black, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
      Assert.Equal( hash1, testBoard.BoardHash.Key );
      //PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      //QueenBitBoard move3 = new QueenBitBoard( ChessPieceColors.White );
      //ulong expectedHash;
      //ulong actualHash;

      //move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.D2 ) | BoardSquare.D3;
      //move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.H7 ) | BoardSquare.H6;
      //move3.Bits = ( testBoard.WhiteQueen.Bits ^ BoardSquare.D1 ) | BoardSquare.D2;

      //expectedHash = testBoard.BoardHash.Key;
      //testBoard.Update( move1 );
      //testBoard.Undo();
      //actualHash = testBoard.BoardHash.Key;

      //Assert.Equal( expectedHash, actualHash );
      //testBoard.Update( move1 );

      //expectedHash = testBoard.BoardHash.Key;
      //testBoard.Update( move2 );
      //testBoard.Undo();
      //actualHash = testBoard.BoardHash.Key;

      //Assert.Equal( expectedHash, actualHash );
      //testBoard.Update( move2 );

      //expectedHash = testBoard.BoardHash.Key;
      //testBoard.Update( move3 );
      //testBoard.Undo();
      //actualHash = testBoard.BoardHash.Key;

      //Assert.Equal( expectedHash, actualHash );
    }
    [Fact]
    public void RandomPeterTest6_equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      ulong expectedHash;
      ulong actualHash;

      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.D7 ) | BoardSquare.D5;
      move3.Bits = ( move1.Bits ^ BoardSquare.H2 ) | BoardSquare.H3;
      move4.Bits = ( move2.Bits ^ BoardSquare.H7 ) | BoardSquare.H5;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move1 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move1 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move2 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move2 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move3 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
      testBoard.Update( move3 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move4 );
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, actualHash );
    }
  }
}

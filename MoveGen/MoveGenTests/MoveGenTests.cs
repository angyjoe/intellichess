using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public class MoveGenTests {
    #region MoveGen tests, for reals

    [Fact]
    public void isOwnKingAttackableTest_White() {
      //Testinput
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.WhiteKing.Bits = ( BoardSquare.D1 );
      inputChessBoard.BlackKing.Bits = ( BoardSquare.D8 );
      inputChessBoard.BlackRook.Bits = ( BoardSquare.E4 );
      KingBitBoard inputMove = new KingBitBoard( ChessPieceColors.White );
      inputMove.Bits = ( BoardSquare.E1 );

      //Output
      Assert.True( MoveGen.isOwnKingAttackable_TEST( inputChessBoard, inputMove, ChessPieceColors.Black ) );
    }

    [Fact]
    public void isOwnKingAttackableTest_Black() {
      ///Testinput
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.WhiteKing.Bits = ( BoardSquare.D1 );
      inputChessBoard.WhiteRook.Bits = ( BoardSquare.E4 );
      inputChessBoard.BlackKing.Bits = ( BoardSquare.D8 );
      KingBitBoard inputMove = new KingBitBoard( ChessPieceColors.Black );
      inputMove.Bits = ( BoardSquare.E8 );

      //Output
      Assert.True( MoveGen.isOwnKingAttackable_TEST( inputChessBoard, inputMove, ChessPieceColors.White ) );
    }

    [Fact]
    public void isEnemyKingAttackableTest_White() {
      //Testinput
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.WhiteKing.Bits = ( BoardSquare.D1 );
      inputChessBoard.BlackKing.Bits = ( BoardSquare.D8 );
      inputChessBoard.WhiteRook.Bits = ( BoardSquare.E4 );
      RookBitBoard inputMove = new RookBitBoard( ChessPieceColors.White );
      inputMove.Bits = ( BoardSquare.D4 );

      //Output
      Assert.True( MoveGen.isEnemyKingAttackable_TEST( inputChessBoard, inputMove, ChessPieceColors.White ) );
    }
    [Fact]
    public void isEnemyKingAttackableTest_Black() {
      //Testinput
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.WhiteKing.Bits = ( BoardSquare.D1 );
      inputChessBoard.BlackKing.Bits = ( BoardSquare.D8 );
      inputChessBoard.BlackRook.Bits = ( BoardSquare.E4 );
      RookBitBoard inputMove = new RookBitBoard( ChessPieceColors.Black );
      inputMove.Bits = ( BoardSquare.D4 );

      //Output
      Assert.True( MoveGen.isEnemyKingAttackable_TEST( inputChessBoard, inputMove, ChessPieceColors.Black ) );
    }

    [Fact]
    public void RemoveOwnKingChecksTest() {
      //Test input
      ChessBoard inputChessBoard = new ChessBoard();
      List<ColoredBitBoard> inputPseudoLegalMoves = new List<ColoredBitBoard>();
      inputChessBoard.WhiteKing.Bits = ( BoardSquare.D1 );
      inputChessBoard.BlackKing.Bits = ( BoardSquare.D8 );
      inputChessBoard.WhiteRook.Bits = ( BoardSquare.H2 );
      inputChessBoard.BlackRook.Bits = ( BoardSquare.E4 );

      KingBitBoard inputMove_1 = new KingBitBoard( ChessPieceColors.White );
      RookBitBoard inputMove_2 = new RookBitBoard( ChessPieceColors.White );
      inputMove_1.Bits = BoardSquare.E1; //Should make the king check
      inputMove_2.Bits = BoardSquare.H3; //Should NOT

      inputPseudoLegalMoves.Add( inputMove_1 );
      inputPseudoLegalMoves.Add( inputMove_2 );

      //Expected output
      List<ColoredBitBoard> expectedOutput = new List<ColoredBitBoard>();
      expectedOutput.Add( inputMove_2 );

      //Actual output
      List<ColoredBitBoard> actualOutput = new List<ColoredBitBoard>();
      actualOutput = MoveGen.RemoveOwnKingChecks_TEST( inputChessBoard, inputPseudoLegalMoves, ChessPieceColors.White );

      List<ColoredBitBoard> expectedOutput_COPY = expectedOutput.Select( p => p ).ToList();

      Assert.Equal( expectedOutput.Count, actualOutput.Count );

      for ( int i = 0; i < expectedOutput.Count; i++ ) {
        for ( int j = 0; j < actualOutput.Count; j++ ) {
          if ( expectedOutput[i].Bits.Equals( actualOutput[j].Bits ) ) {
            expectedOutput_COPY.RemoveAt( expectedOutput_COPY.IndexOf( expectedOutput[i] ) );
          }
        }
      }
      Assert.Equal( 0, expectedOutput_COPY.Count );
    }

    [Fact]
    public void SetDoesCheckTest_White() {
      //input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackKing.Bits = BoardSquare.E8;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;
      inputChessBoard.WhiteRook.Bits = BoardSquare.F1;

      List<ColoredBitBoard> inputMoves = new List<ColoredBitBoard>();
      RookBitBoard move1 = new RookBitBoard( ChessPieceColors.White );
      move1.Bits = BoardSquare.F8;
      RookBitBoard move2 = new RookBitBoard( ChessPieceColors.White );
      move2.Bits = BoardSquare.H1;
      KingBitBoard move3 = new KingBitBoard( ChessPieceColors.White );
      move3.Bits = BoardSquare.D2;
      inputMoves.Add( move1 );
      inputMoves.Add( move2 );
      inputMoves.Add( move3 );


      //expected output
      List<ColoredBitBoard> expectedOutputMoves = new List<ColoredBitBoard>();
      RookBitBoard board1 = new RookBitBoard( ChessPieceColors.White );
      board1.Bits = BoardSquare.F8;
      board1.DoesCheck = true;
      RookBitBoard board2 = new RookBitBoard( ChessPieceColors.White );
      board2.Bits = BoardSquare.H1;
      KingBitBoard board3 = new KingBitBoard( ChessPieceColors.White );
      board3.Bits = BoardSquare.D2;


      expectedOutputMoves.Add( board1 );
      expectedOutputMoves.Add( board2 );
      expectedOutputMoves.Add( board3 );

      //actual output
      List<ColoredBitBoard> actualOuputMoves = MoveGen.SetDoesCheck_TEST( inputChessBoard, inputMoves, ChessPieceColors.White );

      bool doesSetDoesCheckWork = true;

      foreach ( ColoredBitBoard ebb in expectedOutputMoves ) {
        foreach ( ColoredBitBoard abb in actualOuputMoves ) {
          if ( ebb.Bits.Equals( abb.Bits ) && ebb.DoesCheck != abb.DoesCheck ) {
            doesSetDoesCheckWork = false;
          }
        }
      }
      Assert.True( doesSetDoesCheckWork );
    }

    [Fact]
    public void SetDoesCheckTest_Black() {
      //input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackKing.Bits = BoardSquare.E8;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;
      inputChessBoard.BlackRook.Bits = BoardSquare.F8;

      List<ColoredBitBoard> inputMoves = new List<ColoredBitBoard>();
      RookBitBoard move1 = new RookBitBoard( ChessPieceColors.Black );
      move1.Bits = BoardSquare.F1;
      RookBitBoard move2 = new RookBitBoard( ChessPieceColors.Black );
      move2.Bits = BoardSquare.H8;
      KingBitBoard move3 = new KingBitBoard( ChessPieceColors.Black );
      move3.Bits = BoardSquare.D8;
      inputMoves.Add( move1 );
      inputMoves.Add( move2 );
      inputMoves.Add( move3 );


      //expected output
      List<ColoredBitBoard> expectedOutputMoves = new List<ColoredBitBoard>();
      RookBitBoard board1 = new RookBitBoard( ChessPieceColors.Black );
      board1.Bits = BoardSquare.F1;
      board1.DoesCheck = true;
      RookBitBoard board2 = new RookBitBoard( ChessPieceColors.Black );
      board2.Bits = BoardSquare.H8;
      KingBitBoard board3 = new KingBitBoard( ChessPieceColors.Black );
      board3.Bits = BoardSquare.D8;


      expectedOutputMoves.Add( board1 );
      expectedOutputMoves.Add( board2 );
      expectedOutputMoves.Add( board3 );

      //actual output
      List<ColoredBitBoard> actualOuputMoves = MoveGen.SetDoesCheck_TEST( inputChessBoard, inputMoves, ChessPieceColors.Black );

      bool doesSetDoesCheckWork = true;

      foreach ( ColoredBitBoard ebb in expectedOutputMoves ) {
        foreach ( ColoredBitBoard abb in actualOuputMoves ) {
          if ( ebb.Bits.Equals( abb.Bits ) && ebb.DoesCheck != abb.DoesCheck ) {
            doesSetDoesCheckWork = false;
          }
        }
      }
      Assert.True( doesSetDoesCheckWork );
    }
    #endregion

    #region MoveGenUtils tests
    //MoveGenUtils tests

    #region SortCapMoves
    /// <summary>
    /// With mixed input. 
    /// </summary>
    [Fact]
    public void SortByCapturingMovesTest_Mixed() {
    //input
      List<ColoredBitBoard> inputMoves = new List<ColoredBitBoard>();
      ChessBoard inputChessBoard = new ChessBoard();
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      inputChessBoard.BlackPawn.Bits = BoardSquare.B2;

      KingBitBoard nonCap1 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard nonCap2 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard cap1 = new KingBitBoard( ChessPieceColors.White );

      nonCap1.Bits = BoardSquare.A2;
      nonCap2.Bits = BoardSquare.B1;
      cap1.Bits = BoardSquare.B2;

      blackPieces.Bits = cap1.Bits;
      whitePieces.Bits = nonCap1.Bits | nonCap2.Bits;

      inputMoves.Add( nonCap1 );
      inputMoves.Add( cap1 );
      inputMoves.Add( nonCap2 );
      
    //actual output
      List<ColoredBitBoard> actualOutput = new List<ColoredBitBoard>();
      actualOutput = MoveGenUtils.SortMovesByCapturing( inputMoves, inputChessBoard, blackPieces, whitePieces);

      int i = 0;
      for ( i = 0; i < actualOutput.Count(); i++ ) {
        if ( !actualOutput[i].IsCapturing )
          break;         
      }
      for ( int j = i; i < actualOutput.Count(); i++ ) {
        Assert.False( actualOutput[j].IsCapturing );
      }
    }
    /// <summary>
    /// Input wit all noncapturing first and all capturing last
    /// </summary>
    [Fact]
    public void SortByCapturingMovesTest_NC() {
      //input
      List<ColoredBitBoard> inputMoves = new List<ColoredBitBoard>();
      ChessBoard inputChessBoard = new ChessBoard();
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();

      inputChessBoard.BlackPawn.Bits = BoardSquare.B2;

      KingBitBoard nonCap1 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard nonCap2 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard cap1 = new KingBitBoard( ChessPieceColors.White );

      nonCap1.Bits = BoardSquare.A2;
      nonCap2.Bits = BoardSquare.B1;
      cap1.Bits = BoardSquare.B2;

      blackPieces.Bits = cap1.Bits;
      whitePieces.Bits = nonCap2.Bits | nonCap1.Bits;
      inputMoves.Add( nonCap1 );
      inputMoves.Add( nonCap2 );
      inputMoves.Add( cap1 );

      //actual output
      List<ColoredBitBoard> actualOutput = new List<ColoredBitBoard>();
      actualOutput = MoveGenUtils.SortMovesByCapturing( inputMoves, inputChessBoard, blackPieces, whitePieces );

      int i = 0;
      for ( i = 0; i < actualOutput.Count(); i++ ) {
        if ( !actualOutput[i].IsCapturing )
          break;
      }
      for ( int j = i; i < actualOutput.Count(); i++ ) {
        Assert.False( actualOutput[j].IsCapturing );
      }
    }
    /// <summary>
    /// Already sorted:
    /// capturing first, noncapturing last
    /// </summary>
    [Fact]
    public void SortByCapturingMovesTest_CN() {
      //input
      List<ColoredBitBoard> inputMoves = new List<ColoredBitBoard>();
      ChessBoard inputChessBoard = new ChessBoard();

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      inputChessBoard.BlackPawn.Bits = BoardSquare.B2;

      KingBitBoard nonCap1 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard nonCap2 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard cap1 = new KingBitBoard( ChessPieceColors.White );

      nonCap1.Bits = BoardSquare.A2;
      nonCap2.Bits = BoardSquare.B1;
      cap1.Bits = BoardSquare.B2;

      blackPieces.Bits = cap1.Bits;
      whitePieces.Bits = nonCap2.Bits | nonCap1.Bits;

      inputMoves.Add( cap1 );
      inputMoves.Add( nonCap1 );
      inputMoves.Add( nonCap2 );

      //actual output
      List<ColoredBitBoard> actualOutput = new List<ColoredBitBoard>();
      actualOutput = MoveGenUtils.SortMovesByCapturing( inputMoves, inputChessBoard, blackPieces, whitePieces );

      int i = 0;
      for ( i = 0; i < actualOutput.Count(); i++ ) {
        if ( !actualOutput[i].IsCapturing )
          break;
      }
      for ( int j = i; i < actualOutput.Count(); i++ ) {
        Assert.False( actualOutput[j].IsCapturing );
      }
    }
    /// <summary>
    /// Sample only with non capturing moves
    /// </summary>
    [Fact]
    public void SortByCapturingMovesTest_N() {
      //input
      List<ColoredBitBoard> inputMoves = new List<ColoredBitBoard>();
      ChessBoard inputChessBoard = new ChessBoard();

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();

      KingBitBoard nonCap1 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard nonCap2 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard nonCap3 = new KingBitBoard( ChessPieceColors.White );

      nonCap1.Bits = BoardSquare.A2;
      nonCap2.Bits = BoardSquare.B1;
      nonCap3.Bits = BoardSquare.B2;

      whitePieces.Bits = nonCap2.Bits | nonCap1.Bits | nonCap3.Bits;

      inputMoves.Add( nonCap1 );
      inputMoves.Add( nonCap2 );
      inputMoves.Add( nonCap3 );

      //actual output
      List<ColoredBitBoard> actualOutput = new List<ColoredBitBoard>();
      actualOutput = MoveGenUtils.SortMovesByCapturing( inputMoves, inputChessBoard, blackPieces, whitePieces );

      int i = 0;
      for ( i = 0; i < actualOutput.Count(); i++ ) {
        if ( !actualOutput[i].IsCapturing )
          break;
      }
      for ( int j = i; i < actualOutput.Count(); i++ ) {
        Assert.False( actualOutput[j].IsCapturing );
      }
    }

    /// <summary>
    /// Input only with capturing moves
    /// </summary>
    [Fact]
    public void SortByCapturingMovesTest_C() {
      //input
      List<ColoredBitBoard> inputMoves = new List<ColoredBitBoard>();
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackPawn.Bits = BoardSquare.A2 |  BoardSquare.B1 | BoardSquare.B2;

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();

      KingBitBoard Cap1 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard Cap2 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard Cap3 = new KingBitBoard( ChessPieceColors.White );

      Cap1.Bits = BoardSquare.A2;
      Cap2.Bits = BoardSquare.B1;
      Cap3.Bits = BoardSquare.B2;
      
      inputMoves.Add( Cap1 );
      inputMoves.Add( Cap2 );
      inputMoves.Add( Cap3 );

      blackPieces.Bits = Cap1.Bits | Cap2.Bits | Cap3.Bits;
      whitePieces.Bits = BoardSquare.A1;
      
      //actual output
      List<ColoredBitBoard> actualOutput = new List<ColoredBitBoard>();
      actualOutput = MoveGenUtils.SortMovesByCapturing( inputMoves, inputChessBoard, blackPieces, whitePieces );

      int i = 0;
      for ( i = 0; i < actualOutput.Count(); i++ ) {
        if ( !actualOutput[i].IsCapturing )
          break;
      }
      Assert.Equal( i, actualOutput.Count() );
    }

    #endregion

    /// <summary>
    /// SetIsCapture
    /// </summary>
    /// 
    [Fact]
    public void SetIsCapturingTest() {
      List<ColoredBitBoard> inputMoves = new List<ColoredBitBoard>();
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();

      KingBitBoard nonCap1 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard nonCap2 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard cap1 = new KingBitBoard( ChessPieceColors.White );

      blackPieces.Bits = BoardSquare.B2;
      whitePieces.Bits = BoardSquare.A1;

      nonCap1.Bits = BoardSquare.A2;
      nonCap2.Bits = BoardSquare.B1;
      cap1.Bits = BoardSquare.B2;

      inputMoves.Add( nonCap1 );
      inputMoves.Add( cap1 );
      inputMoves.Add( nonCap2 );
      
      List<ColoredBitBoard> actualMoves = MoveGenUtils.SetIsCapturing( inputMoves, blackPieces, whitePieces );

      Assert.True( actualMoves[1].IsCapturing );
    }
    [Fact]
    public void SortCapturingMovesTest_Early() {
    //input
      List<ColoredBitBoard> inputMoves = new List<ColoredBitBoard>();
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackPawn.Bits = BoardSquare.C6;
      inputChessBoard.BlackKnight.Bits = BoardSquare.C7;
      inputChessBoard.BlackBishop.Bits = BoardSquare.C8;
      inputChessBoard.BlackQueen.Bits = BoardSquare.D8;
      inputChessBoard.BlackRook.Bits = BoardSquare.E7;

      QueenBitBoard qMov1 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard qMov2 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard qMov3 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard qMov4 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard qMov5 = new QueenBitBoard( ChessPieceColors.White );

      qMov1.Bits = inputChessBoard.BlackRook.Bits;
      qMov2.Bits = inputChessBoard.BlackQueen.Bits;
      qMov3.Bits = inputChessBoard.BlackPawn.Bits;
      qMov4.Bits = inputChessBoard.BlackKnight.Bits;
      qMov5.Bits = inputChessBoard.BlackBishop.Bits;
      inputChessBoard.SetGameStage( ChessBoardGameStage.Early );

      
      inputMoves.Add( qMov1 );
      inputMoves.Add( qMov2 );
      inputMoves.Add( qMov3 );
      inputMoves.Add( qMov4 );
      inputMoves.Add( qMov5 );

      //expected output
      List<ColoredBitBoard> expectedResult = new List<ColoredBitBoard>() {
        qMov2, 
        qMov1, 
        qMov4, 
        qMov5,
        qMov3
      };

      //actual output

      List<ColoredBitBoard> actualResult = new List<ColoredBitBoard>();
      actualResult = MoveGenUtils.SortCapturingMoves( inputMoves, inputChessBoard );

      bool similar = true; 

      for ( int i = 0; i < actualResult.Count(); i++ ) {
        if ( actualResult[i].Bits != expectedResult[i].Bits ) {
          similar = false;
        }
      }
      Assert.True( similar );
    }
    [Fact]
    public void SortCapturingMovesTest_Late() {
      //input
      List<ColoredBitBoard> inputMoves = new List<ColoredBitBoard>();
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackPawn.Bits = BoardSquare.C6;
      inputChessBoard.BlackKnight.Bits = BoardSquare.C7;
      inputChessBoard.BlackBishop.Bits = BoardSquare.C8;
      inputChessBoard.BlackQueen.Bits = BoardSquare.D8;
      inputChessBoard.BlackRook.Bits = BoardSquare.E7;

      QueenBitBoard qMov1 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard qMov2 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard qMov3 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard qMov4 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard qMov5 = new QueenBitBoard( ChessPieceColors.White );

      qMov1.Bits = inputChessBoard.BlackRook.Bits;
      qMov2.Bits = inputChessBoard.BlackQueen.Bits;
      qMov3.Bits = inputChessBoard.BlackPawn.Bits;
      qMov4.Bits = inputChessBoard.BlackKnight.Bits;
      qMov5.Bits = inputChessBoard.BlackBishop.Bits;
      inputChessBoard.SetGameStage( ChessBoardGameStage.Late );

      inputMoves.Add( qMov1 );
      inputMoves.Add( qMov2 );
      inputMoves.Add( qMov3 );
      inputMoves.Add( qMov4 );
      inputMoves.Add( qMov5 );

      //expected output
      List<ColoredBitBoard> expectedResult = new List<ColoredBitBoard>() {
        qMov2, 
        qMov1,
        qMov5,
        qMov4, 
        qMov3
      };

      //actual output

      List<ColoredBitBoard> actualResult = new List<ColoredBitBoard>();
      actualResult = MoveGenUtils.SortCapturingMoves( inputMoves, inputChessBoard );

      bool similar = true;

      for ( int i = 0; i < actualResult.Count(); i++ ) {
        if ( actualResult[i].Bits != expectedResult[i].Bits ) {
          similar = false;
        }
      }
      Assert.True( similar );
    }

    [Fact]
    public void SetWhiteBoardTest() {
      //test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.WhiteKing.Bits = ( BoardSquare.D1 );
      inputChessBoard.WhitePawn.Bits = ( BoardSquare.D2 | BoardSquare.D3 );
      inputChessBoard.BlackPawn.Bits = ( BoardSquare.C7 );

      //test sample output
      EmptyBitBoard testWhiteBoard = new EmptyBitBoard();
      testWhiteBoard.Bits = ( BoardSquare.D1 | BoardSquare.D2 | BoardSquare.D3 );

      //test result output
      EmptyBitBoard testOutput = new EmptyBitBoard();
      testOutput = MoveGenUtils.SetWhiteBoard( inputChessBoard );

      Assert.Equal( testWhiteBoard.Bits, testOutput.Bits );
    }
    [Fact]
    public void SetBlackBoardTest() {
      //test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackPawn.Bits = ( BoardSquare.D7 | BoardSquare.C7 );
      inputChessBoard.BlackKing.Bits = ( BoardSquare.E8 );
      inputChessBoard.WhitePawn.Bits = ( BoardSquare.D2 );

      //test sample output
      EmptyBitBoard testBlackBoard = new EmptyBitBoard();
      testBlackBoard.Bits = ( BoardSquare.D7 | BoardSquare.C7 | BoardSquare.E8 );

      //test result output
      EmptyBitBoard testOutput = new EmptyBitBoard();
      testOutput = MoveGenUtils.SetBlackBoard( inputChessBoard );

      Assert.Equal( testBlackBoard.Bits, testOutput.Bits );
    }
    [Fact]
    public void SetWholeBoardTest() {
      //test input
      ChessBoard inputChessBoard = new ChessBoard();

      inputChessBoard.WhiteBishop.Bits = ( BoardSquare.D1 );
      inputChessBoard.WhitePawn.Bits = ( BoardSquare.D2 | BoardSquare.C1 );
      inputChessBoard.BlackPawn.Bits = ( BoardSquare.D7 | BoardSquare.C7 );
      inputChessBoard.BlackKing.Bits = ( BoardSquare.E8 );


      //test sample output
      EmptyBitBoard testWholeBoard = new EmptyBitBoard();

      testWholeBoard.Bits = ( BoardSquare.D1 | BoardSquare.D2 | BoardSquare.C1 | BoardSquare.D7 | BoardSquare.C7 | BoardSquare.E8 );


      //test result output
      EmptyBitBoard testOutput = new EmptyBitBoard();
      testOutput = MoveGenUtils.SetWholeBoard( inputChessBoard );

      Assert.Equal( testWholeBoard.Bits, testOutput.Bits );
    }

    #endregion
    #region PawnMoveGen tests
    //Pawn move generation tests
    [Fact]
    public void ComputeBlackPawnTest() {
      //Input params
      PawnBitBoard inputPawnBB = new PawnBitBoard( ChessPieceColors.Black );
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      ChessBoard inputChessBoard = new ChessBoard();

      inputChessBoard.InitializeScenario( new ScenarioList {
        { BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },
        { BoardSquare.D1, ChessPieceType.King, ChessPieceColors.White },
        { BoardSquare.D7, ChessPieceType.Pawn, ChessPieceColors.Black },
        { BoardSquare.C6, ChessPieceType.Pawn, ChessPieceColors.White }
      } );

      blackPieces = MoveGenUtils.SetBlackBoard( inputChessBoard );
      whitePieces = MoveGenUtils.SetWhiteBoard( inputChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( inputChessBoard );
      inputPawnBB.Bits = ( BoardSquare.D7 );
      /*
      inputPawnBB.SetBit(52);
      */

      //Test params
      PawnBitBoard testBitBoard = new PawnBitBoard( ChessPieceColors.Black );
      testBitBoard.Bits = ( BoardSquare.D6 | BoardSquare.D5 | BoardSquare.C6 );
      /*
      testBitBoard.SetBit(44);
      testBitBoard.SetBit(36);
      */

      //TESTING BUSINESS
      PawnBitBoard outputBitBoard = new PawnBitBoard( ChessPieceColors.Black );
      outputBitBoard = PawnMoveGen.Test_ComputeBlackPawn( inputPawnBB, inputChessBoard, blackPieces, whitePieces, allPieces );
      Assert.Equal( testBitBoard.Bits, outputBitBoard.Bits );
    }

    [Fact]
    public void ComputeWhitePawnTest() {
      //Input params
      PawnBitBoard inputPawnBB = new PawnBitBoard( ChessPieceColors.White );
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      ChessBoard inputChessBoard = new ChessBoard();

      inputChessBoard.InitializeScenario( new ScenarioList {
        { BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },
        { BoardSquare.D1, ChessPieceType.King, ChessPieceColors.White },
        { BoardSquare.D2, ChessPieceType.Pawn, ChessPieceColors.White },
        { BoardSquare.C3, ChessPieceType.Pawn, ChessPieceColors.Black }
      } );

      blackPieces = MoveGenUtils.SetBlackBoard( inputChessBoard );
      whitePieces = MoveGenUtils.SetWhiteBoard( inputChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( inputChessBoard );
      inputPawnBB.Bits = ( BoardSquare.D2 );
      //Test params
      PawnBitBoard testBitBoard = new PawnBitBoard( ChessPieceColors.White );
      testBitBoard.Bits = ( BoardSquare.D3 | BoardSquare.D4 | BoardSquare.C3 );

      //TESTING BUSINESS
      PawnBitBoard outputBitBoard = new PawnBitBoard( ChessPieceColors.White );
      outputBitBoard = PawnMoveGen.Test_ComputeWhitePawn( inputPawnBB, inputChessBoard, blackPieces, whitePieces, allPieces );

      Assert.Equal( testBitBoard.Bits, outputBitBoard.Bits );
    }
        
    [Fact]
    public void PawnChessBoardResultTest_White() {
      //start params
      ChessBoard startChessBoard = new ChessBoard();
      PawnBitBoard startBitBoard = new PawnBitBoard( ChessPieceColors.White );
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      startChessBoard.WhitePawn.Bits = ( BoardSquare.E2 );

      blackPieces = MoveGenUtils.SetBlackBoard( startChessBoard );
      whitePieces = MoveGenUtils.SetWhiteBoard( startChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( startChessBoard );
      //test params
      List<PawnBitBoard> testPawnBBs = new List<PawnBitBoard>();
      PawnBitBoard testPawnBB_1 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard testPawnBB_2 = new PawnBitBoard( ChessPieceColors.White );
      testPawnBB_1.Bits = ( BoardSquare.E3 );
      testPawnBB_2.Bits = ( BoardSquare.E4 );
      testPawnBBs.Add( testPawnBB_1 );
      testPawnBBs.Add( testPawnBB_2 );




      //Out params
      List<PawnBitBoard> outPawnBBs = new List<PawnBitBoard>();
      outPawnBBs = PawnMoveGen.PawnBitBoardResults( startChessBoard, ChessPieceColors.White, blackPieces, whitePieces, allPieces );

      //check if testlist and outputlist are of equal size
      Assert.Equal( testPawnBBs.Count, outPawnBBs.Count );

      //check if test and  out lists contains same elements
      List<PawnBitBoard> testPawnBBs_COPY = new List<PawnBitBoard>();
      testPawnBBs_COPY = testPawnBBs.Select( p => p ).ToList();

      for ( int i = 0; i < testPawnBBs.Count; i++ ) {
        for ( int j = 0; j < outPawnBBs.Count; j++ ) {
          if ( testPawnBBs[i].Bits == outPawnBBs[j].Bits ) {
            testPawnBBs_COPY.RemoveAt( testPawnBBs_COPY.IndexOf( testPawnBBs[i] ) );
          }
        }
      }
      Assert.Equal( 0, testPawnBBs_COPY.Count );
    }

    [Fact]
    public void PawnChessBoardResultTest_Black() {
      //start params
      ChessBoard startChessBoard = new ChessBoard();
      PawnBitBoard startBitBoard = new PawnBitBoard( ChessPieceColors.Black );
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      startChessBoard.BlackPawn.Bits = ( BoardSquare.E7 );

      blackPieces = MoveGenUtils.SetBlackBoard( startChessBoard );
      whitePieces = MoveGenUtils.SetWhiteBoard( startChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( startChessBoard );
      //test params
      List<PawnBitBoard> testPawnBBs = new List<PawnBitBoard>();
      PawnBitBoard testPawnBB_1 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard testPawnBB_2 = new PawnBitBoard( ChessPieceColors.Black );
      testPawnBB_1.Bits = ( BoardSquare.E6 );
      testPawnBB_2.Bits = ( BoardSquare.E5 );
      testPawnBBs.Add( testPawnBB_1 );
      testPawnBBs.Add( testPawnBB_2 );




      //Out params
      List<PawnBitBoard> outPawnBBs = new List<PawnBitBoard>();
      outPawnBBs = PawnMoveGen.PawnBitBoardResults( startChessBoard, ChessPieceColors.Black, blackPieces, whitePieces, allPieces );

      //check if testlist and outputlist are of equal size
      Assert.Equal( testPawnBBs.Count, outPawnBBs.Count );

      //check if test and  out lists contains same elements
      List<PawnBitBoard> testPawnBBs_COPY = new List<PawnBitBoard>();
      testPawnBBs_COPY = testPawnBBs.Select( p => p ).ToList();

      for ( int i = 0; i < testPawnBBs.Count; i++ ) {
        for ( int j = 0; j < outPawnBBs.Count; j++ ) {
          if ( testPawnBBs[i].Bits == outPawnBBs[j].Bits ) {
            testPawnBBs_COPY.RemoveAt( testPawnBBs_COPY.IndexOf( testPawnBBs[i] ) );
          }
        }
      }
      Assert.Equal( 0, testPawnBBs_COPY.Count );
    }

    [Fact]
    public void EnPassantTest_White() {
      //Start params
      ChessBoard inputChessBoard = new ChessBoard();
      PawnBitBoard inputPawnBitBoard = new PawnBitBoard( ChessPieceColors.White );
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      PawnBitBoard blackPawnMoves = new PawnBitBoard( ChessPieceColors.Black );

      inputChessBoard.InitializeScenario( new ScenarioList {
        { BoardSquare.E7, ChessPieceType.Pawn, ChessPieceColors.Black },
        { BoardSquare.F5, ChessPieceType.Pawn, ChessPieceColors.White }
      } );
      blackPawnMoves.Bits = BoardSquare.E5;
      inputChessBoard.Update( blackPawnMoves );

      inputPawnBitBoard.Bits = BoardSquare.F5;
      blackPieces.Bits = BoardSquare.E5;
      whitePieces.Bits = BoardSquare.F5;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;

      //Test params
      PawnBitBoard testPawnBitBoard = new PawnBitBoard( ChessPieceColors.White );
      testPawnBitBoard.Bits = BoardSquare.E6 | BoardSquare.F6;

      //Output params
      PawnBitBoard outputPawnBitBoard = new PawnBitBoard( ChessPieceColors.White );
      outputPawnBitBoard = PawnMoveGen.Test_ComputeWhitePawn( inputPawnBitBoard, inputChessBoard, blackPieces, whitePieces, allPieces );

      Assert.Equal( testPawnBitBoard.Bits, outputPawnBitBoard.Bits );
    }

    [Fact]
    public void EnPassantTest_Black() {
      //Start params
      ChessBoard inputChessBoard = new ChessBoard();
      PawnBitBoard inputPawnBitBoard = new PawnBitBoard( ChessPieceColors.Black );
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      PawnBitBoard whitePawnMoves = new PawnBitBoard( ChessPieceColors.White );

      inputChessBoard.InitializeScenario( new ScenarioList {
        { BoardSquare.F4, ChessPieceType.Pawn, ChessPieceColors.Black },
        { BoardSquare.E2, ChessPieceType.Pawn, ChessPieceColors.White }
      } );
      whitePawnMoves.Bits = BoardSquare.E4;
      inputChessBoard.Update( whitePawnMoves );

      inputPawnBitBoard.Bits = BoardSquare.F4;
      blackPieces.Bits = BoardSquare.F4;
      whitePieces.Bits = BoardSquare.E4;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;

      //Test params
      PawnBitBoard testPawnBitBoard = new PawnBitBoard( ChessPieceColors.Black );
      testPawnBitBoard.Bits = BoardSquare.E3 | BoardSquare.F3;

      //Output params
      PawnBitBoard outputPawnBitBoard = new PawnBitBoard( ChessPieceColors.Black );
      outputPawnBitBoard = PawnMoveGen.Test_ComputeBlackPawn( inputPawnBitBoard, inputChessBoard, blackPieces, whitePieces, allPieces );

      Assert.Equal( testPawnBitBoard.Bits, outputPawnBitBoard.Bits );
    }

    [Fact]
    public void PawnMoveGen_DoesPromoteWhite_Equal() {
      ChessBoard startChessBoard = new ChessBoard();
      PawnBitBoard startBitBoard = new PawnBitBoard( ChessPieceColors.Black );
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      startChessBoard.WhitePawn.Bits = ( BoardSquare.E7 );

      blackPieces = MoveGenUtils.SetBlackBoard( startChessBoard );
      whitePieces = MoveGenUtils.SetWhiteBoard( startChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( startChessBoard );

      //test params
      List<PawnBitBoard> testPawnBBs = new List<PawnBitBoard>();
      PawnBitBoard testPawnBB_R = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard testPawnBB_B = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard testPawnBB_Q = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard testPawnBB_N = new PawnBitBoard( ChessPieceColors.White );
      testPawnBB_R.Bits = ( BoardSquare.E8 );
      testPawnBB_B.Bits = ( BoardSquare.E8 );
      testPawnBB_Q.Bits = ( BoardSquare.E8 );
      testPawnBB_N.Bits = ( BoardSquare.E8 );
      testPawnBB_R.Promote( PawnBitBoard.PromotionPiece.Rook );
      testPawnBB_B.Promote( PawnBitBoard.PromotionPiece.Bishop );
      testPawnBB_Q.Promote( PawnBitBoard.PromotionPiece.Queen );
      testPawnBB_N.Promote( PawnBitBoard.PromotionPiece.Knight );
      testPawnBBs.Add( testPawnBB_R );
      testPawnBBs.Add( testPawnBB_B );
      testPawnBBs.Add( testPawnBB_Q );
      testPawnBBs.Add( testPawnBB_N );

      //Out params
      List<PawnBitBoard> outPawnBBs = new List<PawnBitBoard>();
      outPawnBBs = PawnMoveGen.PawnBitBoardResults( startChessBoard, ChessPieceColors.White, blackPieces, whitePieces, allPieces );

      //check if testlist and outputlist are of equal size
      Assert.Equal( testPawnBBs.Count, outPawnBBs.Count );

      //check if test and  out lists contains same elements
      List<PawnBitBoard> testPawnBBs_COPY = new List<PawnBitBoard>();
      testPawnBBs_COPY = testPawnBBs.Select( p => p ).ToList();
    
      for ( int i = 0; i < testPawnBBs.Count; i++ ) {
        for ( int j = 0; j < outPawnBBs.Count; j++ ) {
          if ((testPawnBBs[i].Bits == outPawnBBs[j].Bits) && (testPawnBBs[i].Promotion == outPawnBBs[j].Promotion)) {
            testPawnBBs_COPY.RemoveAt( testPawnBBs_COPY.IndexOf( testPawnBBs[i] ) );
          }
        }
      }
      Assert.Equal( 0, testPawnBBs_COPY.Count );
    }

    [Fact]
    public void PawnMoveGen_DoesPromoteBlack_Equal() {
      ChessBoard startChessBoard = new ChessBoard();
      PawnBitBoard startBitBoard = new PawnBitBoard( ChessPieceColors.Black );
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard blackPieces = new EmptyBitBoard();      
      EmptyBitBoard allPieces = new EmptyBitBoard();
      startChessBoard.BlackPawn.Bits = ( BoardSquare.E2 );
            
      whitePieces = MoveGenUtils.SetWhiteBoard( startChessBoard );
      blackPieces = MoveGenUtils.SetBlackBoard( startChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( startChessBoard );

      //test params
      List<PawnBitBoard> testPawnBBs = new List<PawnBitBoard>();
      PawnBitBoard testPawnBB_R = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard testPawnBB_B = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard testPawnBB_Q = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard testPawnBB_N = new PawnBitBoard( ChessPieceColors.Black );
      testPawnBB_R.Bits = ( BoardSquare.E1 );
      testPawnBB_B.Bits = ( BoardSquare.E1 );
      testPawnBB_Q.Bits = ( BoardSquare.E1 );
      testPawnBB_N.Bits = ( BoardSquare.E1 );
      testPawnBB_R.Promote( PawnBitBoard.PromotionPiece.Rook );
      testPawnBB_B.Promote( PawnBitBoard.PromotionPiece.Bishop );
      testPawnBB_Q.Promote( PawnBitBoard.PromotionPiece.Queen );
      testPawnBB_N.Promote( PawnBitBoard.PromotionPiece.Knight );
      testPawnBBs.Add( testPawnBB_R );
      testPawnBBs.Add( testPawnBB_B );
      testPawnBBs.Add( testPawnBB_Q );
      testPawnBBs.Add( testPawnBB_N );

      //Out params
      List<PawnBitBoard> outPawnBBs = new List<PawnBitBoard>();
      outPawnBBs = PawnMoveGen.PawnBitBoardResults( startChessBoard, ChessPieceColors.Black, whitePieces, blackPieces, allPieces );

      //check if testlist and outputlist are of equal size
      Assert.Equal( testPawnBBs.Count, outPawnBBs.Count );

      //check if test and  out lists contains same elements
      List<PawnBitBoard> testPawnBBs_COPY = new List<PawnBitBoard>();
      testPawnBBs_COPY = testPawnBBs.Select( p => p ).ToList();

      for ( int i = 0; i < testPawnBBs.Count; i++ ) {
        for ( int j = 0; j < outPawnBBs.Count; j++ ) {
          if ( ( testPawnBBs[i].Bits == outPawnBBs[j].Bits ) && ( testPawnBBs[i].Promotion == outPawnBBs[j].Promotion ) ) {
            testPawnBBs_COPY.RemoveAt( testPawnBBs_COPY.IndexOf( testPawnBBs[i] ) );
          }
        }
      }
      Assert.Equal( 0, testPawnBBs_COPY.Count );
    }
    #endregion
    #region KingMoveGen tests
    [Fact]
    public void ComputeWhiteKingTest() {
      //Test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.WhiteKing.Bits = BoardSquare.D3;
      inputChessBoard.WhitePawn.Bits = BoardSquare.D4;
      inputChessBoard.BlackPawn.Bits = BoardSquare.E3;
      inputChessBoard.BlackKing.Bits = BoardSquare.D8;

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      blackPieces = MoveGenUtils.SetBlackBoard( inputChessBoard );
      whitePieces = MoveGenUtils.SetWhiteBoard( inputChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( inputChessBoard );

      KingBitBoard inputKingBB = new KingBitBoard( ChessPieceColors.White );
      inputKingBB.Bits = BoardSquare.H3;

      //Test sample result
      KingBitBoard testBitBoard = new KingBitBoard( ChessPieceColors.White );
      testBitBoard.Bits = ( BoardSquare.H4 | BoardSquare.G3 | BoardSquare.H2 | BoardSquare.G2 | BoardSquare.G4);

      //Test output result
      KingBitBoard outputBitBoard = KingMoveGen.Test_ComputeWhiteKing( inputKingBB, inputChessBoard, blackPieces, whitePieces, allPieces );
      Assert.Equal( testBitBoard.Bits, outputBitBoard.Bits );
    }
    [Fact]
    public void ComputeBlackKingTest() {
      //Test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackKing.Bits = BoardSquare.D6;
      inputChessBoard.BlackPawn.Bits = BoardSquare.D5;
      inputChessBoard.WhitePawn.Bits = BoardSquare.E6;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      blackPieces = MoveGenUtils.SetBlackBoard( inputChessBoard );
      whitePieces = MoveGenUtils.SetWhiteBoard( inputChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( inputChessBoard );

      KingBitBoard inputKingBB = new KingBitBoard( ChessPieceColors.Black );
      inputKingBB.Bits = BoardSquare.A6;

      //Test sample result
      KingBitBoard testBitBoard = new KingBitBoard( ChessPieceColors.Black );
      testBitBoard.Bits = ( BoardSquare.A7 | BoardSquare.A5 | BoardSquare.B7 | BoardSquare.B6 | BoardSquare.B5 );

      //Test output result
      KingBitBoard outputBitBoard = KingMoveGen.Test_ComputeBlackKing( inputKingBB, inputChessBoard, blackPieces, whitePieces, allPieces );
      Assert.Equal( testBitBoard.Bits, outputBitBoard.Bits );
    }
    [Fact]
    public void ComputeWhiteKingTest_ClippingH() {
      //Test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.WhiteKing.Bits = BoardSquare.D3;
      inputChessBoard.WhitePawn.Bits = BoardSquare.D4;
      inputChessBoard.BlackPawn.Bits = BoardSquare.E3;
      inputChessBoard.BlackKing.Bits = BoardSquare.D8;

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      blackPieces = MoveGenUtils.SetBlackBoard( inputChessBoard );
      whitePieces = MoveGenUtils.SetWhiteBoard( inputChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( inputChessBoard );

      KingBitBoard inputKingBB = new KingBitBoard( ChessPieceColors.White );
      inputKingBB.Bits = BoardSquare.H3;

      //Test sample result
      KingBitBoard testBitBoard = new KingBitBoard( ChessPieceColors.White );
      testBitBoard.Bits = ( BoardSquare.H4 | BoardSquare.G3 | BoardSquare.H2 | BoardSquare.G2 | BoardSquare.G4 );

      //Test output result
      KingBitBoard outputBitBoard = KingMoveGen.Test_ComputeWhiteKing( inputKingBB, inputChessBoard, blackPieces, whitePieces, allPieces );
      Assert.Equal( testBitBoard.Bits, outputBitBoard.Bits );
    }
    [Fact]
    public void ComputeBlackKingTest_ClippingA() {
      //Test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackKing.Bits = BoardSquare.D6;
      inputChessBoard.BlackPawn.Bits = BoardSquare.D5;
      inputChessBoard.WhitePawn.Bits = BoardSquare.E6;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      blackPieces = MoveGenUtils.SetBlackBoard( inputChessBoard );
      whitePieces = MoveGenUtils.SetWhiteBoard( inputChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( inputChessBoard );

      KingBitBoard inputKingBB = new KingBitBoard( ChessPieceColors.Black );
      inputKingBB.Bits = BoardSquare.A6;

      //Test sample result
      KingBitBoard testBitBoard = new KingBitBoard( ChessPieceColors.Black );
      testBitBoard.Bits = ( BoardSquare.A7 | BoardSquare.A5 | BoardSquare.B7 | BoardSquare.B6 | BoardSquare.B5 );

      //Test output result
      KingBitBoard outputBitBoard = KingMoveGen.Test_ComputeBlackKing( inputKingBB, inputChessBoard, blackPieces, whitePieces, allPieces );
      Assert.Equal( testBitBoard.Bits, outputBitBoard.Bits );
    }
    [Fact]
    public void KingBitBoardResultsTest_White() {
      //test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackKing.Bits = BoardSquare.D8;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;
      inputChessBoard.WhitePawn.Bits = BoardSquare.D2;

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      blackPieces = MoveGenUtils.SetBlackBoard( inputChessBoard );
      whitePieces = MoveGenUtils.SetWhiteBoard( inputChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( inputChessBoard );

      //test sample result
      List<KingBitBoard> kingBitBoards = new List<KingBitBoard>();
      KingBitBoard kingBitBoard_1 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard kingBitBoard_2 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard kingBitBoard_3 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard kingBitBoard_4 = new KingBitBoard( ChessPieceColors.White );

      kingBitBoard_1.Bits = BoardSquare.C1;
      kingBitBoard_2.Bits = BoardSquare.C2;
      kingBitBoard_3.Bits = BoardSquare.E1;
      kingBitBoard_4.Bits = BoardSquare.E2;
      kingBitBoards.Add( kingBitBoard_1 );
      kingBitBoards.Add( kingBitBoard_2 );
      kingBitBoards.Add( kingBitBoard_3 );
      kingBitBoards.Add( kingBitBoard_4 );

      //test output result
      List<KingBitBoard> outputBitBoards = new List<KingBitBoard>();
      outputBitBoards = KingMoveGen.Test_KingBitBoardResults( inputChessBoard, ChessPieceColors.White, blackPieces, whitePieces, allPieces );

      Assert.Equal( kingBitBoards.Count, outputBitBoards.Count );

      List<KingBitBoard> kingBitBoards_COPY = new List<KingBitBoard>();
      kingBitBoards_COPY = kingBitBoards.Select( p => p ).ToList();

      for ( int i = 0; i < kingBitBoards.Count; i++ ) {
        for ( int j = 0; j < outputBitBoards.Count; j++ ) {
          if ( kingBitBoards[i].Bits.Equals( outputBitBoards[j].Bits ) ) {
            kingBitBoards_COPY.RemoveAt( kingBitBoards_COPY.IndexOf( kingBitBoards[i] ) );
          }
        }
      }
      Assert.Equal( 0, kingBitBoards_COPY.Count );
    }
    [Fact]
    public void KingBitBoardResultsTest_Black() {
      //test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackKing.Bits = BoardSquare.D8;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;
      inputChessBoard.BlackPawn.Bits = BoardSquare.D7;

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      blackPieces = MoveGenUtils.SetBlackBoard( inputChessBoard );
      whitePieces = MoveGenUtils.SetWhiteBoard( inputChessBoard );
      allPieces = MoveGenUtils.SetWholeBoard( inputChessBoard );

      //test sample result
      List<KingBitBoard> kingBitBoards = new List<KingBitBoard>();
      KingBitBoard kingBitBoard_1 = new KingBitBoard( ChessPieceColors.Black );
      KingBitBoard kingBitBoard_2 = new KingBitBoard( ChessPieceColors.Black );
      KingBitBoard kingBitBoard_3 = new KingBitBoard( ChessPieceColors.Black );
      KingBitBoard kingBitBoard_4 = new KingBitBoard( ChessPieceColors.Black );

      kingBitBoard_1.Bits = BoardSquare.C8;
      kingBitBoard_2.Bits = BoardSquare.C7;
      kingBitBoard_3.Bits = BoardSquare.E8;
      kingBitBoard_4.Bits = BoardSquare.E7;
      kingBitBoards.Add( kingBitBoard_1 );
      kingBitBoards.Add( kingBitBoard_2 );
      kingBitBoards.Add( kingBitBoard_3 );
      kingBitBoards.Add( kingBitBoard_4 );

      //test output result
      List<KingBitBoard> outputBitBoards = new List<KingBitBoard>();
      outputBitBoards = KingMoveGen.Test_KingBitBoardResults( inputChessBoard, ChessPieceColors.Black, blackPieces, whitePieces, allPieces );

      Assert.Equal( kingBitBoards.Count, outputBitBoards.Count );

      List<KingBitBoard> kingBitBoards_COPY = new List<KingBitBoard>();
      kingBitBoards_COPY = kingBitBoards.Select( p => p ).ToList();

      for ( int i = 0; i < kingBitBoards.Count; i++ ) {
        for ( int j = 0; j < outputBitBoards.Count; j++ ) {
          if ( kingBitBoards[i].Bits.Equals( outputBitBoards[j].Bits ) ) {
            kingBitBoards_COPY.RemoveAt( kingBitBoards_COPY.IndexOf( kingBitBoards[i] ) );
          }
        }
      }
      Assert.Equal( 0, kingBitBoards_COPY.Count );
    }
    [Fact]
    public void KingCastlingKingSide_White() {
      //input
      ChessBoard inputChessBoard = new ChessBoard();

      inputChessBoard.InitializeScenario( new ScenarioList {
        {BoardSquare.E1, ChessPieceType.King, ChessPieceColors.White},
        {BoardSquare.H1, ChessPieceType.Rook, ChessPieceColors.White},
        {BoardSquare.D2, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.E2, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.F2, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.E8, ChessPieceType.King, ChessPieceColors.Black}
      } );
      
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      blackPieces.Bits = BoardSquare.E8;
      whitePieces.Bits = BoardSquare.E1 | BoardSquare.H1 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits; 

      //expected output
      KingBitBoard exOut1 = new KingBitBoard( ChessPieceColors.White );

      exOut1.Bits = BoardSquare.D1 | BoardSquare.F1 | BoardSquare.G1;

      //actual output
      KingBitBoard actualOutput = new KingBitBoard(ChessPieceColors.White);

      actualOutput = KingMoveGen.Test_ComputeWhiteKing( inputChessBoard.WhiteKing, inputChessBoard, blackPieces, whitePieces, allPieces );


      Assert.Equal( exOut1.Bits, actualOutput.Bits );


    }
    [Fact]
    public void KingCastlingQueenSide_White() {
      //input
      ChessBoard inputChessBoard = new ChessBoard();

      inputChessBoard.InitializeScenario( new ScenarioList {
        {BoardSquare.E1, ChessPieceType.King, ChessPieceColors.White},
        {BoardSquare.A1, ChessPieceType.Rook, ChessPieceColors.White},
        {BoardSquare.D2, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.E2, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.F2, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.E8, ChessPieceType.King, ChessPieceColors.Black}
      } );

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      blackPieces.Bits = BoardSquare.E8;
      whitePieces.Bits = BoardSquare.E1 | BoardSquare.H1 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;

      //expected output
      KingBitBoard exOut1 = new KingBitBoard( ChessPieceColors.White );

      exOut1.Bits = BoardSquare.D1 | BoardSquare.F1 | BoardSquare.C1;

      //actual output
      KingBitBoard actualOutput = new KingBitBoard( ChessPieceColors.White );

      actualOutput = KingMoveGen.Test_ComputeWhiteKing( inputChessBoard.WhiteKing, inputChessBoard, blackPieces, whitePieces, allPieces );


      Assert.Equal( exOut1.Bits, actualOutput.Bits );


    }
    [Fact]
    public void KingCastlingKingside_Black() {
      //input
      ChessBoard inputChessBoard = new ChessBoard();

      inputChessBoard.InitializeScenario( new ScenarioList {
        {BoardSquare.E8, ChessPieceType.King, ChessPieceColors.Black},
        {BoardSquare.H8, ChessPieceType.Rook, ChessPieceColors.Black},
        {BoardSquare.D7, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.E7, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.F7, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.E1, ChessPieceType.King, ChessPieceColors.White}
      } );

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      whitePieces.Bits = BoardSquare.E1;
      blackPieces.Bits = BoardSquare.E8 | BoardSquare.H8 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;

      //expected output
      KingBitBoard exOut1 = new KingBitBoard( ChessPieceColors.Black );

      exOut1.Bits = BoardSquare.D8 | BoardSquare.F8 | BoardSquare.G8;

      //actual output
      KingBitBoard actualOutput = new KingBitBoard( ChessPieceColors.Black );

      actualOutput = KingMoveGen.Test_ComputeBlackKing( inputChessBoard.BlackKing, inputChessBoard, blackPieces, whitePieces, allPieces );


      Assert.Equal( exOut1.Bits, actualOutput.Bits );


    }
    [Fact]
    public void KingCastlingQueenSide_Black() {
      //input
      ChessBoard inputChessBoard = new ChessBoard();

      inputChessBoard.InitializeScenario( new ScenarioList {
        {BoardSquare.E8, ChessPieceType.King, ChessPieceColors.Black},
        {BoardSquare.A8, ChessPieceType.Rook, ChessPieceColors.Black},
        {BoardSquare.D7, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.E7, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.F7, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.E1, ChessPieceType.King, ChessPieceColors.White}
      } );

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      whitePieces.Bits = BoardSquare.E1;
      blackPieces.Bits = BoardSquare.E8 | BoardSquare.H8 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;

      //expected output
      KingBitBoard exOut1 = new KingBitBoard( ChessPieceColors.Black );

      exOut1.Bits = BoardSquare.D8 | BoardSquare.F8 | BoardSquare.C8;

      //actual output
      KingBitBoard actualOutput = new KingBitBoard( ChessPieceColors.Black );

      actualOutput = KingMoveGen.Test_ComputeBlackKing( inputChessBoard.BlackKing, inputChessBoard, blackPieces, whitePieces, allPieces );


      Assert.Equal( exOut1.Bits, actualOutput.Bits );
    }
    #endregion
    #region KnightMoveGen tests
    [Fact]
    public void ComputeWhiteKnightTest() {
      //Test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.WhiteKnight.Bits = BoardSquare.B1;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;
      inputChessBoard.WhitePawn.Bits = BoardSquare.A3;
      inputChessBoard.BlackKing.Bits = BoardSquare.D8;
      inputChessBoard.BlackPawn.Bits = BoardSquare.C3;

      KnightBitBoard inputKnightBB = new KnightBitBoard( ChessPieceColors.White );
      inputKnightBB.Bits = BoardSquare.B1;

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      blackPieces.Bits = BoardSquare.D8 | BoardSquare.C3;
      whitePieces.Bits = BoardSquare.B1 | BoardSquare.D1 | BoardSquare.A3;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;


      //Test sample result
      KnightBitBoard testResults = new KnightBitBoard( ChessPieceColors.White );
      testResults.Bits = BoardSquare.C3 | BoardSquare.D2;
      //Test output
      KnightBitBoard testOutput = new KnightBitBoard( ChessPieceColors.White );
      testOutput = KnightMoveGen.ComputeWhiteKnight_Test( inputChessBoard, inputKnightBB, blackPieces, whitePieces, allPieces );

      Assert.Equal( testResults.Bits, testOutput.Bits );
    }
    [Fact]
    public void ComputeBlackKnightTest() {
      //Test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackKing.Bits = BoardSquare.D8;
      inputChessBoard.BlackKnight.Bits = BoardSquare.B8;
      inputChessBoard.BlackPawn.Bits = BoardSquare.A6;
      inputChessBoard.WhitePawn.Bits = BoardSquare.C6;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      blackPieces.Bits = BoardSquare.D8 | BoardSquare.B8 | BoardSquare.A6;
      whitePieces.Bits = BoardSquare.C6 | BoardSquare.D1;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;

      KnightBitBoard inputKnightBB = new KnightBitBoard( ChessPieceColors.Black );
      inputKnightBB.Bits = BoardSquare.B8;

      //Test sample result
      KnightBitBoard testResults = new KnightBitBoard( ChessPieceColors.Black );
      testResults.Bits = BoardSquare.C6 | BoardSquare.D7;
      //Test output
      KnightBitBoard testOutput = new KnightBitBoard( ChessPieceColors.Black );
      testOutput = KnightMoveGen.ComputeBlackKnight_Test( inputChessBoard, inputKnightBB, blackPieces, whitePieces, allPieces );

      Assert.Equal( testResults.Bits, testOutput.Bits );
    }
    [Fact]
    public void KnightBitBoardResultsTest_Black() {
      //Test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.BlackKnight.Bits = BoardSquare.B8;
      inputChessBoard.BlackKing.Bits = BoardSquare.D8;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      blackPieces.Bits = BoardSquare.B8 | BoardSquare.D8;
      whitePieces.Bits = BoardSquare.D1;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //Test sample result
      List<KnightBitBoard> testResults = new List<KnightBitBoard>();
      KnightBitBoard testResult_1 = new KnightBitBoard( ChessPieceColors.Black );
      KnightBitBoard testResult_2 = new KnightBitBoard( ChessPieceColors.Black );
      KnightBitBoard testResult_3 = new KnightBitBoard( ChessPieceColors.Black );
      testResult_1.Bits = BoardSquare.A6;
      testResult_2.Bits = BoardSquare.C6;
      testResult_3.Bits = BoardSquare.D7;
      testResults.Add( testResult_1 );
      testResults.Add( testResult_2 );
      testResults.Add( testResult_3 );
      //Test output
      List<KnightBitBoard> testOutputs = new List<KnightBitBoard>();
      testOutputs = KnightMoveGen.KnightBitBoardResults( inputChessBoard, ChessPieceColors.Black, blackPieces, whitePieces, allPieces );

      Assert.Equal( testResults.Count, testOutputs.Count );

      List<KnightBitBoard> testResults_COPY = new List<KnightBitBoard>();
      testResults_COPY = testResults.Select( p => p ).ToList();

      for ( int i = 0; i < testResults.Count; i++ ) {
        for ( int j = 0; j < testOutputs.Count; j++ ) {
          if ( testResults[i].Bits.Equals( testOutputs[j].Bits ) ) {
            testResults_COPY.RemoveAt( testResults_COPY.IndexOf( testResults[i] ) );
          }
        }
      }
      Assert.Equal( 0, testResults_COPY.Count );
    }
    [Fact]
    public void KnightBitBoardResultsTest_White() {
      //Test input
      ChessBoard inputChessBoard = new ChessBoard();
      inputChessBoard.WhiteKnight.Bits = BoardSquare.B1;
      inputChessBoard.BlackKing.Bits = BoardSquare.D8;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;

      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      whitePieces.Bits = BoardSquare.B1 | BoardSquare.D1;
      blackPieces.Bits = BoardSquare.D8;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //Test sample result
      List<KnightBitBoard> testResults = new List<KnightBitBoard>();
      KnightBitBoard testResult_1 = new KnightBitBoard( ChessPieceColors.White );
      KnightBitBoard testResult_2 = new KnightBitBoard( ChessPieceColors.White );
      KnightBitBoard testResult_3 = new KnightBitBoard( ChessPieceColors.White );
      testResult_1.Bits = BoardSquare.A3;
      testResult_2.Bits = BoardSquare.C3;
      testResult_3.Bits = BoardSquare.D2;
      testResults.Add( testResult_1 );
      testResults.Add( testResult_2 );
      testResults.Add( testResult_3 );
      //Test output
      List<KnightBitBoard> testOutputs = new List<KnightBitBoard>();
      testOutputs = KnightMoveGen.KnightBitBoardResults( inputChessBoard, ChessPieceColors.White, blackPieces, whitePieces, allPieces );

      Assert.Equal( testResults.Count, testOutputs.Count );

      List<KnightBitBoard> testResults_COPY = new List<KnightBitBoard>();
      testResults_COPY = testResults.Select( p => p ).ToList();

      for ( int i = 0; i < testResults.Count; i++ ) {
        for ( int j = 0; j < testOutputs.Count; j++ ) {
          if ( testResults[i].Bits.Equals( testOutputs[j].Bits ) ) {
            testResults_COPY.RemoveAt( testResults_COPY.IndexOf( testResults[i] ) );
          }
        }
      }
      Assert.Equal( 0, testResults_COPY.Count );

    }
    #endregion
    #region RookMoveGen tests
    [Fact]
    public void ComputeWhiteRookTest() {
      //sample input
      ChessBoard inputChessBoard = new ChessBoard();
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      inputChessBoard.WhiteRook.Bits = BoardSquare.C5;
      inputChessBoard.WhitePawn.Bits = BoardSquare.C2;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;
      inputChessBoard.BlackKing.Bits = BoardSquare.D8;
      inputChessBoard.BlackPawn.Bits = BoardSquare.A5;

      RookBitBoard inputRookBitBoard = new RookBitBoard( ChessPieceColors.White );
      inputRookBitBoard.Bits = inputChessBoard.WhiteRook.Bits;

      blackPieces.Bits = BoardSquare.D8 | BoardSquare.A5;
      whitePieces.Bits = BoardSquare.C2 | BoardSquare.C5 | BoardSquare.D1;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      RookBitBoard expectOutBitBoard = new RookBitBoard( ChessPieceColors.White );
      expectOutBitBoard.Bits = BoardSquare.A5 | BoardSquare.B5 | BoardSquare.D5 | BoardSquare.E5 | BoardSquare.F5 | BoardSquare.G5 | BoardSquare.H5 |
                               BoardSquare.C3 | BoardSquare.C4 | BoardSquare.C6 | BoardSquare.C7 | BoardSquare.C8;
      //actual result
      RookBitBoard actualBitBoard = RookMoveGen.ComputeWhiteRook_Test( inputRookBitBoard, inputChessBoard, blackPieces, whitePieces, allPieces );
      Assert.Equal( expectOutBitBoard.Bits, actualBitBoard.Bits );
    }
    [Fact]
    public void ComputeBlackRookTest() {
      //sample input
      ChessBoard inputChessBoard = new ChessBoard();
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();
      RookBitBoard inputRookBitBoard = new RookBitBoard( ChessPieceColors.White );

      inputChessBoard.WhitePawn.Bits = BoardSquare.A5;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;

      inputChessBoard.BlackRook.Bits = BoardSquare.C5;
      inputChessBoard.BlackKing.Bits = BoardSquare.D8;
      inputChessBoard.BlackPawn.Bits = BoardSquare.C7;

      inputRookBitBoard.Bits = inputChessBoard.BlackRook.Bits;

      blackPieces.Bits = BoardSquare.D8 | BoardSquare.C7 | BoardSquare.C5;
      whitePieces.Bits = BoardSquare.A5 | BoardSquare.D1;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      RookBitBoard expectOutBitBoard = new RookBitBoard( ChessPieceColors.Black );
      expectOutBitBoard.Bits = BoardSquare.A5 | BoardSquare.B5 | BoardSquare.D5 | BoardSquare.E5 | BoardSquare.F5 | BoardSquare.G5 | BoardSquare.H5 |
                               BoardSquare.C1 | BoardSquare.C2 | BoardSquare.C3 | BoardSquare.C4 | BoardSquare.C6;
      //actual result
      RookBitBoard actualBitBoard = RookMoveGen.ComputeBlackRook_Test( inputRookBitBoard, inputChessBoard, blackPieces, whitePieces, allPieces );
      Assert.Equal( expectOutBitBoard.Bits, actualBitBoard.Bits );
    }
    [Fact]
    public void RookBitBoardResultsTest_White() {
      //sample input
      ChessBoard inputChessBoard  = new ChessBoard();
      EmptyBitBoard blackPieces   = new EmptyBitBoard();
      EmptyBitBoard whitePieces   = new EmptyBitBoard();
      EmptyBitBoard allPieces     = new EmptyBitBoard();

      inputChessBoard.WhiteRook.Bits = BoardSquare.A1;
      inputChessBoard.WhitePawn.Bits = BoardSquare.A2;
      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;

      inputChessBoard.BlackKing.Bits = BoardSquare.D8;

      blackPieces.Bits = BoardSquare.D8;
      whitePieces.Bits = BoardSquare.A1 | BoardSquare.A2 | BoardSquare.D1;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      List<RookBitBoard> expectOutBitBoards = new List<RookBitBoard>();
      RookBitBoard expOutBB_1 = new RookBitBoard( ChessPieceColors.White );
      RookBitBoard expOutBB_2 = new RookBitBoard( ChessPieceColors.White );
      expOutBB_1.Bits = BoardSquare.B1;
      expOutBB_2.Bits = BoardSquare.C1;
      expectOutBitBoards.Add( expOutBB_1 );
      expectOutBitBoards.Add( expOutBB_2 );

      List<RookBitBoard> expectOutBitBoards_COPY = expectOutBitBoards.Select( p => p ).ToList();
      //actual result
      List<RookBitBoard> actualOutBitBoards = new List<RookBitBoard>();
      actualOutBitBoards = RookMoveGen.RookBitBoardResults( inputChessBoard, ChessPieceColors.White, blackPieces, whitePieces, allPieces );

      Assert.Equal( expectOutBitBoards.Count, actualOutBitBoards.Count );

      for ( int i = 0; i < expectOutBitBoards.Count; i++ ) {
        for ( int j = 0; j < actualOutBitBoards.Count; j++ ) {
          if ( expectOutBitBoards[i].Bits.Equals( actualOutBitBoards[j].Bits ) ) {
            expectOutBitBoards_COPY.RemoveAt( expectOutBitBoards_COPY.IndexOf( expectOutBitBoards[i] ) );
          }
        }
      }
      Assert.Equal( 0, expectOutBitBoards_COPY.Count );
    }
    [Fact]
    public void RookBitBoardResultsTest_Black() {
      //sample input
      ChessBoard inputChessBoard  = new ChessBoard();
      EmptyBitBoard blackPieces   = new EmptyBitBoard();
      EmptyBitBoard whitePieces   = new EmptyBitBoard();
      EmptyBitBoard allPieces     = new EmptyBitBoard();

      inputChessBoard.BlackRook.Bits = BoardSquare.A8;
      inputChessBoard.BlackPawn.Bits = BoardSquare.A7;
      inputChessBoard.BlackKing.Bits = BoardSquare.D8;

      inputChessBoard.WhiteKing.Bits = BoardSquare.D1;

      whitePieces.Bits = BoardSquare.D1;
      blackPieces.Bits = BoardSquare.A8 | BoardSquare.A7 | BoardSquare.D8;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      List<RookBitBoard> expectOutBitBoards = new List<RookBitBoard>();
      RookBitBoard expOutBB_1 = new RookBitBoard( ChessPieceColors.Black );
      RookBitBoard expOutBB_2 = new RookBitBoard( ChessPieceColors.Black );
      expOutBB_1.Bits = BoardSquare.B8;
      expOutBB_2.Bits = BoardSquare.C8;
      expectOutBitBoards.Add( expOutBB_1 );
      expectOutBitBoards.Add( expOutBB_2 );

      List<RookBitBoard> expectOutBitBoards_COPY = expectOutBitBoards.Select( p => p ).ToList();
      //actual result
      List<RookBitBoard> actualOutBitBoards = new List<RookBitBoard>();
      actualOutBitBoards = RookMoveGen.RookBitBoardResults( inputChessBoard, ChessPieceColors.Black, blackPieces, whitePieces, allPieces );

      Assert.Equal( expectOutBitBoards.Count, actualOutBitBoards.Count );

      for ( int i = 0; i < expectOutBitBoards.Count; i++ ) {
        for ( int j = 0; j < actualOutBitBoards.Count; j++ ) {
          if ( expectOutBitBoards[i].Bits.Equals( actualOutBitBoards[j].Bits ) ) {
            expectOutBitBoards_COPY.RemoveAt( expectOutBitBoards_COPY.IndexOf( expectOutBitBoards[i] ) );
          }
        }
      }
      Assert.Equal( 0, expectOutBitBoards_COPY.Count );
    }
    #endregion
    #region BishopMoveGen tests
    [Fact]
    public void ComputeWhiteBishopTest() {
      //sample input
      ChessBoard sampleInput = new ChessBoard();
      sampleInput.InitializeScenario( new ScenarioList{
        {BoardSquare.D4, ChessPieceType.Bishop, ChessPieceColors.White},
        {BoardSquare.C3, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.G7, ChessPieceType.Pawn,ChessPieceColors.Black},
        {BoardSquare.D1, ChessPieceType.King, ChessPieceColors.White},
        {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black}
      } );
      EmptyBitBoard blackPieces   = new EmptyBitBoard();
      EmptyBitBoard whitePieces   = new EmptyBitBoard();
      EmptyBitBoard allPieces     = new EmptyBitBoard();
      BishopBitBoard inputBishopBitBoard = new BishopBitBoard( ChessPieceColors.White );
      inputBishopBitBoard.Bits = sampleInput.WhiteBishop.Bits;
      blackPieces.Bits = sampleInput.BlackBishop.Bits | sampleInput.BlackKing.Bits | sampleInput.BlackKnight.Bits |
                        sampleInput.BlackPawn.Bits | sampleInput.BlackQueen.Bits | sampleInput.BlackRook.Bits;

      whitePieces.Bits = sampleInput.WhiteBishop.Bits | sampleInput.WhiteKing.Bits | sampleInput.WhiteKnight.Bits |
                        sampleInput.WhitePawn.Bits | sampleInput.WhiteQueen.Bits | sampleInput.WhiteRook.Bits;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      BishopBitBoard expectOutBitBoard = new BishopBitBoard( ChessPieceColors.White );
      expectOutBitBoard.Bits = BoardSquare.F6 | BoardSquare.G7 | BoardSquare.E3 | BoardSquare.F2 | BoardSquare.G1 | BoardSquare.C5 | BoardSquare.B6 | BoardSquare.A7 | BoardSquare.E5;
      //actual result
      BishopBitBoard actualBitBoard = BishopMoveGen.ComputeWhiteBishop_Test( inputBishopBitBoard, sampleInput, blackPieces, whitePieces, allPieces );
      Assert.Equal( expectOutBitBoard.Bits, actualBitBoard.Bits );
    }
    [Fact]
    public void ComputeBlackBishopTest() {
      //sample input
      ChessBoard sampleInput = new ChessBoard();
      sampleInput.InitializeScenario( new ScenarioList{
        {BoardSquare.D4, ChessPieceType.Bishop, ChessPieceColors.Black},
        {BoardSquare.C3, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.G7, ChessPieceType.Pawn,ChessPieceColors.White},
        {BoardSquare.D1, ChessPieceType.King, ChessPieceColors.Black},
        {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.White}
      } );
      EmptyBitBoard blackPieces   = new EmptyBitBoard();
      EmptyBitBoard whitePieces   = new EmptyBitBoard();
      EmptyBitBoard allPieces     = new EmptyBitBoard();
      BishopBitBoard inputBishopBitBoard = new BishopBitBoard( ChessPieceColors.Black );
      inputBishopBitBoard.Bits = sampleInput.BlackBishop.Bits;
      blackPieces.Bits = sampleInput.BlackBishop.Bits | sampleInput.BlackKing.Bits | sampleInput.BlackKnight.Bits |
                        sampleInput.BlackPawn.Bits | sampleInput.BlackQueen.Bits | sampleInput.BlackRook.Bits;

      whitePieces.Bits = sampleInput.WhiteBishop.Bits | sampleInput.WhiteKing.Bits | sampleInput.WhiteKnight.Bits |
                        sampleInput.WhitePawn.Bits | sampleInput.WhiteQueen.Bits | sampleInput.WhiteRook.Bits;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      BishopBitBoard expectOutBitBoard = new BishopBitBoard( ChessPieceColors.Black );
      expectOutBitBoard.Bits = BoardSquare.F6 | BoardSquare.G7 | BoardSquare.E3 | BoardSquare.F2 | BoardSquare.G1 | BoardSquare.C5 | BoardSquare.B6 | BoardSquare.A7 | BoardSquare.E5;
      //actual result
      BishopBitBoard actualBitBoard = BishopMoveGen.ComputeBlackBishop_Test( inputBishopBitBoard, sampleInput, blackPieces, whitePieces, allPieces );
      Assert.Equal( expectOutBitBoard.Bits, actualBitBoard.Bits );
    }
    [Fact]
    public void BishopBitBoardResultsTest_White() {
      //sample input
      ChessBoard sampleInput = new ChessBoard();
      sampleInput.InitializeScenario( new ScenarioList{
        {BoardSquare.D4, ChessPieceType.Bishop, ChessPieceColors.White},
        {BoardSquare.C3, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.G7, ChessPieceType.Pawn,ChessPieceColors.Black},
        {BoardSquare.D1, ChessPieceType.King, ChessPieceColors.White},
        {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black}
      } );
      EmptyBitBoard blackPieces   = new EmptyBitBoard();
      EmptyBitBoard whitePieces   = new EmptyBitBoard();
      EmptyBitBoard allPieces     = new EmptyBitBoard();
      blackPieces.Bits = sampleInput.BlackBishop.Bits | sampleInput.BlackKing.Bits | sampleInput.BlackKnight.Bits |
                        sampleInput.BlackPawn.Bits | sampleInput.BlackQueen.Bits | sampleInput.BlackRook.Bits;

      whitePieces.Bits = sampleInput.WhiteBishop.Bits | sampleInput.WhiteKing.Bits | sampleInput.WhiteKnight.Bits |
                        sampleInput.WhitePawn.Bits | sampleInput.WhiteQueen.Bits | sampleInput.WhiteRook.Bits;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      List<BishopBitBoard> expectedResult = new List<BishopBitBoard>();
      BishopBitBoard move = new BishopBitBoard( ChessPieceColors.White );
      #region GRIMT
      BishopBitBoard expOutBB_1 = new BishopBitBoard( ChessPieceColors.White );
      BishopBitBoard expOutBB_2 = new BishopBitBoard( ChessPieceColors.White );
      BishopBitBoard expOutBB_3 = new BishopBitBoard( ChessPieceColors.White );
      BishopBitBoard expOutBB_4 = new BishopBitBoard( ChessPieceColors.White );
      BishopBitBoard expOutBB_5 = new BishopBitBoard( ChessPieceColors.White );
      BishopBitBoard expOutBB_6 = new BishopBitBoard( ChessPieceColors.White );
      BishopBitBoard expOutBB_7 = new BishopBitBoard( ChessPieceColors.White );
      BishopBitBoard expOutBB_8 = new BishopBitBoard( ChessPieceColors.White );
      BishopBitBoard expOutBB_9 = new BishopBitBoard( ChessPieceColors.White );
      expOutBB_1.Bits = BoardSquare.G7;
      expOutBB_2.Bits = BoardSquare.E3;
      expOutBB_3.Bits = BoardSquare.F2;
      expOutBB_4.Bits = BoardSquare.G1;
      expOutBB_5.Bits = BoardSquare.C5;
      expOutBB_6.Bits = BoardSquare.B6;
      expOutBB_7.Bits = BoardSquare.A7;
      expOutBB_8.Bits = BoardSquare.F6;
      expOutBB_9.Bits = BoardSquare.E5;

      expectedResult.Add( expOutBB_1 );
      expectedResult.Add( expOutBB_2 );
      expectedResult.Add( expOutBB_3 );
      expectedResult.Add( expOutBB_4 );
      expectedResult.Add( expOutBB_5 );
      expectedResult.Add( expOutBB_6 );
      expectedResult.Add( expOutBB_7 );
      expectedResult.Add( expOutBB_8 );
      expectedResult.Add( expOutBB_9 );
      #endregion
      List<BishopBitBoard> expectOutBitBoards_COPY = expectedResult.Select( p => p ).ToList();
      //actual result
      List<BishopBitBoard> actualOutBitBoards = new List<BishopBitBoard>();
      actualOutBitBoards = BishopMoveGen.BishopBitBoardResults( sampleInput, ChessPieceColors.White, blackPieces, whitePieces, allPieces );

      Assert.Equal( expectedResult.Count, actualOutBitBoards.Count );

      for ( int i = 0; i < expectedResult.Count; i++ ) {
        for ( int j = 0; j < actualOutBitBoards.Count; j++ ) {
          if ( expectedResult[i].Bits.Equals( actualOutBitBoards[j].Bits ) ) {
            expectOutBitBoards_COPY.RemoveAt( expectOutBitBoards_COPY.IndexOf( expectedResult[i] ) );
          }
        }
      }
      Assert.Equal( 0, expectOutBitBoards_COPY.Count );
    }
    [Fact]
    public void BishopBitBoardResultsTest_Black() {
      //sample input
      ChessBoard sampleInput = new ChessBoard();
      sampleInput.InitializeScenario( new ScenarioList{
        {BoardSquare.D4, ChessPieceType.Bishop, ChessPieceColors.Black},
        {BoardSquare.C3, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.G7, ChessPieceType.Pawn,ChessPieceColors.White},
        {BoardSquare.D1, ChessPieceType.King, ChessPieceColors.Black},
        {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.White}
      } );
      EmptyBitBoard blackPieces   = new EmptyBitBoard();
      EmptyBitBoard whitePieces   = new EmptyBitBoard();
      EmptyBitBoard allPieces     = new EmptyBitBoard();
      blackPieces.Bits = sampleInput.BlackBishop.Bits | sampleInput.BlackKing.Bits | sampleInput.BlackKnight.Bits |
                        sampleInput.BlackPawn.Bits | sampleInput.BlackQueen.Bits | sampleInput.BlackRook.Bits;

      whitePieces.Bits = sampleInput.WhiteBishop.Bits | sampleInput.WhiteKing.Bits | sampleInput.WhiteKnight.Bits |
                        sampleInput.WhitePawn.Bits | sampleInput.WhiteQueen.Bits | sampleInput.WhiteRook.Bits;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      List<BishopBitBoard> expectedResult = new List<BishopBitBoard>();
      BishopBitBoard move = new BishopBitBoard( ChessPieceColors.Black );
      #region GRIMT
      BishopBitBoard expOutBB_1 = new BishopBitBoard( ChessPieceColors.Black );
      BishopBitBoard expOutBB_2 = new BishopBitBoard( ChessPieceColors.Black );
      BishopBitBoard expOutBB_3 = new BishopBitBoard( ChessPieceColors.Black );
      BishopBitBoard expOutBB_4 = new BishopBitBoard( ChessPieceColors.Black );
      BishopBitBoard expOutBB_5 = new BishopBitBoard( ChessPieceColors.Black );
      BishopBitBoard expOutBB_6 = new BishopBitBoard( ChessPieceColors.Black );
      BishopBitBoard expOutBB_7 = new BishopBitBoard( ChessPieceColors.Black );
      BishopBitBoard expOutBB_8 = new BishopBitBoard( ChessPieceColors.Black );
      BishopBitBoard expOutBB_9 = new BishopBitBoard( ChessPieceColors.Black );
      expOutBB_1.Bits = BoardSquare.G7;
      expOutBB_2.Bits = BoardSquare.E3;
      expOutBB_3.Bits = BoardSquare.F2;
      expOutBB_4.Bits = BoardSquare.G1;
      expOutBB_5.Bits = BoardSquare.C5;
      expOutBB_6.Bits = BoardSquare.B6;
      expOutBB_7.Bits = BoardSquare.A7;
      expOutBB_8.Bits = BoardSquare.F6;
      expOutBB_9.Bits = BoardSquare.E5;

      expectedResult.Add( expOutBB_1 );
      expectedResult.Add( expOutBB_2 );
      expectedResult.Add( expOutBB_3 );
      expectedResult.Add( expOutBB_4 );
      expectedResult.Add( expOutBB_5 );
      expectedResult.Add( expOutBB_6 );
      expectedResult.Add( expOutBB_7 );
      expectedResult.Add( expOutBB_8 );
      expectedResult.Add( expOutBB_9 );
      #endregion
      List<BishopBitBoard> expectOutBitBoards_COPY = expectedResult.Select( p => p ).ToList();
      //actual result
      List<BishopBitBoard> actualOutBitBoards = new List<BishopBitBoard>();
      actualOutBitBoards = BishopMoveGen.BishopBitBoardResults( sampleInput, ChessPieceColors.Black, blackPieces, whitePieces, allPieces );

      Assert.Equal( expectedResult.Count, actualOutBitBoards.Count );

      for ( int i = 0; i < expectedResult.Count; i++ ) {
        for ( int j = 0; j < actualOutBitBoards.Count; j++ ) {
          if ( expectedResult[i].Bits.Equals( actualOutBitBoards[j].Bits ) ) {
            expectOutBitBoards_COPY.RemoveAt( expectOutBitBoards_COPY.IndexOf( expectedResult[i] ) );
          }
        }
      }
      Assert.Equal( 0, expectOutBitBoards_COPY.Count );
    }
    #endregion
    #region QueenMoveGen tests
    [Fact]
    public void ComputeWhiteQueenTest() {
      //sample input
      ChessBoard sampleInput = new ChessBoard();
      sampleInput.InitializeScenario( new ScenarioList{
        {BoardSquare.D4, ChessPieceType.Queen, ChessPieceColors.White},
        {BoardSquare.C3, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.C4, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.C5, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.D5, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.D2, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.E3, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.E4, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.H1, ChessPieceType.King, ChessPieceColors.White},
        {BoardSquare.A8, ChessPieceType.King, ChessPieceColors.Black}
      } );
      EmptyBitBoard blackPieces   = new EmptyBitBoard();
      EmptyBitBoard whitePieces   = new EmptyBitBoard();
      EmptyBitBoard allPieces     = new EmptyBitBoard();
      QueenBitBoard inputQueenBoard = new QueenBitBoard( ChessPieceColors.White );
      inputQueenBoard.Bits = sampleInput.WhiteQueen.Bits;
      blackPieces.Bits = sampleInput.BlackBishop.Bits | sampleInput.BlackKing.Bits | sampleInput.BlackKnight.Bits |
                        sampleInput.BlackPawn.Bits | sampleInput.BlackQueen.Bits | sampleInput.BlackRook.Bits;

      whitePieces.Bits = sampleInput.WhiteBishop.Bits | sampleInput.WhiteKing.Bits | sampleInput.WhiteKnight.Bits |
                        sampleInput.WhitePawn.Bits | sampleInput.WhiteQueen.Bits | sampleInput.WhiteRook.Bits;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      QueenBitBoard expectOutBitBoard = new QueenBitBoard( ChessPieceColors.White );
      expectOutBitBoard.Bits = BoardSquare.D3 | BoardSquare.C3 | BoardSquare.C4 | BoardSquare.C5 | BoardSquare.D5 | BoardSquare.E5 | BoardSquare.F6
                              | BoardSquare.G7 | BoardSquare.H8;
      //actual result
      QueenBitBoard actualBitBoard = QueenMoveGen.ComputeWhiteQueen_Test( inputQueenBoard, whitePieces, allPieces );
      Assert.Equal( expectOutBitBoard.Bits, actualBitBoard.Bits );
    }
    [Fact]
    public void ComputeBlackQueenTest() {
      //sample input
      ChessBoard sampleInput = new ChessBoard();
      sampleInput.InitializeScenario( new ScenarioList{
        {BoardSquare.D4, ChessPieceType.Queen, ChessPieceColors.Black},
        {BoardSquare.C3, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.C4, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.C5, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.D5, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.D2, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.E3, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.E4, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.H1, ChessPieceType.King, ChessPieceColors.White},
        {BoardSquare.A8, ChessPieceType.King, ChessPieceColors.Black}
      } );
      EmptyBitBoard blackPieces   = new EmptyBitBoard();
      EmptyBitBoard whitePieces   = new EmptyBitBoard();
      EmptyBitBoard allPieces     = new EmptyBitBoard();
      QueenBitBoard inputQueenBoard = new QueenBitBoard( ChessPieceColors.Black );
      inputQueenBoard.Bits = sampleInput.BlackQueen.Bits;
      blackPieces.Bits = sampleInput.BlackBishop.Bits | sampleInput.BlackKing.Bits | sampleInput.BlackKnight.Bits |
                        sampleInput.BlackPawn.Bits | sampleInput.BlackQueen.Bits | sampleInput.BlackRook.Bits;

      whitePieces.Bits = sampleInput.WhiteBishop.Bits | sampleInput.WhiteKing.Bits | sampleInput.WhiteKnight.Bits |
                        sampleInput.WhitePawn.Bits | sampleInput.WhiteQueen.Bits | sampleInput.WhiteRook.Bits;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      QueenBitBoard expectOutBitBoard = new QueenBitBoard( ChessPieceColors.Black );
      expectOutBitBoard.Bits = BoardSquare.D3 | BoardSquare.C3 | BoardSquare.C4 | BoardSquare.C5 | BoardSquare.D5 | BoardSquare.E5 | BoardSquare.F6
                              | BoardSquare.G7 | BoardSquare.H8;
      //actual result
      QueenBitBoard actualBitBoard = QueenMoveGen.ComputeBlackQueen_Test( inputQueenBoard, blackPieces, allPieces );
      Assert.Equal( expectOutBitBoard.Bits, actualBitBoard.Bits );
    }
    [Fact]
    public void QueenBitBoardResultsTest_White() {
      //sample input
      ChessBoard sampleInput = new ChessBoard();
      sampleInput.InitializeScenario( new ScenarioList{
        {BoardSquare.D4, ChessPieceType.Queen, ChessPieceColors.White},
        {BoardSquare.C3, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.C4, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.C5, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.D5, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.D2, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.E3, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.E4, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.H1, ChessPieceType.King, ChessPieceColors.White},
        {BoardSquare.A8, ChessPieceType.King, ChessPieceColors.Black}
      } );
      EmptyBitBoard blackPieces   = new EmptyBitBoard();
      EmptyBitBoard whitePieces   = new EmptyBitBoard();
      EmptyBitBoard allPieces     = new EmptyBitBoard();
      QueenBitBoard inputQueenBoard = new QueenBitBoard( ChessPieceColors.White );
      inputQueenBoard.Bits = sampleInput.WhiteBishop.Bits;
      blackPieces.Bits = sampleInput.BlackBishop.Bits | sampleInput.BlackKing.Bits | sampleInput.BlackKnight.Bits |
                        sampleInput.BlackPawn.Bits | sampleInput.BlackQueen.Bits | sampleInput.BlackRook.Bits;

      whitePieces.Bits = sampleInput.WhiteBishop.Bits | sampleInput.WhiteKing.Bits | sampleInput.WhiteKnight.Bits |
                        sampleInput.WhitePawn.Bits | sampleInput.WhiteQueen.Bits | sampleInput.WhiteRook.Bits;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      List<QueenBitBoard> expectedResult = new List<QueenBitBoard>();
      QueenBitBoard move = new QueenBitBoard( ChessPieceColors.White );
      #region GRIMT
      QueenBitBoard expOutBB_1 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard expOutBB_2 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard expOutBB_3 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard expOutBB_4 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard expOutBB_5 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard expOutBB_6 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard expOutBB_7 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard expOutBB_8 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard expOutBB_9 = new QueenBitBoard( ChessPieceColors.White );
      QueenBitBoard expOutBB_10 = new QueenBitBoard( ChessPieceColors.White );

      expOutBB_1.Bits = BoardSquare.D3;
      expOutBB_3.Bits = BoardSquare.C3;
      expOutBB_4.Bits = BoardSquare.C4;
      expOutBB_5.Bits = BoardSquare.C5;
      expOutBB_6.Bits = BoardSquare.D5;
      expOutBB_7.Bits = BoardSquare.E5;
      expOutBB_8.Bits = BoardSquare.F6;
      expOutBB_9.Bits = BoardSquare.G7;
      expOutBB_10.Bits = BoardSquare.H8;
      expectedResult.Add( expOutBB_1 );
      expectedResult.Add( expOutBB_3 );
      expectedResult.Add( expOutBB_4 );
      expectedResult.Add( expOutBB_5 );
      expectedResult.Add( expOutBB_6 );
      expectedResult.Add( expOutBB_7 );
      expectedResult.Add( expOutBB_8 );
      expectedResult.Add( expOutBB_9 );
      expectedResult.Add( expOutBB_10 );
      #endregion
      List<QueenBitBoard> expectOutBitBoards_COPY = expectedResult.Select( p => p ).ToList();
      //actual result
      List<QueenBitBoard> actualOutBitBoards = new List<QueenBitBoard>();
      actualOutBitBoards = QueenMoveGen.QueenBitBoardResults( sampleInput, ChessPieceColors.White, blackPieces, whitePieces, allPieces );

      Assert.Equal( expectedResult.Count, actualOutBitBoards.Count );

      for ( int i = 0; i < expectedResult.Count; i++ ) {
        for ( int j = 0; j < actualOutBitBoards.Count; j++ ) {
          if ( expectedResult[i].Bits.Equals( actualOutBitBoards[j].Bits ) ) {
            expectOutBitBoards_COPY.RemoveAt( expectOutBitBoards_COPY.IndexOf( expectedResult[i] ) );
          }
        }
      }
      Assert.Equal( 0, expectOutBitBoards_COPY.Count );
    }
    [Fact]
    public void QueenBitBoardResultsTest_Black() {
      //sample input
      ChessBoard sampleInput = new ChessBoard();
      sampleInput.InitializeScenario( new ScenarioList{
        {BoardSquare.D4, ChessPieceType.Queen, ChessPieceColors.Black},
        {BoardSquare.C3, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.C4, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.C5, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.D5, ChessPieceType.Pawn, ChessPieceColors.White},
        {BoardSquare.D2, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.E3, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.E4, ChessPieceType.Pawn, ChessPieceColors.Black},
        {BoardSquare.H1, ChessPieceType.King, ChessPieceColors.White},
        {BoardSquare.A8, ChessPieceType.King, ChessPieceColors.Black}
      } );
      EmptyBitBoard blackPieces   = new EmptyBitBoard();
      EmptyBitBoard whitePieces   = new EmptyBitBoard();
      EmptyBitBoard allPieces     = new EmptyBitBoard();
      QueenBitBoard inputQueenBoard = new QueenBitBoard( ChessPieceColors.Black );
      inputQueenBoard.Bits = sampleInput.WhiteBishop.Bits;
      blackPieces.Bits = sampleInput.BlackBishop.Bits | sampleInput.BlackKing.Bits | sampleInput.BlackKnight.Bits |
                        sampleInput.BlackPawn.Bits | sampleInput.BlackQueen.Bits | sampleInput.BlackRook.Bits;

      whitePieces.Bits = sampleInput.WhiteBishop.Bits | sampleInput.WhiteKing.Bits | sampleInput.WhiteKnight.Bits |
                        sampleInput.WhitePawn.Bits | sampleInput.WhiteQueen.Bits | sampleInput.WhiteRook.Bits;
      allPieces.Bits = blackPieces.Bits | whitePieces.Bits;
      //expected result
      List<QueenBitBoard> expectedResult = new List<QueenBitBoard>();
      QueenBitBoard move = new QueenBitBoard( ChessPieceColors.Black );
      #region GRIMT
      QueenBitBoard expOutBB_1 = new QueenBitBoard( ChessPieceColors.Black );
      QueenBitBoard expOutBB_2 = new QueenBitBoard( ChessPieceColors.Black );
      QueenBitBoard expOutBB_3 = new QueenBitBoard( ChessPieceColors.Black );
      QueenBitBoard expOutBB_4 = new QueenBitBoard( ChessPieceColors.Black );
      QueenBitBoard expOutBB_5 = new QueenBitBoard( ChessPieceColors.Black );
      QueenBitBoard expOutBB_6 = new QueenBitBoard( ChessPieceColors.Black );
      QueenBitBoard expOutBB_7 = new QueenBitBoard( ChessPieceColors.Black );
      QueenBitBoard expOutBB_8 = new QueenBitBoard( ChessPieceColors.Black );
      QueenBitBoard expOutBB_9 = new QueenBitBoard( ChessPieceColors.Black );
      QueenBitBoard expOutBB_10 = new QueenBitBoard( ChessPieceColors.Black );

      expOutBB_1.Bits = BoardSquare.D3;
      expOutBB_3.Bits = BoardSquare.C3;
      expOutBB_4.Bits = BoardSquare.C4;
      expOutBB_5.Bits = BoardSquare.C5;
      expOutBB_6.Bits = BoardSquare.D5;
      expOutBB_7.Bits = BoardSquare.E5;
      expOutBB_8.Bits = BoardSquare.F6;
      expOutBB_9.Bits = BoardSquare.G7;
      expOutBB_10.Bits = BoardSquare.H8;
      expectedResult.Add( expOutBB_1 );
      expectedResult.Add( expOutBB_3 );
      expectedResult.Add( expOutBB_4 );
      expectedResult.Add( expOutBB_5 );
      expectedResult.Add( expOutBB_6 );
      expectedResult.Add( expOutBB_7 );
      expectedResult.Add( expOutBB_8 );
      expectedResult.Add( expOutBB_9 );
      expectedResult.Add( expOutBB_10 );
      #endregion
      List<QueenBitBoard> expectOutBitBoards_COPY = expectedResult.Select( p => p ).ToList();
      //actual result
      List<QueenBitBoard> actualOutBitBoards = new List<QueenBitBoard>();
      actualOutBitBoards = QueenMoveGen.QueenBitBoardResults( sampleInput, ChessPieceColors.Black, blackPieces, whitePieces, allPieces );

      Assert.Equal( expectedResult.Count, actualOutBitBoards.Count );

      for ( int i = 0; i < expectedResult.Count; i++ ) {
        for ( int j = 0; j < actualOutBitBoards.Count; j++ ) {
          if ( expectedResult[i].Bits.Equals( actualOutBitBoards[j].Bits ) ) {
            expectOutBitBoards_COPY.RemoveAt( expectOutBitBoards_COPY.IndexOf( expectedResult[i] ) );
          }
        }
      }
      Assert.Equal( 0, expectOutBitBoards_COPY.Count );
    }
    #endregion
  }
}

using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public class Test {

    public static List<ColoredBitBoard> GenerateStaticMoves_White_Depth1( ChessBoard board, ChessPieceColors color ) {
      List<ColoredBitBoard> legalMoves = new List<ColoredBitBoard>();
      //White king moves
      KingBitBoard move1 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard move2 = new KingBitBoard( ChessPieceColors.White );
      KingBitBoard move3 = new KingBitBoard( ChessPieceColors.White );
      move1.Bits = BoardSquare.H2;
      move2.Bits = BoardSquare.G1;
      move3.Bits = BoardSquare.G2;



      //White pawn moves
      PawnBitBoard pawnEnd1 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard pawnEnd2 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard pawnEnd3 = new PawnBitBoard( ChessPieceColors.White );
      pawnEnd1.Bits = BoardSquare.C3;
      pawnEnd2.Bits = BoardSquare.D3;
      pawnEnd3.Bits = BoardSquare.B3;

      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.White );

      move4.Bits = ( board.WhitePawn.Bits ^ BoardSquare.C2 ) | pawnEnd1.Bits;
      move5.Bits = ( board.WhitePawn.Bits ^ BoardSquare.C2 ) | pawnEnd2.Bits;
      move6.Bits = ( board.WhitePawn.Bits ^ BoardSquare.C2 ) | pawnEnd3.Bits;

      legalMoves.Add( move1 );
      legalMoves.Add( move2 );
      legalMoves.Add( move3 );
      legalMoves.Add( move4 );
      legalMoves.Add( move5 );
      legalMoves.Add( move6 );

      return legalMoves;
    }

    public static List<ColoredBitBoard> GenerateStaticMoves_Black_Depth1( ChessBoard board, ChessPieceColors color ) {
      List<ColoredBitBoard> legalMoves = new List<ColoredBitBoard>();
      //Black king moves
      KingBitBoard move1 = new KingBitBoard( ChessPieceColors.Black );
      KingBitBoard move2 = new KingBitBoard( ChessPieceColors.Black );
      KingBitBoard move3 = new KingBitBoard( ChessPieceColors.Black );
      KingBitBoard move4 = new KingBitBoard( ChessPieceColors.Black );
      KingBitBoard move5 = new KingBitBoard( ChessPieceColors.Black );
      KingBitBoard move6 = new KingBitBoard( ChessPieceColors.Black );
      KingBitBoard move7 = new KingBitBoard( ChessPieceColors.Black );
      move1.Bits = BoardSquare.A3;
      move2.Bits = BoardSquare.C3;
      move3.Bits = BoardSquare.A4;
      move4.Bits = BoardSquare.C4;
      move5.Bits = BoardSquare.A5;
      move6.Bits = BoardSquare.B5;
      move7.Bits = BoardSquare.C5;

      //Black Pawn moves, pawn at B3
      PawnBitBoard pawnEnd11 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard pawnEnd12 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard move8 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard move9 = new PawnBitBoard( ChessPieceColors.Black );

      pawnEnd11.Bits = BoardSquare.B2;
      pawnEnd12.Bits = BoardSquare.C2;

      move8.Bits = ( board.BlackPawn.Bits ^ BoardSquare.B3 ) | pawnEnd11.Bits;
      move9.Bits = ( board.BlackPawn.Bits ^ BoardSquare.B3 ) | pawnEnd12.Bits;

      //Pawn at D3
      PawnBitBoard pawnEnd21 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard pawnEnd22 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard move10 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard move11 = new PawnBitBoard( ChessPieceColors.Black );

      pawnEnd21.Bits = BoardSquare.C2;
      pawnEnd22.Bits = BoardSquare.D2;

      move10.Bits = ( board.BlackPawn.Bits ^ BoardSquare.D3 ) | pawnEnd21.Bits;
      move11.Bits = ( board.BlackPawn.Bits ^ BoardSquare.D3 ) | pawnEnd22.Bits;

      legalMoves.Add( move1 );
      legalMoves.Add( move2 );
      legalMoves.Add( move3 );
      legalMoves.Add( move4 );
      legalMoves.Add( move5 );
      legalMoves.Add( move6 );
      legalMoves.Add( move7 );
      legalMoves.Add( move8 );
      legalMoves.Add( move9 );
      legalMoves.Add( move10 );
      legalMoves.Add( move11 );
      return legalMoves;
    }
    public static List<ColoredBitBoard> GenerateStaticMoves_Depth12( ChessBoard board, ChessPieceColors color ) {
      List<ColoredBitBoard> legalMoves = new List<ColoredBitBoard>();
      if ( color == ChessPieceColors.Black ) {
        if ( board.WhitePawn.Bits == BoardSquare.B3 ) {
          PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.Black );
          move1.Bits = ( board.BlackPawn.Bits ^ BoardSquare.D3 ) | BoardSquare.D2;

          KingBitBoard move2 = new KingBitBoard( ChessPieceColors.Black );
          move2.Bits = BoardSquare.B3;
          legalMoves.Add( move1 );
          legalMoves.Add( move2 );
        } else if ( board.WhitePawn.Bits == BoardSquare.D3 ) {
          PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.Black );
          move3.Bits = ( board.BlackPawn.Bits ^ BoardSquare.B3 ) | BoardSquare.B2;

          KingBitBoard move4 = new KingBitBoard( ChessPieceColors.Black );
          move4.Bits = BoardSquare.A3;
          legalMoves.Add( move3 );
          legalMoves.Add( move4 );


        } else if ( board.WhitePawn.Bits == BoardSquare.C3 ) {
          KingBitBoard move8 = new KingBitBoard( ChessPieceColors.Black );
          move8.Bits = BoardSquare.C3;
          KingBitBoard move9 = new KingBitBoard( ChessPieceColors.Black );
          move9.Bits = BoardSquare.C4;

          legalMoves.Add( move8 );
          legalMoves.Add( move9 );
        }
      } else if ( color == ChessPieceColors.White ) {
        PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
        move5.Bits = ( board.WhitePawn.Bits ^ BoardSquare.C2 ) | BoardSquare.B3;
        PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.White );
        move6.Bits = ( board.WhitePawn.Bits ^ BoardSquare.C2 ) | BoardSquare.D3;
        PawnBitBoard move7 = new PawnBitBoard( ChessPieceColors.White );
        move7.Bits = ( board.WhitePawn.Bits ^ BoardSquare.C2 ) | BoardSquare.C3;
        legalMoves.Add( move5 );
        legalMoves.Add( move6 );
        legalMoves.Add( move7 );
      }
      return legalMoves;
    }

    public static List<ColoredBitBoard> GenerateCheckMateMoves( ChessBoard board, ChessPieceColors color ) {
      return new List<ColoredBitBoard>();
    }

    public static int GetStaticMaterialValueDepth1_White( ChessBoard board ) {
      return 1;
    }
    public static int GetStaticMaterialValueDepth1_Black( ChessBoard board ) {
      return -1;
    }
    public static double GetStaticMaterialValueDepth2_White( ChessBoard board ) {
      if ( board.WhitePawn.Bits == BoardSquare.Empty ) {
        return -1;
      } else if ( board.WhitePawn.Bits == BoardSquare.D3 ) {
        return 0;
      } else if ( board.WhitePawn.Bits == BoardSquare.B3 ) {
        return -1;
      } else if ( board.WhitePawn.Bits == BoardSquare.C3 ) {
        return -2;
      } else {
        throw new Exception( "Should never be this value" );
      }
    }

    #region Evaluation
    [Fact]
    public void EvaluateState_WithoutFactors_White_Test_LateGame() {
      ChessBoard fooBoard = new ChessBoard();
      fooBoard.InitializeScenario( new ScenarioList {
                 //White
                 {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },
                 {BoardSquare.B1, ChessPieceType.Bishop, ChessPieceColors.White},
                 {BoardSquare.C1, ChessPieceType.Queen, ChessPieceColors.White},
                 {BoardSquare.D1, ChessPieceType.Rook, ChessPieceColors.White},
                 {BoardSquare.E1, ChessPieceType.Knight, ChessPieceColors.White},
                 {BoardSquare.A2, ChessPieceType.Pawn, ChessPieceColors.White},
                 {BoardSquare.B2, ChessPieceType.Pawn, ChessPieceColors.White},

                 //Black
                 {BoardSquare.A8, ChessPieceType.Rook, ChessPieceColors.Black },
                 {BoardSquare.B8, ChessPieceType.Knight, ChessPieceColors.Black },
                 {BoardSquare.C8, ChessPieceType.Queen, ChessPieceColors.Black },
                 {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },
                 {BoardSquare.E8, ChessPieceType.Bishop, ChessPieceColors.Black },
                 {BoardSquare.A7, ChessPieceType.Pawn, ChessPieceColors.Black }

              } );
      Assert.Equal( 100, Eval.GetMaterialValue_DEBUG( fooBoard ) );

    }

    [Fact]
    public void EvaluateState_WithRookFactor_White_Test() {
      ChessBoard fooBoard = new ChessBoard();
      fooBoard.InitializeScenario( new ScenarioList {
                 //White
                 {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },
                 {BoardSquare.B1, ChessPieceType.Bishop, ChessPieceColors.White},
                 {BoardSquare.C1, ChessPieceType.Queen, ChessPieceColors.White},
                 {BoardSquare.D1, ChessPieceType.Rook, ChessPieceColors.White},
                 {BoardSquare.E1, ChessPieceType.Knight, ChessPieceColors.White},
                 {BoardSquare.A2, ChessPieceType.Pawn, ChessPieceColors.White},
                 {BoardSquare.B2, ChessPieceType.Pawn, ChessPieceColors.White},
                 {BoardSquare.F1, ChessPieceType.Rook, ChessPieceColors.White},

                 //Black
                 {BoardSquare.A8, ChessPieceType.Rook, ChessPieceColors.Black },
                 {BoardSquare.B8, ChessPieceType.Knight, ChessPieceColors.Black },
                 {BoardSquare.C8, ChessPieceType.Queen, ChessPieceColors.Black },
                 {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },
                 {BoardSquare.E8, ChessPieceType.Bishop, ChessPieceColors.Black },
                 {BoardSquare.A7, ChessPieceType.Pawn, ChessPieceColors.Black }

              } );
      Assert.Equal( 992.5, Eval.GetMaterialValue_DEBUG( fooBoard ) );

    }

    [Fact]
    public void EvaluateState_WithBishopFactor_White_Test() {
      ChessBoard fooBoard = new ChessBoard();
      fooBoard.InitializeScenario( new ScenarioList {
                 //White
                 {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },
                 {BoardSquare.B1, ChessPieceType.Bishop, ChessPieceColors.White},
                 {BoardSquare.C1, ChessPieceType.Queen, ChessPieceColors.White},
                 {BoardSquare.D1, ChessPieceType.Rook, ChessPieceColors.White},
                 {BoardSquare.E1, ChessPieceType.Knight, ChessPieceColors.White},
                 {BoardSquare.A2, ChessPieceType.Pawn, ChessPieceColors.White},
                 {BoardSquare.B2, ChessPieceType.Pawn, ChessPieceColors.White},
                 {BoardSquare.F1, ChessPieceType.Bishop, ChessPieceColors.White},

                 //Black
                 {BoardSquare.A8, ChessPieceType.Rook, ChessPieceColors.Black },
                 {BoardSquare.B8, ChessPieceType.Knight, ChessPieceColors.Black },
                 {BoardSquare.C8, ChessPieceType.Queen, ChessPieceColors.Black },
                 {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },
                 {BoardSquare.E8, ChessPieceType.Bishop, ChessPieceColors.Black },
                 {BoardSquare.A7, ChessPieceType.Pawn, ChessPieceColors.Black }

              } );
      Assert.Equal( 681, Eval.GetMaterialValue_DEBUG( fooBoard ) );

    }

    [Fact]
    public void EvaluateState_WithKnightFactor_White_Test() {
      ChessBoard fooBoard = new ChessBoard();
      fooBoard.InitializeScenario( new ScenarioList {
                 //White
                 {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },
                 {BoardSquare.B1, ChessPieceType.Bishop, ChessPieceColors.White},
                 {BoardSquare.C1, ChessPieceType.Queen, ChessPieceColors.White},
                 {BoardSquare.D1, ChessPieceType.Rook, ChessPieceColors.White},
                 {BoardSquare.E1, ChessPieceType.Knight, ChessPieceColors.White},
                 {BoardSquare.A2, ChessPieceType.Pawn, ChessPieceColors.White},
                 {BoardSquare.B2, ChessPieceType.Pawn, ChessPieceColors.White},
                 {BoardSquare.F1, ChessPieceType.Knight, ChessPieceColors.White},

                 //Black
                 {BoardSquare.A8, ChessPieceType.Rook, ChessPieceColors.Black },
                 {BoardSquare.B8, ChessPieceType.Knight, ChessPieceColors.Black },
                 {BoardSquare.C8, ChessPieceType.Queen, ChessPieceColors.Black },
                 {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },
                 {BoardSquare.E8, ChessPieceType.Bishop, ChessPieceColors.Black },
                 {BoardSquare.A7, ChessPieceType.Pawn, ChessPieceColors.Black }

              } );
      Assert.Equal( 660, Eval.GetMaterialValue_DEBUG( fooBoard ) );

    }


    [Fact]
    public void EvaluateState_WithoutFactors_Black_Test() {
      ChessBoard fooBoard = new ChessBoard();
      fooBoard.InitializeScenario( new ScenarioList {
                 //White
                 {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },
                 {BoardSquare.B1, ChessPieceType.Bishop, ChessPieceColors.White},
                 {BoardSquare.C1, ChessPieceType.Queen, ChessPieceColors.White},
                 {BoardSquare.D1, ChessPieceType.Rook, ChessPieceColors.White},
                 {BoardSquare.E1, ChessPieceType.Knight, ChessPieceColors.White},
                 {BoardSquare.A2, ChessPieceType.Pawn, ChessPieceColors.White},
                 {BoardSquare.B2, ChessPieceType.Pawn, ChessPieceColors.Black},

                 //Black
                 {BoardSquare.A8, ChessPieceType.Rook, ChessPieceColors.Black },
                 {BoardSquare.B8, ChessPieceType.Knight, ChessPieceColors.Black },
                 {BoardSquare.C8, ChessPieceType.Queen, ChessPieceColors.Black },
                 {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },
                 {BoardSquare.E8, ChessPieceType.Bishop, ChessPieceColors.Black },
                 {BoardSquare.A7, ChessPieceType.Pawn, ChessPieceColors.Black }

              } );
      Assert.Equal( -100, Eval.GetMaterialValue_DEBUG( fooBoard ) );

    }

    [Fact]
    public void EvaluateState_WithRookFactor_Black_Test() {
      ChessBoard fooBoard = new ChessBoard();
      fooBoard.InitializeScenario( new ScenarioList {
                 //White
                 {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },
                 {BoardSquare.B1, ChessPieceType.Bishop, ChessPieceColors.White},
                 {BoardSquare.C1, ChessPieceType.Queen, ChessPieceColors.White},
                 {BoardSquare.D1, ChessPieceType.Rook, ChessPieceColors.White},
                 {BoardSquare.E1, ChessPieceType.Knight, ChessPieceColors.White},
                 {BoardSquare.A2, ChessPieceType.Pawn, ChessPieceColors.White},
                 {BoardSquare.B2, ChessPieceType.Pawn, ChessPieceColors.Black},
                 {BoardSquare.F1, ChessPieceType.Rook, ChessPieceColors.Black},

                 //Black
                 {BoardSquare.A8, ChessPieceType.Rook, ChessPieceColors.Black },
                 {BoardSquare.B8, ChessPieceType.Knight, ChessPieceColors.Black },
                 {BoardSquare.C8, ChessPieceType.Queen, ChessPieceColors.Black },
                 {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },
                 {BoardSquare.E8, ChessPieceType.Bishop, ChessPieceColors.Black },
                 {BoardSquare.A7, ChessPieceType.Pawn, ChessPieceColors.Black }

              } );
      Assert.Equal( -992.5, Eval.GetMaterialValue_DEBUG( fooBoard ) );

    }

    [Fact]
    public void EvaluateState_WithBishopFactor_Black_Test() {
      ChessBoard fooBoard = new ChessBoard();
      fooBoard.InitializeScenario( new ScenarioList {
                 //White
                 {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },
                 {BoardSquare.B1, ChessPieceType.Bishop, ChessPieceColors.White},
                 {BoardSquare.C1, ChessPieceType.Queen, ChessPieceColors.White},
                 {BoardSquare.D1, ChessPieceType.Rook, ChessPieceColors.White},
                 {BoardSquare.E1, ChessPieceType.Knight, ChessPieceColors.White},
                 {BoardSquare.A2, ChessPieceType.Pawn, ChessPieceColors.White},
                 {BoardSquare.B2, ChessPieceType.Pawn, ChessPieceColors.Black},
                 {BoardSquare.F1, ChessPieceType.Bishop, ChessPieceColors.Black},// 12950

                 //Black
                 {BoardSquare.A8, ChessPieceType.Rook, ChessPieceColors.Black },
                 {BoardSquare.B8, ChessPieceType.Knight, ChessPieceColors.Black },
                 {BoardSquare.C8, ChessPieceType.Queen, ChessPieceColors.Black },
                 {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },
                 {BoardSquare.E8, ChessPieceType.Bishop, ChessPieceColors.Black },
                 {BoardSquare.A7, ChessPieceType.Pawn, ChessPieceColors.Black }//12325

              } );
      Assert.Equal( -681, Eval.GetMaterialValue_DEBUG( fooBoard ) );

    }

    [Fact]
    public void EvaluateState_WithKnightFactor_Black_Test() {
      ChessBoard fooBoard = new ChessBoard();
      fooBoard.InitializeScenario( new ScenarioList {
                 //White
                 {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },
                 {BoardSquare.B1, ChessPieceType.Bishop, ChessPieceColors.White},
                 {BoardSquare.C1, ChessPieceType.Queen, ChessPieceColors.White},
                 {BoardSquare.D1, ChessPieceType.Rook, ChessPieceColors.White},
                 {BoardSquare.E1, ChessPieceType.Knight, ChessPieceColors.White},
                 {BoardSquare.A2, ChessPieceType.Pawn, ChessPieceColors.White},
                 {BoardSquare.B2, ChessPieceType.Pawn, ChessPieceColors.Black},
                 {BoardSquare.F1, ChessPieceType.Knight, ChessPieceColors.Black},

                 //Black
                 {BoardSquare.A8, ChessPieceType.Rook, ChessPieceColors.Black },
                 {BoardSquare.B8, ChessPieceType.Knight, ChessPieceColors.Black },
                 {BoardSquare.C8, ChessPieceType.Queen, ChessPieceColors.Black },
                 {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },
                 {BoardSquare.E8, ChessPieceType.Bishop, ChessPieceColors.Black },
                 {BoardSquare.A7, ChessPieceType.Pawn, ChessPieceColors.Black }

              } );
      Assert.Equal( -660, Eval.GetMaterialValue_DEBUG( fooBoard ) );

    }

    [Fact]
    public void EvaluateState_WithRookFactor_White_LateGame() {
      ChessBoard fooBoard = new ChessBoard();
      fooBoard.InitializeScenario( new ScenarioList {
                 //White
                 {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },    //10000
                 {BoardSquare.B1, ChessPieceType.Bishop, ChessPieceColors.White}, //350
                 {BoardSquare.C1, ChessPieceType.Queen, ChessPieceColors.White}, // 1000
                 {BoardSquare.D1, ChessPieceType.Rook, ChessPieceColors.White}, //1417,5
                 {BoardSquare.E1, ChessPieceType.Rook, ChessPieceColors.White}, //1417,5

                 //Black
                 {BoardSquare.A8, ChessPieceType.Rook, ChessPieceColors.Black },
                 {BoardSquare.B8, ChessPieceType.Knight, ChessPieceColors.Black },
                 {BoardSquare.C8, ChessPieceType.Queen, ChessPieceColors.Black },
                 {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },

              } );
      Assert.Equal( 892.5f, Eval.GetMaterialValue_DEBUG( fooBoard ) ); //12767,5 white - 11875 black
    }
    #endregion

    [Fact]
    public void EvaluatePosition_NonSymmetricBoard_Test() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeScenario( new ScenarioList {
                 //White
                 {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },
                 {BoardSquare.B1, ChessPieceType.Bishop, ChessPieceColors.White},
                 {BoardSquare.C1, ChessPieceType.Queen, ChessPieceColors.White},
                 {BoardSquare.D1, ChessPieceType.Rook, ChessPieceColors.White},
                 {BoardSquare.E1, ChessPieceType.Knight, ChessPieceColors.White},
                 {BoardSquare.A2, ChessPieceType.Pawn, ChessPieceColors.White},

                 //Black
                 {BoardSquare.A8, ChessPieceType.Rook, ChessPieceColors.Black },
                 {BoardSquare.B8, ChessPieceType.Knight, ChessPieceColors.Black },
                 {BoardSquare.C8, ChessPieceType.Queen, ChessPieceColors.Black },
                 {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black },
                 {BoardSquare.E8, ChessPieceType.Bishop, ChessPieceColors.Black },
                 {BoardSquare.A7, ChessPieceType.Pawn, ChessPieceColors.Black }

              } );
      Assert.Equal( 35, Eval.EvaluatePosition_DEBUG( testBoard ) );
    }

    #region NegaMax

    [Fact]
    public void BestMoveTest_BlackDepth1() {
      ChessBoard input = new ChessBoard();
      input.InitializeScenario( new ScenarioList {
            { BoardSquare.C2, ChessPieceType.Pawn, ChessPieceColors.White },
            { BoardSquare.B3, ChessPieceType.Pawn, ChessPieceColors.Black },
            { BoardSquare.H1, ChessPieceType.King, ChessPieceColors.White},
            { BoardSquare.B4, ChessPieceType.King, ChessPieceColors.Black },
            { BoardSquare.D3, ChessPieceType.Pawn, ChessPieceColors.Black}
            } );
      PawnBitBoard bestMoveBlack1 = new PawnBitBoard( ChessPieceColors.Black );
      bestMoveBlack1.Bits = ( input.BlackPawn.Bits ^ BoardSquare.D3 ) | BoardSquare.C2 | BoardSquare.B3;

      PawnBitBoard bestMoveBlack2 = new PawnBitBoard( ChessPieceColors.Black );
      bestMoveBlack2.Bits = ( input.BlackPawn.Bits ^ BoardSquare.B3 ) | BoardSquare.C2 | BoardSquare.D3;

      Tuple<BitBoard, int> outputTuple_Black1 = new Tuple<BitBoard, int>( bestMoveBlack1, -1 );
      Tuple<BitBoard,int> outputTuple_Black2 = new Tuple<BitBoard, int>( bestMoveBlack2, -1 );

      BitBoard blackBoard1 = outputTuple_Black1.Item1;
      BitBoard blackBoard2 = outputTuple_Black2.Item1;

      int blackVal = outputTuple_Black1.Item2;

      //ÆNDRE TEST HER!
      BitBoard testTuple_Black=   NegaMax.GetBestMove( input, 1, ChessPieceColors.Black, GenerateStaticMoves_Black_Depth1, Eval.EvaluateState );

      int blackVal_Test = outputTuple_Black1.Item2;

      bool blackBoardEqual = false;
      bool blackBoardValEqual = false;

      //Black test comparison
      if ( blackVal == blackVal_Test ) {
        blackBoardValEqual = true;
      }
      if ( testTuple_Black is PawnBitBoard ) {
        if ( testTuple_Black.Bits == blackBoard1.Bits | testTuple_Black.Bits == blackBoard2.Bits ) {
          blackBoardEqual = true;
        }
      }
      Assert.True( blackBoardValEqual && blackBoardEqual );
    }

    [Fact]
    public void BestMoveTest_WhiteDepth1() {

      ChessBoard input = new ChessBoard();
      input.InitializeScenario( new ScenarioList {
            { BoardSquare.C2, ChessPieceType.Pawn, ChessPieceColors.White },
            { BoardSquare.B3, ChessPieceType.Pawn, ChessPieceColors.Black },
            { BoardSquare.H1, ChessPieceType.King, ChessPieceColors.White},
            { BoardSquare.B4, ChessPieceType.King, ChessPieceColors.Black },
            { BoardSquare.D3, ChessPieceType.Pawn, ChessPieceColors.Black}
            } );

      //Used for Depth 1
      PawnBitBoard bestMoveWhite1 = new PawnBitBoard( ChessPieceColors.White );
      bestMoveWhite1.Bits = BoardSquare.B3;
      PawnBitBoard bestMoveWhite2 = new PawnBitBoard( ChessPieceColors.White );
      bestMoveWhite2.Bits = BoardSquare.D3;
      Tuple<BitBoard, int> outputTuple_White1 = new Tuple<BitBoard, int>( bestMoveWhite1, 1 );
      Tuple<BitBoard, int> outputTuple_White2 = new Tuple<BitBoard, int>( bestMoveWhite2, 1 );
      BitBoard whiteBoard1= outputTuple_White1.Item1;
      BitBoard whiteBoard2 = outputTuple_White2.Item1;
      int whiteVal = outputTuple_White1.Item2;
      //ÆNDRE TEST HER!
      BitBoard testTuple_White =  NegaMax.GetBestMove( input, 1, ChessPieceColors.White, GenerateStaticMoves_White_Depth1, Eval.EvaluateState );
      int whiteVal_Test = outputTuple_White1.Item2;


      bool whiteBoardEqual = false;
      bool whiteBoardValEqual = false;

      //White test comparison
      if ( whiteVal == whiteVal_Test ) {
        whiteBoardValEqual = true;
      }
      if ( testTuple_White is PawnBitBoard ) {
        if ( testTuple_White.Bits == whiteBoard1.Bits || testTuple_White.Bits == whiteBoard2.Bits ) {
          whiteBoardEqual = true;
        }
      }

      Assert.True( whiteBoardValEqual && whiteBoardEqual );

    }

    [Fact]
    public void BestMoveTest_WhiteDepth2() {
      ChessBoard input = new ChessBoard();
      input.InitializeScenario( new ScenarioList {
            { BoardSquare.C2, ChessPieceType.Pawn, ChessPieceColors.White },
            { BoardSquare.B3, ChessPieceType.Pawn, ChessPieceColors.Black },
            { BoardSquare.H1, ChessPieceType.King, ChessPieceColors.White},
            { BoardSquare.B4, ChessPieceType.King, ChessPieceColors.Black },
            { BoardSquare.D3, ChessPieceType.Pawn, ChessPieceColors.Black}
            } );

      //Used for depth 2
      bool depth2MoveEqual = false;
      ColoredBitBoard depth2Test = NegaMax.GetBestMove( input, 2, ChessPieceColors.White, GenerateStaticMoves_Depth12, GetStaticMaterialValueDepth2_White );
      PawnBitBoard optimalMove = new PawnBitBoard( ChessPieceColors.White );
      optimalMove.Bits = BoardSquare.D3;
      if ( depth2Test is PawnBitBoard ) {
        depth2Test = (PawnBitBoard)depth2Test;
        if ( depth2Test.Bits == optimalMove.Bits ) {
          depth2MoveEqual = true;
        }
      }
      Assert.True( depth2MoveEqual );

    }
    [Fact]
    public void CheckmateTest_Black() {
      //Checkmate state test
      ChessBoard checkmate = new ChessBoard();
      checkmate.InitializeScenario( new ScenarioList{
                {BoardSquare.G8, ChessPieceType.Rook, ChessPieceColors.White },
                {BoardSquare.D6, ChessPieceType.Knight, ChessPieceColors.White },
                {BoardSquare.D8, ChessPieceType.King, ChessPieceColors.Black }
              } );
      checkmate.BlackKing.IsChecked = true;
      NegaMax.TerminalConditions result = NegaMax.IsTerminal_Debug( checkmate, ChessPieceColors.Black, GenerateCheckMateMoves );

      Assert.Equal( NegaMax.TerminalConditions.Win, result );
    }

    [Fact]
    public void NotTerminal_White() {
      ChessBoard notTerminal = new ChessBoard();
      notTerminal.InitializeScenario( new ScenarioList {
                {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },
                 {BoardSquare.H8, ChessPieceType.King, ChessPieceColors.Black },
                 {BoardSquare.A2, ChessPieceType.Pawn, ChessPieceColors.White },
                 {BoardSquare.H7, ChessPieceType.Pawn, ChessPieceColors.Black }
              } );

      NegaMax.TerminalConditions resultWhiteNotTerminal = NegaMax.IsTerminal_Debug( notTerminal, ChessPieceColors.White, GenerateStaticMoves_White_Depth1 );
      Assert.Equal( NegaMax.TerminalConditions.NotTerminal, resultWhiteNotTerminal );
    }

    [Fact]
    public void NotTerminal_Black() {
      ChessBoard notTerminal = new ChessBoard();
      notTerminal.InitializeScenario( new ScenarioList {
                {BoardSquare.A1, ChessPieceType.King, ChessPieceColors.White },
                 {BoardSquare.H8, ChessPieceType.King, ChessPieceColors.Black },
                 {BoardSquare.A2, ChessPieceType.Pawn, ChessPieceColors.White },
                 {BoardSquare.H7, ChessPieceType.Pawn, ChessPieceColors.Black }
              } );

      //Not terminal state test
      NegaMax.TerminalConditions resultBlackNotTerminal = NegaMax.IsTerminal_Debug( notTerminal, ChessPieceColors.Black, GenerateStaticMoves_Black_Depth1 );


      Assert.Equal( NegaMax.TerminalConditions.NotTerminal, resultBlackNotTerminal );
    }

    [Fact]
    public void DrawTest_Black() {
      //Draw state test
      ChessBoard draw = new ChessBoard();
      draw.InitializeScenario( new ScenarioList{
        {BoardSquare.A8, ChessPieceType.King, ChessPieceColors.Black},
        {BoardSquare.B7, ChessPieceType.Rook, ChessPieceColors.White},
        {BoardSquare.H1, ChessPieceType.King, ChessPieceColors.White}
      } );


      NegaMax.TerminalConditions resultBlackDraw = NegaMax.IsTerminal_Debug( draw, ChessPieceColors.Black, GenerateCheckMateMoves );
      Assert.Equal( NegaMax.TerminalConditions.Draw, resultBlackDraw );
    }

    [Fact]
    public void Test_super() {
      Winboard winboard = new Winboard();

       
      winboard.Handler( "new" );
      winboard.Handler( "force" );
      winboard.Handler( "e2e4" );
      winboard.Handler( "e7e5" );
      winboard.Handler( "b1c3" );
      winboard.Handler( "g8f6" );
      winboard.Handler( "f2f3" );
      winboard.Handler( "c7c5" );
      winboard.Handler( "d2d3" );
      winboard.engineColor = winboard.engineColor == ChessPieceColors.White ? ChessPieceColors.Black : ChessPieceColors.White;
      NegaMax.NegaMaxAlgorithm( winboard.chessBoard, 2, int.MinValue + 5, int.MaxValue - 4, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
      //winboard.Handler( "go" );

      Assert.True( true );
    }

    //[Fact]
    //public void TranspositionTable_1() {
    //  Winboard winboard = new Winboard();


    //  //http://www.chessgames.com/perl/chessgame?gid=1011478
    //  winboard.Handler( "new" );
    //  winboard.Handler( "force" );
    //  winboard.Handler( "e2e4" );
    //  winboard.Handler( "d7d6" );
    //  winboard.Handler( "d2d4" );
    //  winboard.Handler( "g8f6" );
    //  winboard.Handler( "b1c3" );
    //  winboard.Handler( "g7g6" );
    //  winboard.Handler( "c1e3" );
    //  winboard.Handler( "f8g7" );
    //  winboard.Handler( "d1d2" );
    //  winboard.Handler( "c7c6" );
    //  winboard.Handler( "f2f3" );
    //  winboard.Handler( "b7b5" );
    //  winboard.Handler( "g1e2" );
    //  winboard.Handler( "b8d7" );
    //  winboard.Handler( "e3h6" );
    //  winboard.Handler( "g7h6" );
    //  winboard.Handler( "d2h6" );
    //  winboard.Handler( "c8b7" );
    //  winboard.Handler( "a2a3" );
    //  winboard.Handler( "e7e5" );
    //  winboard.Handler( "e1c1" );
    //  winboard.Handler( "d8e7" );
    //  winboard.Handler( "c1b1" );
    //  winboard.Handler( "a7a6" );
    //  winboard.Handler( "e2c1" );
    //  winboard.Handler( "e8c8" );
    //  winboard.Handler( "c1b3" );
    //  winboard.Handler( "e5d4" );
    //  winboard.Handler( "d1d4" );
    //  winboard.Handler( "c6c5" );
    //  winboard.Handler( "d4d1" );
    //  winboard.Handler( "d7b6" );
    //  winboard.Handler( "g2g3" );
    //  winboard.Handler( "c8b8" );
    //  winboard.Handler( "b3a5" );
    //  winboard.Handler( "b7a8" );
    //  winboard.Handler( "f1h3" );
    //  winboard.Handler( "d6d5" );
    //  winboard.Handler( "h6f4" );
    //  winboard.Handler( "b8a7" );
    //  winboard.Handler( "h1e1" );
    //  winboard.Handler( "d5d4" );
    //  winboard.Handler( "c3d5" );
    //  winboard.Handler( "b6d5" );
    //  winboard.Handler( "e4d5" );
    //  winboard.Handler( "e7d6" );
    //  winboard.Handler( "d1d4" );
    //  winboard.Handler( "c5d4" );
    //  winboard.Handler( "e1e7" );
    //  winboard.Handler( "a7b6" );
    //  winboard.Handler( "f4d4" );
    //  winboard.Handler( "b6a5" );
    //  winboard.Handler( "b2b4" );
    //  winboard.Handler( "a5a4" );
    //  winboard.Handler( "d4c3" );
    //  winboard.Handler( "d6d5" );
    //  winboard.Handler( "e7a7" );
    //  winboard.Handler( "a8b7" );
    //  winboard.Handler( "a7b7" );
    //  winboard.Handler( "d5c4" );
    //  winboard.Handler( "c3f6" );
    //  winboard.Handler( "a4a3" );
    //  winboard.Handler( "f6a6" );
    //  winboard.Handler( "a3b4" );
    //  winboard.Handler( "c2c3" );
    //  winboard.Handler( "b4c3" );
    //  winboard.Handler( "a6a1" );
    //  winboard.Handler( "c3d2" );
    //  winboard.Handler( "a1b2" );
    //  winboard.Handler( "d2d1" );
    //  winboard.Handler( "h3f1" );
    //  winboard.Handler( "d8d2" );
    //  winboard.Handler( "b7d7" );
    //  winboard.Handler( "d2d7" );
    //  winboard.Handler( "f1c4" );
    //  winboard.Handler( "b5c4" );
    //  winboard.Handler( "b2h8" );
    //  winboard.Handler( "d7d3" );
    //  winboard.Handler( "h8a8" );
    //  winboard.Handler( "c4c3" );
    //  //winboard.Handler( "a8a4" );
    //  //winboard.Handler( "d1e1" );
    //  //winboard.Handler( "f3f4" );
    //  //winboard.Handler( "f6f5" );
    //  //winboard.Handler( "b1c1" );
    //  //winboard.Handler( "d3d2" );
    //  //winboard.Handler( "a4a7" );
    //  winboard.Handler( "go" );

    //  Assert.True( true );
    //}
    #endregion

    [Fact]
    public void Stack_DeepCopy_Equal() {
      Stack<int> input = new Stack<int>();
      input.Push( 1 );
      input.Push( 2 );
      input.Push( 3 );
      input.Push( 4 );
      Stack<int> correct = new Stack<int>();
      correct.Push( 1 );
      correct.Push( 2 );
      correct.Push( 3 );
      correct.Push( 4 );
      Stack<int> inputCopy = new Stack<int>( input );
      inputCopy.Push( 5 );
      Assert.Equal( correct.Count, input.Count );
    }
  }


}
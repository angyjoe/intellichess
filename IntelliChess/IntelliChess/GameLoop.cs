//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
/*
 * Author: Sari Haj Hussein
 */
//namespace P5 {
//  public class GameLoop {
//    public bool IsRunning { private set; get; }
//    public ChessPieceColors PlayingColor { private set; get; }

//    private DataProvider _provider;
//    private ChessBoard _chessBoard;
//    /// <summary>
//    /// Polled from WinBoard
//    /// </summary>
//    //private bool IsTurnToMove {
//    //  get {
//    //    return _provider.GetTurnToMove() == PlayingColor;
//    //  }
//    //}

//    public GameLoop() {
//      _provider = new WinBoardDataProvider( _chessBoard );
//    }


//    private bool InitializeGame( ChessPieceColors color ) {
//      _chessBoard = new ChessBoard();
//      _chessBoard.InitializeGame();
//      if ( _provider.Initialize( new Settings {
//        EngineColor = ChessPieceColors.Black
//      } ) )
//        return true;
//      else
//        return false;
//    }

//    public void Run( ChessPieceColors color ) {
//      //if IsRunning, restart by stopping current game loop first
//      IsRunning = true;
//      //if ( InitializeGame( color ) )
//      //  Task.Factory.StartNew( MainLoop, TaskCreationOptions.LongRunning );

//      MainLoop();
//    }

//    public void Stop() {
//      IsRunning = false;
//      //Stop current game loop
//    }

//    private void MainLoop() {
//      while ( IsRunning ) {
//        if ( PlayingColor == ChessPieceColors.White ) {          
//          /* Make out move */
//          var bitboard = NegaMax.GetBestMove( _chessBoard, 3, PlayingColor, MoveGen.GenerateLegalMoves, NegaMax.GetMaterialValue );
//          _provider.SendMove(_chessBoard.GetOldBitBoardFromBitBoard( bitboard ), bitboard);
//          _chessBoard.Update( bitboard );
          

//          if ( _chessBoard.State != ChessBoardGameState.Running ) 
//            _provider.SendGameResult( _chessBoard.State );

//          /* Make opponent move */
//          var opponentMove = _provider.RecieveMove();
//          _chessBoard.Update( opponentMove );

//          if ( _chessBoard.State != ChessBoardGameState.Running )
//            _provider.SendGameResult( _chessBoard.State );
//        } else {
//          /* Make opponent move */
//          var opponentMove = _provider.RecieveMove();
//          _chessBoard.Update( opponentMove );

//          if ( _chessBoard.State != ChessBoardGameState.Running ) 
//            _provider.SendGameResult( _chessBoard.State );

//          /* Make out move */
//          var bitboard = NegaMax.GetBestMove( _chessBoard, 3, PlayingColor, MoveGen.GenerateLegalMoves, NegaMax.GetMaterialValue );
//          _chessBoard.Update( bitboard );
//          _provider.SendMove( _chessBoard.GetOldBitBoardFromBitBoard( bitboard ), bitboard );

//          if ( _chessBoard.State != ChessBoardGameState.Running ) 
//            _provider.SendGameResult( _chessBoard.State );
//        }
//      }
//    }
//  }
//}

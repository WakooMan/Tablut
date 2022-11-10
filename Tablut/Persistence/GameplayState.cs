using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tablut.Model.GameModel;
using Tablut.ViewModel;

namespace Tablut.Persistence
{
    public class GameplayState: TablutState
    {
        public GameplayState(): base(null) { }

        public GameplayState(GameViewModel model): base(model) { }
        public GameplayState(GameMenuViewModel model) : base(model.Game) { }
        public GameplayState(GameOverViewModel model) : base(model.Game) { }

        protected override void OnRead(BinaryReader reader)
        {
            string saveFileName = reader.ReadString();
            string attackerName = reader.ReadString();
            string defenderName = reader.ReadString();
            PlayerSide side = (PlayerSide)reader.ReadInt32();
            int attackercount = reader.ReadInt32();
            (int x, int y)[] attackerValues = new (int x, int y)[attackercount];
            for (int i = 0; i < attackercount; i++)
            {
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();
                attackerValues[i] = (x, y);
            }
            int defendercount = reader.ReadInt32();
            (int x, int y)[] defenderValues = new (int x, int y)[defendercount];
            for (int i = 0; i < defendercount; i++)
            {
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();
                defenderValues[i] = (x, y);
            }

            bool HasSelectedPiece = reader.ReadBoolean();
            int selectedPieceX = -1;
            int selectedPieceY = -1;
            if(HasSelectedPiece)
            {
                selectedPieceX = reader.ReadInt32();
                selectedPieceY = reader.ReadInt32();
            }
            GameState state = (GameState)reader.ReadInt32();
            GameModel gameModel = new GameModel(attackerName, defenderName, attackerValues, defenderValues, side,selectedPieceX,selectedPieceY,state);
            Model = new GameViewModel(gameModel, saveFileName);
        }

        protected override void OnWrite(BinaryWriter writer)
        {
            GameViewModel model = (GameViewModel)Model;
            GameModel gameModel = model.Model;
            writer.Write(model.SaveFileName);
            writer.Write(gameModel.Attacker.Name);
            writer.Write(gameModel.Defender.Name);
            writer.Write((int)gameModel.CurrentPlayer.Side);
            writer.Write(gameModel.Attacker.AlivePieces.Count());
            foreach (Piece piece in gameModel.Attacker.AlivePieces)
            {
                writer.Write(piece.Place.X);
                writer.Write(piece.Place.Y);
            }
            writer.Write(gameModel.Defender.AlivePieces.Count());
            Piece king = gameModel.Defender.AlivePieces.Where(p => p is King).Single();
            writer.Write(king.Place.X);
            writer.Write(king.Place.Y);
            foreach (Piece piece in gameModel.Defender.AlivePieces.Where(p => p is Soldier))
            {
                writer.Write(piece.Place.X);
                writer.Write(piece.Place.Y);
            }

            bool HasSelectedPiece = gameModel.CurrentPlayer.SelectedPiece != null;
            writer.Write(HasSelectedPiece);
            if (HasSelectedPiece)
            {
                writer.Write(gameModel.CurrentPlayer.SelectedPiece.Place.X);
                writer.Write(gameModel.CurrentPlayer.SelectedPiece.Place.Y);
            }

            writer.Write((int)gameModel.GameState);
        }
    }
}

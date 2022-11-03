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
            GameModel gameModel = new GameModel(attackerName, defenderName, attackerValues, defenderValues, side);
            Model = new GameViewModel(gameModel, saveFileName);

            bool HasSelectedPiece = reader.ReadBoolean();
            if (HasSelectedPiece)
            {
                int selectedPieceX = reader.ReadInt32();
                int selectedPieceY = reader.ReadInt32();
                gameModel.StepOrSelect(selectedPieceX, selectedPieceY);
            }

            GameState state = (GameState)reader.ReadInt32();
            if (state == GameState.Paused)
            {
                gameModel.Pause();
            }
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

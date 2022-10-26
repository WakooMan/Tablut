using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tablut.Model.GameModel;

namespace Tablut.ViewModel
{
    public class GameViewModel: BindingSource
    {
        private GameModel _model;
        public ObservableCollection<FieldViewModel> Fields { get; private set; } = new ObservableCollection<FieldViewModel>();

        public GameViewModel(GameModel model)
        {
            _model = model;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Field f = _model.Table.GetField(i,j);
                    string imgsrc;
                    if (f.Piece != null)
                    {
                        if (f.Piece.Player.Side == PlayerSide.Attacker)
                        {
                            imgsrc = "AttackerSoldier.png";
                        }
                        else
                        {
                            if (f.Piece is King)
                            {
                                imgsrc = "DefenderKing.png";
                            }
                            else
                            {
                                imgsrc = "DefenderSoldier.png";
                            }
                        }
                    }
                    else
                    {
                        imgsrc = "FieldBase.png";
                    }
                    Fields.Add(new FieldViewModel(i,j,imgsrc, new DelegateCommand(Command_SelectOrStep)));
                }
            }
        }

        private void Command_SelectOrStep(object param)
        {
            if (param != null && param is FieldViewModel field)
            {
                _model.SelectOrStep(field.X,field.Y);
            }
        }
    }
}

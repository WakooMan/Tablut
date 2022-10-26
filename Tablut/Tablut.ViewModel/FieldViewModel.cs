using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tablut.ViewModel
{
    public class FieldViewModel: BindingSource
    {
        public readonly int X;
        public readonly int Y;
        public string ImageSource { get; private set; }
        public DelegateCommand StepOrSelectCommand { get; private set; }

        public FieldViewModel(int x,int y,string imageSource,DelegateCommand stepOrSelectCommand)
        {
            X = x;
            Y = y;
            ImageSource = imageSource;
            StepOrSelectCommand = stepOrSelectCommand;
        }
    }
}

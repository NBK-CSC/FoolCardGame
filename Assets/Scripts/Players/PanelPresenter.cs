using System.Collections.Generic;
using States;

namespace Players
{
    public class PanelPresenter
    {
        private Panel _model;
        private PlayingPanel _view;

        private static readonly string _takeText = "Take";
        private static readonly string _passText = "Take";

        private Dictionary<StatusPlayer, string> _textStatusProceed = new Dictionary<StatusPlayer, string>()
        {
            { StatusPlayer.DefenderActivating, _takeText },
            { StatusPlayer.DefenderWaiting, null },
            { StatusPlayer.ThrowerActivating, _passText },
            { StatusPlayer.ThrowerWaiting, null },
            { StatusPlayer.ThrowerDisabled, null }
        };
        
        public PanelPresenter(Panel model, PlayingPanel view)
        {
            _model = model;
            _view = view;
        }
        
        public void Enable()
        {
            _view.OnButtonClicked += Clicked;
            _model.OnStatusChanged += ChangeText;
        }
        
        public void Disable()
        {
            _view.OnButtonClicked -= Clicked;
            _model.OnStatusChanged -= ChangeText;
        }
        
        private void Clicked()
        {
            _model.Proceed();
        }
        
        private void ChangeText(StatusPlayer status)
        {
            _view.Draw(_textStatusProceed[status]);
        }
    }
}
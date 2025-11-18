using System;
using TMPro;
using UI.Models.Game;
using UI.Views.Abstraction;
using UI.Views.ViewComponents;
using UI.ViewsModels.Game;
using UnityEngine;

namespace UI.Views.Game
{
    public class MatchOverSubView : View<MatchOverViewModel>
    {
        private ButtonViewComponent _buttonRetry;
        private ButtonViewComponent _buttonExit;
        [SerializeField] private TextMeshProUGUI textMatchResult;
        [SerializeField] private TextMeshProUGUI textMatchTime;
        
        protected override void Initialize()
        {
            base.Initialize();
            
            //Get components
            var buttonComponents = GetViewComponents<ButtonViewComponent>();
            _buttonRetry = buttonComponents[0];
            _buttonExit = buttonComponents[1];
        }
        
        protected override void SetupDataBindings()
        {
            base.SetupDataBindings();
            
            //Proved actions to Bindables.
            ViewModel.MatchResult.BindTo(OnMatchResultChanged);
            ViewModel.MatchTime.BindTo(OnMatchTimeChanged);
        }
        
        protected override void SetupActionCallbacks()
        {
            base.SetupActionCallbacks();
            
            _buttonRetry.ButtonClicked = OnRetryButtonClick;
            _buttonExit.ButtonClicked = OnExitButtonClick;
        }
        
        private void OnMatchResultChanged(GameOutcome matchResult)
        {
            switch (matchResult)
            {
                case GameOutcome.None:
                    break;
                case GameOutcome.WinX:
                    textMatchResult.text = $"Match won by player 1!";
                    break;
                case GameOutcome.WinO:
                    textMatchResult.text = $"Match won by player 2!";
                    break;
                case GameOutcome.Draw:
                    textMatchResult.text = $"Match was a draw!";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(matchResult), matchResult, null);
            }
        }

        private void OnMatchTimeChanged(double matchTime)
        {
            textMatchTime.text = $"Match time: {matchTime:F1} sec";
        }

        private async void OnRetryButtonClick()
        {
            try
            {
                ViewModel.ResetGameData();
                await ViewModel.StateMachine.TransitionTo(UIView.GameScreen);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void OnExitButtonClick()
        {
            ViewModel.ResetGameData();
            ViewModel.LoadPlayScene();
        }
    }
}

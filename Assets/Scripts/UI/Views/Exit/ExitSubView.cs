using System;
using UI.Views.Abstraction;
using UI.Views.ViewComponents;
using UI.ViewsModels.Exit;
using UnityEditor;
using UnityEngine;

namespace UI.Views.Exit
{
    public class ExitSubView : View<ExitViewModel>
    {
        private ButtonViewComponent _yesButton;
        private ButtonViewComponent _noButton;
        
        protected override void Initialize()
        {
            base.Initialize();
            
            var buttonComponents = GetViewComponents<ButtonViewComponent>();
            _yesButton = buttonComponents[0];
            _noButton = buttonComponents[1];
        }

        protected override void SetupActionCallbacks()
        {
            _yesButton.ButtonClicked = OnYesButtonClick;
            _noButton.ButtonClicked = OnNoButtonClick;
        }
        
        private void OnYesButtonClick()
        {
            if (Application.isEditor)
            {
                EditorApplication.isPlaying = false;
            }
            else
            {
                Application.Quit();                
            }
        }
        
        private async void OnNoButtonClick()
        {
            try
            {
                await ViewModel.StateMachine.TransitionTo(UIView.HomeScreen);
                Debug.Log("Transition to UIView.HomeScreen");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }    
    }
}

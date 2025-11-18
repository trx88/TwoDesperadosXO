using System;
using UI.Models.Theme;
using UI.Views.Abstraction;
using UI.Views.ViewComponents;
using UI.ViewsModels.Theme;
using UnityEngine;

namespace UI.Views.Theme
{
    public class ThemeSubView : View<ThemeViewModel>
    {
        private ButtonViewComponent _themeOneButton;
        private ButtonViewComponent _themeTwoButton;
        private ButtonViewComponent _themeThreeButton;
        private ButtonViewComponent _closeButton;
        
        //Cache
        private ThemeModel _themeData;
        
        protected override void Initialize()
        {
            base.Initialize();
            
            var viewComponents = GetViewComponents<ButtonViewComponent>();
            if (viewComponents.Count > 0)
            {
                _themeOneButton = viewComponents[0];
                _themeTwoButton = viewComponents[1];
                _themeThreeButton = viewComponents[2];
                _closeButton = viewComponents[3];
            }
        }
        
        protected override void SetupDataBindings()
        {
            base.SetupDataBindings();
            
            //Proved actions to Bindables.
            ViewModel.ThemeData.BindTo(OnThemeDataChanged);
        }
        
        protected override void SetupActionCallbacks()
        {
            base.SetupActionCallbacks();
            
            _themeOneButton.ButtonClicked = OnThemeOneButtonClicked;
            _themeTwoButton.ButtonClicked = OnThemeTwoButtonClicked;
            _themeThreeButton.ButtonClicked = OnThemeThreeButtonClicked;
            _closeButton.ButtonClicked = OnCloseButtonClicked;
        }
        
        private void OnThemeDataChanged(ThemeModel themeData)
        {
            _themeData = themeData;
        }
        
        private async void OnThemeOneButtonClicked()
        {
            try
            {
                _themeData.XThemeAsset = ThemeAssetNames.SignXTheme1;
                _themeData.OThemeAsset = ThemeAssetNames.SignOTheme1;
                ViewModel.UpdateTheme(_themeData);
                
                // await ViewModel.StateMachine.TransitionTo(UIView.GameScreen);
                ViewModel.LoadGameScene();
                Debug.Log("Transition to UIView.GameScreen");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        private async void OnThemeTwoButtonClicked()
        {
            try
            {
                _themeData.XThemeAsset = ThemeAssetNames.SignXTheme2;
                _themeData.OThemeAsset = ThemeAssetNames.SignOTheme2;
                ViewModel.UpdateTheme(_themeData);
                
                // await ViewModel.StateMachine.TransitionTo(UIView.GameScreen);
                ViewModel.LoadGameScene();
                Debug.Log("Transition to UIView.GameScreen");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        private async void OnThemeThreeButtonClicked()
        {
            try
            {
                _themeData.XThemeAsset = ThemeAssetNames.SignXTheme3;
                _themeData.OThemeAsset = ThemeAssetNames.SignOTheme3;
                ViewModel.UpdateTheme(_themeData);
                
                // await ViewModel.StateMachine.TransitionTo(UIView.GameScreen);
                ViewModel.LoadGameScene();
                Debug.Log("Transition to UIView.GameScreen");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        private async void OnCloseButtonClicked()
        {
            try
            {
                await ViewModel.StateMachine.TransitionTo(UIView.HomeScreen);
                Debug.Log("Transition to UIView.HomeScreenView");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}

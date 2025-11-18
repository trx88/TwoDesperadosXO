using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JSAM;
using UI.Models.Game;
using UI.Views.Abstraction;
using UI.Views.ViewComponents;
using UI.ViewsModels.Game;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace UI.Views.Game
{
    public class GameSubView : View<GameViewModel>
    {
        [SerializeField] private ButtonViewComponent settingsButton;
        [SerializeField] private float cellSize = 300f;
        [SerializeField] private Image winningLineImage;
        
        private ButtonViewComponent _cellButton0;
        private ButtonViewComponent _cellButton1;
        private ButtonViewComponent _cellButton2;
        private ButtonViewComponent _cellButton3;
        private ButtonViewComponent _cellButton4;
        private ButtonViewComponent _cellButton5;
        private ButtonViewComponent _cellButton6;
        private ButtonViewComponent _cellButton7;
        private ButtonViewComponent _cellButton8;
        // private ButtonViewComponent _settingsButton;
        private ViewComponentHUD _viewComponentHUD;
        
        private List<ButtonViewComponent> _cellButtons;
        //Cache
        private GameModel _gameData;
        private GameObject _xSignAsset;
        private GameObject _oSignAsset;
        
        private async Task LoadAssets()
        {
            _xSignAsset = await AddressablesManager.LoadAssetAsync<GameObject>(ViewModel.ThemeData.Value.XThemeAsset);
            _oSignAsset = await AddressablesManager.LoadAssetAsync<GameObject>(ViewModel.ThemeData.Value.OThemeAsset);
        }

        protected override async void Initialize()
        {
            base.Initialize();
            
            //Get components
            _viewComponentHUD = GetViewComponent<ViewComponentHUD>();
                
            _cellButtons = new List<ButtonViewComponent>();
            var list = GetViewComponents<ButtonViewComponent>();
            for (var index = 0; index < list.Count; index++)
            {
                if (index == list.Count - 1)
                {
                    // _settingsButton = list[index];
                }
                else
                {
                    var cellButton = list[index];
                    _cellButtons.Add(cellButton);
                    cellButton.SetInteractable(true);
                }
            }

            _gameData = null;
        }

        protected override async void Finish()
        {
            try
            {
                base.Finish();
            
                //Additional initialization
                await LoadAssets();
                ViewModel.StartMatch();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        protected override void Deinitialize()
        {
            AddressablesManager.ReleaseAsset(ViewModel.ThemeData.Value.XThemeAsset);
            AddressablesManager.ReleaseAsset(ViewModel.ThemeData.Value.OThemeAsset);
        }
        
        protected override void SetupDataBindings()
        {
            base.SetupDataBindings();
            
            //Proved actions to Bindables.
            ViewModel.GameData.BindTo(OnGameDataChanged);
        }
        
        protected override void SetupActionCallbacks()
        {
            base.SetupActionCallbacks();

            settingsButton.ButtonClicked = OnSettingsButtonClick;
            
            for (int index = 0; index < _cellButtons.Count; index++)
            {
                int cellIndex = index;
                _cellButtons[index].AddCustomListener(() => OnCellButtonClicked(cellIndex));
            }
        }
        
        private void OnGameDataChanged(GameModel gameData)
        {
            //Handle UI here after update
            if (_gameData != null)
            {
                int newSignIndex = 0;
                int newSign = 0;
                for (int index = 0; index < _gameData.Board.Count; index++)
                {
                    if (_gameData.Board[index] != gameData.Board[index])
                    {
                        newSignIndex = index;
                        newSign = gameData.Board[index];
                        break;
                    }
                }

                //Update can happen because timer is counting, but new move is needed to enter this block.
                if (newSign != 0)
                {
                    AudioManager.PlaySound(AudioLibrarySounds.SignPlacement);
                    var signObject = Instantiate(newSign == 1 ? _xSignAsset : _oSignAsset, _cellButtons[newSignIndex].transform);
                    signObject.transform.localScale = new Vector3(2, 2, 2);

                    if (gameData.MatchResult != GameOutcome.None)
                    {
                        MatchOver(gameData.WinningLine);
                    }
                }
            }
            
            _viewComponentHUD?.UpdateData(gameData.PlayerOneMoves, gameData.PlayerTwoMoves, gameData.MatchTime);
            
            _gameData = gameData.Clone() as GameModel;
        }
        
        private async void OnSettingsButtonClick()
        {
            try
            {
                await ViewModel.StateMachine.TransitionTo(UIView.SettingsScreen);
                Debug.Log("Transition to UIView.SettingsScreen");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        private void OnCellButtonClicked(int index)
        {
            _cellButtons[index].SetInteractable(false);
            ViewModel.CellClicked(index);
        }

        private async void MatchOver(int[] winningLine)
        {
            try
            {
                _gameData = null;
                AudioManager.PlaySound(AudioLibrarySounds.Strike);
                if (winningLine != null)
                {
                    await AnimateWinningLine(winningLine);   
                }
                await ViewModel.StateMachine.TransitionTo(UIView.MatchOverScreen);
                
                ResetUIElements();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void ResetUIElements()
        {
            foreach (var cellButton in _cellButtons)
            {
                cellButton.SetInteractable(true);
                    
                if (cellButton.gameObject.transform.childCount > 1)
                {
                    var image = cellButton.gameObject.transform.GetChild(1).gameObject;
                    Destroy(image);
                }
            }
            winningLineImage.gameObject.SetActive(false);
            winningLineImage.fillAmount = 0f;
        }

        private async Task AnimateWinningLine(int[] winningLine)
        {
            // private readonly int[][] _winLines =
            // {
            //     new []{0,1,2}, new []{3,4,5}, new []{6,7,8}, // rows
            //     new []{0,3,6}, new []{1,4,7}, new []{2,5,8}, // cols
            //     new []{0,4,8}, new []{2,4,6}                 // diagonals
            // };
            var cellTransform =  _cellButtons[winningLine[0]].GetComponent<RectTransform>();
            
            if (winningLine[1] - winningLine[0] == 1)
            {
                //Rows
                var newRectTransform = cellTransform.localPosition - new Vector3(cellSize / 2f, 0f, 0f);
                winningLineImage.rectTransform.localPosition = newRectTransform;
                winningLineImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                winningLineImage.gameObject.SetActive(true);
            }
            else if (winningLine[1] - winningLine[0] == 3)
            {
                //Columns
                var newRectTransform = cellTransform.localPosition + new Vector3(0f, cellSize / 2f, 0f);
                winningLineImage.rectTransform.localPosition = newRectTransform;
                winningLineImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 270f);
                winningLineImage.gameObject.SetActive(true);
            }
            else
            {
                //Diagonals
                var newRectTransform = cellTransform.localPosition + new Vector3(0f, 0f, 0f);
                winningLineImage.rectTransform.localPosition = newRectTransform;
                if (winningLine[0] == 0)
                {
                    //Top left to bottom right
                    winningLineImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 315f);    
                }
                else
                {
                    //Top right to bottom left
                    winningLineImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 225f);
                }
                winningLineImage.gameObject.SetActive(true);
            }
            
            float currentFill = winningLineImage.fillAmount;
            while (currentFill < 1f)
            {
                currentFill += 0.05f;
                winningLineImage.fillAmount = currentFill;
                await Task.Delay(1);
            }
        }
    }
}

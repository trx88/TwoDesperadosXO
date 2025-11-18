using System;
using TMPro;
using UI.Views.Abstraction;
using UnityEngine;

namespace UI.Views.ViewComponents
{
    public class ViewComponentHUD : ViewComponentBase
    {
        [SerializeField] private TextMeshProUGUI textPlayerOneMoves;
        [SerializeField] private TextMeshProUGUI textPlayerTwoMoves;
        [SerializeField] private TextMeshProUGUI textMatchTime;

        protected override void Initialize()
        {
            base.Initialize();
            textPlayerOneMoves.text = "Player 1 moves: 0";
            textPlayerTwoMoves.text = "Player 2 moves: 0";
            textMatchTime.text = "Match time: 0.0 sec";
        }

        public void UpdateData(int playerOneMoves, int playerTwoMoves, double matchTime)
        {
            textPlayerOneMoves.text = $"Player 1 moves: {playerOneMoves}";
            textPlayerTwoMoves.text = $"Player 2 moves: {playerTwoMoves}";
            textMatchTime.text = $"Match time: {matchTime:F1} sec";
        }
    }
}

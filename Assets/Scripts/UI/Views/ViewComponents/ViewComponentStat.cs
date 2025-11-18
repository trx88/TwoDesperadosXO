using TMPro;
using UI.Views.Abstraction;
using UnityEngine;

namespace UI.Views.ViewComponents
{
    public class ViewComponentStat : ViewComponentBase
    {
        [SerializeField] private TextMeshProUGUI textStatName;
        [SerializeField] private TextMeshProUGUI textStatValue;
        [SerializeField] private string statNameOverride;

        private void Awake()
        {
            if (!string.IsNullOrEmpty(statNameOverride))
            {
                textStatName.text = statNameOverride;
            }
        }

        public void UpdateStatName(string statName)
        {
            textStatName.text = statName;
        }

        public void UpdateStatValue(string statValue)
        {
            textStatValue.text = statValue;
        }
    }
}

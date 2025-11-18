using System;
using System.Threading.Tasks;
using TMPro;
using UI.Views.Abstraction;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Views.ViewComponents
{
    public class ButtonViewComponent : ViewComponentBase
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;
        
        public Action ButtonClicked;

        private void Awake()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public override async Task Show()
        {
            await base.Show();
            
            button.onClick.AddListener(OnButtonClicked);
        }

        public override async Task Hide()
        {
            await base.Hide();
            
            button.onClick.RemoveListener(OnButtonClicked);
        }
        
        private void OnButtonClicked()
        {
            ButtonClicked?.Invoke();
        }

        public void AddCustomListener(UnityAction action)
        {
            button.onClick.RemoveListener(OnButtonClicked);
            button.onClick.AddListener(action);
        }

        public void UpdateButtonText(string newText)
        {
            if (text == null)
            {
                return;
            }
            text.text = newText;
        }
        
        public void SetInteractable(bool interactable)
        {
            button.interactable = interactable;
        }
    }
}
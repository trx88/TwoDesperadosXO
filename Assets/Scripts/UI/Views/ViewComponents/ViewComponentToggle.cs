using System;
using System.Threading.Tasks;
using UI.Views.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.ViewComponents
{
    public class ViewComponentToggle : ViewComponentBase
    {
        [SerializeField] private Toggle toggle;
        
        public Action<bool> ToggleValueChanged;

        public override async Task Show()
        {
            await base.Show();
            
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        public override async Task Hide()
        {
            await base.Hide();
            
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        public void SetToggleValue(bool value)
        {
            toggle.isOn = value;
        }
        
        private void OnToggleValueChanged(bool value)
        {
            ToggleValueChanged?.Invoke(value);
        }
    }
}

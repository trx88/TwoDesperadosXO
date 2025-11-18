using System.Threading.Tasks;
using UnityEngine;

namespace UI.Views.Abstraction
{
    public abstract class ViewBase : MonoBehaviour, IView, IUIState
    {
        public virtual async Task Show()
        {
            transform.gameObject.SetActive(true);
            await Task.CompletedTask;
        }

        public virtual async Task Hide()
        {
            transform.gameObject.SetActive(false);
            await Task.CompletedTask;
        }

        public virtual async Task EnterViewState()
        {
            await Show();
        }

        public virtual async Task ExitViewState()
        {
            await Hide();
        }
    }
}
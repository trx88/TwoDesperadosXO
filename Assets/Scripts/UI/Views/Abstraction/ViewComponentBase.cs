using System.Threading.Tasks;
using UnityEngine;

namespace UI.Views.Abstraction
{
    public class ViewComponentBase : MonoBehaviour, IViewComponent
    {
        [SerializeField] private bool visibleOnStart = true;
        public bool VisibleOnStart => visibleOnStart;

        public virtual async Task Show()
        {
            gameObject.SetActive(true);
            await Task.CompletedTask;
        }

        public virtual async Task Hide()
        {
            gameObject.SetActive(false);
            await Task.CompletedTask;
        }

        protected virtual void Initialize()
        {
            
        }

        private void OnEnable()
        {
            Initialize();
        }
    }
}
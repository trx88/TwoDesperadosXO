using System.Threading.Tasks;

namespace UI.Views.Abstraction
{
    public interface IViewComponent
    {
        Task Show();
        Task Hide();
    }
}
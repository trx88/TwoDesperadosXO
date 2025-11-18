using System.Threading.Tasks;

namespace UI.Views.Abstraction
{
    public interface IView
    {
        Task Show();
        Task Hide();
    }
}
namespace UI.ViewsModels.Abstraction
{
    public interface IViewModel
    {
        void UpdateData();

        void SubscribeToDataChanges();

        void UnsubscribeFromDataChanges();
    }
}
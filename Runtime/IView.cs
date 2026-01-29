namespace ModelViewMediatorController.Runtime
{
    /// <summary>
    /// Represents a view interface responsible for showing and hiding the view.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Displays the view, making it visible.
        /// </summary>
        public void Show();
        
        /// <summary>
        /// Hides the view, making it invisible.
        /// </summary>
        public void Hide();
    }
}
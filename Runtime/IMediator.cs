namespace ModelViewMediatorController.Runtime
{
    /// <summary>
    /// Represents a mediator interface responsible for managing the interaction between the model and view.
    /// </summary>
    /// <typeparam name="TModel">The type of the model. Must implement the IModel interface.</typeparam>
    /// <typeparam name="TSettings">The type of settings associated with the model. Must be a class.</typeparam>
    /// <typeparam name="TView">The type of the view. Must implement the IView interface.</typeparam>
    public interface IMediator<out TModel, TSettings, out TView>
        where TModel : IModel<TSettings>
        where TSettings : class
        where TView : IView
    {
        /// <summary>
        /// Gets the model associated with the mediator.
        /// </summary>
        public TModel Model { get; }
        
        /// <summary>
        /// Gets the view associated with the mediator.
        /// </summary>
        public TView View { get; }
        
        /// <summary>
        /// Initializes the mediator, setting up the necessary subscriptions.
        /// </summary>
        public void Initialize();
        
        /// <summary>
        /// Disposes of the mediator, removing subscriptions.
        /// </summary>
        public void Dispose();
        
        /// <summary>
        /// Sets the subscriptions for the mediator, either subscribing or unsubscribing.
        /// </summary>
        /// <param name="isSubscribed">Whether to subscribe or unsubscribe the mediator.</param>
        public void SetSubscriptions(bool isSubscribed);
    }
}
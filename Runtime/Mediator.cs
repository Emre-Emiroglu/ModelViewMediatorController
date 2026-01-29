namespace ModelViewMediatorController.Runtime
{
    /// <summary>
    /// Abstract base class for a mediator, implementing the IMediator interface.
    /// </summary>
    /// <typeparam name="TModel">The type of the model. Must implement the IModel interface.</typeparam>
    /// <typeparam name="TSettings">The type of settings associated with the model. Must be a class.</typeparam>
    /// <typeparam name="TView">The type of the view. Must implement the IView interface.</typeparam>
    public abstract class Mediator<TModel, TSettings, TView> : IMediator<TModel, TSettings, TView>
        where TModel : IModel<TSettings>
        where TSettings : class
        where TView : IView
    {
        #region Getters
        /// <summary>
        /// Gets the model associated with the mediator.
        /// </summary>
        public TModel Model { get; }
        
        /// <summary>
        /// Gets the view associated with the mediator.
        /// </summary>
        public TView View { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the mediator with the specified model and view.
        /// </summary>
        /// <param name="model">The model associated with the mediator.</param>
        /// <param name="view">The view associated with the mediator.</param>
        public Mediator(TModel model, TView view)
        {
            Model = model;
            View = view;
        }
        #endregion

        #region Core
        /// <summary>
        /// Initializes the mediator, setting up the necessary subscriptions.
        /// </summary>
        public virtual void Initialize() => SetSubscriptions(true);
        
        /// <summary>
        /// Disposes of the mediator, removing subscriptions.
        /// </summary>
        public virtual void Dispose() => SetSubscriptions(false);
        
        /// <summary>
        /// Sets the subscriptions for the mediator, either subscribing or unsubscribing.
        /// </summary>
        /// <param name="isSubscribed">Whether to subscribe or unsubscribe the mediator.</param>
        public abstract void SetSubscriptions(bool isSubscribed);
        #endregion
    }
}
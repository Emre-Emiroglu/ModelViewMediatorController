namespace ModelViewMediatorController.Runtime
{
    /// <summary>
    /// Abstract base class for a controller, implementing the IController interface.
    /// </summary>
    /// <typeparam name="TModel">The type of the model. Must implement the IModel interface.</typeparam>
    /// <typeparam name="TSettings">The type of settings associated with the model. Must be a class.</typeparam>
    /// <typeparam name="TView">The type of the view. Must implement the IView interface.</typeparam>
    public abstract class
        Controller<TModel, TSettings, TView> : IController<TModel, TSettings, TView>
        where TModel : IModel<TSettings>
        where TSettings : class
        where TView : IView
    {
        #region Getters
        /// <summary>
        /// Gets the model associated with the controller.
        /// </summary>
        public TModel Model { get; }
        
        /// <summary>
        /// Gets the view associated with the controller.
        /// </summary>
        public TView View { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the controller with the specified model, view, and mediator.
        /// </summary>
        /// <param name="model">The model associated with the controller.</param>
        /// <param name="view">The view associated with the controller.</param>
        public Controller(TModel model, TView view)
        {
            Model = model;
            View = view;
        }
        #endregion

        #region Executes
        /// <summary>
        /// Executes the controller's actions with the provided parameters. This method must be implemented in derived classes.
        /// The parameters can vary based on the controller's logic and needs.
        /// </summary>
        /// <param name="parameters">The parameters to be passed to the controller's action execution. These can be of any type.</param>
        public abstract void Execute(params object[] parameters);
        #endregion
    }
}
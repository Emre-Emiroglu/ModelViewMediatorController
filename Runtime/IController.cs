namespace ModelViewMediatorController.Runtime
{
    /// <summary>
    /// Represents a controller interface responsible for executing actions with the model and view.
    /// </summary>
    /// <typeparam name="TModel">The type of the model. Must implement the IModel interface.</typeparam>
    /// <typeparam name="TSettings">The type of settings associated with the model. Must be a class.</typeparam>
    /// <typeparam name="TView">The type of the view. Must implement the IView interface.</typeparam>
    public interface IController<out TModel, TSettings, out TView>
        where TModel : IModel<TSettings>
        where TSettings : class
        where TView : IView
    {
        /// <summary>
        /// Gets the model associated with the controller.
        /// </summary>
        public TModel Model { get; }
        
        /// <summary>
        /// Gets the view associated with the controller.
        /// </summary>
        public TView View { get; }
        
        /// <summary>
        /// Executes the controller's actions with the provided parameters.
        /// The parameters can vary based on the controller's logic and needs.
        /// </summary>
        /// <param name="parameters">The parameters to be passed to the controller's action execution. These can be of any type.</param>
        public void Execute(params object[] parameters);
    }
}
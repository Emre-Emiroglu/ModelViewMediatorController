namespace ModelViewMediatorController.Runtime
{
    /// <summary>
    /// Abstract base class for a model, implementing the IModel interface.
    /// </summary>
    /// <typeparam name="TSettings">The type of settings associated with the model. Must be a class.</typeparam>
    public abstract class Model<TSettings> : IModel<TSettings> where TSettings : class
    {
        #region Getters
        /// <summary>
        /// Gets the settings associated with the model.
        /// </summary>
        public TSettings Settings { get; }
        #endregion
        
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the model with the specified settings.
        /// </summary>
        /// <param name="settings">The settings associated with the model.</param>
        public Model(TSettings settings) => Settings = settings;
        #endregion
        
        #region Executes
        /// <summary>
        /// Loads the model's data. This method must be implemented in derived classes.
        /// </summary>
        public abstract void LoadData();
        
        /// <summary>
        /// Saves the model's data. This method must be implemented in derived classes.
        /// </summary>
        public abstract void SaveData();
        #endregion
    }
}
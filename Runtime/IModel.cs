namespace ModelViewMediatorController.Runtime
{
    /// <summary>
    /// Represents a model interface responsible for loading and saving data.
    /// </summary>
    /// <typeparam name="TSettings">The type of settings associated with the model. Must be a class.</typeparam>
    public interface IModel<out TSettings> where TSettings : class
    {
        /// <summary>
        /// Gets the settings associated with the model.
        /// </summary>
        public TSettings Settings { get; }
        
        /// <summary>
        /// Loads the model's data.
        /// </summary>
        public void LoadData();
        
        /// <summary>
        /// Saves the model's data.
        /// </summary>
        public void SaveData();
    }
}
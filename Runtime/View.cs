using UnityEngine;

namespace ModelViewMediatorController.Runtime
{
    /// <summary>
    /// Abstract base class for a view, implementing the IView interface.
    /// </summary>
    public abstract class View : MonoBehaviour, IView
    {
        #region Executes
        /// <summary>
        /// Displays the view, making it visible. This method can be implemented in derived classes.
        /// </summary>
        public virtual void Show() => gameObject.SetActive(true);
        
        /// <summary>
        /// Hides the view, making it invisible. This method can be implemented in derived classes.
        /// </summary>
        public virtual void Hide() => gameObject.SetActive(false);
        #endregion
    }
}
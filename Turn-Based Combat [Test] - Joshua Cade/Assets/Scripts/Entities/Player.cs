using UnityEngine;

namespace TurnBasedGame.Entities
{
    /// <summary>
    /// Primary Management & Access of the Player Character.
    /// </summary>
    internal class Player : MonoBehaviour
    {
        #region Singleton
        public static Player management;

        #region Unity Methods
        private void Awake()
        {
            if (management != null)
            {
                Debug.Log("More than one {0} instance found!", management);
                return;
            }

            management = this;
        }
        #endregion
        #endregion

        #region Variables
        ///empty for now...
        #endregion

        #region Unity Methods
        ///empty for now...
        #endregion

        #region Methods
        ///empty for now...
        #endregion
    }
}

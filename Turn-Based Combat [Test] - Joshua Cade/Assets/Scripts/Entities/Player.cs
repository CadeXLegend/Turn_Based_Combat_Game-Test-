using UnityEngine;

namespace TurnBasedGame.Entities
{
    /// <summary>
    /// Primary Management & Access of the Player Character.
    /// </summary>
    internal class Player : MonoBehaviour
    {
        #region Singleton
        public static Player access;

        #region Unity Methods
        private void Awake()
        {
            if (access != null)
            {
                Debug.Log("More than one {0} instance found!", access);
                return;
            }

            access = this;
        }
        #endregion
        #endregion

        #region Variables
        #endregion

        #region Unity Methods
        ///empty for now...
        #endregion

        #region Methods
        ///empty for now...
        #endregion
    }
}

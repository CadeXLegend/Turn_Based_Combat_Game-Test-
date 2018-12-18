using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGame.UI
{
    /// <summary>
    /// Populates the Entity Container UI with the Entity.
    /// </summary>
    internal class PopulateEntityContainersUI : MonoBehaviour
    {
        #region Singleton
        public static PopulateEntityContainersUI populate;

        #region Unity Methods
        private void Awake()
        {
            if (populate != null)
            {
                Debug.Log("More than one {0} instance found!", populate);
                return;
            }

            populate = this;
        }
        #endregion
        #endregion

        #region Variables
        [SerializeField]
        private GameObject entityContainer;

        [SerializeField]
        private GameObject entityObject;
        #endregion

        #region Methods
        private GameObject go;
        /// <summary>
        /// Parses the Entity Data Object into the Entity Container UI.
        /// </summary>
        /// <param name="entity"></param>
        public void ParseComponentsData(Entities.Entity entity)
        {
            go = Instantiate(entityObject, entityContainer.transform);
            go.name = entity.EntityName;
        }
        #endregion
    }
}

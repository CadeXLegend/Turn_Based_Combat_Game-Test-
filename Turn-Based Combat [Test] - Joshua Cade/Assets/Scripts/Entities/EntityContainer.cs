using System;
using UnityEngine;

namespace TurnBasedGame.Entities
{
    /// <summary>
    /// The Standard Container for the Entities ScriptableObject.
    /// </summary>
    public class EntityContainer : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Entity entity;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Debug.Assert(entity, "<color=red><b>Warning: </b></color> No Entity has been Assigned.");
        }
        #endregion

        # region Methods
        /// <summary>
        /// Returns the Entity bound to this Container.
        /// </summary>
        /// <returns></returns>
        public Entity RetreiveEntity()
        {
            if(entity != null)
                return entity;
            else
            {
                Debug.Log("<color=red><b>Warning: </b></color>No Entity has been bound to this Container.");
                return ScriptableObject.CreateInstance<Entity>();
            }
        }
        #endregion
    }
}

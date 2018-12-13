using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGame.Systems
{
    internal class PlayerAbilities : MonoBehaviour
    {
        #region Singleton
        public static PlayerAbilities abilities;

        private void Awake()
        {
            if (abilities != null)
            {
                Debug.Log("More than one {0} instance found!", abilities);
                return;
            }

            abilities = this;
        }
        #endregion

        #region Variables
        [SerializeField]
        internal List<Ability> playerAbilities = new List<Ability>();
        #endregion
    }
}

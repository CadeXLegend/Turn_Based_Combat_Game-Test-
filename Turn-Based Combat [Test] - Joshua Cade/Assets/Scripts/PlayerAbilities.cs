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
        private List<Ability> abilitiesDatabase = new List<Ability>();

        [SerializeField]
        internal List<Ability> playerAbilities = new List<Ability>();
        #endregion

        #region Methods
        /// <summary>
        /// Returns the Abilities Database Object.
        /// </summary>
        /// <returns></returns>
        public List<Ability> AbilityDatabase()
        {
            if (abilitiesDatabase.Count <= 0)
            {
                Debug.Log("<color=red><b>Warning: </b></color>Ability Database is not populated.");
                return new List<Ability>();
            }
            else
                return abilitiesDatabase;
        }
        #endregion
    }
}

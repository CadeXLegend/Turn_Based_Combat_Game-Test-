using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGame.UI
{
    /// <summary>
    /// Populates the Queued Actions UI Element with Actions in the Queue.
    /// </summary>
    internal class PopulateQueuedActionsUI : MonoBehaviour
    {
        #region Singleton
        public static PopulateQueuedActionsUI management;

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
        public delegate void PopulatedUI();
        public static event PopulatedUI OnceUIPopulated;

        [SerializeField]
        private GameObject queuedAbilitiesSortingGroup;

        [SerializeField]
        private GameObject actionGraphic;

        //container for the instantiated UI elements.
        private GameObject go;
        #endregion

        #region Methods
        /// <summary>
        /// Adds Abilities as a Queued Actions UI Element.
        /// </summary>
        public void GenerateAbilitiesInUI(Ability ab)
        {
            try
            {
                if (!Systems.ActionManagement.management.CheckIfFull())
                {
                    go = Instantiate(actionGraphic, queuedAbilitiesSortingGroup.transform);
                    go.name = ab.AbilityName + " (" + ab.Target.name + ")";
                    go.GetComponent<Image>().sprite = ab.Icon;
                }
                else
                {
                    Debug.Log("<b>Actions Queue is Full.</b>");
                }
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }

            //OnceUIPopulated();
        }
        #endregion
    }
}

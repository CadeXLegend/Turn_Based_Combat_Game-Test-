using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TurnBasedGame.UI
{
    /// <summary>
    /// This Handles the Main UI Interactions.
    /// </summary>
    public class UIInteractionManager : MonoBehaviour
    {
        #region Singleton
        public static UIInteractionManager management;

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
        [SerializeField]
        private AudioSource primaryAudioSource;
        public AudioSource externalAudioSource { get; set; }

        public GameObject actionsMenuButtonsParent;
        private Ability currentAbility;
        #endregion

        #region Methods
        /// <summary>
        /// Parses the Current GameObject parent of the Button.
        /// </summary>
        /// <returns></returns>
        public GameObject GetCurrentlySelectedGameObject()
        {
            if (EventSystem.current)
            {
                return EventSystem.current.currentSelectedGameObject;
            }
            else
            {
                Debug.Log("<color=red><b>Warning: </b></color> <b>No Current EventSytem Object Actively Selected.</b>");
                return null;
            }

        }

        /// <summary>
        /// Parses the Current Ability attached to the Button's Action.
        /// </summary>
        /// <returns></returns>
        public Ability GetCurrentlySelectedAbility()
        {
            return currentAbility;
        }

        /// <summary>
        /// Parses the Current Ability attached to the Button's Action.
        /// </summary>
        /// <returns></returns>
        public void SetCurrentlySelectedAbility(Ability ability)
        {
            currentAbility = ability;
        }
        
        /// <summary>
        /// Disables the Interactivity of the Buttons contained as children of obj.
        /// </summary>
        /// <param name="parent"></param>
        public void DisableButtons(GameObject obj)
        {
            try
            {
                for(int i = 0; i < obj.transform.childCount; i++)
                {
                    obj.transform.GetChild(i).GetComponent<Button>().interactable = false;
                }
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }
        }

        /// <summary>
        /// Enables the Interactivity of the Buttons contained as children of obj.
        /// </summary>
        /// <param name="parent"></param>
        public void EnableButtons(GameObject obj)
        {
            try
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    obj.transform.GetChild(i).GetComponent<Button>().interactable = true;
                }
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }
        }

        /// <summary>
        /// Parses an AudioClip to the Primary Audio Source to be played once.  Returns whether it succeeded.
        /// </summary>
        /// <param name="audio"></param>
        public bool PlayAudioOnce(AudioClip audio, bool internalSource)
        {
            try
            {
                if (internalSource)
                    primaryAudioSource.PlayOneShot(audio);
                else
                    externalAudioSource.PlayOneShot(audio);

                return true;
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
                return false;
            }
        }

        /// <summary>
        /// Navigate to the Actions menu.
        /// </summary>
        public void ActionsMenu()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }

            Debug.Log("<color=blue><b>Notification: </b></color><b>The Implementation for this Interaction is yet to be implemented.</b>");
            //insert code here
        }

        /// <summary>
        /// Navigate to the Items menu.
        /// </summary>
        public void ItemsMenu()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }

            Debug.Log("<color=blue><b>Notification: </b></color><b>The Implementation for this Interaction is yet to be implemented.</b>");
            //insert code here
        }

        /// <summary>
        /// Pass the current turn.
        /// </summary>
        public void PassTurn()
        {
            try
            {
                TurnManager.management.SetTurn(CombatTurns.Enemy);
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }
        }

        /// <summary>
        /// Flee an active encounter.
        /// </summary>
        public void FleeEncounter()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }

            Debug.Log("<color=blue><b>Notification: </b></color><b>The Implementation for this Interaction is yet to be implemented.</b>");
            //insert code here
        }
        #endregion

    }
}

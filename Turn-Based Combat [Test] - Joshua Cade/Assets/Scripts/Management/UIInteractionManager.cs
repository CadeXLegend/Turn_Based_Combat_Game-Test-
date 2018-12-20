using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField]
        private GameObject endTurnButton;
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
                endTurnButton.GetComponent<Button>().interactable = false;
                StartCoroutine(TESTResetTurn(3f));
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

        GameObject medusa;
        List<GameObject> thralls = new List<GameObject>();
        private IEnumerator TESTResetTurn(float resetTime)
        {
            yield return new WaitForSeconds(resetTime);
            Entities.EntityContainer playerEntityContainer = Entities.Player.access.GetComponent<Entities.EntityContainer>();
            if (playerEntityContainer.statusState == Entities.StatusState.Stunned)
                playerEntityContainer.statusState = Entities.StatusState.None;

            #region Testing Enemy Ability Functionality
            //find and assign the enemies
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (go.name == "Medusa")
                {
                    medusa = go.GetComponent<Entities.SpawnedContainer>().entityParent;
                }
                else if(go.name == "Forsaken Thrall")
                {
                    thralls.Add(go.GetComponent<Entities.SpawnedContainer>().entityParent);
                }
            }

            //queue up damage every turn
            Ability medusaABTest2Damage = Systems.PlayerAbilities.abilities.AbilityDatabase()[4];
            Systems.ActionManagement.management.QueueAction(medusaABTest2Damage,
                    GameObject.FindWithTag("Ally"),
                    medusa);

            foreach(GameObject thrall in thralls)
            {
                yield return new WaitForSeconds(1f);
                Systems.ActionManagement.management.QueueAction(medusaABTest2Damage,
                        GameObject.FindWithTag("Ally"),
                        thrall);
            }
            yield return new WaitForSeconds(1f);

            //check turn counter
            if (TurnManager.management.TurnCount == 2)
            {
                //grab medusa's ability
                Ability medusaABTestStun = Systems.PlayerAbilities.abilities.AbilityDatabase()[3];
                //queue it up to be used on the player
                Systems.ActionManagement.management.QueueAction(medusaABTestStun, 
                    GameObject.FindWithTag("Ally"),
                    medusa);
                yield return new WaitForSeconds(1f);
            }
            #endregion

            endTurnButton.GetComponent<Button>().interactable = true;
            playerEntityContainer.ChangeStat("Mana", playerEntityContainer.RetreiveEntity().MaxMana, true);
            TurnManager.management.SetTurn(CombatTurns.Player);
        }
    }
}

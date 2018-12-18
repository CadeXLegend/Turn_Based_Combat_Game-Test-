using UnityEngine;
using UnityEngine.EventSystems;
using TurnBasedGame.Systems;
using System;

namespace TurnBasedGame.UI
{
    /// <summary>
    /// Populates the Actions UI Field with Abilities.
    /// </summary>
    internal class PopulateAbilitiesUI : MonoBehaviour
    {
        #region Variables
        public delegate void PopulatedUI();
        public static event PopulatedUI OnceUIPopulated;

        [SerializeField]
        private GameObject abilitySlotsSortingGroup;

        [SerializeField]
        private GameObject abilitySlot;

        //container for the instantiated UI elements.
        private GameObject go;
        #endregion

        #region Unity Methods
        private void Start()
        {
            GenerateAbilitiesInUI();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generate the Player's Abilities in the UI Field "Actions".
        /// </summary>
        private void GenerateAbilitiesInUI()
        {
            try
            {
                foreach (Ability ab in PlayerAbilities.abilities.playerAbilities)
                {
                    try
                    {
                        go = Instantiate(abilitySlot, abilitySlotsSortingGroup.transform);
                        go.name = abilitySlot.name + string.Format(" ({0})", ab.AbilityName);
                        go.GetComponent<AbilitySlot>().ParseComponentsData(ab);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("<color=red><b>Error: </b></color>" + e);
                    }

                    try
                    {
                        go.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(
                        delegate
                        {
                            try
                            {
                                ActionManagement.management.QueueAction(UIInteractionManager.management.GetCurrentlySelectedAbility());
                            }
                            catch (Exception e)
                            {
                                Debug.Log("<color=red><b>Error: </b></color>" + e);
                            }
                        });
                    }
                    catch (Exception e)
                    {
                        Debug.Log("<color=red><b>Error: </b></color>" + e);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }

            OnceUIPopulated();
        }
        #endregion
    }
}

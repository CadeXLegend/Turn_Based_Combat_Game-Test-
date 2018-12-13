using UnityEngine;
using UnityEngine.EventSystems;
using TurnBasedGame.Systems;

namespace TurnBasedGame.UI
{
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
            foreach (Ability ab in PlayerAbilities.abilities.playerAbilities)
            {
                go = Instantiate(abilitySlot, abilitySlotsSortingGroup.transform);
                go.name = abilitySlot.name + string.Format(" ({0})", ab.AbilityName);
                go.GetComponent<AbilitySlot>().ParseComponentsData(ab);

                go.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(
                    delegate
                    {
                        ActionManagement.management.QueueAction(UIInteractionManager.management.GetCurrentlySelectedAbility());
                    });
            }

            OnceUIPopulated();
        }
        #endregion
    }
}

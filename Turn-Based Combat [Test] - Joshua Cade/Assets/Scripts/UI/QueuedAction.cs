using UnityEngine.EventSystems;

namespace TurnBasedGame.UI
{
    /// <summary>
    /// Container for the Queued Actions.
    /// </summary>
    internal class QueuedAction : EventTrigger
    {
        #region Variables
        private Ability queuedAction { get; set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            //binding the Ability to this Object Reference when the Ability is selected from the Actions Pane.
            queuedAction = UIInteractionManager.management.GetCurrentlySelectedAbility();
        }

        public override void OnPointerEnter(PointerEventData data)
        {
            PopulateAbilityDescriptionUI.modify.DescriptionBoxManager.SetActive(true);
            PopulateAbilityDescriptionUI.modify.ParseComponentsData(queuedAction);
        }

        public override void OnPointerExit(PointerEventData data)
        {
            PopulateAbilityDescriptionUI.modify.DescriptionBoxManager.SetActive(false);
        }
#endregion
    }
}

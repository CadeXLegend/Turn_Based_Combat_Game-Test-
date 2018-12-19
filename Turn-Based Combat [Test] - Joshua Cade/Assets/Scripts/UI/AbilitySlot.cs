using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TurnBasedGame.UI
{
    /// <summary>
    /// The UI Container for the Ability Class.  This Class Binds an instance of Ability to it, and displays it's information to the IU Elements.
    /// </summary>
    public class AbilitySlot : EventTrigger, Interfaces.IParseData
    {
        #region Variables
        private Ability ability;
        private Image icon;
        private Text nameText;
        private Text costText;
        private Text abilityType;
        #endregion

        #region Constructor
        public void Construct(Image icon, Text nameText, Text costText)
        {
            this.icon = icon;
            this.nameText = nameText;
            this.costText = costText;
        }
        #endregion

        #region  Unity Methods
        private void Awake()
        {
            //This is specifically retreiving the components in this order per heirarchial setup of the "Slot" Prefab.
            //If you change how the Heirarchy is configured, this code will not work anymore!!
            //Modify at your own risk.
            icon = transform.GetChild(1).GetComponent<Image>();
            nameText = transform.GetChild(0).GetComponent<Text>();
            costText = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        }

        public override void OnPointerEnter(PointerEventData data)
        {
            PopulateAbilityDescriptionUI.modify.DescriptionBoxManager.SetActive(true);
            PopulateAbilityDescriptionUI.modify.ParseComponentsData(ability);
        }

        public override void OnPointerExit(PointerEventData data)
        {
            PopulateAbilityDescriptionUI.modify.DescriptionBoxManager.SetActive(false);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            UIInteractionManager.management.SetCurrentlySelectedAbility(ability);
            UIInteractionManager.management.DisableButtons(UIInteractionManager.management.actionsMenuButtonsParent);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            try
            {
                if (ability.AbilityType == AbilityType.Damage || ability.AbilityType == AbilityType.Debuff || ability.AbilityType == AbilityType.Hybrid)
                {
                    if (eventData.pointerCurrentRaycast.gameObject.tag == "Enemy")
                    {
                        ability.Target = eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
                        Debug.Log("<color=blue><b>Target: </b></color>" + ability.Target.name);
                    }
                }
                if(ability.AbilityType == AbilityType.Buff || ability.AbilityType == AbilityType.Heal)
                {
                    if (eventData.pointerCurrentRaycast.gameObject.tag == "Ally")
                    {
                        ability.Target = eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
                        Debug.Log("<color=blue><b>Target: </b></color>" + ability.Target.name);
                    }
                }

                if (ability.Target != null)
                {
                    //queue up the ability and bind it to QueuedAction,
                    //then clear it from the ability's target so it won't
                    //use the old Target and assign invalid Actions.
                    Systems.ActionManagement.management.QueueAction(ability);
                    ability.Target = null;
                }
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }
            UIInteractionManager.management.EnableButtons(UIInteractionManager.management.actionsMenuButtonsParent);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Retreives an instance of Ability and assigns it's components to this AbilitySlot.
        /// </summary>
        /// <param name="ability"></param>
        public void ParseComponentsData(Ability ability)
        {
            try
            {
                this.ability = ability;
                icon.sprite = ability.Icon;
                nameText.text = ability.AbilityName;
                costText.text = "Mana: " + ability.Cost;
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }
        }

        /// <summary>
        /// Sends the assigned Ability by Request.
        /// </summary>
        /// <returns></returns>
        public Ability FetchAssignedAbility()
        {
            if (ability != null)
                return ability;
            else
            {
                Debug.Log("<color=red><b>Warning:</b></color> Ability was not assigned, sending an Empty Ability instead.");
                Ability ab = ScriptableObject.CreateInstance<Ability>();
                ab.Construct();
                return ab;
            }
        }
        #endregion
    }
}

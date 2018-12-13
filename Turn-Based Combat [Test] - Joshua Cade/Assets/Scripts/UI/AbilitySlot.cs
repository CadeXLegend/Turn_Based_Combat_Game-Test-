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
            this.icon = transform.GetChild(1).GetComponent<Image>();
            this.nameText = transform.GetChild(0).GetComponent<Text>();
            this.costText = transform.GetChild(2).GetChild(0).GetComponent<Text>();
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

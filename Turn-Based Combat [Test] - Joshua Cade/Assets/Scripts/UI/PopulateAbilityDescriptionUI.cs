using System;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGame.UI
{
    /// <summary>
    /// Populates the Descriptions UI Field with Ability Information.
    /// </summary>
    internal class PopulateAbilityDescriptionUI : MonoBehaviour, Interfaces.IParseData
    {
        #region Singleton
        public static PopulateAbilityDescriptionUI modify;

        #region Unity Methods
        private void Awake()
        {
            if (modify != null)
            {
                Debug.Log("More than one {0} instance found!", modify);
                return;
            }

            modify = this;

            //This is specifically retreiving the components in this order per heirarchial setup of the "Description Zone" Prefab.
            //If you change how the Heirarchy is configured, this code will not work anymore!!
            //Modify at your own risk.
            this.icon = descriptionBoxManager.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            this.abilityNameText = descriptionBoxManager.transform.GetChild(0).GetChild(1).GetComponent<Text>();
            this.costAndTypeText = descriptionBoxManager.transform.GetChild(0).GetChild(2).GetComponent<Text>();
            this.descriptionText = descriptionBoxManager.transform.GetChild(1).GetComponent<Text>();

            descriptionBoxManager.SetActive(false);
        }
        #endregion
        #endregion

        #region Variables
        [SerializeField]
        private GameObject descriptionBoxManager;
        public GameObject DescriptionBoxManager
        {
            get
            {
                return descriptionBoxManager;
            }
        }

        private Image icon;
        private Text abilityNameText;
        private Text costAndTypeText;
        private Text abilityTypeText;
        private Text descriptionText;
        #endregion

        #region Methods
        /// <summary>
        /// Parses the Data from the Ability to the Description UI Field.
        /// </summary>
        /// <param name="ability"></param>
        public void ParseComponentsData(Ability ability)
        {
            try
            {
            icon.sprite = ability.Icon;
            abilityNameText.text = ability.AbilityName;
            costAndTypeText.text = string.Format("Mana: {0}\n{1}", ability.Cost, ability.AbilityType.ToString());
            descriptionText.text = ability.Description;
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }
        }
        #endregion
    }
}

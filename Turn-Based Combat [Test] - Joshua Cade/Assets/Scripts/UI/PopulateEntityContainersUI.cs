using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGame.UI
{
    /// <summary>
    /// Populates the Entity Container UI with the Entity.
    /// </summary>
    internal class PopulateEntityContainersUI : MonoBehaviour
    {
        #region Singleton
        public static PopulateEntityContainersUI populate;

        #region Unity Methods
        private void Awake()
        {
            if (populate != null)
            {
                Debug.Log("More than one {0} instance found!", populate);
                return;
            }

            populate = this;
        }
        #endregion
        #endregion

        #region Variables
        [SerializeField]
        private GameObject enemyEntityParent;
        [SerializeField]
        private GameObject allyEntityParent;

        [SerializeField]
        private GameObject entityContainer;
        #endregion

        #region Unity Methods
        private void Start()
        {
            //fetches every container in the GameObject Scene.
            foreach (Entities.EntityContainer containers in (Entities.EntityContainer[])FindObjectsOfType(typeof(Entities.EntityContainer)))
            {
                GenerateUIElements(containers.RetreiveEntity());
            }

        }
        #endregion

        #region Methods
        private GameObject go;
        /// <summary>
        /// Generate the Entity Containers in the Entity Parent(s).
        /// </summary>
        /// <param name="entity"></param>
        public void GenerateUIElements(Entities.Entity entity)
        {
            switch(entity.EntityType)
            {
                case Entities.EntityType.Ally:
                    go = Instantiate(entityContainer, allyEntityParent.transform);
                    //transform for the icon
                    Transform allyPortraitPos = go.transform.GetChild(2).transform;
                    //increasing the scale just to emphasize the player (temporary)
                    go.transform.localScale += new Vector3(0.2f, 0.2f);
                    //moving the icon to be appropriate for the UI (maybe temporary)
                    allyPortraitPos.localPosition = new Vector3(
                        (-151), 
                        allyPortraitPos.localPosition.y, 
                        allyPortraitPos.localPosition.z);
                    PopulateEntityInformation(entity, go);
                    break;

                case Entities.EntityType.Enemy:
                    go = Instantiate(entityContainer, enemyEntityParent.transform);
                    PopulateEntityInformation(entity, go);
                    break;
            }

        }

        /// <summary>
        /// Populates the Entity Container with the Data parsed from Entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="go"></param>
        private void PopulateEntityInformation(Entities.Entity entity, GameObject go)
        {
            go.name = entity.EntityName;

            #region Icon
            //This is dependant on the heirarchy of the
            //Entity Container Object.
            //Any changes to the heirarchy will break this code.
            //Please be aware of this fact before changing the saved Prefab.
            go.transform.GetChild(2).GetComponent<Image>().sprite = entity.Icon;
            #endregion

            #region Health Bar
            //This is dependant on the heirarchy of the
            //Entity Container Object.
            //Any changes to the heirarchy will break this code.
            //Please be aware of this fact before changing the saved Prefab.
            Slider healthSlider = go.transform.GetChild(0).GetComponent<Slider>();
            healthSlider.maxValue = entity.MaxHealth;
            healthSlider.value = entity.Health;
            go.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = healthSlider.value + "/" + healthSlider.maxValue;
            #endregion

            #region Mana Bar
            //This is dependant on the heirarchy of the
            //Entity Container Object.
            //Any changes to the heirarchy will break this code.
            //Please be aware of this fact before changing the saved Prefab.
            Slider manaSlider = go.transform.GetChild(1).GetComponent<Slider>();
            manaSlider.maxValue = entity.MaxMana;
            manaSlider.value = entity.Mana;
            go.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = manaSlider.value + "/" + manaSlider.maxValue;
            #endregion
        }
        #endregion
    }
}

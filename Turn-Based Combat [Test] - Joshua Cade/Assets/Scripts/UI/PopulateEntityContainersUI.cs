using System;
using System.Collections.Generic;
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

        public Dictionary<GameObject, Entities.EntityContainer> entitiesSpawned = new Dictionary<GameObject, Entities.EntityContainer>();
        #endregion

        #region Unity Methods
        private void Start()
        {
            //fetches every container in the GameObject Scene.
            foreach (Entities.EntityContainer containers in (Entities.EntityContainer[])FindObjectsOfType(typeof(Entities.EntityContainer)))
            {
                try
                {
                    entitiesSpawned.Add(GenerateUIElements(containers, containers.RetreiveEntity()), containers);
                }
                catch (Exception e)
                {
                    Debug.Log("<color=red><b>Error: </b></color>" + e);
                }
            }

        }
        #endregion

        #region Methods
        private GameObject go;
        /// <summary>
        /// Generate the Entity Containers in the Entity Parent(s).
        /// </summary>
        /// <param name="entity"></param>
        public GameObject GenerateUIElements(Entities.EntityContainer container, Entities.Entity entity)
        {
            switch(entity.EntityType)
            {
                case Entities.EntityType.Ally:
                    try
                    {
                        go = Instantiate(entityContainer, allyEntityParent.transform);
                        go.transform.GetChild(2).tag = "Ally";
                        //transform for the icon
                        Transform allyPortraitPos = go.transform.GetChild(2).transform;
                        //increasing the scale just to emphasize the player (temporary)
                        go.transform.localScale += new Vector3(0.2f, 0.2f);
                        //moving the icon to be appropriate for the UI (maybe temporary)
                        allyPortraitPos.localPosition = new Vector3(
                            (-151),
                            allyPortraitPos.localPosition.y,
                            allyPortraitPos.localPosition.z);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("<color=red><b>Error: </b></color>" + e);
                        return null;
                    }

                    try
                    {
                        PopulateEntityInformation(container, entity, go);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("<color=red><b>Error: </b></color>" + e);
                        return null;
                    }
                    return go;

                case Entities.EntityType.Enemy:
                    try
                    {
                        go = Instantiate(entityContainer, enemyEntityParent.transform);
                        go.transform.GetChild(2).tag = "Enemy";
                    }
                    catch (Exception e)
                    {
                        Debug.Log("<color=red><b>Error: </b></color>" + e);
                        return null;
                    }

                    try
                    {
                        PopulateEntityInformation(container, entity, go);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("<color=red><b>Error: </b></color>" + e);
                        return null;
                    }
                    return go;
                default:
                    return null;
            }

        }

        /// <summary>
        /// Populates the Entity Container with the Data parsed from Entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="go"></param>
        private void PopulateEntityInformation(Entities.EntityContainer container, Entities.Entity entity, GameObject go)
        {
            go.name = entity.EntityName;

            #region Icon
            //This is dependant on the heirarchy of the
            //Entity Container Object.
            //Any changes to the heirarchy will break this code.
            //Please be aware of this fact before changing the saved Prefab.
            try
            {
                go.transform.GetChild(2).GetComponent<Image>().sprite = entity.Icon;
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }
            #endregion

            #region Health Bar
            //This is dependant on the heirarchy of the
            //Entity Container Object.
            //Any changes to the heirarchy will break this code.
            //Please be aware of this fact before changing the saved Prefab.
            try
            {

                Slider healthSlider = go.transform.GetChild(0).GetComponent<Slider>();
                healthSlider.maxValue = entity.MaxHealth;
                healthSlider.value = entity.Health;
                Text healthText = go.transform.GetChild(0).GetChild(2).GetComponent<Text>();
                healthText.text = healthSlider.value + "/" + healthSlider.maxValue;
                container.HealthSlider = healthSlider;
                container.HealthText = healthText;
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }
            #endregion

            #region Mana Bar
            //This is dependant on the heirarchy of the
            //Entity Container Object.
            //Any changes to the heirarchy will break this code.
            //Please be aware of this fact before changing the saved Prefab.
            try
            {
                Slider manaSlider = go.transform.GetChild(1).GetComponent<Slider>();
                manaSlider.maxValue = entity.MaxMana;
                manaSlider.value = entity.Mana;
                Text manaText = go.transform.GetChild(1).GetChild(2).GetComponent<Text>();
                manaText.text = manaSlider.value + "/" + manaSlider.maxValue;
                container.ManaSlider = manaSlider;
                container.ManaText = manaText;
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }
            #endregion
        }
        #endregion
    }
}

﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGame.Entities
{
    /// <summary>
    /// The Standard Container for the Entities ScriptableObject.
    /// </summary>
    public class EntityContainer : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Entity entity;

        public GameObject Target { get; set; }
        private int health;
        public int Health
        {
            get
            {
                if (health >= MaxHealth)
                    health = MaxHealth;
                return health;
            }
            set
            {
                health = value;
            }
        }
        private int MaxHealth { get; set; }
        private int mana;
        public int Mana
        {
            get
            {
                if (mana >= MaxMana)
                    mana = MaxMana;
                return mana;
            }
            set
            {
                mana = value;
            }
        }
        private int MaxMana { get; set; }

        public Text HealthText { get; set; }
        public Text ManaText { get; set; }

        public Slider HealthSlider { get; set; }
        public Slider ManaSlider { get; set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Debug.Assert(entity, "<color=red><b>Warning: </b></color> No Entity has been Assigned.");
        }

        private void Start()
        {
            Health = entity.Health;
            MaxHealth = entity.MaxHealth;
            Mana = entity.Mana;
            MaxMana = entity.MaxMana;
        }
        #endregion

        # region Methods
        /// <summary>
        /// Returns the Entity bound to this Container.
        /// </summary>
        /// <returns></returns>
        public Entity RetreiveEntity()
        {
            if(entity != null)
                return entity;
            else
            {
                Debug.Log("<color=red><b>Warning: </b></color>No Entity has been bound to this Container.");
                return ScriptableObject.CreateInstance<Entity>();
            }
        }

        /// <summary>
        /// Modify a Stat by the Given Amount.
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="amount"></param>
        /// <param name="add"></param>
        public void ChangeStat(string stat, int amount, bool add)
        {
            switch (stat)
            {
                case "Health":
                    Health = !add ? Health - amount : Health + amount;
                    HealthSlider.value = Health;
                    HealthText.text = Health  + "/" + entity.MaxHealth;
                    break;
                case "Mana":
                    Mana = !add ? Mana - amount : Mana + amount;
                    ManaSlider.value = Mana;
                    ManaText.text = Mana + "/" + entity.MaxMana;
                    break;
                default:
                    Debug.Log("<color=red><b>Warning: </b></color>That stat does not exist or is invalid.");
                    break;
            }
        }
        #endregion
    }
}

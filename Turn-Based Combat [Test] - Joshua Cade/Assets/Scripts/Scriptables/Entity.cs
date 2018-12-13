using UnityEngine;

namespace TurnBasedGame.Entities
{
    /// <summary>
    /// The Base Data Asset used to Create Entities.
    /// </summary>
    [CreateAssetMenu(fileName = "New Entity", menuName = "Entities/New Entity")]
    public class Entity : ScriptableObject, Interfaces.IConstruction
    {
        #region Variables
        [Header("Primary Attributes")]

        [SerializeField]
        [Tooltip("The desired Name of the Entity.")]
        private string entityName;
        public string EntityName
        {
            get
            {
                return entityName;
            }
        }

        [Space(10)]
        [SerializeField]
        [TextArea()]
        [Tooltip("The desired Description of the Entity.")]
        private string entityDescription;
        public string EntityDescription
        {
            get
            {
                return entityDescription;
            }
        }

        [Space(10)]
        [SerializeField]
        [Tooltip("")]
        private int health;
        public int Health
        {
            get
            {
                //limiting HP to not bypass maximum capacity.
                if (health >= maxHealth)
                    health = maxHealth;
                return health;
            }
            set
            {
                health = value;
            }
        }

        [Space(5)]
        [SerializeField]
        [Tooltip("")]
        private int maxHealth;
        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }
        }

        [Space(10)]
        [SerializeField]
        [Tooltip("")]
        private int mana;
        public int Mana
        {
            get
            {
                //limiting MP to not bypass maximum capacity.
                if (mana >= maxMana)
                    mana = maxMana;
                return mana;
            }
            set
            {
                mana = value;
            }
        }

        [Space(5)]
        [SerializeField]
        [Tooltip("")]
        private int maxMana;
        public int MaxMana
        {
            get
            {
                return maxMana;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// The default Constructor that will auto-generate a Default Template Entity.
        /// </summary>
        public void Construct()
        {
            entityName = "New Entity";
            entityDescription = "A new Entity.";
            health = 100;
            maxHealth = 100;
            mana = 20;
            maxMana = 20;
        }

        /// <summary>
        /// The custom Constructor that will allow you to Generate your own Entity with the Parameters provided.
        /// </summary>
        /// <param name="EntityName"></param>
        /// <param name="EntityDescription"></param>
        /// <param name="Health"></param>
        /// <param name="MaxHealth"></param>
        /// <param name="Mana"></param>
        /// <param name="MaxMana"></param>
        public void Construct(string EntityName, string EntityDescription, int Health, int MaxHealth, int Mana, int MaxMana)
        {
            entityName = EntityName;
            entityDescription = EntityDescription;
            health = Health;
            maxHealth = MaxHealth;
            mana = Mana;
            maxMana = MaxMana;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Increases the desired Stat Maximum Cap by the Integer Provided.
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="increase"></param>
        public void IncreaseStatMax(string stat, int increase)
        {
            switch (stat)
            {
                case "Health":
                    maxHealth += increase;
                    break;
                case "Mana":
                    maxMana += increase;
                    break;
                default:
                    Debug.Log("<color=red><b>Warning: </b></color> The Stat you have requested does not exist.");
                    break;
            }

        }
        #endregion
    }
}

using UnityEngine;

namespace TurnBasedGame
{

    public enum AbilityType
    {
        Heal, Damage, Buff, Debuff, Hybrid, Summon
    }

    /// <summary>
    /// The Base Data Asset used to Create Abilities.
    /// </summary>
    [CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/New Ability")]
    public class Ability : ScriptableObject, Interfaces.IConstruction
    {
        #region Variables
        [Header("Ability Configuration")]

        [SerializeField]
        [Tooltip("The Ability's Icon.")]
        private Sprite icon;
        public Sprite Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
            }
        }

        [SerializeField]
        [Space(10)]
        [Tooltip("What the Ability is called.")]
        private string abilityName;
        public string AbilityName
        {
            get
            {
                return abilityName;
            }
        }

        [SerializeField]
        [Space(10)]
        [Tooltip("What the Ability does.")]
        [TextArea()]
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        [SerializeField]
        [Space(10)]
        [Tooltip("The Sound Effect that is played when the Ability is Used.")]
        private AudioClip soundEffect;
        public AudioClip SoundEffect
        {
            get
            {
                return soundEffect;
            }
        }

        [SerializeField]
        [Space(10)]
        [Tooltip("What the Ability Does.")]
        private AbilityType abilityType;
        public AbilityType AbilityType
        {
            get
            {
                return abilityType;
            }
        }

        [Space(10)]
        [Header("Statistic Modifiers")]

        [SerializeField]
        [Space(10)]
        [Tooltip("How much the Ability costs.")]
        private int cost;
        public int Cost
        {
            get
            {
                return cost;
            }
        }

        [SerializeField]
        [Space(10)]
        [Tooltip("How much the Ability deals in damage.")]
        private int damage;
        public int Damage
        {
            get
            {
                return damage;
            }
        }

        [SerializeField]
        [Space(10)]
        [Tooltip("How much the Ability heals for.")]
        private int healing;
        public int Healing
        {
            get
            {
                return healing;
            }
        }

        [Space(10)]
        [Header("Ability Effect")]

        [SerializeField]
        [Space(10)]
        [Tooltip("What the Ability's Effect is.")]
        private Entities.StatusState effect;
        public Entities.StatusState Effect
        {
            get
            {
                return effect;
            }
        }

        [SerializeField]
        [Space(10)]
        [Tooltip("How long the Ability's Effect lasts for.")]
        private int effectDuration;
        public int EffectDuration
        {
            get
            {
                return effectDuration;
            }
        }

        private GameObject target;
        /// <summary>
        /// The Ability's assigned Target.
        /// </summary>
        public GameObject Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }
        #endregion

        #region Constructor
        public void Construct()
        {
            abilityName = "New Ability";
            abilityType = AbilityType.Damage;
            description = "A New Ability.";
            cost = 5;
            damage = 1;
            effectDuration = 0;
        }

        public void Construct(Sprite icon)
        {
            this.icon = icon;
            abilityName = "New Ability";
            abilityType = AbilityType.Damage;
            description = "A New Ability.";
            cost = 5;
            damage = 1;
            effectDuration = 0;
        }
        #endregion
    }
}

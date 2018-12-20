using System;
using UnityEngine;

namespace TurnBasedGame
{
    public enum CombatTurns
    {
        Player, Enemy, Default
    }

    /// <summary>
    /// Provides a Set of Tools for the Management of Turns in the Game's Combat Phase (State).
    /// </summary>
    public class TurnManager : MonoBehaviour, Interfaces.ITurnManagement
    {
        #region Singleton
        public static TurnManager management;

        #region Unity Methods
        private void Awake()
        {
            if (management != null)
            {
                Debug.Log("More than one {0} instance found!", management);
                return;
            }

            management = this;
            turnCount = 0;

            #region Event Subscriptions
            TurnPhaseChanged += IncrementTurnCount;
            TurnPhaseEnded += AuthorizeActions;
            UI.PopulateAbilitiesUI.OnceUIPopulated += SetDefaultTurn;
            #endregion
        }
        #endregion
        #endregion

        #region Variables
        public delegate void TurnPhase();
        public event TurnPhase TurnPhaseChanged;
        public event TurnPhase TurnPhaseEnded;

        private CombatTurns currentTurn;
        public CombatTurns CurrentTurn
        {
            get
            {
                return currentTurn;
            }
        }

        private int turnCount;
        /// <summary>
        /// The amount of turns that have Passed in the current Combat Phase.
        /// </summary>
        public int TurnCount
        {
            get
            {
                return turnCount;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Retreives the current Turn State.
        /// </summary>
        /// <returns></returns>
        public CombatTurns GetTurn()
        {
            return currentTurn;
        }

        private void IncrementTurnCount()
        {
            turnCount++;
        }

        /// <summary>
        /// Sets the turn to the Requested Turn State.
        /// </summary>
        /// <param name="turn"></param>
        public void SetTurn(CombatTurns turn)
        {
            try
            {
                currentTurn = turn;
                if(TurnPhaseChanged != null)
                    TurnPhaseChanged();
                if (TurnPhaseChanged != null)
                    TurnPhaseEnded();
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }
        }

        /// <summary>
        /// Assigns a Turn based on the Default Settings.
        /// </summary>
        public void SetDefaultTurn()
        {
            try
            {
                currentTurn = CombatTurns.Player;
                if (TurnPhaseChanged != null)
                    TurnPhaseChanged();
            }
            catch (Exception e)
            {
                Debug.Log("<color=red><b>Error: </b></color>" + e);
            }
        }

        /// <summary>
        /// Manages which Entities can use Actions based upon the turn state.
        /// </summary>
        private void AuthorizeActions()
        {
            Entities.EntityContainer playerContainer = Entities.Player.access.GetComponent<Entities.EntityContainer>();
            switch (currentTurn)
            {
                case CombatTurns.Player:
                    if (playerContainer.statusState != Entities.StatusState.Stunned)
                    {
                        UI.UIInteractionManager.management.EnableButtons(GameObject.Find("Actions Button Grid Layout Group"));
                    }
                    else
                    {
                        UI.UIInteractionManager.management.DisableButtons(GameObject.Find("Actions Button Grid Layout Group"));
                    }
                    break;

                case CombatTurns.Enemy:
                    UI.UIInteractionManager.management.DisableButtons(GameObject.Find("Actions Button Grid Layout Group"));
                    break;

                case CombatTurns.Default:
                    Debug.Log("<color=red><b>Warning:</b></color> No Turn State has been Assigned!");
                    break;
            }
        }
        
        #endregion
    }
}

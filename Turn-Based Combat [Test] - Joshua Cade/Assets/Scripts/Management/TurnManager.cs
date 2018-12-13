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

            TurnPhaseChanged += AuthorizeActions;
            UI.PopulateAbilitiesUI.OnceUIPopulated += SetDefaultTurn;
        }
        #endregion
        #endregion

        #region Variables
        public delegate void TurnPhase();
        public event TurnPhase TurnPhaseChanged;

        private CombatTurns currentTurn;
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

        /// <summary>
        /// Sets the turn to the Requested Turn State.
        /// </summary>
        /// <param name="turn"></param>
        public void SetTurn(CombatTurns turn)
        {
            try
            {
                currentTurn = turn;
                TurnPhaseChanged();
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
            switch(currentTurn)
            {
                case CombatTurns.Player:
                    UI.UIInteractionManager.management.EnableButtons(GameObject.Find("Actions Button Grid Layout Group"));
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

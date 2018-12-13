using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGame.Systems
{
    /// <summary>
    /// Manages the Sequencing, Queueing, Sorting, and Resolving of Actions.
    /// </summary>
    internal class ActionManagement : MonoBehaviour, Interfaces.IActionManagement
    {
        #region Singleton
        public static ActionManagement management;

        #region Unity Methods
        private void Awake()
        {
            if (management != null)
            {
                Debug.Log("More than one {0} instance found!", management);
                return;
            }

            management = this;
        }
        #endregion
        #endregion

        #region Variables
        private Queue<Ability> actionsQueue = new Queue<Ability>();

        [SerializeField]
        private int maxActionsInQueue = 5;
        #endregion

        #region Methods
        /// <summary>
        /// Cancels an Action if/when it is to be Executed.
        /// </summary>
        /// <param name="action"></param>
        public void CancelAction(string action)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Dequeue the Action from the Actions Queue.
        /// </summary>
        public void DeQueueAction()
        {
            actionsQueue.Dequeue();
        }

        /// <summary>
        /// Enqueue the Action into the Actions Queue.
        /// </summary>
        /// <param name="action"></param>
        public void QueueAction(Ability action)
        {
            if (!CheckIfFull())
            {
                try
                {
                    UI.PopulateQueuedActionsUI.management.GenerateAbilitiesInUI(action);
                    actionsQueue.Enqueue(action);
                    Debug.Log("<color=green><b>Queued: </b></color>" + action.AbilityName);
                }
                catch (Exception e)
                {
                    Debug.Log("<color=red><b>Error: </b></color>" + e);
                }
            }
            else
            {
                Debug.Log("<color=red><b>Could not Queue: </b></color>" + action.AbilityName);
            }
        }

        /// <summary>
        /// Checks whether the Actions Queue is Full or Not.
        /// </summary>
        /// <returns></returns>
        public bool CheckIfFull()
        {
            if (actionsQueue.Count + 1 <= maxActionsInQueue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Resolves the Action before Parsing it into the Queue.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool ResolveAction(Ability action)
        {
            //if player has mana
            //if queue isn't full??
            //if ability isn't invalid
            //etc...
            return true;
        }

        /// <summary>
        /// Sorts the Execution of Abilities if Necessary.
        /// </summary>
        public void SortExecutionOrder()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}

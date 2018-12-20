using System;
using System.Collections;
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
        private Queue<GameObject> generatedUIActions = new Queue<GameObject>();
        private Queue<GameObject> targets = new Queue<GameObject>();

        [SerializeField]
        private bool showDebugLog;

        [SerializeField]
        private int maxActionsInQueue = 5;
        #endregion

        #region Unity Methods
        private void Start()
        {
            TurnManager.management.TurnPhaseChanged += ResolveActionsInQueue;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Cancels an Action if/when it is to be Executed.
        /// </summary>
        /// <param name="action"></param>
        public void CancelAction(string action)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dequeue the Action from the Actions Queue.
        /// </summary>
        public void DeQueueAction()
        {
            if(targets.Count > 0)
                targets.Dequeue();
            if (actionsQueue.Count > 0)
                actionsQueue.Dequeue();
        }

        /// <summary>
        /// Enqueue the Action into the Actions Queue.
        /// </summary>
        /// <param name="action"></param>
        public void QueueAction(Ability action, GameObject target, GameObject caster)
        {
            if (!CheckIfFull())
            {
                try
                {
                    GameObject generatedUI = UI.PopulateQueuedActionsUI.management.GenerateAbilitiesInUI(action, target);
                    caster.GetComponent<Entities.EntityContainer>().ChangeStat("Mana", action.Cost, false);
                    if (generatedUI != null)
                        generatedUIActions.Enqueue(generatedUI);
                    if(action != null)
                        actionsQueue.Enqueue(action);
                    if (target != null)
                        targets.Enqueue(target);
                    if(showDebugLog)
                        Debug.Log("<color=green><b>Queued: </b></color>" + action.AbilityName);
                }
                catch (Exception e)
                {
                    Debug.Log("<color=red><b>Error: </b></color>" + e);
                }
            }
            else
            {
                if(showDebugLog)
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

        private void ResolveActionsInQueue()
        {
            StartCoroutine(ResolveActions(1.2f));
        }

        private IEnumerator ResolveActions(float delay)
        {
            while (actionsQueue.Count > 0)
            {
                if(generatedUIActions.Count > 0)
                    Destroy(generatedUIActions.Dequeue());
                ResolveAction(actionsQueue.Peek(), targets.Peek());
                yield return new WaitForSeconds(delay);
                DeQueueAction();
            }
        }

        /// <summary>
        /// Resolves an Action from the Queue.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public void ResolveAction(Ability action, GameObject target)
        {
            //placeholder, testing code, needs expanding on... :P
            //but works for now!
            foreach (KeyValuePair<GameObject, Entities.EntityContainer> entities in UI.PopulateEntityContainersUI.populate.entitiesSpawned)
            {
                if (entities.Value.UniqueID == target.GetComponent<Entities.SpawnedContainer>().entityParent.GetComponent<Entities.EntityContainer>().UniqueID)
                {
                    switch (action.AbilityType)
                    {
                        case AbilityType.Damage:
                            entities.Value.ChangeStat("Health", action.Damage, false);
                            break;
                        case AbilityType.Heal:
                            /* I think this is unnecessary, why did I write it??
                            if (TurnManager.management.GetTurn() == CombatTurns.Player)
                                Entities.Player.management.GetComponent<Entities.EntityContainer>().ChangeStat("Health", action.Healing, true);
                            if (TurnManager.management.GetTurn() == CombatTurns.Enemy)
                            */
                                entities.Value.ChangeStat("Health", action.Healing, true);
                            break;
                        case AbilityType.Debuff:
                            target.GetComponent<Entities.SpawnedContainer>().entityParent.GetComponent<Entities.EntityContainer>().statusState = action.Effect;
                            break;
                        case AbilityType.Buff:
                            //also do nothing for now...
                            break;
                        case AbilityType.Hybrid:
                            //this is tricky because it's too open ended... didn't think about hybrid enough oops
                            break;
                        case AbilityType.Summon:
                            //will handle this some other day but it's basically entity instantiating
                            //but with new tags called "AllySummon" & "EnemySummon"
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Sorts the Execution of Abilities if Necessary.
        /// </summary>
        public void SortExecutionOrder()
        {
            //this might not be needed, we'll see.
            throw new NotImplementedException();
        }
        #endregion
    }
}

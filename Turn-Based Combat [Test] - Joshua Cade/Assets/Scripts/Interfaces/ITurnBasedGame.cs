using UnityEngine;

namespace TurnBasedGame.Interfaces
{
    #region Data & File Management (Saving, etc)
    /// <summary>
    /// Implements Data Parsing & Encryption.
    /// </summary>
    internal interface IDataManagement
    {
        void SaveData<T>(T data);

        void LoadData<T>(T data);

        void EncryptData<T>(T data);
    }

    /// <summary>
    /// Implements simple File Management Functionalities.
    /// </summary>
    internal interface IFileManagement
    {
        void CreateFile<T>(string fileName, string filePath, T data);

        void DeleteFile<T>(string fileName, string filePath);

        T GetFile<T>(string fileName, string filePath);
    }
    #endregion

    #region Inventory Management
    /// <summary>
    /// Implements the basic Inventory functionalities.
    /// </summary>
    internal interface IInventory
    {
        void AddToInventory<T>(T item);

        void RemoveFromInventory(string itemID);

        T GetFromInventory<T>(string itemID);

        void CheckForInventoryLimit();
    }

    /// <summary>
    /// Implements the basic Interactions for an Inventory.
    /// </summary>
    internal interface IInventoryInteractions
    {
        void DragItem<T>(T item);

        void UseItem<T>(T item);

        void DeleteItem();

        void RearrangeItems();

        void SortBy(AbilityType type);
    }
    #endregion

    #region Action Management
    /// <summary>
    /// Implements an Action Management System for Handling Actions in a Queued Structure.
    /// </summary>
    internal interface IActionManagement
    {
        void QueueAction(Ability action, GameObject target);

        void DeQueueAction();

        void ResolveAction(Ability action, GameObject target);

        void CancelAction(string action);

        void SortExecutionOrder();
    }
    #endregion

    #region Turn Management
    /// <summary>
    /// Implements Management of Turns within the Combat Phase.
    /// </summary>
    internal interface ITurnManagement
    {
        void SetTurn(CombatTurns turn);

        CombatTurns GetTurn();
    }
    #endregion

    #region UI
    /// <summary>
    /// Implements Parsing of Data for Content Population.
    /// </summary>
    internal interface IParseData
    {
        void ParseComponentsData(Ability ability);
    }
    #endregion

    #region Defaults
    internal interface IConstruction
    {
        void Construct();
    }
    #endregion
}

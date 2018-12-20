using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGame.Entities
{
    /// <summary>
    /// The Container which holds the information from the Spawned Entity.
    /// </summary>
    public class SpawnedContainer : MonoBehaviour
    {
        public GameObject entityParent { get; set; }
        public string GeneratedID { get; set; }
    }
}

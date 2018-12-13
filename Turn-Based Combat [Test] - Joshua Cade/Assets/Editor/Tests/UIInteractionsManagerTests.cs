using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;

namespace TurnBasedGame.Tests
{
    internal sealed class UIInteractionsManagerTests : MonoBehaviour
    {
        #region Early Arrangements
        GameObject actualParent = new GameObject();
        GameObject expectedParent = new GameObject();
        UI.UIInteractionManager manager = new UI.UIInteractionManager();
        #endregion

        [Test]
        public void DisableButtonsTest()
        {
            #region Arrangements
            GameObject actualButton = Instantiate(new GameObject(), actualParent.transform);
            Button actualResult = actualButton.AddComponent<Button>();
            GameObject expectedButton = Instantiate(new GameObject(), expectedParent.transform);
            Button expectedResult = expectedButton.AddComponent<Button>();
            expectedResult.interactable = false;
            #endregion

            #region Actions
            manager.DisableButtons(actualParent);
            #endregion

            #region Assertions
            Assert.AreEqual(expectedResult.interactable, actualResult.interactable);
            #endregion
        }

        [Test]
        public void EnableButtonsTest()
        {
            #region Arrangements
            GameObject actualButton = Instantiate(new GameObject(), actualParent.transform);
            Button actualResult = actualButton.AddComponent<Button>();
            GameObject expectedButton = Instantiate(new GameObject(), expectedParent.transform);
            Button expectedResult = expectedButton.AddComponent<Button>();
            expectedResult.interactable = true;
            #endregion

            #region Actions
            manager.EnableButtons(actualParent);
            #endregion

            #region Assertions
            Assert.AreEqual(expectedResult.interactable, actualResult.interactable);
            #endregion
        }

        [Test]
        public void PlayAudioOnceTest()
        {
            #region Arrangements
            AudioClip emptyClip = AudioClip.Create("Test Clip", 10000, 1, 1200, false);
            manager.externalAudioSource = GameObject.Find("UI Manager").GetComponent<AudioSource>();
            #endregion

            #region Actions & Assertions
            Assert.IsTrue(manager.PlayAudioOnce(emptyClip, false));
            #endregion
        }

    }
}

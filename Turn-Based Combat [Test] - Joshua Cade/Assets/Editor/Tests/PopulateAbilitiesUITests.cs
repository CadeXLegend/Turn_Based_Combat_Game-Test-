using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using NUnit.Framework;

namespace TurnBasedGame.Tests
{
    internal sealed class PopulateAbilitiesUI
    {
        [Test]
        public void GenerateAbilitiesInUITest()
        {
            #region Arrangements
            Ability expectedResult = ScriptableObject.CreateInstance<Ability>();

            Sprite icon = Sprite.Create(new Texture2D(1, 1), new Rect(), new Vector2(0, 0));
            GameObject iconObject = Object.Instantiate(new GameObject());
            GameObject nameTextObject = Object.Instantiate(new GameObject());
            GameObject costTextObject = Object.Instantiate(new GameObject());
            iconObject.AddComponent<Image>();
            nameTextObject.AddComponent<Text>();
            costTextObject.AddComponent<Text>();

            expectedResult.Construct();
            expectedResult.Icon = icon;

            List<Ability> abilities = new List<Ability>();
            for (int i = 0; i < 1; i++)
            {
                abilities.Add(ScriptableObject.CreateInstance<Ability>());
                abilities[i].Construct();
                abilities[i].Icon = icon;
            }

            GameObject go;
            #endregion

            #region Actions
            foreach (Ability ab in abilities)
            {
                go = Object.Instantiate(new GameObject(), GameObject.Find("Actions Button Grid Layout Group").transform);
                go.AddComponent<UI.AbilitySlot>();
                go.GetComponent<UI.AbilitySlot>().Construct(
                    iconObject.GetComponent<Image>(), 
                    nameTextObject.GetComponent<Text>(), 
                    costTextObject.GetComponent<Text>());

                go.name = "Slot" + string.Format(" ({0})", ab.AbilityName);
                go.GetComponent<UI.AbilitySlot>().ParseComponentsData(ab);

                Ability actualResult = go.GetComponent<UI.AbilitySlot>().FetchAssignedAbility();
                #endregion

                #region Assertions
                Assert.AreEqual(expectedResult.Icon, actualResult.Icon);
                Assert.AreEqual(expectedResult.AbilityName, actualResult.AbilityName);
                Assert.AreEqual(expectedResult.AbilityType, actualResult.AbilityType);
                Assert.AreEqual(expectedResult.Description, actualResult.Description);
                Assert.AreEqual(expectedResult.Cost, actualResult.Cost);
                Assert.AreEqual(expectedResult.Damage, actualResult.Damage);
                Assert.AreEqual(expectedResult.EffectDuration, actualResult.EffectDuration);
                #endregion
            }
        }
    }
}

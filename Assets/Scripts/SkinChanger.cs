using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace GreenTeam
{
    public enum Skin { Placeholder, Oscar, Carlos };
    public class SkinChanger : MonoBehaviour
    {
        [SerializeField] Skin initialSkin = Skin.Oscar;
        Dictionary<Skin, string> skins = new Dictionary<Skin, string>();
        [SerializeField] List<SpriteResolver> spriteResolvers = new List<SpriteResolver>();
        List<string> categorys = new List<string>();

        public void SetCurrentEquippedSkin(Skin skin) => PlayerPrefs.SetInt("CurrentEquippedSkin", (int)skin);

        void Start()
        {
            int skinsCount = Enum.GetNames(typeof(Skin)).Length;//count how many skins have
            for (int s = 0; s < skinsCount; s++)//add skins to the dictionary
            {
                skins.Add((Skin)s, Enum.GetName(typeof(Skin), s));
            }

            SetupInitialSkin();
            ChangeToEquippedSkin();

            // spriteResolver.SetCategoryAndLabel(spriteResolver.GetCategory(), "Placeholder");
        }

        public void SetupInitialSkin()
        {
            if (!PlayerPrefs.HasKey("CurrentEquippedSkin"))
            {
                SetCurrentEquippedSkin(initialSkin);
                PlayerPrefs.SetInt("BoughtSkin" + initialSkin.ToString(), 1);
            }
        }

        public void ChangeSkin(Skin newSkin)
        {
            foreach (SpriteResolver sr in spriteResolvers)
            {
                sr.SetCategoryAndLabel(sr.GetCategory(), skins[newSkin]);
            }
        }

        public void ChangeSkin(int skinValue)
        {
            Skin newSkin = (Skin)skinValue;
            ChangeSkin(newSkin);
        }

        public void ChangeToEquippedSkin()
        {
            Skin currentEquippedSkin = (Skin)PlayerPrefs.GetInt("CurrentEquippedSkin");
            ChangeSkin(currentEquippedSkin);
        }

    }
}

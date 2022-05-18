using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace GreenTeam
{
    public class SkinChanger : MonoBehaviour
    {
        public enum Skin {Placeholder, Oscar, Carlos};
        [SerializeField]Skin initialSkin = Skin.Oscar;
        Dictionary<Skin, string> skins = new Dictionary<Skin, string>();
        [SerializeField] List<SpriteResolver> spriteResolvers = new List<SpriteResolver>();
        List<string> categorys = new List<string>();
        void Start()
        {
            // Debug.Log(Enum.GetNames(typeof(Skin)).Length);
            // Debug.Log(spriteResolvers[0].GetCategory());

            int skinsCount = Enum.GetNames(typeof(Skin)).Length;//count how many skins have
            for (int s = 0; s < skinsCount; s++)//add skins to the dictionary
            {
                skins.Add((Skin)s, Enum.GetName(typeof(Skin), s));
                // Debug.Log(skins[(Skin)s]);
            }

            ChangeSkin(initialSkin); // muda para a skin inicial
            
            // spriteResolver.SetCategoryAndLabel(spriteResolver.GetCategory(), "Placeholder");
        }

        // Update is called once per frame
        void Update()
        {
            // for (int sr = 0; sr < spriteResolvers.Count; sr++)//atualiza os sprite para o sprite inicial
            // {
            //     Debug.Log(categorys[sr]); 
            //     spriteResolvers[sr].SetCategoryAndLabel(categorys[sr], skins[initialSkin]);
            // }
                // spriteResolvers[1].SetCategoryAndLabel(spriteResolvers[1].GetCategory(), skins[initialSkin]);
                // Debug.Log(skins[initialSkin]);
        }

        public void ChangeSkin(Skin newSkin)
        {
            foreach (SpriteResolver sr in spriteResolvers)
            {
                sr.SetCategoryAndLabel(sr.GetCategory(), skins[newSkin]);
                // Debug.Log(sr);
            }
        }

        public void ChangeSkin(int skinValue)
        {
            Skin newSkin = (Skin)skinValue;
            foreach (SpriteResolver sr in spriteResolvers)
            {
                sr.SetCategoryAndLabel(sr.GetCategory(), skins[newSkin]);
                // Debug.Log(sr);
            }
        }
    }
}

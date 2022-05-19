using UnityEngine;

namespace GreenTeam
{
    public class SkinUIButton : MonoBehaviour
    {
        [SerializeField] Skin skin = Skin.Oscar;
        [SerializeField] int skinPrice = 200;
        

        public void ChangeSkinInUI()
        {
            ShopManager shopManager = FindObjectOfType<ShopManager>();

            if (shopManager == null) return;

            shopManager.ChangeSkinInUI(skin, skinPrice);
        }
    }
}

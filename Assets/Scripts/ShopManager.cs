using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GreenTeam
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] Text textNumberOfLikes;
        [SerializeField] Button wearButton;

        int numberOfLikes = 0;
        int currentSkinBeingShownPrice;
        Skin currentSkinBeingShown = Skin.Oscar;
        SkinChanger skinChanger;
        TextMeshProUGUI wearButtonText;

        void Awake()
        {
            skinChanger = FindObjectOfType<SkinChanger>();
            if (wearButton != null)
            {
                wearButtonText = wearButton.GetComponentInChildren<TextMeshProUGUI>();
            }
        }

        void OnEnable() => UpdateUI();
        void OnDisable() => skinChanger.ChangeToEquippedSkin();
        void SetLikes() => PlayerPrefs.SetInt("Likes", numberOfLikes);
        bool CheckIfSkinWasBought(Skin skin) => PlayerPrefs.HasKey("BoughtSkin" + skin.ToString());

        void UpdateUI()
        {
            if (PlayerPrefs.HasKey("Likes"))
            {
                numberOfLikes = PlayerPrefs.GetInt("Likes");
            }

            if (textNumberOfLikes != null)
                textNumberOfLikes.text = numberOfLikes.ToString();

            UpdateWearButtonText(PlayerPrefs.GetInt("CurrentEquippedSkin"), 0);
        }

        public void ChangeSkinInUI(Skin skin, int skinPrice)
        {
            int skinNumber = (int)skin;
            currentSkinBeingShown = skin;
            skinChanger.ChangeSkin(skinNumber);
            UpdateWearButtonText(skinNumber, skinPrice);
        }

        public void WearButton()
        {
            if (CheckIfSkinWasBought(currentSkinBeingShown))
            {
                skinChanger.SetCurrentEquippedSkin(currentSkinBeingShown);
            }
            else
            {
                BuySkin();
            }

            UpdateUI();
        }

        private void BuySkin()
        {
            if (currentSkinBeingShownPrice > numberOfLikes) return;

            numberOfLikes -= currentSkinBeingShownPrice;
            SetLikes();

            skinChanger.SetCurrentEquippedSkin(currentSkinBeingShown);
            PlayerPrefs.SetInt("BoughtSkin" + currentSkinBeingShown.ToString(), 1);
        }

        private void UpdateWearButtonText(int skinNumber, int skinPrice)
        {
            Skin skin = (Skin)skinNumber;
            currentSkinBeingShownPrice = skinPrice;

            if (CheckIfSkinWasBought(skin))
            {
                wearButtonText.SetText("Equip Skin");
            }
            else
            {
                wearButtonText.SetText("Buy: " + skinPrice);
            }
        }

        [ContextMenu(nameof(DEBUG_SHOP))]
        void DEBUG_SHOP()
        {
            PlayerPrefs.DeleteKey("CurrentEquippedSkin");
            skinChanger.SetupInitialSkin();
            numberOfLikes = 999;
            SetLikes();

            foreach (Skin skin in (Skin[])Enum.GetValues(typeof(Skin)))
            {
                string key = "BoughtSkin " + skin.ToString();
                if (PlayerPrefs.HasKey(key))
                {
                    PlayerPrefs.DeleteKey(key);
                }
            }
        }

    }
}

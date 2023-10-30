using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ChooseGunSlot : MonoBehaviour
{
    public RectTransform rectTransform;

    private int newGunId;
    private Inventory inventory;

    public TMP_Text newGunText;
    public GameObject itemCard;

    public void SetupCards(ref Inventory inventory, int newGunId)
    {
        this.inventory = inventory;
        this.newGunId = newGunId;
        
        newGunText.text = GunCollection.GetGun(newGunId).GetName();

        int slotCount = inventory.GetMaxSlot();

        float cardWidth = rectTransform.rect.width / slotCount;

        RectTransform itemCardRT;
        itemCardRT = itemCard.GetComponent<RectTransform>();
        Rect oriPos = new Rect();
        oriPos.Set((1 - slotCount % 2) * cardWidth / 2 + 0 - slotCount / 2 * cardWidth, 
                            0, 
                            cardWidth,
                            itemCardRT.rect.height);
        itemCardRT.localPosition = oriPos.position;
        itemCardRT.sizeDelta = oriPos.size;
        itemCardRT.GetChild(0).GetComponent<TMP_Text>().text = GunCollection.GetGun(inventory.GetGunIdAt(0)).GetName();

        for (int i = 1; i < slotCount; i++)
        {
            GameObject newCard = Instantiate(itemCard, transform);
            itemCardRT = newCard.GetComponent<RectTransform>();
            itemCardRT.localPosition = oriPos.position + new Vector2(cardWidth * i, 0f);
            itemCardRT.GetChild(0).GetComponent<TMP_Text>().text = GunCollection.GetGun(inventory.GetGunIdAt(i)).GetName();
        }
    }

    public void OnClickedCard()
    {
        inventory.ReplaceGun(EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex() - 1, newGunId);
        GameManager.Instance.playerScript.UpdateGun();
        EndChoosing();
    }

    public void EndChoosing()
    {
        for (int i = transform.childCount - 1; i >= 2; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        itemCard = transform.GetChild(1).gameObject;

        UIManager.Instance.replacingGun = false;

        GameManager.Instance.ThawGame();
        gameObject.SetActive(false);
    }
}

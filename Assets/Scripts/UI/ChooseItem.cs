using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ChooseItem : MonoBehaviour
{
    public RectTransform rectTransform;

    private List<ItemCrystal> items = new List<ItemCrystal>();

    public GameObject itemCard;

    public void SetupCards(int itemCount, ref List<ItemCrystal> items)
    {
        this.items = items;

        float cardWidth = rectTransform.rect.width / itemCount;

        RectTransform itemCardRT;
        itemCardRT = itemCard.GetComponent<RectTransform>();
        Rect oriPos = new Rect();
        oriPos.Set((1 - itemCount % 2) * cardWidth / 2 + 0 - itemCount / 2 * cardWidth, 
                            0, 
                            cardWidth,
                            itemCardRT.rect.height);
        itemCardRT.localPosition = oriPos.position;
        itemCardRT.sizeDelta = oriPos.size;
        itemCardRT.GetChild(0).GetComponent<TMP_Text>().text = items[0].name;

        for (int i = 1; i < itemCount; i++)
        {
            GameObject newCard = Instantiate(itemCard, transform);
            itemCardRT = newCard.GetComponent<RectTransform>();
            itemCardRT.localPosition = oriPos.position + new Vector2(cardWidth * i, 0f);
            itemCardRT.GetChild(0).GetComponent<TMP_Text>().text = items[i].name;
        }
    }

    public void OnClickedItemCard()
    {
        items[EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex()].BreakCrystal();
        // GameManager.Instance.playerScript.UpdateGun();
        EndChoosing();
    }

    public void EndChoosing()
    {
        GameManager.Instance.ThawGame();
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Public stuff")]
    public GameObject inv;
    public GameObject extraInfoObj;
    public GameObject invPanel;
    public GameObject chestPanel;
    bool mayMoveItem = false;
    bool mayDropItem = false;
    public Text nameText;
    public Text amountTextDisplay;

    [Header("invetory")]
    public ItemClassScriptableObject itemScriptableObject;
    public SlotInformation[] slotInformationArray;

    public Color highLightColor;
    public Color colorReset;
    int indexHolder = 99;
    //SlotInformation slotInformationHolder;
    CharacterMovement charMovement;
    //public List<SlotInformation> slotInformationList = new List<SlotInformation>();

    void Start()
    {
        /*foreach (Transform slot in invPanel.transform)
        {
            slotInformationList.Add(new SlotInformation() {slotImage = slot.GetComponent<Image>() });
        }
        slotInformationArray = slotInformationList.ToArray();
        */
        extraInfoObj.SetActive(false);
        inv.SetActive(false);
        chestPanel.SetActive(false);
        charMovement = gameObject.GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory") && inv.activeInHierarchy == false && chestPanel.activeInHierarchy == false && charMovement.allowMovement == true)
        {
            inv.SetActive(true);
            charMovement.allowMovement = false;
        }       
        else if(Input.GetButtonDown("Inventory") && inv.activeInHierarchy == true && chestPanel.activeInHierarchy == false && charMovement.allowMovement == false)
        {
            inv.SetActive(false);
            charMovement.allowMovement = true;
        }
    }

    public void MoveItemIntFunction(int intje)
    {
        //if in lijst inv of in lijst chest
        if (gameObject.GetComponent<StorageSystem>())
        {
            Debug.Log("LIST IN CHEST");
            ItemMove(intje, gameObject.GetComponent<StorageSystem>().chestSlotArray);
        }
        else
        {
            Debug.Log("LIST IN INV");
            ItemMove(intje, slotInformationArray);
        }
        //moet lijst hebben 
    }

    public void ItemMove(int i, SlotInformation[] slot)//lijst
    {
        if (mayMoveItem == true)
        {
            if (slot[i].index != slot[indexHolder].index)
            {
                if (slot[i].slotImage.sprite == null)
                {
                    slot[i].slotImage.color = highLightColor;//lijst
                    slot[i].slotImage.sprite = slot[indexHolder].slotImage.sprite;
                    slot[i].itemGameobjectHolder = slot[indexHolder].itemGameobjectHolder;

                    slot[indexHolder].slotImage.color = colorReset;
                    slot[indexHolder].slotImage.sprite = null;
                    slot[indexHolder].itemGameobjectHolder = null;
                    indexHolder = 99;
                }
                else //aanpassen
                {
                    Sprite saveSprite = slot[i].slotImage.sprite;
                    GameObject tempGameObjecy = slot[i].itemGameobjectHolder;

                    slot[i].slotImage.color = highLightColor;
                    slot[i].itemGameobjectHolder = slot[indexHolder].itemGameobjectHolder;
                    slot[i].slotImage.sprite = slot[indexHolder].slotImage.sprite;

                    slot[indexHolder].slotImage.sprite = saveSprite;
                    slot[indexHolder].itemGameobjectHolder = tempGameObjecy;
                    slot[indexHolder].slotImage.color = colorReset;

                    indexHolder = 99;
                }

            }
            slot[i].slotImage.color = colorReset;
            mayMoveItem = false;
        }
        else if(slot[i].slotImage.sprite != null && mayMoveItem == false)
        {
            for (int it = 0; it < slot.Length; it++)
            {
                if (slot[it].slotImage.color == highLightColor)
                {
                    slot[it].slotImage.color = colorReset;
                    indexHolder = 99;
                    mayMoveItem = false;
                    continue;
                }
            }

            slot[i].slotImage.color = highLightColor;

            indexHolder = i;
            //slotInformationHolder = slot[i];
            
            extraInfoObj.SetActive(true);
            //aanpassen
            //nameText.text = slotInformationArray[i].itemGameobjectHolder.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[slotInformationArray[i].itemGameobjectHolder.GetComponent<ItemIndex>().index].name;
            //amountTextDisplay.text = slotInformationArray[i].itemGameobjectHolder.GetComponent<ItemIndex>().amoundInItem.ToString();

        }
    }

    public void DropItem(bool b, int indexDrop)
    {
        if (b == true)
        {
            slotInformationArray[indexDrop].itemGameobjectHolder.GetComponent<ItemIndex>().mayAdd = false;
            slotInformationArray[indexDrop].slotImage.color = colorReset;
            slotInformationArray[indexDrop].slotImage.sprite = null;
            //Insatniate object
            GameObject g = Instantiate(slotInformationArray[indexDrop].itemGameobjectHolder, transform.position + (transform.forward*2), Quaternion.identity);
            StartCoroutine(CoolDownItemDrop(2, g));
            slotInformationArray[indexDrop].itemGameobjectHolder = null;
            mayDropItem = false;
            indexDrop = 99;
        }
    }

    public void SwitchItemButton()
    {
        extraInfoObj.SetActive(false);
        mayMoveItem = true;
    }

    public void DropItemButton()
    {
        extraInfoObj.SetActive(false);
        mayDropItem = true;
        DropItem(mayDropItem, indexHolder);
    }

    public void AddItem(int index, GameObject itemObject)
    {

        for (int forint = 0; forint < slotInformationArray.Length; forint++)
        {
            //add item
            if (slotInformationArray[forint].slotImage.sprite == itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[index].Sprite && itemObject.GetComponent<ItemIndex>().mayAdd == true && slotInformationArray[forint].amount + itemObject.GetComponent<ItemIndex>().amoundInItem <= itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[index].maxStack)
            {
                Debug.Log("IN DE EERSTE IF");
                slotInformationArray[forint].slotImage.sprite = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[index].Sprite;
                slotInformationArray[forint].amount += itemObject.GetComponent<ItemIndex>().amoundInItem;
                slotInformationArray[forint].itemGameobjectHolder = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[itemObject.GetComponent<ItemIndex>().index].itemGameObject;
                Destroy(itemObject);
                break;
            }
            //next if amout is full
            else if (slotInformationArray[forint].slotImage.sprite == itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[index].Sprite && slotInformationArray[forint].amount + itemObject.GetComponent<ItemIndex>().amoundInItem > itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[index].maxStack)
            {
                Debug.Log("IN DE TWEEDE IF");
                int temp;
                temp = slotInformationArray[forint].amount + itemObject.GetComponent<ItemIndex>().amoundInItem - itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[index].maxStack;
                slotInformationArray[forint].amount = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[index].maxStack;
                for (int iiii = 0; iiii < slotInformationArray.Length; iiii++)
                {
                    if (slotInformationArray[iiii].slotImage.sprite == null)
                    {
                        slotInformationArray[index].slotImage.sprite = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[index].Sprite;
                        slotInformationArray[index].amount += temp;
                        slotInformationArray[index].itemGameobjectHolder = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[itemObject.GetComponent<ItemIndex>().index].itemGameObject;
                        Destroy(itemObject);
                        break;
                    }
                    else
                    {
                        itemObject.GetComponent<ItemIndex>().amoundInItem = temp;
                        Debug.Log("DE ELSE IN DE IFIF");
                        break;
                    }
                }
                break;
            }
            //add if slot is null
            else if (slotInformationArray[forint].slotImage.sprite == null && itemObject.GetComponent<ItemIndex>().mayAdd == true && slotInformationArray[forint].amount + itemObject.GetComponent<ItemIndex>().amoundInItem <= itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[index].maxStack)
            {
                Debug.Log("IN DE DEREDE IF");
                slotInformationArray[forint].slotImage.sprite = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[index].Sprite;
                slotInformationArray[forint].amount += itemObject.GetComponent<ItemIndex>().amoundInItem;
                slotInformationArray[forint].itemGameobjectHolder = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[itemObject.GetComponent<ItemIndex>().index].itemGameObject;
                Destroy(itemObject);
                break;
            }
        }
    }

    public IEnumerator CoolDownItemDrop(float coolDown,GameObject itemGameObject)
    {
        yield return new WaitForSeconds(coolDown);
        itemGameObject.GetComponent<ItemIndex>().mayAdd = true;
    }

 /*
 * move item chage
 * 
 * inex selected
 * ref kist
 * //
 * 2 indexen nodig alse slots //refrence
 * als allebei ingevuld // while loop
 * dan voer void uit om te wisselen
 * 
 */

}

[System.Serializable]
public class SlotInformation
{
    public Image slotImage;
    public int index;
    public int amount;
    public GameObject itemGameobjectHolder;
}

[System.Serializable]
public class SlotRefrenceInformation
{
    public int witchIndex;
    public StorageSystem storageSystem;
}
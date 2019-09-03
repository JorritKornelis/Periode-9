using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndex : MonoBehaviour
{
    public int index;
    public int amoundInItem;
    public bool mayAdd;

    public ItemClassScriptableObject itemClassScriptableObject;

    public IEnumerator CoolDownItemDrop(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        mayAdd = true;
    }
}

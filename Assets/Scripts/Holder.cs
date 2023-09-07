using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public List<GameObject> slots;

    int index = -1;

    public void ActiveSlot()
    {
        index += 1;

        if (index > slots.Count - 1)
        {
            return;
        }

        slots[index].SetActive(true);

        if (index == slots.Count - 1)
        {
            StartCoroutine(WaitForRemoveThisHolderFromList());
        }
    }

    IEnumerator WaitForRemoveThisHolderFromList()
    {
        yield return new WaitForSeconds(1);

        //Remove here
        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].holders.Remove(this);
    }
}

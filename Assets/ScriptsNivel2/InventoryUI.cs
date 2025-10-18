using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject[] arrayCandysInventory;

    private void OnEnable()
    {
        CandyController.OnGetCandy += TurnOnCandy;
        CandyController.OnSetCandy += TurnOffCandy;
    }

    private void OnDisable()
    {
        CandyController.OnGetCandy -= TurnOnCandy;
        CandyController.OnSetCandy -= TurnOffCandy;
    }
    void TurnOnCandy(int index)
    {
        arrayCandysInventory[index].SetActive(true);
    }

    void TurnOffCandy(int index)
    {
        arrayCandysInventory[index].SetActive(false);
    }
}

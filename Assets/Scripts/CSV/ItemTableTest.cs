using UnityEngine;

public class ItemTableTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(DataTableManager.ItemTable.Get("Item2"));
        }
    }
}

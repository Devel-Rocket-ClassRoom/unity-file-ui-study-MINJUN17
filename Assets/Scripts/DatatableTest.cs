using UnityEngine;

public class DatatableTest : MonoBehaviour
{
    public static StringTable stringTable {  get; private set; }

    private void Start()
    {
        stringTable = new StringTable();
    }
    public void OnClickKorean()
    {
        Debug.Log(DataTableManager.StringTable.Get("HELLO"));
        Debug.Log(DataTableManager.StringTable.Get("BYE"));
        Debug.Log(DataTableManager.StringTable.Get("YOU DIE"));
    }
    public void OnClickJapanese()
    {
        stringTable.Load("StringTableJp");
        Debug.Log(stringTable.Get("HELLO"));
        Debug.Log(stringTable.Get("BYE"));
        Debug.Log(stringTable.Get("YOU DIE"));
    }
    public void OnClickEnglish()
    {
        stringTable.Load("StringTableEn");
        Debug.Log(stringTable.Get("HELLO"));
        Debug.Log(stringTable.Get("BYE"));
        Debug.Log(stringTable.Get("YOU DIE"));
    }
}

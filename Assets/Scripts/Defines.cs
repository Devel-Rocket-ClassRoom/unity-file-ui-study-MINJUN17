using System;

public enum Languages
{
    Korean,
    English,
    Japanese,
}
public enum ItemType
{
    Weapon,
    Equip,
    Consumable,
}

public static class Variables
{
    public static event System.Action OnLanguageChanged;
    public static event System.Action<Languages> OnAllLanguageChanged;
    public static void OnChangedAllLanguage(Languages lang)
    {
        OnAllLanguageChanged?.Invoke(lang);
    }
    private static Languages language = Languages.Korean;
    public static Languages Languages
    {
        get {  return language; }
        set
        {
            if(language == value)
            {
                return;
            }
            language = value;
            DataTableManager.ChangeLanguage(language);
            OnLanguageChanged?.Invoke();
        }
    }
}

public static class DataTableIds
{
    public static readonly string[] StringTableIds =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp"
    };
    public static string String => StringTableIds[(int)Variables.Languages];
    public static readonly string Item = "ItemTable";
}

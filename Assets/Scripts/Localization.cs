using TMPro;
using Unity.VisualScripting;
using UnityEngine;
[ExecuteInEditMode]
public class Localization : MonoBehaviour
{
    public Languages editorLanguage;
    public string id;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
#if UNITY_EDITOR
        Variables.OnAllLanguageChanged += OnChangedLanguage;
#endif
        Variables.OnLanguageChanged += OnChangedId;
        if (Application.isPlaying)
        {
            OnChangedId();
        }
#if UNITY_EDITOR
        else
        {
            OnChangedLanguage(editorLanguage);
        }
#endif
    }
    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangedLanguage(editorLanguage);
        //OnChangedId();
#endif
    }
    private void OnDisable()
    {
        Variables.OnLanguageChanged -= OnChangedId;
#if UNITY_EDITOR
        Variables.OnAllLanguageChanged -= OnChangedLanguage;
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Variables.Languages = Languages.Korean;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Variables.Languages = Languages.English;

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Variables.Languages = Languages.Japanese;
        }
    }
    
    public void OnChangedId()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }
    public void OnChangedLanguage()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }

#if UNITY_EDITOR
    public void OnChangedLanguage(Languages lang)
    {
        var stringTable = DataTableManager.GetStringTable(lang);
        text.text = stringTable.Get(id);
    }
#endif
#if UNITY_EDITOR
    public void OnChangedAllLanguage(Languages lang)
    {
        editorLanguage = lang;
        var stringTable = DataTableManager.GetStringTable(lang);
        text.text = stringTable.Get(id);
    }

    [ContextMenu("Change All Language")]
    private void ApplyLanguageToAll()
    {
        Variables.OnChangedAllLanguage(editorLanguage);
    }
#endif
}

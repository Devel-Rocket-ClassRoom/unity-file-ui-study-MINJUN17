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
}

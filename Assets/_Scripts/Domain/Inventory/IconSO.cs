using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/IconSO")]
public class IconSO: SORuntimeLoadable<IconIds>
{
    public IconIds iconId;
    public Sprite icon;
}

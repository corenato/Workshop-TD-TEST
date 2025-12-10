using UnityEngine;

public class TooltipTarget : MonoBehaviour
{
    [SerializeField] private string titleText;
    [TextArea(3,6)]
    [SerializeField] private string descriptionText;

    public void OnShowTooltip()
    {
        Tooltip.Instance.ShowTooltip(titleText, descriptionText);
    }

    public void OnHideTooltip()
    {
        Tooltip.Instance.HideTooltip(); 
    }
}

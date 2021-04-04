using UnityEngine;
using UnityEngine.UI;

public abstract class SelectableOption<T> : MonoBehaviour where T : class
{
    public delegate void OnClick(T selected);
    public static event OnClick OnSelecttableClicked;

    protected T associatedSelection;
    protected Image background;

    public abstract void InitiateSelectableOption(T option, bool isSelected);

    public virtual void InitiateSelectableOptionasdsdds(T option, bool isSelected) {
        if (option == null) {
            this.associatedSelection = null;

            this.background.color = new Color(0, 0, 0);
            this.gameObject.SetActive(false);
        }
        else {
            this.associatedSelection = option;

            this.background.color = isSelected ? new Color(0, 255, 0) : new Color(255, 255, 255);
            this.gameObject.SetActive(true);
        }
    }

    public void HighlightSelectableOption(bool shouldHighlight) {

    }

    public void OnSelectableClick() {
        OnSelecttableClicked?.Invoke(this.associatedSelection);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class ListSelectionTool<T> : MonoBehaviour where T : class
{
    public delegate void OnToolFinish(string callerId, T selection = null);
    public static event OnToolFinish OnToolFinished;
    private string callerId;
    private bool closedTroughEvents = false;

    public List<SelectableOption<T>> selectableOptions;
    public List<T> queriedSelection;

    public InputField inputField;
    private T selectedObject;

    protected int currentPage;
    protected int maxPages;
    protected int pageSize;

    public Text message;
    public Text page;
    public Button nextPage;
    public Button previousPage;

    // Start is called before the first frame update
    protected void Start() {
        SelectableOption<T>.OnSelecttableClicked += OnSelection;
    }

    private void OnDestroy() {
        SelectableOption<T>.OnSelecttableClicked -= OnSelection;
    }

    private void OnEnable() {
        this.SetInitiateToolValues();
        this.queriedSelection = this.GetAllSelectableMembers();

        this.RenderSelectionableOptions(true);
    }

    private void OnDisable() {
        if (!this.closedTroughEvents) {
            this.OnCloseToolWithoutSelection();
        }
        else {
            this.closedTroughEvents = false;
        }
    }

    public void OnEnableTool(string callerId) {
        this.callerId = callerId;
        this.gameObject.SetActive(true);
    }

    abstract protected void SetInitiateToolValues();

    abstract protected List<T> GetAllSelectableMembers();
    abstract protected List<T> FilterSelectableMembersByQuery(string query);
    abstract protected int GetIndexOfSelectionInList(T selected);

    public void OnSearchInputChanged() {
        string query = this.inputField.text;
        this.queriedSelection = this.FilterSelectableMembersByQuery(query);

        this.RenderSelectionableOptions(true);
    }

    protected void OnSelection(T selected) {
        if (this.selectedObject == selected) {
            this.selectedObject = null;
            return;
        }

        this.selectedObject = selected;
        RenderSelectionableOptions();
    }

    public void OnPagination(bool forward) {

        this.currentPage = forward ? this.currentPage + 1 : this.currentPage - 1;

        if (this.currentPage <= 0) {
            this.currentPage = 0;
            this.previousPage.enabled = false;
        }
        else {
            this.previousPage.enabled = true;
        }

        if (this.currentPage >= this.maxPages) {
            this.currentPage = this.maxPages;
            this.nextPage.enabled = false;
        }
        else {
            this.nextPage.enabled = true;
        }

        this.RenderSelectionableOptions();
    }

    private void RenderSelectionableOptions(bool repaginate = false) {
        List<T> toShowSelectable = this.queriedSelection.Skip(this.pageSize * this.currentPage).Take(this.pageSize).ToList();
        int indexOfSelected = this.selectedObject == null ? -1 : this.GetIndexOfSelectionInList(this.selectedObject);

        for (int i = 0; i < pageSize; i++) {
            this.selectableOptions[i].InitiateSelectableOption(toShowSelectable.Count > i ? toShowSelectable[i] : null, i == indexOfSelected);
        }

        if (repaginate) {
            this.currentPage = 0;
            this.maxPages = (this.queriedSelection.Count + this.pageSize - 1) / this.pageSize;
        }

        this.page.text = (this.currentPage + 1).ToString() + " of " + this.maxPages.ToString();
    }

    public void SubmitSelected() {
        if (this.selectedObject == null) {
            this.message.text = "You need to select a trait before Submitting";
            return;
        }
        this.closedTroughEvents = true;
        this.gameObject.SetActive(false);

        OnToolFinished?.Invoke(this.callerId, this.selectedObject);
        this.selectedObject = null;
    }

    public void OnCloseToolWithoutSelection() {
        this.selectedObject = null;
        this.closedTroughEvents = true;
        this.gameObject.SetActive(false);

        OnToolFinished?.Invoke(this.callerId);
    }
}

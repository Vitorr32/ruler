using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class ListSelectionTool<T> : MonoBehaviour where T : class
{
    public delegate void OnToolFinish(string callerId, List<T> selection = null);
    public static event OnToolFinish OnToolFinished;
    private string callerId;
    private bool closedTroughEvents = false;

    public List<SelectableOption<T>> selectableOptions;
    public List<T> queriedSelection;

    public InputField inputField;
    private List<T> selectedObjects;
    private bool isMultiSelect = false;

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

    public void OnEnableTool(string callerId, bool isMultiSelect = false) {
        this.callerId = callerId;
        this.isMultiSelect = isMultiSelect;
        this.selectedObjects = new List<T>();
        this.gameObject.SetActive(true);
    }

    abstract protected void SetInitiateToolValues();

    abstract protected List<T> GetAllSelectableMembers();
    abstract protected List<T> FilterSelectableMembersByQuery(string query);
    abstract protected List<int> GetIndexesOfSelectionInList(List<T> selected);
    abstract protected int GetIndexOfElementOnArray(T element, List<T> array);

    public void OnSearchInputChanged() {
        string query = this.inputField.text;
        this.queriedSelection = this.FilterSelectableMembersByQuery(query);

        this.RenderSelectionableOptions(true);
    }

    protected void OnSelection(T selected) {
        int indexOnList = this.GetIndexOfElementOnArray(selected, this.selectedObjects);
        if (indexOnList != -1) {
            this.selectedObjects.RemoveAt(indexOnList);
            this.RenderSelectionableOptions();
            return;
        }

        if (this.isMultiSelect) {
            this.selectedObjects.Add(selected);
        }
        else {
            this.selectedObjects = new List<T>() { selected };
        }

        this.RenderSelectionableOptions();
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
        List<int> indexesOfSelected = this.selectedObjects == null || this.selectedObjects.Count == 0 ? null : this.GetIndexesOfSelectionInList(this.selectedObjects);

        for (int i = 0; i < pageSize; i++) {            
            this.selectableOptions[i].InitiateSelectableOption(toShowSelectable.Count > i ? toShowSelectable[i] : null, indexesOfSelected != null ? indexesOfSelected.Contains(i) : false);
        }

        if (repaginate) {
            this.currentPage = 0;
            this.maxPages = (this.queriedSelection.Count + this.pageSize - 1) / this.pageSize;
        }

        this.page.text = (this.currentPage + 1).ToString() + " of " + this.maxPages.ToString();
    }

    public void SubmitSelected() {
        if (this.selectedObjects == null) {
            this.message.text = "You need to select a trait before Submitting";
            return;
        }
        this.closedTroughEvents = true;
        this.gameObject.SetActive(false);

        OnToolFinished?.Invoke(this.callerId, this.isMultiSelect && this.selectedObjects.Count == 0 ? null : this.selectedObjects);
        this.selectedObjects = null;
    }

    public void OnCloseToolWithoutSelection() {
        this.selectedObjects = null;
        this.closedTroughEvents = true;
        this.gameObject.SetActive(false);

        OnToolFinished?.Invoke(this.callerId);
    }
}

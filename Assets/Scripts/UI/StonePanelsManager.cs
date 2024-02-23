using System.Collections.Generic;
using UnityEngine;

public class StonePanelsManager : MonoBehaviour
{
    public bool refresh = false;
    public RowStone StoneContainerPrefab;
    public int numberOfContainers = 10;
    [SerializeField] private PebbleRegistry registry;
    
    private List<RowStone> _panels = new List<RowStone>();


    private void Start() {
        PopulateRockSelection();
    }

    public void PopulateRockSelection() {
        //clear VBox
        if (_panels.Count > 0) {
            foreach (RowStone panel in _panels) 
                Destroy(panel.gameObject);
            // UnityEditor.EditorApplication.delayCall+=() => DestroyImmediate(panel.gameObject);
            _panels.Clear();
        }

        SelectionStones();
    }

    public void SelectionStones() {
        if (StoneContainerPrefab) {
            _panels.Clear();
            
            // populate VBox
            RectTransform vBoxTr = transform as RectTransform;
            if (vBoxTr) {
                float rowSize = vBoxTr.rect.height / numberOfContainers;
                RowStone.ClearExistingRockList();
                for (int i = 0; i < numberOfContainers; i++)
                {
                    RowStone panel = Instantiate(StoneContainerPrefab, transform);
                    RectTransform rowTr = panel.transform as RectTransform;
                    if (rowTr) {
                        Vector3 position = rowTr.localPosition;
                        rowTr.localPosition = new Vector3(position.x, position.y - i * rowSize, position.z);
                            
                        rowTr.sizeDelta = new Vector2(rowTr.sizeDelta.x, rowSize);
                        if (registry.Pebbles.Count != 0) panel.SetupRowStone(registry.Pebbles.ToArray(), vBoxTr.rect.width, rowSize);
                    }
                    _panels.Add(panel);
                }
            }
        }
    }
    
    private void OnValidate() {
        if (refresh) {
            SelectionStones();
            refresh = false;
        }
    }
}

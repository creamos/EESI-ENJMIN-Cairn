using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowStone : MonoBehaviour
{
	public SelectPebbleButton StoneButtonPrefab;
	public float minWightButton = 70;
	public float heightButton = 70;

	private static List<Rock> ExistingRockList = new List<Rock>();

	public void SetupRowStone(Rock[] rockList, float panelWight, float panelHeight) {
		if (StoneButtonPrefab) {
			Rock selectedRock = RandomSelectionStone(rockList);
			float wightButton = minWightButton * selectedRock.rockWidth;
			float randWight = panelWight - wightButton;
			float randHeight = panelHeight - heightButton;
			float randX = Random.Range(0, randWight);
			float randY = Random.Range(0, randHeight);
			SelectPebbleButton rockButton = Instantiate(StoneButtonPrefab, transform);
			rockButton.Rock = selectedRock;
			rockButton.UpdateImage();
			RectTransform tr = rockButton.transform as RectTransform;
			if (tr) {
				tr.localPosition = new Vector3(randX, -randY, 0);
				tr.sizeDelta = new Vector2(wightButton, heightButton);
			}
		}
	}

	public static void ClearExistingRockList() {
		ExistingRockList.Clear();
	}

	private Rock RandomSelectionStone(Rock[] rockList) {
		if (ExistingRockList.Count >= rockList.Length) ClearExistingRockList();
		Rock selectedRock = rockList[Random.Range(0, rockList.Length)];
		if (ExistingRockList.Contains(selectedRock)) selectedRock = RandomSelectionStone(rockList);
		else ExistingRockList.Add(selectedRock);
		return selectedRock;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowStone : MonoBehaviour
{
	public SelectPebbleButton StoneButtonPrefab;
	public float minWightButton = 80;
	public float heightButton = 80;

	private static List<Rock> ExistingRockList = new List<Rock>();

	public void SetupRowStone(Rock[] rockList, float rockWight, float rockHeight) {
		if (StoneButtonPrefab) {
			Rock selectedRock = RandomSelectionStone(rockList);
			float wightButton = minWightButton * selectedRock.rockWidth;
			float randX = Random.Range(0, rockHeight - heightButton/2);
			float randY = Random.Range(0, rockWight - wightButton/2);
			SelectPebbleButton rockButton = Instantiate(StoneButtonPrefab, transform);
			rockButton.Rock = selectedRock;
			rockButton.UpdateImage();
			RectTransform tr = rockButton.transform as RectTransform;
			if (tr) {
				Vector3 position = tr.localPosition;
				tr.localPosition = new Vector3(position.x + randX, position.y + randY, 0);
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

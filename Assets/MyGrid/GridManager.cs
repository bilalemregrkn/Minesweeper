using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyGrid
{
	public class GridManager : MonoBehaviour
	{
		public static GridManager Instance { get; private set; }

		[SerializeField] private TileController tilePrefabs;
		[SerializeField] private Vector2 size;
		[SerializeField, Range(0, 5f)] private float distance;
		[SerializeField] private bool changeDistance;
		
		public List<TileController> ListGridController => listGridController;
		[SerializeField] private List<TileController> listGridController;

		private void Awake()
		{
			Instance = this;
		}

		[ContextMenu("Create Grid")]
		public void CreateGrid()
		{
			listGridController.Clear();
			Vector3 gridPosition = Vector3.zero;
			GameObject parent = new GameObject();
			parent.transform.name = "Parent Grid";
			for (int i = 0; i < size.y; i++)
			{
				gridPosition.y = i * distance;
				for (int j = 0; j < size.x; j++)
				{
					gridPosition.x = j * distance;
					TileController tile = Instantiate(tilePrefabs, gridPosition, Quaternion.identity);
					tile.transform.name = $"Grid [{j},{i}]";
					tile.coordinate = new Vector2(j, i);
					tile.transform.SetParent(parent.transform);
					listGridController.Add(tile);
				}
			}

			SetNeighbor();
		}
		
		private void OnValidate()
		{
			if (changeDistance)
				SetDistance(distance);
		}

		private void SetDistance(float newDistance)
		{
			if (listGridController != null)
			{
				if (listGridController.Count != 0)
				{
					Vector3 gridPosition = Vector3.zero;
					var distance = newDistance;
					for (int i = 0; i < size.y; i++)
					{
						gridPosition.y = i * distance;
						for (int j = 0; j < size.x; j++)
						{
							gridPosition.x = j * distance;
							listGridController[(int) ((i * size.y) + j)].transform.position = gridPosition;
						}
					}
				}
			}
		}

#if UNITY_EDITOR
		[ContextMenu("Create Grid as Prefabs")]
		public void CreateGridAsPrefabs()
		{
			GameObject prefabs = Selection.activeObject as GameObject;
			if (prefabs == null) return;
			TileController tileController = prefabs.GetComponent<TileController>();
			if (tileController == null)
			{
				Debug.LogError("Prefabs must be GridController");
				return;
			}

			listGridController.Clear();
			Vector3 gridPosition = Vector3.zero;
			GameObject parent = new GameObject();
			parent.transform.name = "Parent Grid";

			for (int i = 0; i < size.y; i++)
			{
				gridPosition.y = i * distance;
				for (int j = 0; j < size.x; j++)
				{
					gridPosition.x = j * distance;

					GameObject gridGameObject = PrefabUtility.InstantiatePrefab(prefabs) as GameObject;
					TileController tile = gridGameObject.GetComponent<TileController>();
					if (tile == null)
					{
						print("Selection Prefabs must be grid controller");
						return;
					}

					tile.transform.name = $"Grid [{j},{i}]";
					tile.transform.position = gridPosition;
					tile.coordinate = new Vector2(j, i);
					tile.transform.SetParent(parent.transform);
					listGridController.Add(tile);
				}
			}

			SetNeighbor();
		}
#endif

		public TileController GetGrid(Vector2 coordinate)
		{
			foreach (var item in listGridController)
			{
				if (item.coordinate == coordinate)
					return item;
			}

			return null;
		} 


		private void SetNeighbor()
		{
			foreach (var grid in listGridController)
			{
				Vector2 gridCoordinate = grid.coordinate;

				Vector2 gridUpCoordinate = gridCoordinate;
				Vector2 gridDownCoordinate = gridCoordinate;
				Vector2 gridRightCoordinate = gridCoordinate;
				Vector2 gridLeftCoordinate = gridCoordinate;

				gridUpCoordinate.y++;
				gridDownCoordinate.y--;
				gridRightCoordinate.x++;
				gridLeftCoordinate.x--;

				grid.up = GetGrid(gridUpCoordinate);
				grid.down = GetGrid(gridDownCoordinate);
				grid.right = GetGrid(gridRightCoordinate);
				grid.left = GetGrid(gridLeftCoordinate);
			}
		}
	}
}
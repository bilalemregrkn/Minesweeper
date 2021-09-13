using System.Collections.Generic;
using UnityEngine;

namespace MyGrid
{
	public class SetCenterPosition : MonoBehaviour
	{
		[ContextMenu(nameof(SetCenter))]
		private void SetCenter()
		{
			var children = new List<Transform>();

			for (int i = 0; i < transform.childCount; i++)
				children.Add(transform.GetChild(i));

			Vector3 totalPos = Vector3.zero;
			foreach (var item in children)
				totalPos += item.position;

			totalPos /= children.Count;

			var parent = transform.parent;
			foreach (var item in children)
				item.SetParent(parent);

			transform.position = totalPos;
			foreach (var item in children)
				item.SetParent(transform);
		}
	}
}
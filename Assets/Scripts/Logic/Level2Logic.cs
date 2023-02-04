using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
	using Touchable;

	public class Level2Logic : BaseManagerMono<Level2Logic>
	{
		[SerializeField]
		protected List<PopupAnim> popupPrefabs;

		protected int m_prefabsSize = 0;
		protected List<Stack<PopupAnim>> m_popupWindowPools = new List<Stack<PopupAnim>>();

		protected void Awake()
		{
			TryRegisterThis();
			m_prefabsSize = popupPrefabs.Count;
			m_popupWindowPools.Clear();
			foreach (var _ in popupPrefabs)
				m_popupWindowPools.Add(new Stack<PopupAnim>());
		}

		protected override void InitMgrSuccess()
		{
			m_prefabsSize = popupPrefabs.Count;
			m_popupWindowPools.Clear();
			foreach (var _ in popupPrefabs)
				m_popupWindowPools.Add(new Stack<PopupAnim>());
		}


		protected PopupAnim CreatePopupWindow(int index)
		{
			if (0 <= index && index < m_prefabsSize)
			{
				var prefab = popupPrefabs[index];
				var popupAnim = Instantiate(prefab);
				popupAnim.PopupWindowStyleId = index + 1;
				popupAnim.transform.SetParent(transform);
				return popupAnim;
			}
			return null;
		}

		protected PopupAnim GetPopupWindow(int index)
		{
			if (0 <= index && index < m_prefabsSize)
			{
				var pool = m_popupWindowPools[index];
				var popupAnim = pool.Count > 0 ? pool.Pop() : CreatePopupWindow(index);
				popupAnim.gameObject.SetActive(true);
				popupAnim.SetLayerOrderZ(GetPopupWindowTopZ());
				return popupAnim;
			}
			return null;
		}

		public void ReleasePopupWindow(PopupAnim popupWindow)
		{
			var index = popupWindow.PopupWindowStyleId - 1;
			if (0 <= index && index < m_prefabsSize)
			{
				popupWindow.gameObject.SetActive(false);
				m_popupWindowPools[index].Push(popupWindow);
			}
			else
			{
				popupWindow.gameObject.SetActive(false);
				DestroyImmediate(popupWindow);
			}
		}


		public int RandomPopupWindowStyle()
		{
			return Random.Range(0, m_prefabsSize);
		}

		public void ShowNewPopupWindow()
		{
			var popupWindow = GetPopupWindow(RandomPopupWindowStyle());
			popupWindow.SetRandomPosition();
			popupWindow.ShowPopupWindow();
		}


		protected int m_popupWindowTopZ = 10;
		public int GetPopupWindowTopZ()
		{
			m_popupWindowTopZ += 3;
			return m_popupWindowTopZ;
		}
	}
}

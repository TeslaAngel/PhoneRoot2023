using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
	using Touchable;
	using UnityEngine.SceneManagement;

	public class Level3Logic : MonoBehaviour
	{
		#region Singleton

		protected static Level3Logic instance;
		public static Level3Logic Inst => instance;

		protected void RegisterInstThis()
		{
			instance = this;
		}
		protected void UnregisterInstThis()
		{
			if (instance == this)
				instance = null;
		}

		#endregion Singleton


		#region UnityMessage

		protected void Awake()
		{
			RegisterInstThis();

			InitLevel();
		}

		protected void Start()
		{
			StartLevel();
		}

		protected void OnDestory()
		{
			UnregisterInstThis();
		}

		#endregion UnityMessage


		protected void InitLevel()
		{
			InitHackIcons();
			ResetSurvivalHackIconCount();
		}

		protected void StartLevel()
		{
			// ...
		}


		#region WaterDrop Pool

		[SerializeField]
		protected WaterDropAnim waterDropPrefab;

		protected Stack<WaterDropAnim> m_waterDropPool = new Stack<WaterDropAnim>();


		protected WaterDropAnim CreateWaterDrop()
		{
			var waterDrop = Instantiate(waterDropPrefab);
			waterDrop.transform.SetParent(transform);
			return waterDrop;
		}

		public WaterDropAnim GetWaterDrop()
		{
			var pool = m_waterDropPool;
			var waterDrop = pool.Count > 0 ? pool.Pop() : CreateWaterDrop();
			waterDrop.gameObject.SetActive(true);
			return waterDrop;
		}

		public void ReleaseWaterDrop(WaterDropAnim waterDrop)
		{
			waterDrop.gameObject.SetActive(false);
			m_waterDropPool.Push(waterDrop);
		}

		#endregion WaterDrop Pool


		[SerializeField]
		protected List<HackIconAnim> hackIcons = new List<HackIconAnim>();

		[System.Serializable]
		protected struct HackIconCfg
		{
			public Sprite normalSprite;
			public Sprite hitSprite;
		}

		[SerializeField]
		protected List<HackIconCfg> hackIconSpriteConfig;

		protected void InitHackIcons()
		{
			var configSize = hackIconSpriteConfig.Count;
			foreach (var each in hackIcons)
			{
				var cfg = hackIconSpriteConfig[Random.Range(0, configSize)];
				each.SetupNormalAndHitSprite(cfg.normalSprite, cfg.hitSprite);
				each.ResetHp();
			}
		}


		public void OnHitHackIcon(HackIconAnim hackIcon, WaterDropAnim waterDrop)
		{
			waterDrop.HideAndReleaseWaterDrop();
			hackIcon.ProcessHit();
		}


		protected int m_curSurvivalHackIconCount = 0;

		protected void ResetSurvivalHackIconCount()
		{
			m_curSurvivalHackIconCount = hackIcons.Count;
			RefreshProcessBarForDefeat();
		}

		public void OnDefeatHackIcon()
		{
			// 暂时不考虑重复触发
			if (m_curSurvivalHackIconCount > 0)
			{
				--m_curSurvivalHackIconCount;
				RefreshProcessBarForDefeat();
				if (m_curSurvivalHackIconCount <= 0)
					OnDefeatAllHackIcon();
			}
		}


		[SerializeField]
		protected ProcessBarLv5Anim processBarAnim;

		protected void RefreshProcessBarForDefeat()
		{
			processBarAnim.SetProceeBarSprite(m_curSurvivalHackIconCount);
		}


		[SerializeField]
		protected float changeSceneWaitTime = 2f;

		protected void OnDefeatAllHackIcon()
		{
			StartCoroutine(OnDefeatAllHackIcon_Coroutine());
		}

		protected IEnumerator OnDefeatAllHackIcon_Coroutine()
		{
			processBarAnim.StartDropAnim();
			yield return new WaitForSeconds(changeSceneWaitTime);
			GotoNextScene();
		}


		[SerializeField]
		protected string nextSceneName = "Level5.1";

		protected void GotoNextScene()
		{
			SceneManager.LoadScene(nextSceneName);
		}
	}
}

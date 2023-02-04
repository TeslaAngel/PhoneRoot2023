using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
	using Touchable;
	using UnityEngine.SceneManagement;

	public class Level2Logic : MonoBehaviour
	{
		#region Singleton

		protected static Level2Logic instance;
		public static Level2Logic Inst => instance;

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
			InitPopupPools();

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


		#region PopupWindow

		[SerializeField]
		protected List<PopupAnim> popupPrefabs;

		protected int m_prefabsSize = 0;
		protected List<Stack<PopupAnim>> m_popupWindowPools = new List<Stack<PopupAnim>>();

		protected void InitPopupPools()
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
				OnGetPopupWindow();
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
			OnReleasePopupWindow();
		}


		public int RandomPopupWindowStyle()
		{
			return Random.Range(0, m_prefabsSize);
		}

		public void ShowNewPopupWindow(bool center = false)
		{
			var popupWindow = GetPopupWindow(RandomPopupWindowStyle());
			if (center)
			{
				SetPopupCenterPosition(popupWindow);
			}
			else
			{
				//popupWindow.SetRandomPosition();
				SetPopupRandomPosition(popupWindow);
			}
			popupWindow.ShowPopupWindow();
			OnShowNewPopupWindow();
		}


		[SerializeField]
		protected Rect popupPosRange = new Rect(-2, -3, 2, 3);

		public void SetPopupRandomPosition(PopupAnim popupAnim)
		{
			var range = popupPosRange;
			var pos = new Vector3(
				Random.Range(range.x, range.width),
				Random.Range(range.y, range.height),
				popupAnim.transform.position.z);
			popupAnim.transform.position = pos;
		}

		public void SetPopupCenterPosition(PopupAnim popupAnim)
		{
			var range = popupPosRange;
			var pos = new Vector3(
				(range.x + range.width) / 2,
				(range.y + range.height) / 2,
				popupAnim.transform.position.z);
			popupAnim.transform.position = pos;
		}


		protected int m_popupWindowTopZ = 10;
		//protected int m_popupWindowTopZStep = 3;
		protected int m_popupWindowTopZStep = 2;
		public int GetPopupWindowTopZ()
		{
			m_popupWindowTopZ += m_popupWindowTopZStep;
			return m_popupWindowTopZ;
		}


		public void OnShowNewPopupWindow()
		{
			PlaySeAudio(openPopupAudio);
		}

		#endregion PopupWindow


		#region Audio

[SerializeField]
		protected List<AudioSource> audioSources = new List<AudioSource>();

		public void PlaySeAudio(AudioClip audio)
		{
			if (audio == null)
				return;
			foreach (var each in audioSources)
			{
				if (each.clip == audio && !each.isPlaying)
				{
					each.Play();
					return;
				}
			}
			foreach (var each in audioSources)
			{
				if (each.clip == null || !each.isPlaying)
				{
					each.clip = audio;
					each.Play();
					return;
				}
			}
		}

		[SerializeField]
		protected AudioClip lockWechatAudio;
		[SerializeField]
		protected AudioClip openPopupAudio;

		#endregion Audio


		[Space()]

		[SerializeField]
		protected List<TouchableItem> appIconItems = new List<TouchableItem>();
		[SerializeField]
		protected TouchableItem wechatIconItem = null;

		protected WechatIconAnim m_wechatAnim = null;
		[SerializeField]
		protected VagueAnim vagueAnim = null;
		[SerializeField]
		protected GhostAnim ghostAnim = null;
		[SerializeField]
		protected MemoryWarnAnim memoryWarnAnim = null;
		[SerializeField]
		protected GhostTalkAnim ghostTalkAnim = null;


		protected void InitLevel()
		{
			DisableAllAppIconItems();
			wechatIconItem?.SetTouchEnabled(false);
			m_wechatAnim = wechatIconItem?.GetComponent<WechatIconAnim>();

			HideGhost();
			StopGhostAnimFrames();
		}

		protected void EnableAllAppIconItems()
		{
			foreach (var item in appIconItems)
				item?.SetTouchEnabled(true);
		}
		protected void DisableAllAppIconItems()
		{
			foreach (var item in appIconItems)
				item?.SetTouchEnabled(false);
		}


		protected void ShowGhost()
		{
			ghostAnim?.gameObject?.SetActive(true);
		}
		protected void HideGhost()
		{
			ghostAnim?.gameObject?.SetActive(false);
		}

		protected void StartGhostAnimFrames()
		{
			ghostAnim?.SetAnimEnabled(true);
		}
		protected void StopGhostAnimFrames()
		{
			ghostAnim?.SetAnimEnabled(false);
		}

		[Space()]

		[SerializeField]
		protected float ghoseMoveDist = 2f;
		[SerializeField]
		protected float ghoseMoveLeftTime = 1.2f;
		[SerializeField]
		protected float ghoseMoveRightTime = 1.2f;

		protected void StartGhostMoveLeft()
		{
			ghostAnim.StartMoveXAnim(-ghoseMoveDist, ghoseMoveLeftTime);
		}
		protected void StartGhostMoveRight()
		{
			ghostAnim.StartMoveXAnim(ghoseMoveDist, ghoseMoveRightTime);
		}


		protected void StartVagueAnim()
		{
			vagueAnim.StartVague();
		}


		#region MemoryWarn

		[SerializeField, Min(1)]
		protected int memoryWarnLimit1 = 10;
		[SerializeField, Min(1)]
		protected int memoryWarnLimit2 = 20;
		[SerializeField, Min(1)]
		protected int memoryWarnLimit3 = 28;
		[SerializeField, Min(1)]
		protected int memoryWarnLimit4 = 30;

		protected int m_openPopupCount = 0;

		protected bool m_memoryWarned1 = false;
		protected bool m_memoryWarned2 = false;
		protected bool m_memoryWarned3 = false;
		protected bool m_memoryWarned4 = false;

		protected void OnGetPopupWindow()
		{
			++m_openPopupCount;
			RefreshForPopupCountChanged();
		}
		protected void OnReleasePopupWindow()
		{
			--m_openPopupCount;
			RefreshForPopupCountChanged();
		}

		protected int CurrentPopupCountWarnType()
		{
			if (m_openPopupCount < memoryWarnLimit1)
				return 0;
			else if (memoryWarnLimit1 <= m_openPopupCount && m_openPopupCount < memoryWarnLimit2)
				return 1;
			else if (memoryWarnLimit2 <= m_openPopupCount && m_openPopupCount < memoryWarnLimit3)
				return 2;
			else if (memoryWarnLimit3 <= m_openPopupCount && m_openPopupCount < memoryWarnLimit4)
				return 3;
			else
				return 4;
		}
		protected void RefreshForPopupCountChanged()
		{
			var warnType = CurrentPopupCountWarnType();
			if (warnType == 1 && !m_memoryWarned1)
			{
				m_memoryWarned1 = true;
				TipMemoryWarn1();
			}
			else if (warnType == 2 && !m_memoryWarned2)
			{
				m_memoryWarned2 = true;
				TipMemoryWarn2();
			}
			else if (warnType == 3 && !m_memoryWarned3)
			{
				m_memoryWarned3 = true;
				TipMemoryWarn3();
			}
			else if (warnType == 4 && !m_memoryWarned4)
			{
				m_memoryWarned4 = true;
				TipMemoryWarn4();
			}
		}

		protected void TipMemoryWarn1()
		{
			memoryWarnAnim.StartWarn1Anim();
			// ...
		}
		protected void TipMemoryWarn2()
		{
			memoryWarnAnim.StartWarn2Anim();
			// ...
		}
		protected void TipMemoryWarn3()
		{
			memoryWarnAnim.StartWarn3Anim();
			// ...
		}
		protected void TipMemoryWarn4()
		{
			GotoNextScene();
		}

		#endregion MemoryWarn


		[Space()]

		[SerializeField]
		protected string nextSceneName = "Level3";
		
		protected void GotoNextScene()
		{
			SceneManager.LoadScene(nextSceneName);
		}


		#region GhostTalk

		[SerializeField, Multiline(3)]
		protected List<string> ghostTalkTexts = new List<string>();

		protected void PrintGhostTalkText(int index)
		{
			if (0 <= index && index < ghostTalkTexts.Count)
				ghostTalkAnim.SetTextWithPrint(ghostTalkTexts[index]);
		}

		protected void ClearGhostTalkText()
		{
			ghostTalkAnim.ClearText();
		}

		protected void FadeOutGhostTalkText()
		{
			ghostTalkAnim.StartFadeOutText();
		}

		public float PrintTextTotalDuration(int index)
		{
			if (0 <= index && index < ghostTalkTexts.Count)
				return ghostTalkAnim.PrintTextTotalDuration(ghostTalkTexts[index]);
			return 0f;
		}

		#endregion GhostTalk


		#region Timeline

		[Space()]

		[SerializeField]
		protected float waitTimeBeforeStart = 1f;
		[SerializeField]
		protected float waitTimeBeforeLockCage = .2f;
		[SerializeField]
		protected float waitTimeAfterLockCage = 3f;

		[SerializeField]
		protected float waitTimeAfterVague = .5f;

		[SerializeField]
		protected float waitTimeAfterGhostTalk0 = .5f;

		//[SerializeField]
		//protected float waitTimeAfterGhostLeft = 6f;
		[SerializeField]
		protected float waitTimeAfterGhostRight = 1f;

		protected void StartLevel()
		{
			StartCoroutine(StartLevel_Coroutine());
		}

		public IEnumerator StartLevel_Coroutine()
		{
			Debug.Log("Start Level2");

			yield return new WaitForSeconds(waitTimeBeforeStart);
			m_wechatAnim.LockCage();
			yield return new WaitForSeconds(waitTimeBeforeLockCage);
			PlaySeAudio(lockWechatAudio);
			yield return new WaitForSeconds(waitTimeAfterLockCage);

			StartGhostAnimFrames();
			ShowGhost();
			StartGhostMoveLeft();
			StartVagueAnim();
			yield return new WaitForSeconds(vagueAnim.AnimTotalDuration());

			yield return new WaitForSeconds(waitTimeAfterVague);
			PrintGhostTalkText(0);
			yield return new WaitForSeconds(PrintTextTotalDuration(0) + waitTimeAfterGhostTalk0);

			StartGhostMoveRight();
			FadeOutGhostTalkText();
			yield return new WaitForSeconds(ghoseMoveRightTime);
			HideGhost();
			StopGhostAnimFrames();
			ClearGhostTalkText();
			yield return new WaitForSeconds(waitTimeAfterGhostRight);

			ShowNewPopupWindow(true);
			yield return new WaitForSeconds(0.8f);
			ShowNewPopupWindow();
			yield return new WaitForSeconds(0.3f);
			ShowNewPopupWindow();
			yield return new WaitForSeconds(0.15f);
			ShowNewPopupWindow();
			yield return new WaitForSeconds(0.15f);
			ShowNewPopupWindow();

			yield return new WaitForSeconds(0.1f);
			EnableAllAppIconItems();
		}

		#endregion Timeline


		// TODO : ´ò×Ö»úÒôÐ§
	}
}

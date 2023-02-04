using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
	using Touchable;

	public class MemoryWarnAnim : MonoBehaviour
	{
		[SerializeField]
		protected SpriteItemAnimData radLightAnimData;

		[SerializeField]
		protected float warn1Alpha = .5f;
		[SerializeField]
		protected float warn1Time = .5f;

		[SerializeField]
		protected float warn2Alpha = .8f;
		[SerializeField]
		protected float warn2Time = .5f;

		[SerializeField]
		protected float warn3Alpha = 1f;
		[SerializeField]
		protected float warn3Time = .5f;


		public void StartWarn1Anim()
		{
			StartCoroutine(StartWarnAnim_Coroutine(warn1Alpha, warn1Time));
		}
		public void StartWarn2Anim()
		{
			StartCoroutine(StartWarnAnim_Coroutine(warn2Alpha, warn2Time));
		}
		public void StartWarn3Anim()
		{
			StartCoroutine(StartWarnAnim_Coroutine(warn3Alpha, warn3Time));
		}

		public IEnumerator StartWarnAnim_Coroutine(float alpha, float time)
		{
			radLightAnimData
				.FinishAllAnims()
				.SetValueImmedi(TargetType.Alpha, 0);

			radLightAnimData
				.StartValueAnim(TargetType.Alpha, alpha, time);
			yield return new WaitForSeconds(time);
			radLightAnimData
				.StartValueAnim(TargetType.Alpha, 0, time);
			yield return new WaitForSeconds(time);
			radLightAnimData
				.StartValueAnim(TargetType.Alpha, alpha, time);
			yield return new WaitForSeconds(time);
			radLightAnimData
				.StartValueAnim(TargetType.Alpha, 0, time);
		}
	}
}

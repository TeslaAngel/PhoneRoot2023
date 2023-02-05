using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    using Touchable;

    public class StaffAnim : MonoBehaviour
	{
		[SerializeField]
		protected SpriteItemAnimData animData;

		[SerializeField]
		protected float fadeInTime = 2f;

		protected void Start()
		{
			animData
				.FinishAllAnims()
				.SetValueImmedi(TargetType.Alpha, 0)
				.StartValueAnim(TargetType.Alpha, 1, fadeInTime);
		}
	}
}

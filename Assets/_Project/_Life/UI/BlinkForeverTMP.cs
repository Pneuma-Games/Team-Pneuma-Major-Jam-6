using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Life
{
    public class BlinkForeverTMP : MonoBehaviour
    {
        void Start()
        {
            GetComponent<TextMeshProUGUI>().DOFade(0.0f, 0.25f).SetEase(Ease.InOutQuint).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
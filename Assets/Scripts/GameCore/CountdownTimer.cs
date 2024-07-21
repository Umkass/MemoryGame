using System;
using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class CountdownTimer : MonoBehaviour
    {
        public Action OnTimerFinish;
        private Coroutine _timerCoroutine;

        public void StartTimer(int time, Action<int> textUpdater)
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);
            _timerCoroutine = StartCoroutine(TimerCoroutine(time, textUpdater));
        }

        private IEnumerator TimerCoroutine(int time, Action<int> textUpdater)
        {
            int remainingTime = time;
            while (remainingTime > 0)
            {
                textUpdater(remainingTime);
                yield return new WaitForSeconds(1);
                remainingTime--;
            }

            textUpdater(remainingTime);
            OnTimerFinish?.Invoke();
        }
    }
}
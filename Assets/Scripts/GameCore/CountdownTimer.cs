using System;
using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class CountdownTimer : MonoBehaviour
    {
        public Action OnTimerFinish;
        public void StartTimer(int time, Action<int> textUpdater) => 
            StartCoroutine(TimerCoroutine(time, textUpdater));

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
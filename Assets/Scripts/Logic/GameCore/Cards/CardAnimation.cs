using System;
using System.Collections;
using Data;
using UnityEngine;

namespace Logic.GameCore.Cards
{
    public class CardAnimation : MonoBehaviour
    {
        [SerializeField] private Animation _animation;
        public Action<bool> OnAnimationStateChanged;

        public void PlayCardTurnOver(bool isRevealed)
        {
            OnAnimationStateChanged?.Invoke(true);
            _animation.Play(isRevealed
                ? AnimationNames.CardFaceDown
                : AnimationNames.CardFaceUp);

            StartCoroutine(WaitForAnimationEnd());
        }

        private IEnumerator WaitForAnimationEnd()
        {
            yield return new WaitForSeconds(_animation.clip.length);
            OnAnimationStateChanged?.Invoke(false);
        }
    }
}
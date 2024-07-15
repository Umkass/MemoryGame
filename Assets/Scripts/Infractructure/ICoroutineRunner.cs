using System.Collections;
using UnityEngine;

namespace Infractructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
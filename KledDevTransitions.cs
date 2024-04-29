using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KledDev.Transitions
{
    public class KledDevTransitions : MonoBehaviour
    {
        static KledDevTransitions instance;
        static void CreateInstance()
        {
            GameObject @object = new("KledDevUtils");
            @object.AddComponent<KledDevTransitions>();
            instance = @object.GetComponent<KledDevTransitions>();
        }
        static KledDevTransitions GetInstance()
        {
            if (instance == null)
            {
                CreateInstance();
            }
            return instance;
        }

        #region Call
        /// <summary>
        /// Todas as janelas que forem utilizar efeitos de fade precisam ter um CanvasGroup.
        /// </summary>
        /// <param name="desactiveInTheEnd" >Deseja que a janela seja desativada no fim do efeito?</param>
        public static void FadeEffect(CanvasGroup screen,float startAlpha, float endAlpha, float duration, Action action)
        {
            GetInstance().StartCoroutine(Fade(screen, startAlpha,endAlpha,duration,action));
        }
        public static void ZoomEffect(Transform screen, float startZoom, float endZoom, float duration, Action action)
        {
            GetInstance().StartCoroutine(Zoom(screen, startZoom, endZoom, duration, action));
        }
        #endregion

        #region Routines
        static IEnumerator Fade(CanvasGroup group, float startAlpha, float endAlpha, float duration, Action action)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                group.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            group.alpha = endAlpha; // Garante que o valor final seja alcançado

            if (group.alpha == endAlpha)
            {
                if(action != null)
                {
                    action.Invoke();
                }
            }
        }

        static IEnumerator Zoom(Transform screen, float startZoom, float endZoom, float duration, Action action)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                screen.localScale = Vector3.one*Mathf.Lerp(startZoom, endZoom, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            screen.localScale = Vector3.one * endZoom;

            if (screen.localScale.x == endZoom)
            {
                if (action != null)
                {
                    action.Invoke();
                }
            }
        }

        #endregion


    }
}
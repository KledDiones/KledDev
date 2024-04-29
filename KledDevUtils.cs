using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KledDev.Utils
{
    public class KledDevUtils : MonoBehaviour
    {
        static KledDevUtils instance;

        static void CreateInstance()
        {
            GameObject @object = new("KledDevUtils");
            @object.AddComponent<KledDevUtils>();
            instance = @object.GetComponent<KledDevUtils>();
        }
        static KledDevUtils GetInstance()
        {
            if (instance == null)
            {
                CreateInstance();
            }
            return instance;
        }

        public static void WaitXSecondsForPlayAction(float xSeconds, Action playAction)
        {
            GetInstance().StartCoroutine(WaitXSecondsForPLayActionCoroutine(xSeconds,playAction));
        }
        public static string RemoveAccents(string text)
        {
            string decomposed = text.Normalize(NormalizationForm.FormD);
            Regex regex = new Regex(@"\p{M}");
            string noAccents = regex.Replace(decomposed, string.Empty);
            return noAccents;
        }
        public static void ClearChilds(Transform content)
        {
            for(int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
        }
        
        public static void FillContent(List<int> objIDList, Transform content,GameObject viewObject) {
        
            for(int i = 0; i < objIDList.Count; i++)
            {
                GameObject view = Instantiate(viewObject, content);
                view.GetComponent<View>().ID = objIDList[i];
            }
        }

        static IEnumerator WaitXSecondsForPLayActionCoroutine(float xSeconds, Action playAction)
        {
            yield return new WaitForSeconds(xSeconds);
            playAction.Invoke();
        }
    }
}

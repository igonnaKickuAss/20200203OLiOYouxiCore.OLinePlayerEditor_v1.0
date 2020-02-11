using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace OLiOYouxi
{
    using Sentence = List<LinePlayerController.Letter>;
    using Debug = OSystem.DebugFather;
    using Button = OLiOYouxiCore.OAttributes.ButtonAttribute;
    using MinVal = OLiOYouxiCore.OAttributes.MinValueAttribute;
    using MaxVal = OLiOYouxiCore.OAttributes.MaxValueAttribute;
    internal class LinePlayerController : MonoBehaviour
    {
        #region -- Struct --
        internal struct Letter
        {
            public char value;
            public float remain;
        }
        #endregion

        #region -- Private Data --
        private Sentence sentence = null;
        private TextController textController = null;
        private Coroutine coroutine = null;
        private Dictionary<float, WaitForSeconds> waitForSecondsMapper = null;

        #endregion

        #region -- Public Data --
        public Text text = null;
        #endregion

        #region -- Test APIMethods --
        [Button("测试播放按钮")]
        public void TestSentencePlay() => coroutine = coroutine ?? (coroutine = StartCoroutine(PlaySentence()));
        #endregion

        #region -- MONO APIMethods --
        private void Awake()
        {
            waitForSecondsMapper = new Dictionary<float, WaitForSeconds>();
            textController = new TextController();
            Letter letter1 = new Letter { value = '啊', remain = .09f };
            Letter letter2 = new Letter { value = '波', remain = .9f };
            Letter letter3 = new Letter { value = '吃', remain = 1.9f };
            sentence = new Sentence();
            for (int i = 0, length = 2; i < length; i++) sentence.Add(letter1);
            for (int i = 0, length = 4; i < length; i++) sentence.Add(letter2);
            for (int i = 0, length = 6; i < length; i++) sentence.Add(letter3);
        }
        private void Update()
        {
            if (textController.HasNoChange) return;
            text.text = textController.OutValue();
        }
        #endregion

        #region -- Private APIMethods --
        private IEnumerator PlaySentence()
        {
            Sentence letters = sentence;
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
            for (int i = 0, count = letters.Count; i < count; i++)
            {
                Letter letter = letters[i];
                WaitForSeconds seconds = waitForSecondsMapper.ContainsKey(letter.remain) ?
                    waitForSecondsMapper[letter.remain] :
                    waitForSecondsMapper[letter.remain] = new WaitForSeconds(letter.remain);
                yield return seconds;
                //TODO.. update.. text value
                //..
                textController.AppendValue(ref letter.value);
                yield return waitForEndOfFrame;
            }
            coroutine = null;
        }

        #endregion

        #region -- TextController --
        private class TextController
        {
            #region -- Private Data --
            private StringBuilder stringBuilder = null;
            #endregion

            #region -- VAR --
            int before = 0;
            int after = 0;
            #endregion

            #region -- New --
            public TextController() => stringBuilder = new StringBuilder();

            #endregion

            #region -- Public ShotC --
            public bool HasNoChange => after == before;
            #endregion

            #region -- Public APIMethods --
            public void AppendValue(ref char value)
            {
                stringBuilder.Append(value);
                after = 1;
            }
            public string OutValue()
            {
                after = 0;
                return stringBuilder.ToString();
            }
            public void ClearValue() => stringBuilder.Clear();
            #endregion
        }
        #endregion
    }

}


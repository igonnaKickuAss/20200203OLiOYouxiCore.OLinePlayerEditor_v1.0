using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace OLiOYouxiCore.OLinePlayer
{
    using Button = OAttributes.ButtonAttribute;
    using MinVal = OAttributes.MinValueAttribute;
    using MaxVal = OAttributes.MaxValueAttribute;
    using WordCollection = List<_WordScriptableObj>;
    using Sentence = _SentenceScriptableObj;

    internal class LinePlayerTest : MonoBehaviour
    {
        #region -- Private Data --
        private TextController textController = null;
        private Dictionary<float, WaitForSeconds> waitForSecondsMapper = null;
        private WordCollection words = null;
        private Coroutine coroutine = null;
        #endregion

        #region -- Public Data --
        public Text text = null;
        public _ChatboxScriptableObj chatBox = null;
        [MinVal(0), MaxVal(9)] public int selectedConversation = 0;
        [MinVal(0), MaxVal(9)] public int selectedSentence = 0;
        [MinVal(0), MaxVal(9)] public int selectedWord = 0;

        [Button("测试播放指定句子")]
        public void TestSentencePlay() => coroutine = StartCoroutine(PlaySentence(words[selectedWord].Value[selectedSentence]));

        #endregion

        #region -- MONO APIMethods --
        void Start()
        {
            textController = TextController.ForNewOrExistingDisplayText();
            waitForSecondsMapper = new Dictionary<float, WaitForSeconds>();
            words = chatBox.Value[selectedConversation].Value;
        }
        
        void Update()
        {
            if (textController.HasNoChanged) return;
            text.text = textController.OutValue();
        }
        
        #endregion

        #region -- Private APIMethods --
        private IEnumerator PlaySentence(Sentence sentence)
        {
            WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
            for (int i = 0, count = sentence.Value.Count; i < count; i++)
            {
                Letter letter = sentence.Value[i];
                WaitForSeconds seconds = waitForSecondsMapper.ContainsKey(letter.remain) ?
                    waitForSecondsMapper[letter.remain] :
                    waitForSecondsMapper[letter.remain] = new WaitForSeconds(letter.remain);
                yield return seconds;
                textController.AppendValue(ref letter.value);
                yield return endOfFrame;
            }
            char space = '\n';
            textController.AppendValue(ref space);
        }
        #endregion

        private class TextController
        {
            #region -- Private Data --
            private StringBuilder stringBuilder = null;
            #endregion

            #region -- VAR --
            int before = 0;
            int after = 0;
            #endregion

            #region -- Public ShotC --
            public bool HasNoChanged => before == after;

            #endregion

            #region -- 单例 --
            static private TextController instance = null;
            private TextController() => this.stringBuilder = new StringBuilder();
            internal static TextController ForNewOrExistingDisplayText()
                => instance == null ? instance = new TextController() : instance;
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
    }

}

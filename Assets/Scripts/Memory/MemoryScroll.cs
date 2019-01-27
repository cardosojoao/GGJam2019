using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Memory
{
    public class MemoryScroll : MonoBehaviour
    {
        private static Dictionary<string, Vector2> _directionVector = new Dictionary<string, Vector2>
        {
            { "Vertical", new Vector2(0,1) },
            { "Horizontal", new Vector2(1,0) },
        };

        public bool Scrolling { get; private set; }
        public RectTransform ScrollTransform;
        public RectTransform ViewportTransform;
        public MemorySlice MemorySlice;
        public float ScrollTime = 0.6f;
        public MemoryReel Reel;
        public Fader ContinueFader;

        [SerializeField, Range(0, 3)]
        private int _page;
        public int Page
        {
            get { return _page; }
            set
            {
                var nextPage = Mathf.Clamp(value, 0, Reel.SpriteCount - 1);
                if (nextPage != _page)
                {
                    _page = nextPage;
                    SetPage();
                }
            }
        }

        private Coroutine _scrollRoutine = null;

        private int _prevPage;
        private Vector2 _prevDirectionVector;
        private void OnEnable()
        {
            Scrolling = false;
#if UNITY_EDITOR
            _prevPage = _page;
#endif
        }
        private void Update()
        {
            if (Scrolling)
                return;


            Vector2 directionVector = Vector2.zero;
            foreach (KeyValuePair<string, Vector2> direction in _directionVector)
            {
                var axisInput = Input.GetAxisRaw(direction.Key);
                directionVector += axisInput * direction.Value;
            }

            if (_prevDirectionVector.x * directionVector.x > 0)
                return;

            ChangePageDirection(directionVector);
            _prevDirectionVector = directionVector;

#if UNITY_EDITOR
            if (directionVector == Vector2.zero && _prevPage != _page)
                SetPage();
            _prevPage = _page;
#endif
        }

        private void SetPage()
        {
            if (_scrollRoutine != null)
            {
                StopCoroutine(_scrollRoutine);
                _scrollRoutine = null;
            }

            if (!Application.isPlaying || !gameObject.activeInHierarchy)
                SetScrollX(_page);
            else
                _scrollRoutine = StartCoroutine(ScrollToPage(_page));

        }

        private void ChangePageDirection(Vector2 directionVector)
        {
            if (directionVector == Vector2.zero)
                return;
            if (directionVector.x > 0)
                Page += 1;
            else
                Page -= 1;
        }

        private IEnumerator ScrollToPage(int page)
        {
            if (page == Reel.SpriteCount - 1)
                ContinueFader.FadeIn();
            if (page < 0 || page >= Reel.SpriteCount)
                yield break;

            var startX = ScrollTransform.anchoredPosition.x;
            var targetX = GetPageX(page);

            var startTime = Time.time;
            if (page < Reel.SliceArray.Length)
                Reel.SliceArray[page].Revealed = true;
            while (Time.time - startTime < ScrollTime)
            {
                var deltaTime = (Time.time - startTime) / ScrollTime;
                var currentPosition = ScrollTransform.anchoredPosition;
                var nextX = EaseOutQuad(startX, targetX, deltaTime);
                currentPosition.x = nextX;
                ScrollTransform.anchoredPosition = currentPosition;
                yield return null;
            }

            if (page == Reel.SpriteCount - 1)
                ContinueFader.FadeIn();

            _scrollRoutine = null;
        }


        private void SetScrollX(int page)
        {
            var targetX = GetPageX(page);
            var currentPosition = ScrollTransform.anchoredPosition;
            currentPosition.x = targetX;
            ScrollTransform.anchoredPosition = currentPosition;
        }

        private float GetPageX(int page)
        {
            var startX = ScrollTransform.anchoredPosition.x;
            var vpWidth = ViewportTransform.rect.width;
            var containerWidth = ScrollTransform.rect.width;
            var sliceWidth = MemorySlice.LayoutElement.minWidth;
            return vpWidth / 2f - (page + 0.5f) * sliceWidth;
        }


        private static float EaseOutQuad(float start, float end, float value)
        {
            if (value <= 0)
                return start;

            if (value >= 1)
                return end;

            end -= start;
            return -end * value * (value - 2) + start;
        }

    }
}

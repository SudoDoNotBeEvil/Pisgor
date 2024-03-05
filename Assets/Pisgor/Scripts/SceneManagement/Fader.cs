using System.Collections;
using UnityEngine;

using UnityEngine.UI;

namespace CasuShop.SceneManagement {

    [RequireComponent(typeof(CanvasGroup))]
    public class Fader : MonoBehaviour {
        [SerializeField] CanvasGroup _canvasGroup = null;
        [SerializeField] float _alphaOnStart = 1.0f;
        [SerializeField] bool _fadeInOnStart = true;
        [SerializeField] float _fadeDurationDefault = 0.2f;


        void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();

            _canvasGroup.alpha = _alphaOnStart;
            if (_fadeInOnStart)
                StartFade(false);
        }

        public void StartFade(bool fadeState = true,
                              float fadeTime = -1) {
            if (fadeTime == -1)
                fadeTime = _fadeDurationDefault;

            StopAllCoroutines();

            if (fadeState)
                StartCoroutine(CFadeOut(fadeTime));
            else
                StartCoroutine(CFadeIn(fadeTime));
        }



        [ContextMenu("FadeOut")]
        private void FadeOutTest() {
            StartFade(true);
        }

        [ContextMenu("FadeIn")]
        private void FadeInTest() {
            StartFade(false);
        }


        public IEnumerator CFadeOut(float fadeTime = -1) {
            if (fadeTime == -1)
                fadeTime = _fadeDurationDefault;

            while (_canvasGroup.alpha < 1) {
                _canvasGroup.alpha += Time.deltaTime / fadeTime;
                yield return null;
            }
            _canvasGroup.alpha = 1;
        }

        public IEnumerator CFadeIn(float fadeTime = -1) {
            if (fadeTime == -1)
                fadeTime = _fadeDurationDefault;

            while (_canvasGroup.alpha > 0) {
                _canvasGroup.alpha -= Time.deltaTime / fadeTime;
                yield return null;
            }
            _canvasGroup.alpha = 0;
        }

    }

}
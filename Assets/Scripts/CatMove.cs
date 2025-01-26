using System.Collections;
using UnityEngine;

public class CatMove : MonoBehaviour {
    [SerializeField] private Vector2 newPos;
    private Vector2 startPos;
    private RectTransform rectTransform;
    [SerializeField] private GameEvent onCatShow;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        StartCoroutine(RandomlyMove());
    }

    private IEnumerator RandomlyMove() {
        while (true) {
            float waitTime = Random.Range(10f, 60f);
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(Move(startPos, newPos));
        }
    }

    private IEnumerator Move(Vector2 start, Vector2 destination) {
        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            rectTransform.anchoredPosition = Vector2.Lerp(start, destination, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = destination;
        if(onCatShow) onCatShow.Raise();

        yield return new WaitForSeconds(2f);

        elapsedTime = 0f;
        while (elapsedTime < duration) {
            rectTransform.anchoredPosition = Vector2.Lerp(destination, start, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = start;
    }
}

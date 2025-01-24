using System.Collections;
using UnityEngine;

/// <summary>
/// Thank you for trying on my plugin! 
/// Here is a quick summary of its functionality.
/// 
/// This script handles the sliding functionality of a UI element. 
/// Allows for vertical or horizontal movement, manages menu visibility, 
/// and ensures smooth transitions between positions.
/// 
/// Place it on the ui element you want to apply the animation to.
/// 
/// *The panelSliderButtonManager is the scriptable object attached in the folder.
/// </summary>


public class PanelSlider : MonoBehaviour {
    [Header("Movement Settings")]
    [Tooltip("If true, the panel will move vertically. " +
        "If false, it will move horizontally.")]
    [SerializeField] private bool moveVertically;
    [Tooltip("The offset distance the panel moves.")]
    [SerializeField] private float offset = 200;

    [Header("UI Elements")]
    [Tooltip("The CanvasGroup to diable when panel is moving (This prevents " +
        "from letting user to click button while the panel is moving).")]
    [SerializeField] private CanvasGroup canva;
    [SerializeField] private PanelSliderButtonManager panelSliderButtonManager;

    [Header("Configuration")]
    [Tooltip("Speed at which the panel moves.")]
    [SerializeField] private float movingSpeed = 0.05f;
    [Tooltip("Check if this menu is alredy at open state.")]
    [SerializeField] private bool menuOpened;
    [SerializeField] private bool swapStartingPosWithDesPos;

    private RectTransform rt;
    private Vector2 startingPosition;
    private Vector2 destinationPosition;
    private float timeCount;
    private IEnumerator currentMoveCoroutine;

    private void Awake() {
        rt = GetComponent<RectTransform>();
    }

    private void Start() {
        // Store the starting position of the RectTransform
        startingPosition = rt.anchoredPosition;

        Vector2 side = new(
            Mathf.Clamp(startingPosition.x, -1, 1), 
            Mathf.Clamp(startingPosition.y, -1, 1)
        );

        if (side == Vector2.zero) side = Vector2.one;

        // Calculate destination position for y
        destinationPosition = new Vector2(-offset * side.x, startingPosition.y);

        if (moveVertically) {
            // Calculate destination position for x
            destinationPosition = new Vector2(startingPosition.x, -offset * side.y);
        }

        if (swapStartingPosWithDesPos) {
            (destinationPosition, startingPosition) = (startingPosition, destinationPosition);
        }
    }

    public void StartMoving() {
        if (currentMoveCoroutine != null) {
            return;
        }

        // Toggle main menu buttons
        if (canva != null) {
            panelSliderButtonManager.ToggleCanvasGroup(canva);
        }

        // Check if the menu should be closed or open
        if (!menuOpened) {
            // Check if there is other open menu
            PanelSlider otherMenu = PanelSliderManager.instance.GetOpenedMenu();
            if (otherMenu != null) {
                // Hide the other open menu
                panelSliderButtonManager.ToggleMenu(otherMenu);
            }

            // Set new open menu to the open one
            PanelSliderManager.instance.SetOpenedMenu(this);

            // Open the new menu
            currentMoveCoroutine = SlowlyMove(destinationPosition);
            StartCoroutine(currentMoveCoroutine);
            menuOpened = true;
            return;
        }

        menuOpened = false;

        // Close now open menu
        currentMoveCoroutine = SlowlyMove(startingPosition);
        StartCoroutine(currentMoveCoroutine);
        PanelSliderManager.instance.SetOpenedMenu(null);
    }

    private IEnumerator SlowlyMove(Vector2 destination) {
        timeCount = 0;

        // Loop while the menu to open is not yet at the destination
        while (CanMove(destination)) {
            float t = timeCount * movingSpeed;

            // Calculate slowly way to the destination
            Vector2 finalVector = Vector2.Lerp(
                rt.anchoredPosition,
                destination,
                t
            );

            // Set new position of the menu
            rt.anchoredPosition = finalVector;

            timeCount += Time.deltaTime;
            yield return null;
        }

        // Toggle main menu buttons
        if (canva != null) {
            panelSliderButtonManager.ToggleCanvasGroup(canva);
        }
        currentMoveCoroutine = null;
    }

    private bool CanMove(Vector2 destination) {
        return 
            Mathf.Abs(Vector2.Distance(rt.anchoredPosition, destination)) > 10;
    }
}

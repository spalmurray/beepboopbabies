using TheKiwiCoder;
using UnityEngine;

public class DisplayDialog : DecoratorNode
{
    public Sprite emotionSprite;
    protected override void OnStart() {
        context.babyState.uiController.EnableDialogBox(emotionSprite);
    }
    protected override void OnStop() {
        context.babyState.uiController.DisableDialogBox();
    }

    protected override State OnUpdate()
    {
        return child.Update();
    }
}

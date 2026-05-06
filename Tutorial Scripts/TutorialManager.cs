using SubmarineHunt.Core;

public class TutorialManager : ALevelManager
{
    protected override GameScene whatIam => GameScene.Tutorial;

    public void SkipTutorial()
    {
        SoundManager.Instance.StopAllSound();
        GameManager.Instance.LoadNextScene(whatIam);
    }
}
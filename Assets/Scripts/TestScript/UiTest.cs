using UnityEngine;

namespace TestScript
{
    public class UiTest : MonoBehaviour
    {
        [SerializeField] private ScoreScriptable scoreScriptable;
        
        
        [ContextMenu("Draw")]
        public void Test_Draw()
        {
            scoreScriptable.GameOver();
        }
        
        [ContextMenu("Winner_p1 ")]
        public void Test_w_p1()
        {
            scoreScriptable.Player1Score = 10;
            scoreScriptable.Player2Score = 0;
            
            scoreScriptable.GameOver();
        }
        
        [ContextMenu("Winner_p2 ")]
        public void Test_w_p2()
        {
            scoreScriptable.Player1Score = 0;
            scoreScriptable.Player2Score = 10;
            
            scoreScriptable.GameOver();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UI_with_controller : MonoBehaviour
{

    enum ButtonSelected
    {
        Button_None,
        Button_Start,
        Button_Rematch,
        Button_Exit
    }

    public enum Screen
    {
        Screen_Start,
        Screen_Finish
    }

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    GameSceneManage gameSceneManage;

    public Button start;
    public Button rematch;
    public Button exit;

    ButtonSelected selected_butt = ButtonSelected.Button_None;
    public Screen screen = Screen.Screen_Start;

    // Start is called before the first frame update
    void Start()
    {
        gameSceneManage = GetComponent<GameSceneManage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
        state = GamePad.GetState((PlayerIndex)0);

        if (state.ThumbSticks.Left.X > 0.95f) //Right
        {
            if(screen == Screen.Screen_Start)
                HandleStartButt();

            else if(screen == Screen.Screen_Finish)
            {
                EventSystem.current.SetSelectedGameObject(exit.gameObject);
                selected_butt = ButtonSelected.Button_Exit;
            }
        }

        if (state.ThumbSticks.Left.X < -0.95f) //Left
        {
            if (screen == Screen.Screen_Start)
                HandleStartButt();

            else if(screen == Screen.Screen_Finish)
            {
                EventSystem.current.SetSelectedGameObject(rematch.gameObject);
                selected_butt = ButtonSelected.Button_Rematch;
            }
        }

        if (state.Buttons.A == ButtonState.Pressed)
        {
            if (gameSceneManage)
            {
                switch (selected_butt)
                {
                    case ButtonSelected.Button_Start:
                        gameSceneManage.LoadStoryScene();
                        break;
                    case ButtonSelected.Button_Rematch:
                        gameSceneManage.LoadMainScene();
                        break;
                    case ButtonSelected.Button_Exit:
                        gameSceneManage.QuitApplication();
                        break;
                }
            }
            
        }

    }


    void HandleStartButt()
    {
        if (selected_butt == ButtonSelected.Button_None)
        {
            EventSystem.current.SetSelectedGameObject(start.gameObject);
            selected_butt = ButtonSelected.Button_Start;
        }
    }
}

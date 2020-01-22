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

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;


    public Button start;

    ButtonSelected selected_butt = ButtonSelected.Button_None;

    // Start is called before the first frame update
    void Start()
    {
        
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

        if (state.ThumbSticks.Left.X > 0.95f)
        {
            HandleStartButt();
            Debug.Log("Input Right");
        }

        if (state.ThumbSticks.Left.X < -0.95f)
        {
            HandleStartButt();
            Debug.Log("Input Left");
        }

        if (state.Buttons.A > ButtonState.Pressed)
        {
            
        }

    }


    void HandleStartButt()
    {
        if (selected_butt == ButtonSelected.Button_None)
        {
            EventSystem.current.SetSelectedGameObject(start.gameObject);
            selected_butt = ButtonSelected.Button_Start;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            selected_butt = ButtonSelected.Button_None;
        }
    }
}

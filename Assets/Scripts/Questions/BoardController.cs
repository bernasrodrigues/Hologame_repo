using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    #region State_Data
    public BoardState currentState;
    public BoardState questionState = new BoardQuestionState();
    public BoardState waitingState = new BoardWaitingState();
    public BoardState offState = new BoardQuestionState();
    #endregion State_Data

    public QuestionUI questionUI;

    public List<QuestionData> questions;

    

    public float averageTimeIntervalPerQuestion = 20f;      // average time interval per question
    public float questionAnswerTime = 60f;                  // time the player has to answer the question
    public int maxFailedAnswers = 3;                        // maximum number of questions the player can fail
    public int maxQuestionsInSession = 5;                   // maimum number of questions a player is given is each scene


    [ReadOnly]
    public int rightAnswers = 0;
    [ReadOnly]
    public int failedAnswers = 0;
    [ReadOnly]
    public int answeredQuestions = 0;

    [ReadOnly]
    public QuestionData activeQuestion;

    private bool loggerOn = false;
    private Logger logger;



    // Start is called before the first frame update
    void Start()
    {
        logger = new Logger(loggerOn);

        ChangeState(waitingState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }



    public void QuestionCheck(int index)                                       // check if the right answer has been selected
    {
        if (index == activeQuestion.correctAnswerIndex)
        {
            AnswerGuessCorrect();
        }
        else
        {
            AnswerGuessFail();
        }

    }


    public void AnswerGuessFail()                                      // if the answer selecterd was wrong
    {
        logger.Log("Incorrect answer.");
        failedAnswers += 1;

        AfterQuestionCheck();
    }


    private void AnswerGuessCorrect()
    {
        logger.Log("Correct answer.");
        rightAnswers += 1;

        AfterQuestionCheck();
    }                               // if the answer selected was right




    private void AfterQuestionCheck()                                       // check after answer calculation is done
    {
        if (failedAnswers >= maxFailedAnswers)
        {
            //TODO
            return;
        }
        
        if (answeredQuestions >= maxQuestionsInSession)
        {
            //TODO
            ChangeState(offState);
            return;
        }


        ChangeState(waitingState);


    }

    public void SetQuestion(int index = -1)                            // check
                                                                       // -1 is the default value for 
    {

        if (index == -1)
        {
            index = Random.Range(0, questions.Count);                         //  get random question from the question list

        }

        activeQuestion = questions[index];            //  set active question to question list
        questions.RemoveAt(index);                                            // remove the question from the question list


        questionUI.setQuestion(activeQuestion);
    }


    public void ChangeState(BoardState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState(this);
    }

}



#region BoardStates
public abstract class BoardState
{
    public abstract void EnterState(BoardController questionBoardController);
    public abstract void UpdateState();
    public abstract void ExitState();
}


public class BoardQuestionState : BoardState                        // State where a question is active and the user has to asnwer the question
{
    public BoardController questionBoardController;
    public float timeInQuestion;


    public override void EnterState(BoardController questionBoardController)
    {
        questionBoardController.questionUI.gameObject.SetActive(true);


        this.questionBoardController = questionBoardController;
        timeInQuestion = 0f;

        questionBoardController.SetQuestion();

    }
    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        if (timeInQuestion >= questionBoardController.questionAnswerTime)        // if reached timeLimit
        {
            questionBoardController.AnswerGuessFail();
        }
        else
        {
            timeInQuestion += Time.deltaTime;
        }
    }
}

public class BoardOffState : BoardState                                 //State where the question board is inactive
{
    public BoardController questionBoardController;


    public override void EnterState(BoardController questionBoardController)
    {
        this.questionBoardController = questionBoardController;
        questionBoardController.questionUI.gameObject.SetActive(false);

    }
    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }
}


public class BoardWaitingState : BoardState                                 // State the board is waiting for a new question
{
    public BoardController questionBoardController;
    public float waitingTime;
    public float timeToWait;

    public override void EnterState(BoardController questionBoardController)
    {
        this.questionBoardController = questionBoardController;
        waitingTime = 0f;
        timeToWait = GaussianRandom.NextGaussian(questionBoardController.averageTimeIntervalPerQuestion);

        questionBoardController.questionUI.gameObject.SetActive(false);
    }
    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        if (waitingTime >= timeToWait)
        {
            questionBoardController.ChangeState(questionBoardController.questionState);
        }
        waitingTime += Time.deltaTime;

    }

    public static class GaussianRandom
    {
        private static bool hasSpare;
        private static float spare;

        public static float NextGaussian(float mean)
        {
            if (hasSpare)
            {
                hasSpare = false;
                return mean + spare;
            }
            else
            {
                float u, v, s;
                do
                {
                    u = 2.0f * Random.value - 1.0f;
                    v = 2.0f * Random.value - 1.0f;
                    s = u * u + v * v;
                } while (s >= 1.0f || s == 0.0f);

                s = Mathf.Sqrt(-2.0f * Mathf.Log(s) / s);
                spare = v * s;
                hasSpare = true;
                return mean + u * s;
            }
        }
    }
}
#endregion BoardStates
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public QuestionDatabase questionDatabase;
    private int level;
    private QuestionSet currentQuestionSet;

    private Question currentQuestion;
    private int currentQuestionIndex;

    private int correctAnswers;

    [SerializeField]
    private Transform questionPanel;

    [SerializeField]
    private Transform answerPanel;

    [SerializeField]
    private Transform scoreScreen, questionScreen;

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreStats, scorePercentage;


    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("level", 0);
        level = 0; // testing
        LoadQuestionSet();
        UseQuestionTemplate(currentQuestion.questionType);
    }

    void LoadQuestionSet()
    {
        currentQuestionSet = questionDatabase.GetQuestionSet(level);
        currentQuestion = currentQuestionSet.questions[0];
    }

    void ClearAnswers()
    {
        foreach (Transform buttons in answerPanel)
        {
            Destroy(buttons.gameObject);
        }
    }

    // choose question template
    void UseQuestionTemplate(Question.QuestionType questionType)
    {
        for (int i = 0; i < questionPanel.childCount; i++)
        {
            questionPanel.GetChild(i).gameObject.SetActive(i == (int)questionType); // true
            
            if (i == (int)questionType)
            {
                questionPanel.GetChild(i).GetComponent<QuestionUI>().UpdateQuestionInfo(currentQuestion);
            }
        }
    }

    public void NextQuestionSet()
    {
        if (level < questionDatabase.questionSets.Length - 1)
        {
            correctAnswers = 0;
            currentQuestionIndex = 0;
            level++;
            PlayerPrefs.SetInt("level", level);
            scoreScreen.gameObject.SetActive(false);
            questionScreen.gameObject.SetActive(true);
            LoadQuestionSet();
            UseQuestionTemplate(currentQuestion.questionType);
        }
        
        else 
        {
            // load start menu
        }
    }

    void NextQuestion() 
    {
        // more questions avaliable
        if (currentQuestionIndex < currentQuestionSet.questions.Count-1)
        {
            currentQuestionIndex++;
            currentQuestion = currentQuestionSet.questions[currentQuestionIndex];

            // change template of question, load new template
            UseQuestionTemplate(currentQuestion.questionType);
        }
        
        // no more question
        else
        {
            scoreScreen.gameObject.SetActive(true);
            questionScreen.gameObject.SetActive(false);
            scorePercentage.text = string.Format("Score:\n {0}%", (float)correctAnswers/(float)currentQuestionSet.questions.Count * 100);
            scoreStats.text = string.Format("Questions: {0}\nCorrect: {1}", currentQuestionSet.questions.Count, correctAnswers);
        }
    }

    public void CheckAnswer(string answer)
    {
        if (answer == currentQuestion.correctAnswerKey)
        {
            correctAnswers++;
            Debug.Log("Správna odpoved!");
        }

        ClearAnswers();
        NextQuestion();
    }
}

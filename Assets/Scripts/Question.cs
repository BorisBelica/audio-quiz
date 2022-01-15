using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    // define what type of question it is
    public enum QuestionType { 
        Text = 0, 
        ImageWithCaption = 1, 
        Audio = 2 
    }; 

   public string questionText;
   public Sprite questionImage;
   public AudioClip questionAudio;
   public string correctAnswerKey;
   public string[] answerChoices;
   public QuestionType questionType;
}

using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsQuestion
    {
        public int QuestionID {  get; set; }
        public string QuestionText {  get; set; }
        public string OptionA {  get; set; }
        public string OptionB {  get; set; }
        public string OptionC {  get; set; }
        public string OptionD {  get; set; }
        public int CorrectOptionIndex {  get; set; }
        public int QuestionLevelID {  get; set; }
        clsQuestionLevels QuestionlevelInfo { get; set; }


        public clsQuestion()
        {
            this.QuestionID = -1;
            this.QuestionText = "";
            this.OptionA = "";
            this.OptionB = "";
            this.OptionC = "";
            this.OptionD = "";
            this.CorrectOptionIndex =-1;
            this.QuestionLevelID = -1;
        }

        public clsQuestion(int questionID, string questionText, string optionA,
            string optionB, string optionC, string optionD,
            int correctOptionIndex, int questionLevelID)
        {
            this.QuestionID = questionID;
            this.QuestionText = questionText;
            this.OptionA = optionA;
            this.OptionB = optionB;
            this.OptionC = optionC;
            this.OptionD = optionD;
            this.CorrectOptionIndex = correctOptionIndex;
            this.QuestionLevelID = questionLevelID;

            QuestionlevelInfo = clsQuestionLevels.GetQuestionLevelInfoByID(this.QuestionLevelID);
        }

        public static DataTable GetAllQuestion()
        {
            return clsQuestionData.GetAllQuestion();
        }

        public static int GetAllQuestionCount()
        {
           return  clsQuestionData.GetAllQuestionCount();
        }
    }
}

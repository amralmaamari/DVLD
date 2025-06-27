using DvldDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldDataBusinessLayer
{
    public class clsQuestionLevels
    {
        public int QuestionLevelID { get; set; }    
        public string LevelName { get; set; }    
        public clsQuestionLevels()
        {
            QuestionLevelID = -1;
            LevelName = "";
        }

        public clsQuestionLevels(int questionLevelID, string levelName)
        {
            this.QuestionLevelID = questionLevelID;
            this.LevelName = levelName;
        }

        public static clsQuestionLevels GetQuestionLevelInfoByID(int QuestionLevelID)
        {
            string LevelName = "";

            if (clsQuestionLevelData.GetQuestionLevelInfoByID(QuestionLevelID, ref LevelName))
            {
                return new clsQuestionLevels(QuestionLevelID, LevelName);
            }
            else
                return null;

        }
    }
}

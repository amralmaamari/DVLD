using DvldDataBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmWrittenTest : Form
    {
        //delegate it is reference datatype which hold referefence of Method
        public delegate void DataBackEvenHandler(object sender, bool IsPassExam,string Notes);

        public event DataBackEvenHandler DataBack;

       private  DataTable _dtAllQuestions;
       private  int _currentQuestionIndex = 0;
       private  DataRow _currentQuestion;
       private  int _correctCurrentQuestion;
       private  int _countCorrectAnswer = 0;

        public frmWrittenTest()
        {
            InitializeComponent();
          
        }

        private bool _DidPassExam()
        {
            return (_countCorrectAnswer >= (_dtAllQuestions.Rows.Count / 2));
        }

        private void _SetQustion()
        {
            if (_dtAllQuestions.Rows.Count > _currentQuestionIndex)
            {
                rbOptionA.Checked = true;
                _currentQuestion = _dtAllQuestions.Rows[_currentQuestionIndex];

                lblQuestion.Text = _currentQuestion["QuestionText"].ToString();
                rbOptionA.Text = _currentQuestion["OptionA"].ToString();
                rbOptionB.Text = _currentQuestion["OptionB"].ToString();
                rbOptionC.Text = _currentQuestion["OptionC"].ToString();
                rbOptionD.Text = _currentQuestion["OptionD"].ToString();
                lblQustionCount.Text = $"Qustion {_currentQuestionIndex + 1} of {clsQuestion.GetAllQuestionCount()}.";

                string _levelName = clsQuestionLevels.GetQuestionLevelInfoByID((int)_currentQuestion["QuestionLevelID"]).LevelName;
                lblLevel.Text= _levelName;
                _correctCurrentQuestion =Convert.ToInt32(_currentQuestion["CorrectOptionIndex"]);
            }
            else
            {
                // End of questions
                btnNext.Enabled = false;
                btnNext.Visible = false;
                btnSubmit.Enabled = true;
                btnSubmit.Visible = true;

                //is it acceptable but is not Readable to other devlopment
                rbOptionA.Enabled = rbOptionB.Enabled = rbOptionC.Enabled = rbOptionD.Enabled = false;

                //Tigger the event to send data abck to Form1
                DataBack?.Invoke(this, _DidPassExam(), $" Answer {_countCorrectAnswer}  of {clsQuestion.GetAllQuestionCount()} Qustions");
            }
        }

        private RadioButton _GetCheckedRadioButton()
        {
            if (rbOptionA.Checked)
                return rbOptionA;
            if (rbOptionB.Checked)
                return rbOptionB;
            if (rbOptionC.Checked)
                return rbOptionC;
            if (rbOptionD.Checked)
                return rbOptionD;
            return null; // If no radio button is checked
        }

        private bool _CheckedCorrectAnswer()
        {
            return (Convert.ToInt32(_GetCheckedRadioButton().Tag) == _correctCurrentQuestion);
                
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            _dtAllQuestions = clsQuestion.GetAllQuestion();
            _SetQustion();

        }

      
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_GetCheckedRadioButton() == null)
                return;

            if (_CheckedCorrectAnswer())
                _countCorrectAnswer++;

            
            _currentQuestionIndex++;
            _SetQustion();
           

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
            this.Close();

            

        }

       
    }
}

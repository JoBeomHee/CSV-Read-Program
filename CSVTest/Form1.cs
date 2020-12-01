using MSSQL_Connect_Program.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSVTest
{
    public partial class Form1 : Form
    {
        public enum COLUMNS { NAME, AGE, GRADE, PHONENUMBER }

        List<string> csvList = null;
        List<STUDENT> stuList = new List<STUDENT>();

        public Form1()
        {
            InitializeComponent();

            //이벤트 선언
            InitEvent();
        }

        /// <summary>
        /// 이벤트 선언 메서드
        /// </summary>
        private void InitEvent()
        {
            //Load Event
            this.Load += FormLoad_Event;

            //CSV read Button Click Event
            uiBtn_ReadCsv.Click += uiBtn_ReadCsv_Click;

            //DataBase Insert Button Click Event
            uiBtn_DBInsert.Click += uiBtn_DBInsert_Click;
        }

        /// <summary>
        /// Form Load Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLoad_Event(object sender, EventArgs e)
        {
            //MSSQL DataBase 연결
            this.Cursor = Cursors.WaitCursor;

            //Connect to DB
            ConnectDatabase();

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// CSV Read Button Click Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiBtn_ReadCsv_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)

            {
                string[] fileName = Directory.GetFiles(fbd.SelectedPath); //폴더 읽어와

                csvList = fileName.Where(x => x.IndexOf(".csv", StringComparison.OrdinalIgnoreCase) >= 0)
                               .Select(x => x).ToList();

                try
                {
                    GetCSVData(csvList); //CSV 파일 내용 읽어오기
                    DataSouceGridView(); //DataGridView 에 CSV 내용 바인딩
                }
                catch { }
            }
        }

        /// <summary>
        /// DB Insert Button Click Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiBtn_DBInsert_Click(object sender, EventArgs e)
        {
            DBInsert();
        }

        /// <summary>
        /// CSV 파일 읽는 메서드
        /// </summary>
        /// <param name="csvList"></param>
        private void GetCSVData(List<string> csvList)
        {
            for (int idx = 0; idx < csvList.Count; idx++)
            {
                using (var sr = new System.IO.StreamReader(csvList[idx], Encoding.Default, true))
                {
                    while (!sr.EndOfStream)
                    {
                        string array = sr.ReadLine();
                        string[] values = array.Split(',');

                        //컬럼명은 건너뛰기
                        if (array.Contains("NAME"))
                            continue;

                        STUDENT stu = new STUDENT();
                        stuList.Add(SetData(stu, values));
                    }
                }
            }
        }

        /// <summary>
        /// Database 연결 메서드
        /// </summary>
        private void ConnectDatabase()
        {
            if (SqlDBManager.Instance.GetConnection() == false)
            {
                string msg = $"Failed to Connect to Database";
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Student 객체에 데이터 저장
        /// </summary>
        /// <param name="stu"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private STUDENT SetData(STUDENT stu, string[] values)
        {
            stu.NAME = values[(int)COLUMNS.NAME].ToString();
            stu.AGE = values[(int)COLUMNS.AGE].ToString();
            stu.GRADE = values[(int)COLUMNS.GRADE].ToString();
            stu.PHONENUMBER = values[(int)COLUMNS.PHONENUMBER].ToString();

            return stu;
        }

        /// <summary>
        /// DataGridView 컨트롤에 데이터 바인딩
        /// </summary>
        private void DataSouceGridView()
        {
            // 값들이 입력된 테이블을 DataGridView에 입력합니다.
            uiGridView_CSV.DataSource = GetDataTable();
        }

        /// <summary>
        /// DataTable 생성
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();

            //컬럼 추가
            CreateColumn(dt);

            //Row 추가
            CreateRow(dt);

            return dt;
        }

        /// <summary>
        /// 컬럼 생성
        /// </summary>
        /// <param name="dt"></param>
        private void CreateColumn(DataTable dt)
        {
            dt.Columns.Add("Name");
            dt.Columns.Add("Age");
            dt.Columns.Add("Grade");
            dt.Columns.Add("PHONENUMBER");
        }

        /// <summary>
        /// Row데이터 넣기
        /// </summary>
        /// <param name="dt"></param>
        private void CreateRow(DataTable dt)
        {
            for(int idx = 0; idx < stuList.Count; idx++)
            {
                dt.Rows.Add(new string[] { stuList[idx].NAME,
                                           stuList[idx].AGE,
                                           stuList[idx].GRADE,
                                           stuList[idx].PHONENUMBER });
            }
        }

        /// <summary>
        /// DB Insert 메서드
        /// </summary>
        private void DBInsert()
        {
            string name = string.Empty;
            string age = string.Empty;
            string grade = string.Empty;
            string phoneNumber = string.Empty;

            string query = string.Empty;

            for (int idx = 0; idx < stuList.Count; idx++)
            {
                name = stuList[idx].NAME.ToString();
                age = stuList[idx].AGE.ToString();
                grade = stuList[idx].GRADE.ToString();
                phoneNumber = stuList[idx].PHONENUMBER.ToString();

                query = @"
            INSERT INTO dbo.STUDENT 
            VALUES ('#NAME', '#AGE', '#GRADE', '#PHONENUMBER')
            ";

                query = query.Replace("#NAME", name);
                query = query.Replace("#AGE", age);
                query = query.Replace("#GRADE", grade);
                query = query.Replace("#PHONENUMBER", phoneNumber);

                int result = SqlDBManager.Instance.ExecuteNonQuery(query);

                if (result < 0)
                {
                    MessageBox.Show("DB Insert 실패");
                }
            }

            MessageBox.Show("데이터베이스 Insert 성공");
        }
    }

    public class STUDENT
    {
        public string NAME { get; set; }
        public string AGE { get; set; }
        public string GRADE { get; set; }
        public string PHONENUMBER { get; set; }
    }
}

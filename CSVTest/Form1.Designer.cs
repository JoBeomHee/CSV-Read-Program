namespace CSVTest
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiBtn_ReadCsv = new System.Windows.Forms.Button();
            this.uiGridView_CSV = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.uiGridView_CSV)).BeginInit();
            this.SuspendLayout();
            // 
            // uiBtn_ReadCsv
            // 
            this.uiBtn_ReadCsv.Location = new System.Drawing.Point(419, 312);
            this.uiBtn_ReadCsv.Name = "uiBtn_ReadCsv";
            this.uiBtn_ReadCsv.Size = new System.Drawing.Size(75, 56);
            this.uiBtn_ReadCsv.TabIndex = 0;
            this.uiBtn_ReadCsv.Text = "CSV Read";
            this.uiBtn_ReadCsv.UseVisualStyleBackColor = true;
            // 
            // uiGridView_CSV
            // 
            this.uiGridView_CSV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uiGridView_CSV.Location = new System.Drawing.Point(12, 12);
            this.uiGridView_CSV.Name = "uiGridView_CSV";
            this.uiGridView_CSV.RowTemplate.Height = 23;
            this.uiGridView_CSV.Size = new System.Drawing.Size(482, 294);
            this.uiGridView_CSV.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 375);
            this.Controls.Add(this.uiGridView_CSV);
            this.Controls.Add(this.uiBtn_ReadCsv);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.uiGridView_CSV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uiBtn_ReadCsv;
        private System.Windows.Forms.DataGridView uiGridView_CSV;
    }
}


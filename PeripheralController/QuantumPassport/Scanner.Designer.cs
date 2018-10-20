namespace QuantumPassport
{
    partial class Scanner
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scanner));
            this.btn_thresholdSet = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.CommandWatcher = new System.IO.FileSystemWatcher();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CommandWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_thresholdSet
            // 
            this.btn_thresholdSet.Location = new System.Drawing.Point(0, 0);
            this.btn_thresholdSet.Name = "btn_thresholdSet";
            this.btn_thresholdSet.Size = new System.Drawing.Size(75, 23);
            this.btn_thresholdSet.TabIndex = 0;
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // CommandWatcher
            // 
            this.CommandWatcher.EnableRaisingEvents = true;
            this.CommandWatcher.Filter = "Command.txt";
            this.CommandWatcher.NotifyFilter = System.IO.NotifyFilters.LastWrite;
            this.CommandWatcher.Path = "C:\\inetpub\\wwwroot\\Passport";
            this.CommandWatcher.SynchronizingObject = this;
            this.CommandWatcher.Changed += new System.IO.FileSystemEventHandler(this.CommandWatcher_Changed);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = ".. Listening ..";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Scanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(297, 37);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "Scanner";
            this.Text = "PassportScanner";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.FormScan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CommandWatcher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_thresholdSet;
        private System.Windows.Forms.Timer timer2;
        private System.IO.FileSystemWatcher CommandWatcher;
        private System.Windows.Forms.Label label1;
    }
}
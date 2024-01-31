using MHXYSupport.Extensions;

namespace MHXYSupport
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox_msg = new TextBox();
            button_search = new Button();
            radioButton_sm = new RadioButton();
            groupBox_task = new GroupBox();
            radioButton_yb = new RadioButton();
            radioButton_zg = new RadioButton();
            radioButton_jjc = new RadioButton();
            radioButton_sjqy = new RadioButton();
            radioButton_jyl = new RadioButton();
            radioButton_bprw = new RadioButton();
            radioButton_bt_w = new RadioButton();
            radioButton_bt_d = new RadioButton();
            button_run = new Button();
            listBox_processes = new ListBox();
            textBox_curProcess = new TextBox();
            textBox_search = new TextBox();
            radioButton_mjxy = new RadioButton();
            groupBox_task.SuspendLayout();
            SuspendLayout();
            // 
            // textBox_msg
            // 
            textBox_msg.Location = new Point(20, 470);
            textBox_msg.Multiline = true;
            textBox_msg.Name = "textBox_msg";
            textBox_msg.Size = new Size(560, 300);
            textBox_msg.TabIndex = 9;
            textBox_msg.TextChanged += textBox_msg_TextChanged;
            // 
            // button_search
            // 
            button_search.Location = new Point(480, 21);
            button_search.Name = "button_search";
            button_search.Size = new Size(100, 40);
            button_search.TabIndex = 8;
            button_search.Text = "搜索";
            button_search.UseVisualStyleBackColor = true;
            button_search.Click += button_search_Click;
            // 
            // radioButton_sm
            // 
            radioButton_sm.AutoSize = true;
            radioButton_sm.Location = new Point(10, 25);
            radioButton_sm.Name = "radioButton_sm";
            radioButton_sm.Size = new Size(17, 16);
            radioButton_sm.TabIndex = 1;
            radioButton_sm.TabStop = true;
            radioButton_sm.UseVisualStyleBackColor = true;
            radioButton_sm.CheckedChanged += radioButton_task_CheckedChanged;
            // 
            // groupBox_task
            // 
            groupBox_task.Controls.Add(radioButton_yb);
            groupBox_task.Controls.Add(radioButton_zg);
            groupBox_task.Controls.Add(radioButton_mjxy);
            groupBox_task.Controls.Add(radioButton_jjc);
            groupBox_task.Controls.Add(radioButton_sjqy);
            groupBox_task.Controls.Add(radioButton_jyl);
            groupBox_task.Controls.Add(radioButton_bprw);
            groupBox_task.Controls.Add(radioButton_bt_w);
            groupBox_task.Controls.Add(radioButton_bt_d);
            groupBox_task.Controls.Add(radioButton_sm);
            groupBox_task.Location = new Point(20, 244);
            groupBox_task.Name = "groupBox_task";
            groupBox_task.Size = new Size(454, 210);
            groupBox_task.TabIndex = 0;
            groupBox_task.TabStop = false;
            groupBox_task.Text = "任务";
            // 
            // radioButton_yb
            // 
            radioButton_yb.AutoSize = true;
            radioButton_yb.Location = new Point(10, 100);
            radioButton_yb.Name = "radioButton_yb";
            radioButton_yb.Size = new Size(17, 16);
            radioButton_yb.TabIndex = 4;
            radioButton_yb.TabStop = true;
            radioButton_yb.UseVisualStyleBackColor = true;
            radioButton_yb.CheckedChanged += radioButton_task_CheckedChanged;
            // 
            // radioButton_zg
            // 
            radioButton_zg.AutoSize = true;
            radioButton_zg.Location = new Point(10, 125);
            radioButton_zg.Name = "radioButton_zg";
            radioButton_zg.Size = new Size(17, 16);
            radioButton_zg.TabIndex = 5;
            radioButton_zg.TabStop = true;
            radioButton_zg.UseVisualStyleBackColor = true;
            radioButton_zg.CheckedChanged += radioButton_task_CheckedChanged;
            // 
            // radioButton_jjc
            // 
            radioButton_jjc.AutoSize = true;
            radioButton_jjc.Location = new Point(110, 50);
            radioButton_jjc.Name = "radioButton_jjc";
            radioButton_jjc.Size = new Size(17, 16);
            radioButton_jjc.TabIndex = 7;
            radioButton_jjc.TabStop = true;
            radioButton_jjc.UseVisualStyleBackColor = true;
            radioButton_jjc.CheckedChanged += radioButton_task_CheckedChanged;
            // 
            // radioButton_sjqy
            // 
            radioButton_sjqy.AutoSize = true;
            radioButton_sjqy.Location = new Point(110, 25);
            radioButton_sjqy.Name = "radioButton_sjqy";
            radioButton_sjqy.Size = new Size(17, 16);
            radioButton_sjqy.TabIndex = 7;
            radioButton_sjqy.TabStop = true;
            radioButton_sjqy.UseVisualStyleBackColor = true;
            radioButton_sjqy.CheckedChanged += radioButton_task_CheckedChanged;
            // 
            // radioButton_jyl
            // 
            radioButton_jyl.AutoSize = true;
            radioButton_jyl.Location = new Point(10, 175);
            radioButton_jyl.Name = "radioButton_jyl";
            radioButton_jyl.Size = new Size(17, 16);
            radioButton_jyl.TabIndex = 7;
            radioButton_jyl.TabStop = true;
            radioButton_jyl.UseVisualStyleBackColor = true;
            radioButton_jyl.CheckedChanged += radioButton_task_CheckedChanged;
            // 
            // radioButton_bprw
            // 
            radioButton_bprw.AutoSize = true;
            radioButton_bprw.Location = new Point(10, 150);
            radioButton_bprw.Name = "radioButton_bprw";
            radioButton_bprw.Size = new Size(17, 16);
            radioButton_bprw.TabIndex = 6;
            radioButton_bprw.TabStop = true;
            radioButton_bprw.UseVisualStyleBackColor = true;
            radioButton_bprw.CheckedChanged += radioButton_task_CheckedChanged;
            // 
            // radioButton_bt_w
            // 
            radioButton_bt_w.AutoSize = true;
            radioButton_bt_w.Location = new Point(10, 75);
            radioButton_bt_w.Name = "radioButton_bt_w";
            radioButton_bt_w.Size = new Size(17, 16);
            radioButton_bt_w.TabIndex = 3;
            radioButton_bt_w.TabStop = true;
            radioButton_bt_w.UseVisualStyleBackColor = true;
            radioButton_bt_w.CheckedChanged += radioButton_task_CheckedChanged;
            // 
            // radioButton_bt_d
            // 
            radioButton_bt_d.AutoSize = true;
            radioButton_bt_d.Location = new Point(10, 50);
            radioButton_bt_d.Name = "radioButton_bt_d";
            radioButton_bt_d.Size = new Size(17, 16);
            radioButton_bt_d.TabIndex = 2;
            radioButton_bt_d.TabStop = true;
            radioButton_bt_d.UseVisualStyleBackColor = true;
            radioButton_bt_d.CheckedChanged += radioButton_task_CheckedChanged;
            // 
            // button_run
            // 
            button_run.Location = new Point(480, 244);
            button_run.Name = "button_run";
            button_run.Size = new Size(100, 40);
            button_run.TabIndex = 7;
            button_run.Text = "执行";
            button_run.UseVisualStyleBackColor = true;
            button_run.Click += button_run_Click;
            // 
            // listBox_processes
            // 
            listBox_processes.FormattingEnabled = true;
            listBox_processes.Location = new Point(20, 68);
            listBox_processes.Name = "listBox_processes";
            listBox_processes.Size = new Size(454, 104);
            listBox_processes.TabIndex = 10;
            listBox_processes.SelectedIndexChanged += listBox_processes_SelectedIndexChanged;
            // 
            // textBox_curProcess
            // 
            textBox_curProcess.Location = new Point(20, 185);
            textBox_curProcess.Multiline = true;
            textBox_curProcess.Name = "textBox_curProcess";
            textBox_curProcess.ReadOnly = true;
            textBox_curProcess.Size = new Size(560, 50);
            textBox_curProcess.TabIndex = 11;
            // 
            // textBox_search
            // 
            textBox_search.Location = new Point(20, 21);
            textBox_search.Multiline = true;
            textBox_search.Name = "textBox_search";
            textBox_search.Size = new Size(454, 40);
            textBox_search.TabIndex = 12;
            textBox_search.Text = "MyGame_x64r";
            // 
            // radioButton_mjxy
            // 
            radioButton_mjxy.AutoSize = true;
            radioButton_mjxy.Location = new Point(110, 75);
            radioButton_mjxy.Name = "radioButton_mjxy";
            radioButton_mjxy.Size = new Size(17, 16);
            radioButton_mjxy.TabIndex = 7;
            radioButton_mjxy.TabStop = true;
            radioButton_mjxy.UseVisualStyleBackColor = true;
            radioButton_mjxy.CheckedChanged += radioButton_task_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(602, 803);
            Controls.Add(textBox_search);
            Controls.Add(textBox_curProcess);
            Controls.Add(listBox_processes);
            Controls.Add(groupBox_task);
            Controls.Add(button_run);
            Controls.Add(button_search);
            Controls.Add(textBox_msg);
            Location = new Point(100, 200);
            Name = "Form1";
            Text = "Form1";
            groupBox_task.ResumeLayout(false);
            groupBox_task.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_msg;
        private Button button_search;
        private Button button_run;
        private GroupBox groupBox_task;
        private RadioButton radioButton_sm;        
        private RadioButton radioButton_bt_d;
        private RadioButton radioButton_bt_w;
        private RadioButton radioButton_yb;
        private RadioButton radioButton_zg;
        private RadioButton radioButton_jyl;
        private RadioButton radioButton_bprw;
        private ListBox listBox_processes;
        private TextBox textBox_curProcess;
        private TextBox textBox_search;
        private RadioButton radioButton_sjqy;
        private RadioButton radioButton_jjc;
        private RadioButton radioButton_mjxy;
    }
}

using MHXYWF.Tasks;
using MHXYWF.Utility;
using System.Diagnostics;
using MHXYWF.Extensions;

namespace MHXYWF;

public partial class Form1 : Form
{
    private Dictionary<int, Process> _processDic;
    private Process _curProcess;
    private string _curTask;

    public Form1()
    {
        InitializeComponent();
        _processDic = new();
        _curProcess = new();
        this.button_search_Click(null,null);
        this.radioButton_sm.Text = MHTask.SM.GetDescription();
        this.radioButton_bt_d.Text = MHTask.DBT.GetDescription();
        this.radioButton_bt_w.Text = MHTask.WBT.GetDescription();
        this.radioButton_yb.Text = MHTask.YB.GetDescription();
        this.radioButton_zg.Text = MHTask.ZG.GetDescription();
        this.radioButton_jyl.Text = MHTask.JYL.GetDescription();
        this.radioButton_bprw.Text = MHTask.BPRW.GetDescription();
        this.radioButton_sjqy.Text = MHTask.SJQY.GetDescription();
        this.radioButton_jjc.Text = MHTask.JJC.GetDescription();
        this.radioButton_mjxy.Text = MHTask.MJXY.GetDescription();
    }

    private static Tasks.IMain CreateTaskInstance(string task)
    {
        if (task == MHTask.SM.GetDescription()) return new MHXYWF.Tasks.SM.Main();
        if (task == MHTask.DBT.GetDescription()) return new MHXYWF.Tasks.BT.DMain();
        if (task == MHTask.WBT.GetDescription()) return new MHXYWF.Tasks.BT.WMain();
        if (task == MHTask.YB.GetDescription()) return new MHXYWF.Tasks.YB.Main();
        if (task == MHTask.ZG.GetDescription()) return new MHXYWF.Tasks.ZG.Main();
        if (task == MHTask.JYL.GetDescription()) return new MHXYWF.Tasks.JYL.Main();
        if (task == MHTask.BPRW.GetDescription()) return new MHXYWF.Tasks.BPRW.Main();
        if (task == MHTask.SJQY.GetDescription()) return new MHXYWF.Tasks.SJQY.Main();
        if (task == MHTask.JJC.GetDescription()) return new MHXYWF.Tasks.JJC.Main();
        if (task == MHTask.MJXY.GetDescription()) return new MHXYWF.Tasks.MJXY.Main();

        throw new NotImplementedException();
    }

    public void SetTextBoxMessage(string msg)
    {
        this.Invoke(() =>
        {
            this.textBox_msg.Text = msg;
            this.Update();
        });
    }

    public void AppendTextBoxMessage(string msg)
    {
        this.Invoke(() =>
        {
            this.textBox_msg.Text = this.textBox_msg.Text + Environment.NewLine + msg;
            this.Update();
        });
    }

    public void ClearTextBoxMessage()
    {
        this.textBox_msg.Text = "";
        this.Update();
    }

    private void textBox_msg_TextChanged(object sender, EventArgs e)
    {
        this.textBox_msg.SelectionStart = this.textBox_msg.Text.Length;
        this.textBox_msg.SelectionLength = 0;
        this.textBox_msg.ScrollToCaret();
    }

    private void radioButton_task_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton radio = (RadioButton)sender;
        _curTask = radio.Text;
    }

    private void listBox_processes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox listBox = (ListBox)sender;
        this.textBox_curProcess.Text = (string)listBox_processes.Items[listBox.SelectedIndex];
        int id = int.Parse(this.textBox_curProcess.Text.Split("_")[0]);
        _curProcess = _processDic[id];
    }

    private void button_search_Click(object sender, EventArgs e)
    {
        this.listBox_processes.Items.Clear();
        this._processDic = new();
        Process[] processes = Process.GetProcessesByName(this.textBox_search.Text);
        foreach (Process process in processes)
        {
            var rect = WindowsApi.GetPrecessRect(process);
            this.listBox_processes.Items.Add($"{process.Id}_{process.ProcessName}_{process.MainWindowHandle}_(x:{rect.X},y:{rect.Y})");
            _processDic.Add(process.Id, process);
        }
    }

    private async void button_run_Click(object sender, EventArgs e)
    {
        IMain main = CreateTaskInstance(_curTask);
        await main.Run(this, _curProcess);
    }
}

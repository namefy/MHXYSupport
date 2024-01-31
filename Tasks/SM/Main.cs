using MHXYWF.Utility;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace MHXYWF.Tasks.SM;

public class Main : IMain
{
    public async Task Run(Form1 form, Process process)
    {
        form.SetTextBoxMessage("师门 开始");
        string imgPath = MHXYWF.Const.ScreenshotDirectory + $"{process.Id}_{Const.ScreenshotName}";
        await Task.Run(() =>
        {
            form.SetTextBoxMessage("师门 进行中");
            while (true)
            {
                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                WindowsApi.RECT rect = WindowsApi.GetPrecessRect(process);
                var ocrResult = PaddleOCR.FindRegion(imgPath);
                Utility.Action.ClickTargetButton(process, ocrResult
                    ,Tasks.Const.JX, Const.SM_RW, Tasks.Const.GM,Tasks.Const.SJ, Tasks.Const.SY
                    ,Const.SFQ,Const.SSJQ
                    , Const.SM, Const.RC_SM);
                var result = ocrResult.Regions.Where(p => p.Text.Contains(Const.SM)).OrderBy(p => p.Text.Length).FirstOrDefault();
                if (result != default)
                {
                    form.AppendTextBoxMessage($"当前进度：{Tasks.Const.ProgressRegex.Match(result.Text).Value}");
                }
                Thread.Sleep(Tasks.Const.RetryTime);
            }
        });
        form.AppendTextBoxMessage("师门 完成");
    }
}

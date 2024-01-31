using MHXYWF.Utility;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace MHXYWF.Tasks.JJC;

public class Main : IMain
{
    public async Task Run(Form1 form, Process process)
    {
        form.SetTextBoxMessage("竞技场 开始");
        string imgPath = MHXYWF.Const.ScreenshotDirectory + $"{process.Id}_{Const.ScreenshotName}";
        await Task.Run(() =>
        {
            form.SetTextBoxMessage("竞技场 进行中");
            while (true)
            {
                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                WindowsApi.RECT rect = WindowsApi.GetPrecessRect(process);
                var ocrResult = PaddleOCR.FindRegion(imgPath);
                Utility.Action.ClickTargetButton(process, ocrResult
                     , Tasks.Const.ZD,Const.KSPP);
                //var result = ocrResult.Regions.Where(p => p.Text.Contains(Const.RC_ZG)).OrderBy(p => p.Text.Length).FirstOrDefault();
                //if (result != default)
                //{
                //    form.AppendTextBoxMessage($"当前进度：{Tasks.Const.ProgressRegex.Match(result.Text).Value}");
                //}
                Thread.Sleep(Tasks.Const.RetryTime);
            }
        });
        form.AppendTextBoxMessage("竞技场 完成");
    }
}

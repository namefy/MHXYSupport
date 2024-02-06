using MHXYSupport.Utility;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace MHXYSupport.Tasks.YB;

public class Main : IMain
{
    public async Task Run(Form1 form, Process process)
    {
        form.SetTextBoxMessage("运镖 开始");
        string imgPath = MHXYSupport.Const.ScreenshotDirectory + $"{process.Id}_{Const.ScreenshotName}";

        bool success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.HD, Tasks.Const.HDOffset);
        if (!success) return;
        success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.YB, Tasks.Const.YBOffset);
        if (!success) return;
        Regex regex = new(@"\d+");
        int i = 0;
        await Task.Run(() =>
        {
            while (true)
            {
                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                var ocrResult = PaddleOCR.FindRegion(imgPath);

                bool success = Utility.Action.ClickTargetButton(process, ocrResult
                    , Tasks.Const.QD, Const.YSPTBY);

                if (success)
                {
                    Thread.Sleep(Const.WaitSeconds * 1000);
                    continue;
                }
                var region = ocrResult.Regions.Where(p => p.Text.Contains(Const.Second)).OrderBy(p => p.Text.Length).FirstOrDefault();
                if (region != default)
                {
                    string secondStr = regex.Match(region.Text).Value;
                    int second = int.Parse(secondStr);
                    form.AppendTextBoxMessage($"距离运镖结束{second}秒");
                    Thread.Sleep(second * 1000);
                    continue;
                }

                if (ocrResult.Text.Contains(Const.JB))
                {
                    Thread.Sleep(Tasks.Const.RetryTime);
                    continue;
                }
                if (i == Tasks.Const.RetryCount) return;
                i++;
                Thread.Sleep(Tasks.Const.RetryTime);
            }
        });
        form.AppendTextBoxMessage($"运镖 结束");
    }
}

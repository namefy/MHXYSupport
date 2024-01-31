using MHXYSupport.Utility;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace MHXYSupport.Tasks.YB;

public class Main : IMain
{
    public async Task Run(Form1 form, Process process)
    {
        form.SetTextBoxMessage("押镖 开始");
        string imgPath = MHXYSupport.Const.ScreenshotDirectory + $"{process.Id}_{Const.ScreenshotName}";

        bool success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.HD, Tasks.Const.HDOffset);
        if (!success) return;
        success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.YB, Tasks.Const.YBOffset);
        if (!success) return;
        int count = 1;
        await Task.Run(() =>
        {
            while (true)
            {
                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                var ocrResult = PaddleOCR.FindRegion(imgPath);

                success = Utility.Action.ClickTargetButton(process, ocrResult, Const.YSPTBY);
                if (!success) continue;

                Thread.Sleep(Const.WaitSeconds * 1000);

                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                ocrResult = PaddleOCR.FindRegion(imgPath);

                success = Utility.Action.ClickTargetButton(process, ocrResult, Tasks.Const.QD);
                if (!success) continue;

                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                ocrResult = PaddleOCR.FindRegion(imgPath);

                string timeStr = ocrResult.Regions.Where(p => p.Text.Contains(Const.Second)).FirstOrDefault().Text;
                Regex regex = new(@"\d+");
                int time = int.Parse(regex.Match(timeStr).Value);
                form.AppendTextBoxMessage($"第{count}次押镖 进行中 预计{time}秒完成");
                Thread.Sleep(time * 1000);
                while (true)
                {
                    WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                    ocrResult = PaddleOCR.FindRegion(imgPath);
                    if (!ocrResult.Text.Contains(Const.Finsih) && !ocrResult.Text.Contains(Const.QiangDao)) break;
                    Thread.Sleep(Const.WaitSeconds2 * 1000);
                }
                form.AppendTextBoxMessage($"第{count}次押镖 完成");
                if (count == Const.YSCount)
                {
                    form.AppendTextBoxMessage("押镖 完成");
                    return;
                }
                count++;
            }
        });

    }
}

using MHXYSupport.Utility;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace MHXYSupport.Tasks.BPRW;

public class Main : IMain
{
    public async Task Run(Form1 form, Process process)
    {
        form.SetTextBoxMessage("帮派任务 开始");
        string imgPath = MHXYSupport.Const.ScreenshotDirectory + $"{process.Id}_{Const.ScreenshotName}";
        bool success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.HD, Tasks.Const.HDOffset);
        if (!success) return;
        success = Utility.Action.ClickTargetButton(process, imgPath, Tasks.Const.BPRW, Tasks.Const.BPRWOffset);
        if (!success) return;
        await Task.Run(() =>
        {
            form.SetTextBoxMessage("帮派任务 进行中");
            while (true)
            {
                WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
                WindowsApi.RECT rect = WindowsApi.GetPrecessRect(process);
                var ocrResult = PaddleOCR.FindRegion(imgPath);
                Utility.Action.ClickTargetButton(process, ocrResult
                    , Const.ZLYL
                     , Const.QL, Const.BH, Const.ZQ, Const.XW);
                Thread.Sleep(Tasks.Const.RetryTime);
            }
        });
        form.AppendTextBoxMessage("帮派任务 完成");
    }
}

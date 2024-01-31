using MHXYSupport.Utility;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace MHXYSupport.Tasks.JYL;

public class Main : IMain
{
    public async Task Run(Form1 form, Process process)
    {
        string imgPath = MHXYSupport.Const.ScreenshotDirectory + $"{process.Id}_{Const.ScreenshotName}";
        bool success = false;
        #region 领取
        //bool success = Utility.Action.ClickTargetButton(MHXYSupport.Const.ProgressName, imgPath, Tasks.Const.HD, Tasks.Const.HDOffset);
        //if (!success) return;
        //success = Utility.Action.ClickTargetButton(MHXYSupport.Const.ProgressName, imgPath, Tasks.Const.JYRWL, Tasks.Const.JYRWLOffset);
        //if (!success) return;
        //success = Utility.Action.ClickTargetButton(MHXYSupport.Const.ProgressName, imgPath, Const.JYRWL);
        //if (!success) return;
        //success = Utility.Action.ClickTargetButton(MHXYSupport.Const.ProgressName, imgPath, Const.LQJYRWL);
        //if (!success) return;
        #endregion

        #region 跑环
        form.SetTextBoxMessage("经验链 开始");
        Regex regex = new(@"\d+");
        bool isLast = false;
        while (true)
        {
            WindowsApi.Screenshot(process, imgPath, ImageFormat.Jpeg);
            var ocrResult = PaddleOCR.FindRegion(imgPath);
            success = Utility.Action.ClickTargetButton(process,ocrResult
                , Const.JYRWL_XC, Const.JYRWL_XW, Const.GM, Const.SJ
                , Const.JYRWL_XR
                , Const.JYRWL_ZD, Const.KSZD);
            if (success)
            {
                await Task.Delay(Const.WaitTime);
                continue;
            }
            success = Utility.Action.ClickTargetButton(process, ocrResult, Const.JYL);
            if (success)
            {
                var region = ocrResult.Regions.Where(p => p.Text.Contains(Const.JYL)).OrderBy(p => p.Text.Length).First();
                form.AppendTextBoxMessage(region.Text);
                if (regex.Match(region.Text).Value == "200")
                {
                    isLast = true;
                }
                await Task.Delay(Const.WaitTimeLong);
                continue;
            }
            if (isLast)
            {
                success = Utility.Action.FindNoKeyword(process, imgPath, Const.JYL);
                if (success)
                {
                    form.AppendTextBoxMessage("跑环 完成");
                    return;
                }
            }

        }
        #endregion
    }
}

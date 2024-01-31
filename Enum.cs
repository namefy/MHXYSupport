using System.ComponentModel;

namespace MHXYSupport;

public enum MHTask
{
    /// <summary>
    /// 师门
    /// </summary>
    [Description("师门")]
    SM = 1,
    /// <summary>
    /// 打宝图
    /// </summary>
    [Description("打宝图")]
    DBT = 2,
    /// <summary>
    /// 挖宝图
    /// </summary>
    [Description("挖宝图")]
    WBT = 3,
    /// <summary>
    /// 运镖
    /// </summary>
    [Description("运镖")]
    YB = 4,
    /// <summary>
    /// 捉鬼
    /// </summary>
    [Description("捉鬼")]
    ZG = 5,
    /// <summary>
    /// 经验链
    /// </summary>
    [Description("经验链")]
    JYL = 6,
    /// <summary>
    /// 帮派任务
    /// </summary>
    [Description("帮派任务")]
    BPRW = 7,
    /// <summary>
    /// 三界奇缘
    /// </summary>
    [Description("三界奇缘")]
    SJQY = 8,
    /// <summary>
    /// 竞技场
    /// </summary>
    [Description("竞技场")]
    JJC =9,
    /// <summary>
    /// 秘境降妖
    /// </summary>
    [Description("秘境降妖")]
    MJXY = 10,
}
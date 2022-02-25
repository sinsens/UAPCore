using Business.Shared.Consts;

namespace Business.Core.Options
{
    public class AppIdAndSecretOptions
    {
        public int AppKeyLength { get; set; } = AppConsts.MaxAppIdLength / 2;
        public int AppSecretLength { get; set; } = AppConsts.MaxAppSecretLength / 2;
    }
}

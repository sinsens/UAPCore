using Business.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Business.Permissions
{
    public class BusinessPermissionDefinitionProvider: PermissionDefinitionProvider
    {

        public override void Define(IPermissionDefinitionContext context)
        {
            var Business = context.AddGroup(BusinessPermissions.Business, L("Business"), MultiTenancySides.Tenant);

            //Code generation...
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BusinessResource>(name);
        }
    }
}

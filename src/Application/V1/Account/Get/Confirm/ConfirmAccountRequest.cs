using Application.Shared;
using Application.V1.Account.Get.Confirm.Models;

namespace Application.V1.Account.Get.Confirm
{
    public class ConfirmAccountRequest : LoggedRequest<AccountConfirmResponseModel>
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public override bool SaveChanges()
            => true;
    }
}

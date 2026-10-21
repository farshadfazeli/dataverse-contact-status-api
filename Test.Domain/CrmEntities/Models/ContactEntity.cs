using Test.Domain.CrmEntities.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace Test.Domain.CrmEntities.Models
{

    [EntityLogicalName("contact")]
    public class ContactEntity : Entity
    {
        public ContactEntity() : base(ContactResource.LogicalName)
        {
        }

        public string MobilePhone
        {
            get => GetAttributeValue<string>(ContactResource.MobilePhone);
            set => SetAttributeValue(ContactResource.MobilePhone, value);
        }

        public OptionSetValue StatusReason
        {
            get => GetAttributeValue<OptionSetValue>(ContactResource.StatusReason);
            set => SetAttributeValue(ContactResource.StatusReason, value);
        }
    }
}

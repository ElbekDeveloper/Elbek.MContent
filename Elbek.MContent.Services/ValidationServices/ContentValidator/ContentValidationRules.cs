using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;
using Elbek.MContent.Services.Utils;
using System;

namespace Elbek.MContent.Services.ValidationServices.ContentValidator
{
    public interface IContentValidationRules: IGenericValidationRules
    {
        string ValidateTypeRange(string type);
        string ValidateUniqueContentId(Content authorWithUniqueId);
        string ValidateUniqueTitleOfItsType(Content contentWithUniqueTitle);
        string ValidateContentWasFound(Guid id, Content content);
    }
    public class ContentValidationRules : GenericValidationRules, IContentValidationRules
    {

        public string ValidateUniqueContentId(Content contentWithUniqueId)
        {
            return (contentWithUniqueId != null) ? ValidationErrorMessages.EntityWithIdExists<Content>(contentWithUniqueId.Id) : string.Empty;
        }

        public string ValidateTypeRange(string type)
        {
            var isParsable = EnumUtils.TryParseWithMemberName<ContentType>(type, out _);
            var types = Enum.GetNames(typeof(ContentType));

            return (!isParsable) ? ValidationErrorMessages.InvalidTypeRange(type, types) : string.Empty;
        }

        public string ValidateUniqueTitleOfItsType(Content contentWithUniqueTitle)
        {

            return (contentWithUniqueTitle != null) 
                ?
                ValidationErrorMessages.InvalidTitleForThisType(contentWithUniqueTitle.Title, contentWithUniqueTitle.Type.ToString()) 
                :
                string.Empty;
        }

        public string ValidateContentWasFound(Guid id, Content content)
        {
            return (content == null) ? ValidationErrorMessages.EntityNotFound<Content>(id) : string.Empty;
        }
    }
}

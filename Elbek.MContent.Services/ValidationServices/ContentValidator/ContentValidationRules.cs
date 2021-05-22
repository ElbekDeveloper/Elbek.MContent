using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;
using System;

namespace Elbek.MContent.Services.ValidationServices.ContentValidator
{
    public interface IContentValidationRules: IGenericValidationRules
    {
        string ValidateTypeRange(int type);
        string ValidateUniqueContentId(Content authorWithUniqueId);
        string ValidateUniqueTitleOfItsType(Content contentWithUniqueTitle);
        string ValidateContentWasFound(Guid id, Content content);
    }
    public class ContentValidationRules : GenericValidationRules, IContentValidationRules
    {

        //вынеси все валидационные сообщения в отдельный класс
        public string ValidateUniqueContentId(Content contentWithUniqueId)
        {
            return (contentWithUniqueId != null) ? $"Content with Id '{contentWithUniqueId.Id}' already exists" : string.Empty;
        }

        public string ValidateTypeRange(int type)
        {
            var types = Enum.GetNames(typeof(ContentTypeDto));

            //а что если у тебя в enum числа будет не стандартные ?
            //например
            //public enum ContentTypeDto
            //{
            //    Book = 14,
            //    Song = 0,
            //    Movie = 100,
            //    Podcast = 42
            //}
            //
            //валидацию переделать
            if (type > types.Length - 1
                || type < 0)
            {
                return $"Type '{type}' should have one of the following values: {string.Join(", ", types)}";
            }
            return string.Empty;
        }

        public string ValidateUniqueTitleOfItsType(Content contentWithUniqueTitle)
        {
            var types = Enum.GetNames(typeof(ContentTypeDto));                                          ////а что если у тебя в enum числа будет не стандартные ?
                                                                                                        //сообщение переделать
            return (contentWithUniqueTitle != null) ? $"Content '{contentWithUniqueTitle}' with Type{types[(int)contentWithUniqueTitle.Type]} already exists" : string.Empty;
        }

        public string ValidateContentWasFound(Guid id, Content content)
        {
            return (content == null) ? $"Content with Id {id} not found" : string.Empty;
        }
    }
}

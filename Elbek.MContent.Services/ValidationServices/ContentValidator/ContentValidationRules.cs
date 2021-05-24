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
            return (contentWithUniqueId != null) ? ValidationErrorMessages.EntityWithIdExists<Content>(contentWithUniqueId.Id) : string.Empty;
        }

        public string ValidateTypeRange(int type)
        {
            throw new NotImplementedException();
            string[] types = Enum.GetNames(typeof(ContentType));

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
                //return InvalidTypeRange(type, types);
            }
            return string.Empty;
        }

        public string ValidateUniqueTitleOfItsType(Content contentWithUniqueTitle)
        {
            var types = Enum.GetNames(typeof(ContentTypeDto));                                          ////а что если у тебя в enum числа будет не стандартные ?
                                                                                                        //сообщение переделать
            return (contentWithUniqueTitle != null) 
                ?
                ValidationErrorMessages.InvalidTitleForThisType(contentWithUniqueTitle.Title, types[(int)contentWithUniqueTitle.Type]) 
                :
                string.Empty;
        }

        public string ValidateContentWasFound(Guid id, Content content)
        {
            return (content == null) ? ValidationErrorMessages.EntityNotFound<Content>(id) : string.Empty;
        }
    }
}

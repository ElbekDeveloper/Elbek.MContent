using System;

namespace Elbek.MContent.Services.ValidationServices
{
    public static class ValidationErrorMessages
    {
        //General
        public static string UnmatchingIds(Guid idFromRoute, Guid idFromBody) => $"Id supplied in route '{idFromRoute}' doesn't correspond to that of body '{idFromBody}'.";
        public static string EntityWithIdExists<TEntity>(Guid id) => $"{typeof(TEntity).Name} with Id '{id}' already exists.";
        public static string EntityNotFound<TEntity>(Guid id) => $"{typeof(TEntity).Name} with Id {id} not found.";
        public static string EntityRequired<TEntity>() => $"At least, one {typeof(TEntity).Name.ToLower()} required.";
        public static string FieldRequired(string field) => $"Field {nameof(field)} is required.";
        public static string FieldRequired(Guid id) => $"Field {id} is required. Default values are not accepted.";
        //Content related
        public static string InvalidTypeRange(string type, string[] types) => $"Type '{type}' should have one of the following values: {string.Join(", ", types)}.";
        public static string InvalidTitleForThisType(string title, string type) => $"Content '{title}' with Type {type} already exists.";
        //Author related 
        public static string DuplicateAuthors() => $"Authors cannot contain duplicates.";
        public static string AuthorNotFound(string name) => $"Author with Name {name} not found.";
        public static string AuthorNotFound(string ids, string names) => $"Author with {ids} or {names} was not found.";
        public static string AuthorExists(string name) => $"Author  '{name}' already exists.";


        public class Author
        {
            public static string AuthorNotFoundId(Guid id) => $"Author with Id '{id}' was not found.";
            public static string AuthorNotFoundName(string name) => $"Author with Name '{name}' was not found.";
        }
    }
}

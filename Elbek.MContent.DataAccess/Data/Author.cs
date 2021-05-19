using System.Collections.Generic;

namespace Elbek.MContent.DataAccess.Data
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<AuthorContent> AuthorContents { get; set; }
        public Author()
        {
            this.AuthorContents = new HashSet<AuthorContent>();
        }
    }
}

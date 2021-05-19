using System;
using System.Collections.Generic;

namespace Elbek.MContent.DataAccess.Data
{
    public class Content
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Type Type { get; set; }
        public virtual ICollection<ContentAuthors> ContentAuthors { get; set; }
        public Content()
        {
            this.ContentAuthors = new HashSet<ContentAuthors>();
        }
    }

    public enum Type
    {
        Book,
        Song, 
        Movie, 
        Podcast
    }
}

﻿using System;
using System.Collections.Generic;

namespace Elbek.MContent.DataAccess.Data
{
    public class Content : BaseEntity
    {
        public string Title { get; set; }
        public ContentType Type { get; set; }
        public virtual ICollection<ContentAuthors> ContentAuthors { get; set; }

        public Content()
        {
            this.ContentAuthors = new HashSet<ContentAuthors>();
        }
    }
}
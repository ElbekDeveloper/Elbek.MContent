using System;

namespace Elbek.MContent.DataAccess.Data
{
public class ContentAuthors
{
    public Guid Id {
        get;
        set;
    }
    public Guid AuthorId {
        get;
        set;
    }
    public virtual Author Author {
        get;
        set;
    }
    public Guid ContentId {
        get;
        set;
    }
    public virtual Content Content {
        get;
        set;
    }
}
}

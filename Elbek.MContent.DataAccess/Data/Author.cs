using System.Collections.Generic;

namespace Elbek.MContent.DataAccess.Data {
  public class Author : BaseEntity {
    public string Name {
      get;
      set;
    }
    public virtual ICollection<ContentAuthors> ContentAuthors {
      get;
      set;
    }
    public Author() { this.ContentAuthors = new HashSet<ContentAuthors>(); }
  }
}

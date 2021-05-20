using System;
using System.Collections.Generic;

namespace Elbek.MContent.Services.Models {
  public class ContentDto {
    public Guid Id {
      get;
      set;
    }
    public string Title {
      get;
      set;
    }
    public int Type {
      get;
      set;
    }
    public ICollection<AuthorDto> Authors {
      get;
      set;
    }
  }
}

using System;

namespace Elbek.MContent.Services.Models
{
    public class ContentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
    }
}
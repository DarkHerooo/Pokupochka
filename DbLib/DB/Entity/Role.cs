using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public partial class Role
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? DbImageId { get; set; }
        public string? Image { get; set; }
        public List<User> Users { get; set; } = new();

        // Дополнительные поля

        [NotMapped]
        public string? CorrectImage
        {
            get { return Image != null ? "/Images/Roles/" + Image : "/Images/Roles/Unk.png"; }
            set { }
        }

        [NotMapped]
        public string? CorrectDescription
        {
            get { return Description ?? "Информация отсутствует"; }
            set { }
        }
    }
}

using Calabonga.EntityFrameworkCore.Entities.Base;
using System.ComponentModel;

namespace NLPDB.Web.Data
{
    public class Category : Identity
    {
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }

        public ICollection<Algorithm> Algorithms { get; set; }
        public ICollection<TaskAlg> TasksAlg { get; set; }
    }
}

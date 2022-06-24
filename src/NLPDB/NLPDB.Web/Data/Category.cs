using Calabonga.EntityFrameworkCore.Entities.Base;
namespace NLPDB.Web.Data
{
    public class Category : Identity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Algorithm> Algorithms { get; set; }
        public ICollection<TaskAlg> TasksAlg { get; set; }
    }
}

using Calabonga.EntityFrameworkCore.Entities.Base;

namespace NLPDB.Web.Data
{
    public class TaskAlg : Identity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Algorithm> Algorithms { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}

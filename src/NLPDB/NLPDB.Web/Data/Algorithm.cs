using Calabonga.EntityFrameworkCore.Entities.Base;

namespace NLPDB.Web.Data
{
    public class Algorithm : Auditable
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public Category Category { get; set; }
        public ICollection<TaskAlg> TasksAlg { get; set; }
    }
}

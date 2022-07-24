using Entities.Concrete;

namespace ProjeITPreProjectMvcUI.Areas.Admin.ViewModels
{
    public class CommentsListViewModel
    {
        public List<Comment>? Comments { get; set; }
        public int TaskId { get; set; }
    }
}

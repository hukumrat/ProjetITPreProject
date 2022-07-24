namespace ProjeITPreProjectMvcUI.Areas.Admin.Views.ViewModels
{
    public class UsersListViewModel
    {
        public UsersListViewModel()
        {
            Roles = new List<string>();
        }
        public string? Id { get; set; }
        public string? Username { get; set; }
        public List<string>? Roles { get; set; }
    }
}

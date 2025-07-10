namespace LMS_UI.Models.ViewModels
{
    public class UserViewModel
    {
        public int userID
        { get; set; }
        public string userName
        { get; set; }
        public string passwordHash
        { get; set; }
        public string userRole
        { get; set; }
    }
}

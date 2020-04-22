namespace ShappingList.Entities
{
    public class Invitation
    {
        public int Id { get; set; }
        public User User { get; set; }
        public UserGroup UserGroup { get; set; }
        public bool IsAccepted { get; set; }
    }
}